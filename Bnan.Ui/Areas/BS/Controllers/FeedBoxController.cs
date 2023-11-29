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
            var userInformation = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationLessor == lessorCode && x.CrMasUserInformationCode == userLogin.CrMasUserInformationCode, new[] { "CrMasUserBranchValidities.CrMasUserBranchValidity1" });
            var branchesValidite = userInformation.CrMasUserBranchValidities.Where(x => x.CrMasUserBranchValidityBranchStatus == Status.Active);
            List<CrCasBranchInformation> branches= new List<CrCasBranchInformation>();
            if (branchesValidite!=null)
            {
                foreach (var item in branchesValidite)
                {
                    branches.Add(item.CrMasUserBranchValidity1);
                }
            }
            else
            {
                return RedirectToAction("Logout", "Account", new { area = "Identity" });
            }

            var selectBranch = userLogin.CrMasUserInformationDefaultBranch;
            if (selectBranch == null||selectBranch=="000") selectBranch = "100";
            var checkBranch = branches.Find(x => x.CrCasBranchInformationCode == selectBranch);
            if (checkBranch == null) selectBranch = branches.FirstOrDefault().CrCasBranchInformationCode;
            var branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == selectBranch);
            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                SelectedBranch = selectBranch,
                CrCasBranchInformation = branch
            };
            return View(bSLayoutVM);
        }
    }
}
