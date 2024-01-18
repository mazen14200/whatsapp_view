using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
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
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class HomeController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(IStringLocalizer<HomeController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            //To Set Title 
            var userLogin = await _userManager.GetUserAsync(User);
            if (CultureInfo.CurrentUICulture.Name== "en-US") await ViewData.SetPageTitleAsync("Branches", "", "", "", "", userLogin.CrMasUserInformationEnName);
            else await ViewData.SetPageTitleAsync("الفروع", "", "", "", "", userLogin.CrMasUserInformationArName);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bSLayoutVM = await GetBranchesAndLayout();


            var Cars = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationStatus != Status.Deleted && x.CrCasCarInformationStatus != Status.Sold && x.CrCasCarInformationBranch == bSLayoutVM.SelectedBranch, new[] { "CrCasCarInformationDistributionNavigation" }).ToList();
            var carsRented = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == bSLayoutVM.SelectedBranch && x.CrCasCarInformationStatus == Status.Rented, new[] { "CrCasCarInformationDistributionNavigation" }).ToList();

            var carsAvailable = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == bSLayoutVM.SelectedBranch && x.CrCasCarInformationStatus == Status.Active &&
                                                                                x.CrCasCarInformationPriceStatus == true && x.CrCasCarInformationBranchStatus == Status.Active &&
                                                                                x.CrCasCarInformationOwnerStatus == Status.Active &&
                                                                               (x.CrCasCarInformationForSaleStatus == Status.Active || x.CrCasCarInformationForSaleStatus == Status.RendAndForSale),
                                                                               new[] { "CrCasCarInformationDistributionNavigation", "CrCasCarInformationDistributionNavigation.CrCasPriceCarBasics" }).ToList();
            var carsUnAvailable = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == bSLayoutVM.SelectedBranch && (x.CrCasCarInformationStatus == Status.Hold ||
                                                                                x.CrCasCarInformationStatus == Status.Maintaince || x.CrCasCarInformationPriceStatus == false ||
                                                                                x.CrCasCarInformationBranchStatus != Status.Active || x.CrCasCarInformationOwnerStatus != Status.Active ||
                                                                               (x.CrCasCarInformationStatus == Status.Active && x.CrCasCarInformationForSaleStatus == Status.ForSale)),
                                                                               new[] { "CrCasCarInformationDistributionNavigation", "CrCasCarInformationDistributionNavigation.CrCasPriceCarBasics" }).ToList();
            var documentsMaintenance = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(x => x.CrCasCarDocumentsMaintenanceLessor == lessorCode && x.CrCasCarDocumentsMaintenanceBranch == bSLayoutVM.SelectedBranch, new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation" }).ToList();
            ViewBag.carCount = Cars.Count();
            ViewBag.AvaliableCars = carsAvailable.Count();
            ViewBag.UnAvaliableCars = carsUnAvailable.Count();
            ViewBag.RentedCars = carsRented.Count();
            var userInfo = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == userLogin.CrMasUserInformationCode, new[] { "CrMasUserBranchValidities" });
            var branchValidity = userInfo.CrMasUserBranchValidities.FirstOrDefault(x => x.CrMasUserBranchValidityBranch == bSLayoutVM.SelectedBranch);

            var Documents = _unitOfWork.CrCasBranchDocument.FindAll(x => x.CrCasBranchDocumentsLessor == lessorCode && x.CrCasBranchDocumentsBranch == bSLayoutVM.SelectedBranch).ToList();
            ViewBag.CompanyDocumentsRenewedBS = Documents.Where(x => x.CrCasBranchDocumentsStatus == Status.Renewed).Count();
            ViewBag.CompanyDocumentsAboutExpireBS = Documents.Where(x => x.CrCasBranchDocumentsStatus == Status.AboutToExpire).Count();
            ViewBag.CompanyDocumentsExpiredBS = Documents.Where(x => x.CrCasBranchDocumentsStatus == Status.Expire).Count();

            var DocumentsCar = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(x => x.CrCasCarDocumentsMaintenanceLessor == lessorCode && x.CrCasCarDocumentsMaintenanceBranch == bSLayoutVM.SelectedBranch && x.CrCasCarDocumentsMaintenanceProceduresClassification == "12").ToList();
            ViewBag.CompanyDocumentsCarRenewedBS = DocumentsCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.Renewed).Count();
            ViewBag.CompanyDocumentsCarAboutExpireBS = DocumentsCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.AboutToExpire).Count();
            ViewBag.CompanyDocumentsCarExpiredBS = DocumentsCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.Expire).Count();

            var MaintainceCar = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(x => x.CrCasCarDocumentsMaintenanceLessor == lessorCode && x.CrCasCarDocumentsMaintenanceBranch == bSLayoutVM.SelectedBranch && x.CrCasCarDocumentsMaintenanceProceduresClassification == "13").ToList();
            ViewBag.CompanyMaintainceCarRenewedBS = MaintainceCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.Renewed).Count();
            ViewBag.CompanyMaintainceCarAboutExpireBS = MaintainceCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.AboutToExpire).Count();
            ViewBag.CompanyMaintainceCarExpiredBS = MaintainceCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.Expire).Count();

            var PriceCar = _unitOfWork.CrCasPriceCarBasic.FindAll(x => x.CrCasPriceCarBasicLessorCode == lessorCode).ToList();
            ViewBag.PriceCarRenewedBS = PriceCar.Where(x => x.CrCasPriceCarBasicStatus == Status.Renewed).Count();
            ViewBag.PriceCarAboutExpireBS = PriceCar.Where(x => x.CrCasPriceCarBasicStatus == Status.AboutToExpire).Count();
            ViewBag.PriceCarExpiredBS = PriceCar.Where(x => x.CrCasPriceCarBasicStatus == Status.Expire).Count();

            ViewBag.Adminstritive = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresLessor == lessorCode &&
                                                                                x.CrCasSysAdministrativeProceduresTargeted == userLogin.CrMasUserInformationCode &&
                                                                                x.CrCasSysAdministrativeProceduresCode == "303" &&
                                                                                x.CrCasSysAdministrativeProceduresStatus == Status.Insert).Count();

            var Contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicLessor == lessorCode && x.CrCasRenterContractBasicBranch == bSLayoutVM.SelectedBranch).ToList();
            var AlertContract = _unitOfWork.CrCasRenterContractAlert.FindAll(x => x.CrCasRenterContractAlertLessor == lessorCode&& x.CrCasRenterContractAlertBranch== bSLayoutVM.SelectedBranch).ToList();

           //For Charts 
           var AccountReceipt= _unitOfWork.CrCasAccountReceipt.FindAll(x=>x.CrCasAccountReceiptLessorCode==lessorCode&&x.CrCasAccountReceiptBranchCode== bSLayoutVM.SelectedBranch &&
                                                                          x.CrCasAccountReceiptIsPassing!="4"&& x.CrCasAccountReceiptPaymentMethod!="30"&&
                                                                          x.CrCasAccountReceiptPaymentMethod!="40").ToList();
            //For Branch
            ViewBag.CashBalance = AccountReceipt.Where(x => x.CrCasAccountReceiptPaymentMethod == "10").Sum(x => x.CrCasAccountReceiptPayment);
            ViewBag.MadaaBalance = AccountReceipt.Where(x => x.CrCasAccountReceiptPaymentMethod == "20").Sum(x => x.CrCasAccountReceiptPayment);
            ViewBag.VisaBalance = AccountReceipt.Where(x => x.CrCasAccountReceiptPaymentMethod == "21").Sum(x => x.CrCasAccountReceiptPayment);
            ViewBag.MasterBalance = AccountReceipt.Where(x => x.CrCasAccountReceiptPaymentMethod == "22").Sum(x => x.CrCasAccountReceiptPayment);
            ViewBag.ExpressBalance = AccountReceipt.Where(x => x.CrCasAccountReceiptPaymentMethod == "23").Sum(x => x.CrCasAccountReceiptPayment);
            //For User
            ViewBag.UserCashBalance = AccountReceipt.Where(x => x.CrCasAccountReceiptPaymentMethod == "10" &&x.CrCasAccountReceiptUser==userLogin.CrMasUserInformationCode).Sum(x => x.CrCasAccountReceiptPayment);
            ViewBag.UserMadaaBalance = AccountReceipt.Where(x => x.CrCasAccountReceiptPaymentMethod == "20" && x.CrCasAccountReceiptUser == userLogin.CrMasUserInformationCode).Sum(x => x.CrCasAccountReceiptPayment);
            ViewBag.UserVisaBalance = AccountReceipt.Where(x => x.CrCasAccountReceiptPaymentMethod == "21" && x.CrCasAccountReceiptUser == userLogin.CrMasUserInformationCode).Sum(x => x.CrCasAccountReceiptPayment);
            ViewBag.UserMasterBalance = AccountReceipt.Where(x => x.CrCasAccountReceiptPaymentMethod == "22" && x.CrCasAccountReceiptUser == userLogin.CrMasUserInformationCode).Sum(x => x.CrCasAccountReceiptPayment);
            ViewBag.UserExpressBalance = AccountReceipt.Where(x => x.CrCasAccountReceiptPaymentMethod == "23" && x.CrCasAccountReceiptUser == userLogin.CrMasUserInformationCode).Sum(x => x.CrCasAccountReceiptPayment);

            ViewBag.RenterLessorCount = _unitOfWork.CrCasRenterLessor.FindAll(x => x.CrCasRenterLessorCode == lessorCode).Count();
            ViewBag.AcccountReceiptCount = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptLessorCode == lessorCode &&
                                                                                     x.CrCasAccountReceiptBranchCode==bSLayoutVM.SelectedBranch).Count();
            bSLayoutVM.RentedCars = null;
            bSLayoutVM.UnAvaliableCars = null;
            bSLayoutVM.AvaliableCars = null;
            bSLayoutVM.CrMasUserBranchValidity = branchValidity;
            bSLayoutVM.BasicContracts = Contracts;
            bSLayoutVM.AlertContract = AlertContract;
           
            return View(bSLayoutVM);
        }
        public async Task<IActionResult> ChangeBranch(string selectedBranch)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var checkBranchCode = await _unitOfWork.CrCasBranchInformation.FindAsync(x => x.CrCasBranchInformationLessor == lessorCode && x.CrCasBranchInformationCode == selectedBranch);
            if (checkBranchCode != null) userLogin.CrMasUserInformationDefaultBranch = selectedBranch;
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
            var RentedCars = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == BranchCode && x.CrCasCarInformationStatus == Status.Rented, new[] { "CrCasCarInformationDistributionNavigation" }).ToList();
            var branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == lessorCode).ToList();

            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                RentedCars = RentedCars,
                DocumentsMaintenances = null,
                UnAvaliableCars = null,
                AvaliableCars = null,
                SelectedBranch = BranchCode,
            };
            return PartialView("_RentedCars", bSLayoutVM);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetUnAvaliableCars()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var BranchCode = userLogin.CrMasUserInformationDefaultBranch;
            var carsUnAvailable = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == BranchCode && (x.CrCasCarInformationStatus == Status.Rented || x.CrCasCarInformationStatus == Status.Hold ||
                                                                                           x.CrCasCarInformationStatus == Status.Maintaince || x.CrCasCarInformationPriceStatus == false ||
                                                                                           x.CrCasCarInformationBranchStatus != Status.Active || x.CrCasCarInformationOwnerStatus != Status.Active ||
                                                                                          (x.CrCasCarInformationStatus == Status.Active && x.CrCasCarInformationForSaleStatus == Status.ForSale)),
                                                                                          new[] { "CrCasCarInformationDistributionNavigation", "CrCasCarInformationDistributionNavigation.CrCasPriceCarBasics" }).ToList();

            var branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == lessorCode&&x.CrCasBranchInformationStatus!=Status.Deleted).ToList();

            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                RentedCars = null,
                UnAvaliableCars = carsUnAvailable,
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
            var branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == lessorCode && x.CrCasBranchInformationStatus != Status.Deleted).ToList();
            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                RentedCars = null,
                UnAvaliableCars = null,
                AvaliableCars = carsAvailable,
                DocumentsMaintenances = documentsMaintenance,
                SelectedBranch = BranchCode,

            };
            return PartialView("_AvaliableCar", bSLayoutVM);
        }
        [HttpGet]
        public async Task<IActionResult> CheckCompanyDocuments(string selectedBranch)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var documents = _unitOfWork.CrCasBranchDocument.FindAll(x => x.CrCasBranchDocumentsLessor == lessorCode && x.CrCasBranchDocumentsBranch == selectedBranch);
            var documentNotActive = documents.Where(x=>x.CrCasBranchDocumentsStatus==Status.Renewed || x.CrCasBranchDocumentsStatus==Status.Expire).ToList();
            var userContractValidity = _unitOfWork.CrMasUserContractValidity.Find(x => x.CrMasUserContractValidityUserId == userLogin.Id);
            var check = "true";
            foreach ( var document in documentNotActive )
            {
                if (document.CrCasBranchDocumentsProcedures=="100"&& userContractValidity.CrMasUserContractValidityRegister == false) check="100";
                else if (document.CrCasBranchDocumentsProcedures=="101" && userContractValidity.CrMasUserContractValidityChamber == false) check = "101";
                else if (document.CrCasBranchDocumentsProcedures == "102" && userContractValidity.CrMasUserContractValidityTransferPermission == false)check = "102";
                else if (document.CrCasBranchDocumentsProcedures == "103" && userContractValidity.CrMasUserContractValidityLicenceMunicipale == false)check = "103";
                else if (document.CrCasBranchDocumentsProcedures == "104" && userContractValidity.CrMasUserContractValidityCompanyAddress == false)check = "104";
                else check = "true";
            }
            return Json(check);
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
