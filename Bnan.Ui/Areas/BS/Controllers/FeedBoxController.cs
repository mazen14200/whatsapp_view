using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class FeedBoxController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<FeedBoxController> _localizer;
        public FeedBoxController(IStringLocalizer<FeedBoxController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bSLayoutVM = await GetBranchesAndLayout();


            var adminstrive = _unitOfWork.CrCasSysAdministrativeProcedure.Find(x => x.CrCasSysAdministrativeProceduresLessor == lessorCode &&
                                                                                 x.CrCasSysAdministrativeProceduresTargeted == userLogin.CrMasUserInformationCode &&
                                                                                 x.CrCasSysAdministrativeProceduresCode == "303" &&
                                                                                 x.CrCasSysAdministrativeProceduresStatus == Status.Insert);
            bSLayoutVM.CrCasSysAdministrativeProcedure = adminstrive;
            return View(bSLayoutVM);
        }
        [HttpPost]
        public async Task<IActionResult> AcceptOrNot(string status,string reasons)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var adminstrive = _unitOfWork.CrCasSysAdministrativeProcedure.Find(x => x.CrCasSysAdministrativeProceduresLessor == lessorCode &&
                                                                                 x.CrCasSysAdministrativeProceduresTargeted == userLogin.CrMasUserInformationCode &&
                                                                                 x.CrCasSysAdministrativeProceduresCode == "303" &&
                                                                                 x.CrCasSysAdministrativeProceduresStatus == Status.Insert);
            if (status == Status.Accept) adminstrive.CrCasSysAdministrativeProceduresStatus = Status.Accept;
            else adminstrive.CrCasSysAdministrativeProceduresStatus = Status.Reject;
            adminstrive.CrCasSysAdministrativeProceduresReasons= reasons;
            if (await _unitOfWork.CompleteAsync() > 0) return Json(true);

            return Json(false);

        }

        public IActionResult SuccesssMessageForFeedBox()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index", "Home");
        }
    }
}
