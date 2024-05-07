using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class SalesPointsController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IUserService _userService;
        private readonly IAccountBank _accountBank;
        private readonly IToastNotification _toastNotification;
        private readonly ISalesPoint _salesPoint;
        private readonly IStringLocalizer<AccountBankController> _localizer;

        public SalesPointsController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IUserLoginsService userLoginsService,
            IAdminstritiveProcedures adminstritiveProcedures, IUserService userService, IAccountBank accountBank,
            IToastNotification toastNotification, IStringLocalizer<AccountBankController> localizer, ISalesPoint salesPoint) : base(userManager, unitOfWork, mapper)
        {
            _userLoginsService = userLoginsService;
            _adminstritiveProcedures = adminstritiveProcedures;
            _userService = userService;
            _accountBank = accountBank;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _salesPoint = salesPoint;
        }

        public async Task<IActionResult> SalesPoints()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser1) = await SetTrace("207", "2207004", "2");

            await _userLoginsService.SaveTracing(currentUser1.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            //sidebar Active
            ViewBag.id = "#sidebarServices";
            ViewBag.no = "3";
            var currentUser = await _userManager.GetUserAsync(User);
            var titles = await setTitle("207", "2207004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var salesPointByLessor = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == currentUser.CrMasUserInformationLessor && x.CrCasAccountSalesPointStatus == Status.Active && x.CrCasAccountSalesPointBank != "00",
                new[] { "CrCasAccountSalesPointAccountBankNavigation", "CrCasAccountSalesPointBankNavigation", "CrCasAccountSalesPointNavigation" });

            return View(salesPointByLessor);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetSalesPointsByStatus(string status)
        {
            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);
            if (userLessor != null)
            {
                if (!string.IsNullOrEmpty(status))
                {
                    var SalesPointbyStatusAll = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == userLessor.CrMasUserInformationLessor && x.CrCasAccountSalesPointBank != "00",
                        new[] { "CrCasAccountSalesPointAccountBankNavigation", "CrCasAccountSalesPointBankNavigation", "CrCasAccountSalesPointNavigation" });
                    if (status == Status.All)
                    {
                        return PartialView("_DataTableSalesPoints", SalesPointbyStatusAll.Where(x=>x.CrCasAccountSalesPointStatus!=Status.Deleted));
                    }
                    return PartialView("_DataTableSalesPoints", SalesPointbyStatusAll.Where(x => x.CrCasAccountSalesPointStatus == status));
                }
            }

            return PartialView();
        }
        public async Task<IActionResult> AddSalesPoint()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            //sidebar Active
            ViewBag.id = "#sidebarServices";
            ViewBag.no = "3";
            //To Set Title;
            var titles = await setTitle("207", "2207004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "اضافة", "Create", titles[3]);
            var branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationStatus == Status.Active && x.CrCasBranchInformationLessor == currentUser.CrMasUserInformationLessor).ToList();
            ViewData["BranchesAr"] = branches.Select(x => new SelectListItem { Value = x.CrCasBranchInformationCode.ToString(), Text = x.CrCasBranchInformationArShortName }).ToList();
            ViewData["BranchesEn"] = branches.Select(x => new SelectListItem { Value = x.CrCasBranchInformationCode.ToString(), Text = x.CrCasBranchInformationEnShortName }).ToList();
            var accountBanks = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankStatus == Status.Active && x.CrCasAccountBankLessor == currentUser.CrMasUserInformationLessor && x.CrCasAccountBankNo != "00").ToList();
            ViewData["AccountBanksAr"] = accountBanks.Select(x => new SelectListItem { Value = x.CrCasAccountBankCode.ToString(), Text = x.CrCasAccountBankArName }).ToList();
            ViewData["AccountBanksEn"] = accountBanks.Select(x => new SelectListItem { Value = x.CrCasAccountBankCode.ToString(), Text = x.CrCasAccountBankEnName }).ToList();

            return View();
        }
        public string[] GetBankInfo(string code)
        {

            var bank = _unitOfWork.CrCasAccountBank.Find(x => x.CrCasAccountBankCode == code, new[] { "CrCasAccountBankNoNavigation" });
            if (bank != null)
            {
                string[] list = { "true", bank.CrCasAccountBankIban, bank.CrCasAccountBankNoNavigation.CrMasSupAccountBankArName, bank.CrCasAccountBankNoNavigation.CrMasSupAccountBankEnName };
                return list;
            }
            else
            {
                string[] list2 = { "false" };
                return list2;
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddSalesPoint(SalesPointsVM salesPointsVM)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var IsSalesPointNo = _unitOfWork.CrCasAccountSalesPoint.FindAll(l => l.CrCasAccountSalesPointNo == salesPointsVM.CrCasAccountSalesPointNo).Count() > 0;

            var IsSalesPointArName = _unitOfWork.CrCasAccountSalesPoint.FindAll(l => l.CrCasAccountSalesPointArName == salesPointsVM.CrCasAccountSalesPointArName&&
                                                                                l.CrCasAccountSalesPointLessor==currentUser.CrMasUserInformationLessor).Count() > 0;

            var IsSalesPointEnName = _unitOfWork.CrCasAccountSalesPoint.FindAll(l => l.CrCasAccountSalesPointEnName == salesPointsVM.CrCasAccountSalesPointEnName&&
                                                                                l.CrCasAccountSalesPointLessor==currentUser.CrMasUserInformationLessor).Count() > 0;
            if (IsSalesPointNo) ModelState.AddModelError("IsSalesPointNo", _localizer["IsSalesPointNo"]);
            if (IsSalesPointArName) ModelState.AddModelError("IsSalesPointArName", _localizer["IsExist"]);
            if (IsSalesPointEnName) ModelState.AddModelError("IsSalesPointEnName", _localizer["IsExist"]);
            var bank = _unitOfWork.CrMasSupAccountBanks.Find(x => x.CrMasSupAccountBankArName == salesPointsVM.CrCasAccountSalesPointBankNavigation.CrMasSupAccountBankArName ||
            x.CrMasSupAccountBankEnName == salesPointsVM.CrCasAccountSalesPointBankNavigation.CrMasSupAccountBankEnName);
            if (ModelState.IsValid)
            {
                var SalesPoint = _mapper.Map<CrCasAccountSalesPoint>(salesPointsVM);
                SalesPoint.CrCasAccountSalesPointBank = bank.CrMasSupAccountBankCode;
                SalesPoint.CrCasAccountSalesPointLessor = currentUser.CrMasUserInformationLessor;
                var createdSalesPoint = await _salesPoint.CreateSalesPoint(SalesPoint, currentUser.CrMasUserInformationCode);
                if (createdSalesPoint != null) await _unitOfWork.CompleteAsync();
                // SaveTracing
                var (mainTask, subTask, system, currentUserr) = await SetTrace("207", "2207004", "2");
                await _userLoginsService.SaveTracing(currentUserr.CrMasUserInformationCode, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "244", "20", currentUser.CrMasUserInformationLessor, "100",
                    createdSalesPoint.CrCasAccountSalesPointCode, null, null, null, null, null, null, null, null, "اضافة", "Add", "I", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("SalesPoints", "SalesPoints");
            }

            var branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationStatus == Status.Active && x.CrCasBranchInformationLessor == currentUser.CrMasUserInformationLessor).ToList();
            ViewData["BranchesAr"] = branches.Select(x => new SelectListItem { Value = x.CrCasBranchInformationCode.ToString(), Text = x.CrCasBranchInformationArShortName }).ToList();
            ViewData["BranchesEn"] = branches.Select(x => new SelectListItem { Value = x.CrCasBranchInformationCode.ToString(), Text = x.CrCasBranchInformationEnShortName }).ToList();
            var accountBanks = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankStatus == Status.Active && x.CrCasAccountBankLessor == currentUser.CrMasUserInformationLessor && x.CrCasAccountBankNo != "00").ToList();
            ViewData["AccountBanksAr"] = accountBanks.Select(x => new SelectListItem { Value = x.CrCasAccountBankCode.ToString(), Text = x.CrCasAccountBankArName }).ToList();
            ViewData["AccountBanksEn"] = accountBanks.Select(x => new SelectListItem { Value = x.CrCasAccountBankCode.ToString(), Text = x.CrCasAccountBankEnName }).ToList();
            return View(salesPointsVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarServices";
            ViewBag.no = "3";
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("207", "2207004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var salesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x=>x.CrCasAccountSalesPointCode==id,
                new[] { "CrCasAccountSalesPointAccountBankNavigation", "CrCasAccountSalesPointBankNavigation", "CrCasAccountSalesPointNavigation" });
            var branch = await _unitOfWork.CrCasBranchInformation.FindAsync(x => x.CrCasBranchInformationCode == salesPoint.CrCasAccountSalesPointNavigation.CrCasBranchInformationCode);
            if (branch!=null)
            {
                ViewBag.ArShortBranch = branch.CrCasBranchInformationArShortName;
                ViewBag.EnShortBranch = branch.CrCasBranchInformationEnShortName;
            }
            if (salesPoint == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var SalesPointData = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointStatus == Status.Active && x.CrCasAccountSalesPointCode == salesPoint.CrCasAccountSalesPointCode);
            var model = _mapper.Map<SalesPointsVM>(salesPoint);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(SalesPointsVM model)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            var salespoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointCode == model.CrCasAccountSalesPointCode,
                new[] { "CrCasAccountSalesPointAccountBankNavigation", "CrCasAccountSalesPointBankNavigation", "CrCasAccountSalesPointNavigation" });
            if (user != null)
            {
                if (salespoint != null)
                {
                    salespoint.CrCasAccountSalesPointReasons = model.CrCasAccountSalesPointReasons;
                    _unitOfWork.CrCasAccountSalesPoint.Update(salespoint);
                    _unitOfWork.Complete();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207004", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "244", "20", currentUser.CrMasUserInformationLessor, "100",
                        salespoint.CrCasAccountSalesPointCode, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("SalesPoints", "SalesPoints");
        }





        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var salesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointCode == code, new[] { "CrCasAccountSalesPointNavigation" });
            if (salesPoint != null)
            {
                if (await CheckUserSubValidationProcdures("2207004", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Stop";
                        salesPoint.CrCasAccountSalesPointStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Delete";
                        salesPoint.CrCasAccountSalesPointStatus = Status.Deleted;
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع";
                        sEn = "Retrieve";
                        if (salesPoint.CrCasAccountSalesPointBankStatus==Status.Deleted)
                        {
                            _toastNotification.AddErrorToastMessage(_localizer["ToastFailedSalesPointDeleteBank"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                            return View(salesPoint);
                        }
                        if (salesPoint.CrCasAccountSalesPointNavigation.CrCasBranchInformationStatus==Status.Deleted)
                        {
                            _toastNotification.AddErrorToastMessage(_localizer["ToastFailedSalesPointDeleteBranch"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                            return View(salesPoint);
                        }
                        salesPoint.CrCasAccountSalesPointStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207004", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "244", "20", currentUser.CrMasUserInformationLessor, "100",
                        salesPoint.CrCasAccountSalesPointCode, null, null, null, null, null, null, null, null, sAr, sEn, "U", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("SalesPoints", "SalesPoints");
                }
            }


            return View(salesPoint);

        }

    }
}
