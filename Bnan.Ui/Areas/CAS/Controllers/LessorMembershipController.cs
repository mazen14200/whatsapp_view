using AutoMapper;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class LessorMembershipController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<LessorMembershipController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IMembershipConditions _membershipConditions;
        public LessorMembershipController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IUserLoginsService userLoginsService, IUserService userService, IToastNotification toastNotification, IStringLocalizer<LessorMembershipController> localizer, IAdminstritiveProcedures adminstritiveProcedures, IMembershipConditions membershipConditions) : base(userManager, unitOfWork, mapper)
        {
            _userLoginsService = userLoginsService;
            _userService = userService;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _adminstritiveProcedures = adminstritiveProcedures;
            _membershipConditions = membershipConditions;
        }

        public async Task<IActionResult> LessorMembership()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207002", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            //sidebar Active
            ViewBag.id = "#sidebarServices";
            ViewBag.no = "1";
            var userLogin = await _userManager.GetUserAsync(User);
            var titles = await setTitle("207", "2207002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var lessorMemberships = _unitOfWork.CrCasLessorMembership.FindAll(x => x.CrCasLessorMembershipConditionsLessor == userLogin.CrMasUserInformationLessor &&
                                                                                   x.CrCasLessorMembershipConditions != "1600000006", new[] { "CrCasLessorMembershipConditionsNavigation" })
                                                           .OrderBy(x => x.CrCasLessorMembershipConditions).ToList();
            return View(lessorMemberships);
        }
        [HttpGet]
        public JsonResult GetMembershipGroup(string No)
        {
            var group =_unitOfWork.CrMasSysProbabilityMembership.Find(g => g.CrMasSysProbabilityMembershipCode == No);
            if (group != null) return Json(group.CrMasSysProbabilityMembershipGroup);
            else return Json(" ");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormCollection collection)
        {
            var userLogin = await _userManager.GetUserAsync(User);
           
            var lessorMemberships = _unitOfWork.CrCasLessorMembership.FindAll(x => x.CrCasLessorMembershipConditionsLessor == userLogin.CrMasUserInformationLessor &&
                                                                                   x.CrCasLessorMembershipConditions != "1600000006", new[] { "CrCasLessorMembershipConditionsNavigation" }).ToList();
            if (collection.Keys!=null)
            {
                foreach (string item in collection.Keys)
                {
                    if (item.StartsWith("CrCasLessorMembershipConditionsAmount-"))
                    {
                        var code = item.Replace("CrCasLessorMembershipConditionsAmount-", "");
                        if (code != null && code != "")
                        {

                            var amount = collection["CrCasLessorMembershipConditionsAmount-" + code].ToString();
                            //var cond1 = collection["Conditions1_" + code];
                            var KM = collection["CrCasLessorMembershipConditionsKm-" + code].ToString();
                            //var cond2 = collection["Conditions2_" + code];
                            var NoContract = collection["CrCasLessorMembershipConditionsContractNo-" + code].ToString();
                            var result = collection["result-" + code].ToString();
                            var Group = collection["Group-" + code].ToString();
                            var ConditionInsert = collection["CrCasLessorMembershipConditionsActivate-" + code].ToString();
                            var link1 = result[1].ToString();
                            var link2 = result[3].ToString();
                            var isActivate = false;
                            if (ConditionInsert == "on") isActivate = true;
                            if (Group!="N")
                            {
                                if (!await _membershipConditions.AddRenterMembership(userLogin?.CrMasUserInformationLessor, code, amount, link1, KM, link2, NoContract, isActivate, Group))
                                {
                                    _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                            else
                            {
                                if (!await _membershipConditions.AddRenterMembership(userLogin?.CrMasUserInformationLessor, code, "0", "3", "0", "3", "0", false, "N"))
                                {
                                    _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                            
                        }
                    }
                }
                if (await _unitOfWork.CompleteAsync() > 0)
                {
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("LessorMembership");
                }
            }
            
            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index", "Home");
        }
    }
}
