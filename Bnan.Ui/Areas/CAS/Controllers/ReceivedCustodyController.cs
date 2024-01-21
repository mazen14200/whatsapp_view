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
    public class ReceivedCustodyController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<ReceivedCustodyController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;


        public ReceivedCustodyController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
             IMapper mapper, IUserService userService, IAccountBank accountBank,
             IUserLoginsService userLoginsService, IToastNotification toastNotification,
             IStringLocalizer<ReceivedCustodyController> localizer, IAdminstritiveProcedures adminstritiveProcedures) :
             base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _adminstritiveProcedures = adminstritiveProcedures;
        }

        public async Task<IActionResult> Index()
        { 
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "1";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("204", "2204002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var adminstritives= _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x=>x.CrCasSysAdministrativeProceduresLessor== lessorCode && x.CrCasSysAdministrativeProceduresCode=="304", new[] { "CrCasSysAdministrativeProceduresUserInsertNavigation" }).ToList();
            return View(adminstritives);
        }


        [HttpGet]
        public async Task<PartialViewResult> GetCustodyStatus(string status)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;

            if (!string.IsNullOrEmpty(status))
            {
                var AdminstritiveAll = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresLessor == lessorCode &&x.CrCasSysAdministrativeProceduresCode=="304", new[] { "CrCasSysAdministrativeProceduresUserInsertNavigation" }).ToList();
                if (status == Status.All) return PartialView("_CustodyData", AdminstritiveAll);
                return PartialView("_CustodyData", AdminstritiveAll.Where(l => l.CrCasSysAdministrativeProceduresStatus == status));
            }
            return PartialView();
        }
    }
}
