using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.Areas.CAS.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using NToastNotify;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class HomeController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(IStringLocalizer<HomeController> localizer, BnanKSAContext context, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == lessorCode&&x.CrCasBranchInformationStatus!=Status.Deleted).ToList();
            var selectBranch = userLogin.CrMasUserInformationDefaultBranch;
            if (selectBranch == null) selectBranch = "100";
            var Cars = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationStatus != Status.Deleted&&x.CrCasCarInformationStatus!=Status.Sold && x.CrCasCarInformationBranch == selectBranch, new[] { "CrCasCarInformationDistributionNavigation" }).ToList();
            var carsRented = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode &&x.CrCasCarInformationBranch== selectBranch && x.CrCasCarInformationStatus == Status.Rented, new[] { "CrCasCarInformationDistributionNavigation" }).ToList();

            var carsAvailable = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == selectBranch && x.CrCasCarInformationStatus == Status.Active &&
                                                                                x.CrCasCarInformationPriceStatus == true && x.CrCasCarInformationBranchStatus == Status.Active &&
                                                                                x.CrCasCarInformationOwnerStatus == Status.Active &&
                                                                               (x.CrCasCarInformationForSaleStatus == Status.Active || x.CrCasCarInformationForSaleStatus == Status.RendAndForSale),
                                                                               new[] { "CrCasCarInformationDistributionNavigation", "CrCasCarInformationDistributionNavigation.CrCasPriceCarBasics" }).ToList();
            var carsUnAvailable = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == selectBranch && ( x.CrCasCarInformationStatus == Status.Hold ||
                                                                                x.CrCasCarInformationStatus == Status.Maintaince || x.CrCasCarInformationPriceStatus == false ||
                                                                                x.CrCasCarInformationBranchStatus != Status.Active || x.CrCasCarInformationOwnerStatus != Status.Active ||
                                                                               (x.CrCasCarInformationStatus == Status.Active && x.CrCasCarInformationForSaleStatus == Status.ForSale)),
                                                                               new[] { "CrCasCarInformationDistributionNavigation", "CrCasCarInformationDistributionNavigation.CrCasPriceCarBasics" }).ToList();
            var documentsMaintenance = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(x => x.CrCasCarDocumentsMaintenanceLessor == lessorCode && x.CrCasCarDocumentsMaintenanceBranch == selectBranch, new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation" }).ToList();
            ViewBag.carCount = Cars.Count();
            ViewBag.AvaliableCars = carsAvailable.Count();
            ViewBag.UnAvaliableCars = carsUnAvailable.Count();
            ViewBag.RentedCars = carsRented.Count();
            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                RentedCars = null,
                AvaliableCars = null,
                UnAvaliableCars = null,
                SelectedBranch = selectBranch,
            };
            return View(bSLayoutVM);
        }
        public async Task<IActionResult> ChangeBranch(string selectedBranch)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var checkBranchCode = await _unitOfWork.CrCasBranchInformation.FindAsync(x=>x.CrCasBranchInformationLessor== lessorCode && x.CrCasBranchInformationCode==selectedBranch);
            if (checkBranchCode!=null) userLogin.CrMasUserInformationDefaultBranch = selectedBranch;
            else userLogin.CrMasUserInformationDefaultBranch = "100";
            await _unitOfWork.CompleteAsync();
            return Json(true);
        }
        [HttpGet]
        public async Task<PartialViewResult> GetRentedCars()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var BranchCode = userLogin.CrMasUserInformationDefaultBranch;
            var RentedCars = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode&&x.CrCasCarInformationBranch== BranchCode && x.CrCasCarInformationStatus == Status.Rented, new[] { "CrCasCarInformationDistributionNavigation" }).ToList();
            var branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == lessorCode).ToList();

            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                RentedCars= RentedCars,
                DocumentsMaintenances=null,
                UnAvaliableCars=null,
                AvaliableCars= null,
                SelectedBranch= BranchCode,
            };
            return PartialView("_RentedCars", bSLayoutVM);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetUnAvaliableCars()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var BranchCode = userLogin.CrMasUserInformationDefaultBranch;
            var carsUnAvailable = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == BranchCode && ( x.CrCasCarInformationStatus == Status.Rented || x.CrCasCarInformationStatus == Status.Hold ||
                                                                                           x.CrCasCarInformationStatus == Status.Maintaince || x.CrCasCarInformationPriceStatus == false ||
                                                                                           x.CrCasCarInformationBranchStatus != Status.Active || x.CrCasCarInformationOwnerStatus != Status.Active ||
                                                                                          (x.CrCasCarInformationStatus == Status.Active && x.CrCasCarInformationForSaleStatus == Status.ForSale)),
                                                                                          new[] { "CrCasCarInformationDistributionNavigation", "CrCasCarInformationDistributionNavigation.CrCasPriceCarBasics" }).ToList();

            var branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == lessorCode).ToList();

            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                RentedCars = null,
                UnAvaliableCars= carsUnAvailable,
                AvaliableCars = null,
                DocumentsMaintenances = null,
                SelectedBranch = BranchCode,
            };
            return PartialView("_UnAvailableCar", bSLayoutVM);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetAvaliableCars()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var BranchCode = userLogin.CrMasUserInformationDefaultBranch;

            var carsAvailable = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == BranchCode && x.CrCasCarInformationStatus == Status.Active &&
                                                                                x.CrCasCarInformationPriceStatus == true && x.CrCasCarInformationBranchStatus == Status.Active &&
                                                                                x.CrCasCarInformationOwnerStatus == Status.Active &&
                                                                               (x.CrCasCarInformationForSaleStatus == Status.Active || x.CrCasCarInformationForSaleStatus == Status.RendAndForSale),
                                                                               new[] { "CrCasCarInformationDistributionNavigation", "CrCasCarInformationDistributionNavigation.CrCasPriceCarBasics" }).ToList();
            var documentsMaintenance = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(x => x.CrCasCarDocumentsMaintenanceLessor == lessorCode && x.CrCasCarDocumentsMaintenanceBranch == "100", new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation" }).ToList();

            var branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == lessorCode).ToList();

            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                RentedCars = null,
                UnAvaliableCars = null,
                AvaliableCars= carsAvailable,
                DocumentsMaintenances = documentsMaintenance,
                SelectedBranch = BranchCode,

            };
            return PartialView("_AvaliableCar", bSLayoutVM);
        }
        
        [HttpGet]
        public IActionResult SetLanguage(string returnUrl, string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}
