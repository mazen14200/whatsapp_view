using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class AccountBankController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IAccountBank _accountBank;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<AccountBankController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;


        public AccountBankController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
             IMapper mapper, IUserService userService, IAccountBank accountBank,
             IUserLoginsService userLoginsService, IToastNotification toastNotification,
             IStringLocalizer<AccountBankController> localizer, IAdminstritiveProcedures adminstritiveProcedures) :
             base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _accountBank = accountBank;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _adminstritiveProcedures = adminstritiveProcedures;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207003", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            //sidebar Active
            ViewBag.id = "#sidebarServices";
            ViewBag.no = "2";
            var userLogin = await _userManager.GetUserAsync(User);
            var titles = await setTitle("207", "2207003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var AccountBanks = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankStatus == "A" && x.CrCasAccountBankNo != "00" && x.CrCasAccountBankLessor == userLogin.CrMasUserInformationLessor, new[] { "CrCasAccountBankNoNavigation" });
            return View(AccountBanks);
        }


        [HttpGet]
        public async Task<PartialViewResult> GetAccountBanksByStatus(string status)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            if (!string.IsNullOrEmpty(status))
            {
                var AccountBankbyStatusAll = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankNo != "00" && x.CrCasAccountBankLessor == userLogin.CrMasUserInformationLessor, new[] { "CrCasAccountBankNoNavigation" });
                if (status == Status.All) return PartialView("_DataTableAccountBank", AccountBankbyStatusAll.Where(x => x.CrCasAccountBankStatus != Status.Deleted));
                return PartialView("_DataTableAccountBank", AccountBankbyStatusAll.Where(l => l.CrCasAccountBankStatus == status));
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddAccountBank()
        {

            //sidebar Active
            ViewBag.id = "#sidebarServices";
            ViewBag.no = "2";
            var titles = await setTitle("207", "2207003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var AccountBanks = await _unitOfWork.CrCasAccountBank.GetAllAsync();
            // View the Banks
            var BanksData = _unitOfWork.CrMasSupAccountBanks.FindAll(x => x.CrMasSupAccountBankStatus == Status.Active && x.CrMasSupAccountBankCode != "00");
            var BanksDatasAr = BanksData.Select(c => new SelectListItem { Value = c.CrMasSupAccountBankCode.ToString(), Text = c.CrMasSupAccountBankArName }).ToList();
            var BanksDatasEn = BanksData.Select(c => new SelectListItem { Value = c.CrMasSupAccountBankCode.ToString(), Text = c.CrMasSupAccountBankEnName }).ToList();

            ViewData["BanksDataAr"] = BanksDatasAr;
            ViewData["BanksDataEn"] = BanksDatasEn;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAccountBank(AccountBankVM AccountBankmodel)
        {
            var BanksData = _unitOfWork.CrMasSupAccountBanks.FindAll(x => x.CrMasSupAccountBankStatus == Status.Active);
            var BanksDatasAr = BanksData.Select(c => new SelectListItem { Value = c.CrMasSupAccountBankCode.ToString(), Text = c.CrMasSupAccountBankArName }).ToList();
            var BanksDatasEn = BanksData.Select(c => new SelectListItem { Value = c.CrMasSupAccountBankCode.ToString(), Text = c.CrMasSupAccountBankEnName }).ToList();

            ViewData["BanksDataAr"] = BanksDatasAr;
            ViewData["BanksDataEn"] = BanksDatasEn;
            // Get Lessor Code
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            AccountBankmodel.CrCasAccountBankLessor = lessorCode;

            var Lrecord = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankLessor == lessorCode &&
                                                                    x.CrCasAccountBankNo == AccountBankmodel.CrCasAccountBankNo)
                                                      .Max(x => x.CrCasAccountBankCode.Substring(x.CrCasAccountBankCode.Length - 2, 2));
            string Serial;
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                Serial = val.ToString("00");
            }
            else
            {
                Serial = "01";
            }
           
            var ConcateBankcode = lessorCode.ToString() + AccountBankmodel.CrCasAccountBankNo.ToString() + Serial.ToString();
            AccountBankmodel.CrCasAccountBankCode = ConcateBankcode;
            AccountBankmodel.CrCasAccountBankSerail = Serial.ToString();

            var existingIBank =  _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankIban == AccountBankmodel.CrCasAccountBankIban&&x.CrCasAccountBankLessor==lessorCode);
            var existingIBankArName = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankArName == AccountBankmodel.CrCasAccountBankArName && x.CrCasAccountBankLessor == lessorCode);
            var existingIBankEnName = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankEnName == AccountBankmodel.CrCasAccountBankEnName && x.CrCasAccountBankLessor == lessorCode);

            if (existingIBank.Count() > 0) ModelState.AddModelError("ExistCodeAr", _localizer["IBanIsExist"]);
            if (existingIBankArName.Count() > 0) ModelState.AddModelError("ExistAr", _localizer["FieldIsExist"]);
            if (existingIBankEnName.Count() > 0) ModelState.AddModelError("ExistEn", _localizer["FieldIsExist"]);

            if (ModelState.IsValid)
            {

                AccountBankmodel.CrCasAccountBankStatus = "A";
                var AccountBankVMTAccountBank = _mapper.Map<CrCasAccountBank>(AccountBankmodel);
                await _unitOfWork.CrCasAccountBank.AddAsync(AccountBankVMTAccountBank);
                _unitOfWork.Complete();
                var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207003", "2");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة حساب", "Add Account", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "243", "20", currentUser.CrMasUserInformationLessor, "100",
               AccountBankVMTAccountBank.CrCasAccountBankCode, null, null, null, null, null, null, null, null, "اضافة", "Insert", "I", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("Index");
            }
            return View(AccountBankmodel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarServices";
            ViewBag.no = "2";
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("207", "2207003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var bank = await _unitOfWork.CrCasAccountBank.GetByIdAsync(id);
            if (bank == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var BanksData = _unitOfWork.CrMasSupAccountBanks.FindAll(x => x.CrMasSupAccountBankStatus == Status.Active && x.CrMasSupAccountBankCode == bank.CrCasAccountBankNo);
            var BanksDatasAr = BanksData.FirstOrDefault().CrMasSupAccountBankArName.ToString();
            var BanksDatasEn = BanksData.FirstOrDefault().CrMasSupAccountBankEnName.ToString();
            var SalesPointCount = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointAccountBank == bank.CrCasAccountBankCode && x.CrCasAccountSalesPointStatus != Status.Deleted).Count();
            ViewBag.BankNameAr = BanksDatasAr;
            ViewBag.BankNameEn = BanksDatasEn;
            ViewBag.SalesPointCount = SalesPointCount;
            var model = _mapper.Map<AccountBankVM>(bank);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountBankVM model)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            var bank = _mapper.Map<CrCasAccountBank>(model);
            var bank2 = await _unitOfWork.CrCasAccountBank.GetByIdAsync(model.CrCasAccountBankCode);
            if (user != null)
            {
                if (bank != null)
                {
                    bank2.CrCasAccountBankReasons = model.CrCasAccountBankReasons;
                    //_unitOfWork.CrCasAccountBank.Update(bank);
                    _unitOfWork.Complete();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207003", "2");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "243", "20", currentUser.CrMasUserInformationLessor, "100",
                    bank2.CrCasAccountBankCode, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "AccountBank");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            // Get Lessor Code
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            string sAr = "";
            string sEn = "";
            var bank = await _unitOfWork.CrCasAccountBank.GetByIdAsync(code);

            if (bank != null)
            {
                var salesPoints = _unitOfWork.CrCasAccountSalesPoint.FindAll(l => l.CrCasAccountSalesPointLessor == lessorCode && l.CrCasAccountSalesPointAccountBank == bank.CrCasAccountBankCode);
                if (await CheckUserSubValidationProcdures("2207003", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Stop";
                        bank.CrCasAccountBankStatus = Status.Hold;
                        foreach (var salesPoint in salesPoints) salesPoint.CrCasAccountSalesPointBankStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Delete";
                        bank.CrCasAccountBankStatus = Status.Deleted;
                        foreach (var salesPoint in salesPoints) salesPoint.CrCasAccountSalesPointBankStatus = Status.Deleted;
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع";
                        sEn = "Retrieve";
                        bank.CrCasAccountBankStatus = Status.Active;
                        foreach (var salesPoint in salesPoints) salesPoint.CrCasAccountSalesPointBankStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207003", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "243", "20", currentUser.CrMasUserInformationLessor, "100",
                   bank.CrCasAccountBankCode, null, null, null, null, null, null, null, null, sAr, sEn, "U", null);
                    return RedirectToAction("Index", "AccountBank");
                }
            }


            return View(bank);

        }

    }
}
