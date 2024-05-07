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
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class FeedBoxController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IStringLocalizer<FeedBoxController> _localizer;
        private readonly IToastNotification _toastNotification;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        //private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public FeedBoxController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IUserLoginsService userLoginsService, IToastNotification toastNotification, IStringLocalizer<FeedBoxController> localizer, IAdminstritiveProcedures adminstritiveProcedures, IUserService userService) : base(userManager, unitOfWork, mapper)
        {
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _adminstritiveProcedures = adminstritiveProcedures;
            _userService = userService;
        }

        public async Task<IActionResult> FeedBox()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("204", "2204001", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "0";

            // Set Title
            var titles = await setTitle("204", "2204001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);

            if (userLessor == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // Exclude the current user from the list
            var usersByLessor = await _userService.GetAllUsersByLessor(userLessor.CrMasUserInformationLessor);
            var usersWithOutMangerAndCurrentUser = usersByLessor.Where(x => x.CrMasUserInformationCode != ("CAS" + userLessor.CrMasUserInformationLessor) &&
                                                                            x.CrMasUserInformationCode != userLessor.CrMasUserInformationCode &&
                                                                            x.CrMasUserInformationStatus == Status.Active &&
                                                                            x.CrMasUserInformationAuthorizationBranch == true);

            List<CrMasUserInformation> ListUsers = new List<CrMasUserInformation>();

            if (usersWithOutMangerAndCurrentUser != null)
            {
                foreach (var item in usersWithOutMangerAndCurrentUser)
                {
                    var proc = _unitOfWork.CrCasSysAdministrativeProcedure.Find(a => a.CrCasSysAdministrativeProceduresLessor == userLessor.CrMasUserInformationLessor &&
                     a.CrCasSysAdministrativeProceduresCode == "303" && a.CrCasSysAdministrativeProceduresStatus == Status.Insert
                     && a.CrCasSysAdministrativeProceduresTargeted == item.CrMasUserInformationCode);
                    if (proc == null)
                    {
                        ListUsers.Add(item);
                    }
                }
            }
            return View(ListUsers);
        }
        [HttpGet]
        public async Task<IActionResult> Send(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "0";
            //To Set Title;
            var titles = await setTitle("204", "2204001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "اضافة", "Create", titles[3]);

            ViewBag.Date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            var currentUser = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == id);
            if (currentUser == null)
            {
                _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("FeedBox");
            }

            return View(currentUser);
        }
        [HttpPost]
        public async Task<IActionResult> Send(CrMasUserInformation Model, string FeedValue, string Reasons)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var result = await _adminstritiveProcedures.SaveAdminstritive(userLogin.CrMasUserInformationCode, "1", "303", "30", userLogin.CrMasUserInformationLessor, "100",
            Model.CrMasUserInformationCode, decimal.Parse(FeedValue, CultureInfo.InvariantCulture), null, null, null, null, null, null, null, "تحت الإجراء", "Under Proccessing", "I", Reasons);

            // SaveTracing
            var (mainTask, subTask, system, currentUserr) = await SetTrace("204", "2204001", "2");
            await _userLoginsService.SaveTracing(currentUserr.CrMasUserInformationCode, "تحت الإجراء", "Under Proccessing", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            if (result) _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            else _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("FeedBox");
        }
    }
}
