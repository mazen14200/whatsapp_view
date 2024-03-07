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

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class OwnersController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<AccountBankController> _localizer;
        private readonly IOwner _ownerService;
        private readonly IToastNotification _toastNotification;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        public OwnersController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IUserLoginsService userLoginsService, IWebHostEnvironment webHostEnvironment, IUserService userService, IStringLocalizer<AccountBankController> localizer, IOwner ownerService, IToastNotification toastNotification, IAdminstritiveProcedures adminstritiveProcedures) : base(userManager, unitOfWork, mapper)
        {
            _userLoginsService = userLoginsService;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
            _localizer = localizer;
            _ownerService = ownerService;
            _toastNotification = toastNotification;
            _adminstritiveProcedures = adminstritiveProcedures;
        }
        [HttpGet]
        public async Task<ActionResult> Owners()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201004", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "3";
            // Set Title
            var titles = await setTitle("201", "2201004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var user = await _userService.GetUserLessor(User);
            var lessor = await _unitOfWork.CrMasLessorInformation.FindAsync(x=>x.CrMasLessorInformationCode==user.CrMasUserInformationLessor);
            var Owners = _unitOfWork.CrCasOwner.FindAll(l => l.CrCasOwnersLessorCode == user.CrMasUserInformationLessor &&l.CrCasOwnersCode!=lessor.CrMasLessorInformationGovernmentNo&&l.CrCasOwnersStatus==Status.Active, new[] { "CrCasOwnersLessorCodeNavigation", "CrCasCarInformations" }).ToList();
            foreach (var item in Owners)
            {
                item.CrCasCarInformations.Count();
            }
            return View(Owners);
        }
        [HttpGet]
        public async Task<PartialViewResult> GetOwnersByStatus(string status)
        {
            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);
            var lessor = await _unitOfWork.CrMasLessorInformation.FindAsync(x => x.CrMasLessorInformationCode == userLessor.CrMasUserInformationLessor);

            if (userLessor != null)
            {
                if (!string.IsNullOrEmpty(status))
                {
                    var OwnersbyStatusAll =  _unitOfWork.CrCasOwner.FindAll(l => l.CrCasOwnersLessorCode == userLessor.CrMasUserInformationLessor && l.CrCasOwnersCode != lessor.CrMasLessorInformationGovernmentNo, new[] { "CrCasOwnersLessorCodeNavigation", "CrCasCarInformations" }).ToList();
                    if (status == Status.All.ToLower())
                    {
                        return PartialView("_DataTableOwners", OwnersbyStatusAll.Where(x=>x.CrCasOwnersStatus!=Status.Deleted));
                    }
                    return PartialView("_DataTableOwners", OwnersbyStatusAll.Where(x => x.CrCasOwnersStatus == status));
                }
            }

            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> AddOwner()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "3";
            //To Set Title;
            var titles = await setTitle("201", "2201004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "اضافة", "Create", titles[3]);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddOwner(OwnersVM ownersVM)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            bool OwnerIsExist  = _unitOfWork.CrCasOwner.FindAll(x => x.CrCasOwnersCode == ownersVM.CrCasOwnersCode && x.CrCasOwnersLessorCode == currentUser.CrMasUserInformationLessor).Count() > 0;
            bool NameArIsExist = _unitOfWork.CrCasOwner.FindAll(x => x.CrCasOwnersArName == ownersVM.CrCasOwnersArName && x.CrCasOwnersLessorCode == currentUser.CrMasUserInformationLessor).Count() > 0;
            bool NameEnIsExist = _unitOfWork.CrCasOwner.FindAll(x => x.CrCasOwnersEnName == ownersVM.CrCasOwnersEnName && x.CrCasOwnersLessorCode == currentUser.CrMasUserInformationLessor).Count() > 0;
            if (OwnerIsExist) ModelState.AddModelError("CrCasOwnersCode", _localizer["OwnerIsExist"]);
            if (NameArIsExist) ModelState.AddModelError("CrCasOwnersArName", _localizer["NameOwnerIsExist"]);
            if (NameEnIsExist) ModelState.AddModelError("CrCasOwnersEnName", _localizer["NameOwnerIsExist"]);

            if (ModelState.IsValid)
            {
                var owner = _mapper.Map<CrCasOwner>(ownersVM);
                owner.CrCasOwnersLessorCode = currentUser.CrMasUserInformationLessor;
                await _ownerService.AddOwnerInCas(owner);
                // SaveTracing
                var (mainTask, subTask, system, currentUserr) = await SetTrace("201", "2201004", "2");
                await _userLoginsService.SaveTracing(currentUserr.CrMasUserInformationCode, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "204", "20", currentUser.CrMasUserInformationLessor, "100",
                    owner.CrCasOwnersCode, null, null, null, null, null, null, null, null, "اضافة", "Insert", "I", null);
                return RedirectToAction("Owners", "Owners");
            }
            return View(ownersVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "3";
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("201", "2201004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            var owner = await _unitOfWork.CrCasOwner.FindAsync(x => x.CrCasOwnersCode == id,
                new[] { "CrCasOwnersLessorCodeNavigation", "CrCasCarInformations" });           
            if (owner == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View();
            }
            ViewBag.CarCount=owner.CrCasCarInformations.Where(x=>x.CrCasCarInformationStatus!=Status.Deleted &&x.CrCasCarInformationStatus!=Status.Sold).Count();
            var model = _mapper.Map<OwnersVM>(owner);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OwnersVM ownersVM)
        {
           
            var owner = await _unitOfWork.CrCasOwner.FindAsync(x => x.CrCasOwnersCode == ownersVM.CrCasOwnersCode,
                new[] { "CrCasOwnersLessorCodeNavigation" });
            if (owner == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View();
            }
            var model = _mapper.Map<CrCasOwner>(ownersVM);
            if (await _ownerService.UpdateOwnerInCas(model))
            {
                // SaveTracing
                var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201004", "2");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "204", "20", currentUser.CrMasUserInformationLessor, "100",
                    model.CrCasOwnersCode, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("Owners", "Owners");
            }

            return View(ownersVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            var loginUser = await _userManager.GetUserAsync(User);
            var lessorCode = loginUser.CrMasUserInformationLessor;
            string sAr = "";
            string sEn = "";

            var owner = await _unitOfWork.CrCasOwner.FindAsync(x => x.CrCasOwnersCode == code&&x.CrCasOwnersLessorCode==lessorCode);

            if (owner != null)
            {
                var cars = _unitOfWork.CrCasCarInformation.FindAll(l => l.CrCasCarInformationLessor == lessorCode && l.CrCasCarInformationOwner == owner.CrCasOwnersCode);
                if (await CheckUserSubValidationProcdures("2201004", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Stop";
                        owner.CrCasOwnersStatus = Status.Hold;
                        foreach (var car in cars) car.CrCasCarInformationOwnerStatus = Status.Hold;

                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Delete";
                        owner.CrCasOwnersStatus = Status.Deleted;
                        foreach (var car in cars) car.CrCasCarInformationOwnerStatus = Status.Deleted;

                    }
                    else if (status == Status.Active)
                    {
                        sAr = "استرجاع";
                        sEn = "Retrieve";
                        owner.CrCasOwnersStatus = Status.Active;
                        foreach (var car in cars) car.CrCasCarInformationOwnerStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201004", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "204", "20", currentUser.CrMasUserInformationLessor, "100",
                        owner.CrCasOwnersCode, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("Owners", "Owners");
                }
            }


            return View(owner);

        }
    }
}
