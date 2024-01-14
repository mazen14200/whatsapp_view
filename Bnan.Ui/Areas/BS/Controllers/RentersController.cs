using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class RentersController  : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<RentersController> _localizer;
        private readonly IContract _ContractServices;
        public RentersController(IStringLocalizer<RentersController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IContract contractServices) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _ContractServices = contractServices;
        }
        public async Task<IActionResult> Index()
        {
            //To Set Title 
            var userLogin = await _userManager.GetUserAsync(User);
            if (CultureInfo.CurrentUICulture.Name == "en-US") await ViewData.SetPageTitleAsync("Branches", "Renters", "", "", "", userLogin.CrMasUserInformationEnName);
            else await ViewData.SetPageTitleAsync("الفروع", "المستأجرين", "", "", "", userLogin.CrMasUserInformationArName);
            var lessorCode = userLogin.CrMasUserInformationLessor;

            var userInformation = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationLessor == lessorCode && x.CrMasUserInformationCode == userLogin.CrMasUserInformationCode, new[] { "CrMasUserBranchValidities.CrMasUserBranchValidity1" });
            var branchesValidite = userInformation.CrMasUserBranchValidities.Where(x => x.CrMasUserBranchValidityBranchStatus == Status.Active);
            List<CrCasBranchInformation> branches = new List<CrCasBranchInformation>();
            if (branchesValidite != null)
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
            if (selectBranch == null || selectBranch == "000") selectBranch = "100";
            var checkBranch = branches.Find(x => x.CrCasBranchInformationCode == selectBranch);
            if (checkBranch == null) selectBranch = branches.FirstOrDefault().CrCasBranchInformationCode;
            var branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == selectBranch);

            var RenterAll = _unitOfWork.CrCasRenterLessor.FindAll(x => x.CrCasRenterLessorCode == userLogin.CrMasUserInformationLessor, new[] { "CrCasRenterLessorNavigation" }).ToList();

            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                SelectedBranch = selectBranch,
                CrCasBranchInformation = branch,
                RentersLessor= RenterAll
            };
            return View(bSLayoutVM);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetRentersByStatus(string status, string search)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            BSLayoutVM bSLayoutVM = new BSLayoutVM();
            if (!string.IsNullOrEmpty(status))
            {
                var RenterAll = _unitOfWork.CrCasRenterLessor.FindAll(x => x.CrCasRenterLessorCode == userLogin.CrMasUserInformationLessor, new[] { "CrCasRenterLessorNavigation" }).ToList();
                if (status == Status.All) {
                    bSLayoutVM.RentersLessor = RenterAll.FindAll(x => x.CrCasRenterLessorId.Contains(search) || x.CrCasRenterLessorNavigation.CrMasRenterInformationArName.Contains(search) || x.CrCasRenterLessorNavigation.CrMasRenterInformationEnName.Contains(search)).ToList();
                    return PartialView("_RentersDataTable", bSLayoutVM);
                }

                bSLayoutVM.RentersLessor = RenterAll.Where(x => x.CrCasRenterLessorStatus == status&&
                                                              ( x.CrCasRenterLessorId.Contains(search) ||
                                                                x.CrCasRenterLessorNavigation.CrMasRenterInformationArName.Contains(search) ||
                                                                x.CrCasRenterLessorNavigation.CrMasRenterInformationEnName.Contains(search))).ToList();
               
                return PartialView("_RentersDataTable", bSLayoutVM);
            }
            return PartialView();
        }
        public async Task<IActionResult> Details(string id)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            if (CultureInfo.CurrentUICulture.Name == "en-US") await ViewData.SetPageTitleAsync("Branches", "Renters", "RenterInformation", "", "", userLogin.CrMasUserInformationEnName);
            else await ViewData.SetPageTitleAsync("الفروع", "المستأجرين", "بيانات المستأجر", "", "", userLogin.CrMasUserInformationArName);
            var Renter = await _unitOfWork.CrCasRenterLessor.FindAsync(x=>x.CrCasRenterLessorId==id&&x.CrCasRenterLessorCode==lessorCode,
                                                                       new[] { "CrCasRenterLessorNavigation.CrMasRenterInformationEmployerNavigation",
                                                                               "CrCasRenterLessorNavigation.CrMasRenterInformationDrivingLicenseTypeNavigation",
                                                                               "CrCasRenterLessorNavigation.CrMasRenterPost",
                                                                               "CrCasRenterContractBasicCrCasRenterContractBasicNavigations",
                                                                               "CrCasRenterLessorIdtrypeNavigation",
                                                                               "CrCasRenterLessorMembershipNavigation",
                                                                               "CrCasRenterLessorSectorNavigation",
                                                                               "CrCasRenterLessorStatisticsCityNavigation",
                                                                               "CrCasRenterLessorStatisticsGenderNavigation",
                                                                               "CrCasRenterLessorStatisticsJobsNavigation",
                                                                               "CrCasRenterLessorStatisticsNationalitiesNavigation",
                                                                               "CrCasRenterLessorStatisticsRegionsNavigation"});
            if (Renter==null)
            {
                _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("Index");
            }
            var Contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicRenterId == id && x.CrCasRenterContractBasicLessor == lessorCode, new[] { "CrCasRenterContractBasicCarSerailNoNavigation" }).ToList();
            BSLayoutVM bSLayoutVM = new BSLayoutVM();
            bSLayoutVM.Renter = Renter;
            bSLayoutVM.RenterContracts = Contracts;
            return View(bSLayoutVM);
        }
    }
}
