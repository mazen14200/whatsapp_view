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
using System.Reflection;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class ContractSettlementController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<ContractSettlementController> _localizer;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IContractSettlement _contractSettlement;
        public ContractSettlementController(IStringLocalizer<ContractSettlementController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IContract contractServices, IWebHostEnvironment hostingEnvironment, IContractSettlement contractSettlement) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _hostingEnvironment = hostingEnvironment;
            _contractSettlement = contractSettlement;
        }

        public async Task<IActionResult> Index()
        {
            //To Set Title 
            var titles = await setTitle("501", "5501004", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => (x.CrCasRenterContractBasicStatus == Status.Active || x.CrCasRenterContractBasicStatus == Status.Expire) &&
                                                                               x.CrCasRenterContractBasicLessor == lessorCode && x.CrCasRenterContractBasicBranch == bsLayoutVM.SelectedBranch,
                                                                               new[] { "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasic1" }).ToList();
            var contractReceivingTheCarFromAnotherBranch = _unitOfWork.CrCasRenterContractAdditional.FindAll(x => x.CrCasRenterContractAdditionalCode == "5000000003").ToList();
            foreach (var item in contractReceivingTheCarFromAnotherBranch)
            {
                var contract2 = _unitOfWork.CrCasRenterContractBasic.Find(x => x.CrCasRenterContractBasicNo == item.CrCasRenterContractAdditionalNo && (x.CrCasRenterContractBasicStatus == Status.Active || x.CrCasRenterContractBasicStatus == Status.Expire) &&
                                                                               x.CrCasRenterContractBasicLessor == lessorCode,
                                                                               new[] { "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasic1" });
                if (contract2 != null && !contracts.Contains(contract2)) contracts.Add(contract2);
            }
            var contractMap = _mapper.Map<List<ContractSettlementVM>>(contracts);
            foreach (var contract in contractMap)
            {
                var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode &&
                x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
                var ArInvoice = _unitOfWork.CrCasAccountInvoice.FindAll(x => x.CrCasAccountInvoiceReferenceContract == contract.CrCasRenterContractBasicNo).LastOrDefault().CrCasAccountInvoiceArPdfFile;
                var EnInvoice = _unitOfWork.CrCasAccountInvoice.FindAll(x => x.CrCasAccountInvoiceReferenceContract == contract.CrCasRenterContractBasicNo).LastOrDefault().CrCasAccountInvoiceEnPdfFile;
                contract.InvoiceArPdfPath = ArInvoice;
                contract.InvoiceEnPdfPath = EnInvoice;
                if (authContract != null) contract.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;

            }



            bsLayoutVM.ContractSettlements = contractMap.Where(x => x.AuthEndDate > DateTime.Now).ToList();
            return View(bsLayoutVM);
        }
        [HttpGet]
        public async Task<PartialViewResult> GetContractBySearch(string search)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => (x.CrCasRenterContractBasicStatus == Status.Active || x.CrCasRenterContractBasicStatus == Status.Expire) &&
                                                                               x.CrCasRenterContractBasicLessor == lessorCode && x.CrCasRenterContractBasicBranch == bsLayoutVM.SelectedBranch,
                                                                               new[] { "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasic1" }).ToList();
            var contractReceivingTheCarFromAnotherBranch = _unitOfWork.CrCasRenterContractAdditional.FindAll(x => x.CrCasRenterContractAdditionalCode == "5000000003").ToList();
            foreach (var item in contractReceivingTheCarFromAnotherBranch)
            {
                var contract2 = _unitOfWork.CrCasRenterContractBasic.Find(x => x.CrCasRenterContractBasicNo == item.CrCasRenterContractAdditionalNo && (x.CrCasRenterContractBasicStatus == Status.Active || x.CrCasRenterContractBasicStatus == Status.Expire) &&
                                                                               x.CrCasRenterContractBasicLessor == lessorCode,
                                                                               new[] { "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasic1" });
                if (contract2 != null && !contracts.Contains(contract2)) contracts.Add(contract2);
            }
            var contractMap = _mapper.Map<List<ContractSettlementVM>>(contracts);
            foreach (var contract in contractMap)
            {
                var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode &&
                x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
                if (authContract != null) contract.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;
            }

            if (!string.IsNullOrEmpty(search))
            {

                bsLayoutVM.ContractSettlements = contractMap.Where(x => x.AuthEndDate > DateTime.Now &&
                                                                                               (x.CrCasRenterContractBasicNo.Contains(search) ||
                                                                                                x.CrCasRenterContractBasic5.CrCasRenterLessorNavigation.CrMasRenterInformationArName.Contains(search) ||
                                                                                                x.CrCasRenterContractBasicCarSerailNoNavigation.CrCasCarInformationConcatenateArName.Contains(search) ||
                                                                                                x.CrCasRenterContractBasic5.CrCasRenterLessorNavigation.CrMasRenterInformationEnName.ToLower().Contains(search) ||
                                                                                                x.CrCasRenterContractBasicCarSerailNoNavigation.CrCasCarInformationConcatenateEnName.ToLower().Contains(search))).ToList();
                return PartialView("_ContractSettlement", bsLayoutVM);
            }
            bsLayoutVM.ContractSettlements = contractMap.Where(x => x.AuthEndDate > DateTime.Now).ToList();
            return PartialView("_ContractSettlement", bsLayoutVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            //To Set Title 
            var titles = await setTitle("501", "5501004", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == id,
                                                                                     new[] { "CrCasRenterContractBasic5.CrCasRenterLessorNavigation",
                                                                                             "CrCasRenterContractBasicCarSerailNoNavigation.CrCasCarAdvantages",
                                                                                             "CrCasRenterContractBasic1"}).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();
            var CheckupCars = _unitOfWork.CrMasSupContractCarCheckup.FindAll(x => x.CrMasSupContractCarCheckupStatus == Status.Active).ToList();
            var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode && x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
            var contractMap = _mapper.Map<ContractSettlementVM>(contract);
            var PaymentMethod = _unitOfWork.CrMasSupAccountPaymentMethod.FindAll(x => x.CrMasSupAccountPaymentMethodStatus == Status.Active && x.CrMasSupAccountPaymentMethodClassification != "4").ToList();
            var SalesPoint = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == lessorCode &&
                                                                             x.CrCasAccountSalesPointBrn == bsLayoutVM.SelectedBranch &&
                                                                             x.CrCasAccountSalesPointBankStatus == Status.Active &&
                                                                             x.CrCasAccountSalesPointStatus == Status.Active &&
                                                                             x.CrCasAccountSalesPointBranchStatus == Status.Active).ToList();
            //Get ACcount Receipt
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var autoinc1 = GetContractAccountReceipt(lessorCode, bsLayoutVM.SelectedBranch).CrCasAccountReceiptNo;
            var AccountReceiptNo = y + "-" + "1" + "301" + "-" + lessorCode + bsLayoutVM.SelectedBranch + "-" + autoinc1;
            ViewBag.AccountReceiptNo = AccountReceiptNo;
            contractMap.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;
            contractMap.AuthType = authContract.CrCasRenterContractAuthorizationType;
            contractMap.CasRenterPreviousBalance = contract.CrCasRenterContractBasic5?.CrCasRenterLessorAvailableBalance;
            var advantages = _unitOfWork.CrCasRenterContractAdvantage.FindAll(x => x.CrCasRenterContractAdvantagesNo == contract.CrCasRenterContractBasicNo).Sum(x => x.CrCasContractAdvantagesValue);
            contractMap.AdvantagesValue = advantages?.ToString("N2", CultureInfo.InvariantCulture);
            contractMap.AdvantagesValueTotal = (advantages * contract.CrCasRenterContractBasicExpectedRentalDays)?.ToString("N2", CultureInfo.InvariantCulture);
            contractMap.ChoicesValue = _unitOfWork.CrCasRenterContractChoice.FindAll(x => x.CrCasRenterContractChoiceNo == contract.CrCasRenterContractBasicNo).Sum(x => x.CrCasContractChoiceValue)?.ToString("N2", CultureInfo.InvariantCulture);
            bsLayoutVM.ContractSettlement = contractMap;
            bsLayoutVM.SalesPoint = SalesPoint;
            bsLayoutVM.PaymentMethods = PaymentMethod;
            bsLayoutVM.CarsCheckUp = CheckupCars;
            return View(bsLayoutVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BSLayoutVM bSLayoutVM, string language,string? PdfSaveAr, string? PdfSaveEn,Dictionary<string, string> ReasonsCheckUp)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var ContractInfo = bSLayoutVM.ContractSettlement;
            var OldContract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == ContractInfo.CrCasRenterContractBasicNo).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();

            var Renter = _unitOfWork.CrMasRenterInformation.Find(x => x.CrMasRenterInformationId == OldContract.CrCasRenterContractBasicRenterId);
            var RenterLessor = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == OldContract.CrCasRenterContractBasicRenterId);
            var Car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == OldContract.CrCasRenterContractBasicCarSerailNo);
            var CarPrice = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicNo == OldContract.CrCasRenterContractPriceReference);
            var Branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == bSLayoutVM.SelectedBranch && x.CrCasBranchInformationLessor == lessorCode);

            if (userLogin != null && Renter != null && Car != null && CarPrice != null && Branch != null)
            {
              var  UpdateSettlementContract = await _contractSettlement.UpdateRenterSettlementContract(OldContract.CrCasRenterContractBasicNo, userLogin.CrMasUserInformationCode, ContractInfo.ActualDaysNo, ContractInfo.SettlementMechanism, ContractInfo.CurrentMeter, ContractInfo.AdditionalKm,
                                                                                                     ContractInfo.TaxValue, ContractInfo.DiscountValue, ContractInfo.AmountRequired, ContractInfo.AmountPayed, ContractInfo.ExpensesValue, ContractInfo.ExpensesReasons, ContractInfo.CompensationValue,
                                                                                                     ContractInfo.CompensationReasons, ContractInfo.MaxHours, ContractInfo.MaxMinutes, ContractInfo.ExtraHoursValue, ContractInfo.PrivateDriverValueTotal, ContractInfo.ChoicesValueTotal, ContractInfo.AdvantagesValueTotal,
                                                                                                     ContractInfo.ContractValue, ContractInfo.ContractValueAfterDiscount, ContractInfo.TotalContract, (decimal)RenterLessor.CrCasRenterLessorAvailableBalance);
                //Account Receipt
                var CheckAccountReceipt = true;
                var passing = "";
                var SavePdfAr = "";
                var SavePdfEn = "";
                var CheckBranch = true;
                var CheckSalesPoint = true;
                var CheckBranchValidity = true;
                var CheckUserInformation = true;
                if (UpdateSettlementContract != null)
                {
                    // Account Receipt
                    if (UpdateSettlementContract.CrCasRenterContractBasicAmountPaid > 0)
                    {
                        if (ContractInfo.PaymentMethod == "30")
                        {
                            passing = "4";
                            ContractInfo.AccountNo = ContractInfo.SalesPoint;
                        }
                        else
                        {
                            passing = "1";
                        }
                        // Catch Receipt سند قبض
                        if (UpdateSettlementContract.CrCasRenterContractBasicActualAmountRequired > 0)
                        {
                            //if (!string.IsNullOrEmpty(PdfSaveAr)) SavePdfAr = await FileExtensions.SavePdf(_hostingEnvironment, PdfSaveAr, lessorCode, Branch.CrCasBranchInformationCode, ContractInfo.AccountReceiptNo, "ar");
                            //if (!string.IsNullOrEmpty(PdfSaveEn)) SavePdfEn = await FileExtensions.SavePdf(_hostingEnvironment, PdfSaveEn, lessorCode, Branch.CrCasBranchInformationCode, ContractInfo.AccountReceiptNo, "en");
                            CheckAccountReceipt = await _contractSettlement.AddAccountReceipt(UpdateSettlementContract.CrCasRenterContractBasicNo, lessorCode, Branch.CrCasBranchInformationCode,
                                                                                      ContractInfo.PaymentMethod, ContractInfo.AccountNo, UpdateSettlementContract.CrCasRenterContractBasicCarSerailNo,
                                                                                      ContractInfo.SalesPoint, (decimal)UpdateSettlementContract.CrCasRenterContractBasicAmountPaid,
                                                                                      UpdateSettlementContract.CrCasRenterContractBasicRenterId, userLogin.CrMasUserInformationCode, passing, UpdateSettlementContract.CrCasRenterContractBasicReasons, SavePdfAr, SavePdfEn, "301");
                        }
                        // Receipt سند صرف
                        else if (UpdateSettlementContract.CrCasRenterContractBasicActualAmountRequired < 0)
                        {
                            CheckAccountReceipt = await _contractSettlement.AddAccountReceipt(UpdateSettlementContract.CrCasRenterContractBasicNo, lessorCode, Branch.CrCasBranchInformationCode,
                                                                                      ContractInfo.PaymentMethod, ContractInfo.AccountNo, UpdateSettlementContract.CrCasRenterContractBasicCarSerailNo,
                                                                                      ContractInfo.SalesPoint, (decimal)UpdateSettlementContract.CrCasRenterContractBasicAmountPaid,
                                                                                      UpdateSettlementContract.CrCasRenterContractBasicRenterId, userLogin.CrMasUserInformationCode, passing, UpdateSettlementContract.CrCasRenterContractBasicReasons, SavePdfAr, SavePdfEn, "302");
                        }
                        //Update Branch Balance , But first Check if passing equal 4 or not 
                        if (passing != "4") CheckBranch = await _contractSettlement.UpdateBranchBalance(Branch.CrCasBranchInformationCode, lessorCode, (decimal)UpdateSettlementContract.CrCasRenterContractBasicAmountPaid, (decimal)UpdateSettlementContract.CrCasRenterContractBasicActualAmountRequired);
                        //Update SalesPoint Balance , But first Check if passing equal 4 or not 
                        if (!string.IsNullOrEmpty(ContractInfo.SalesPoint) && passing != "4") CheckSalesPoint = await _contractSettlement.UpdateSalesPointBalance(Branch.CrCasBranchInformationCode, lessorCode, ContractInfo.SalesPoint, (decimal)UpdateSettlementContract.CrCasRenterContractBasicAmountPaid, (decimal)UpdateSettlementContract.CrCasRenterContractBasicActualAmountRequired);
                        // UpdateBranchValidity
                        if (passing != "4") CheckBranchValidity = await _contractSettlement.UpdateBranchValidity(Branch.CrCasBranchInformationCode, lessorCode, userLogin.CrMasUserInformationCode, ContractInfo.PaymentMethod, (decimal)UpdateSettlementContract.CrCasRenterContractBasicAmountPaid, (decimal)UpdateSettlementContract.CrCasRenterContractBasicActualAmountRequired);
                        // UpdateUserBalance
                        if (passing != "4") CheckUserInformation = await _contractSettlement.UpdateUserBalance(Branch.CrCasBranchInformationCode, lessorCode, userLogin.CrMasUserInformationCode, ContractInfo.PaymentMethod, (decimal)UpdateSettlementContract.CrCasRenterContractBasicAmountPaid, (decimal)UpdateSettlementContract.CrCasRenterContractBasicActualAmountRequired);

                    }



                    // Renter Balance 
                    var CheckUpdateRenterBalance = true;
                    var TotalContractValue = UpdateSettlementContract.CrCasRenterContractBasicActualTotal - UpdateSettlementContract.CrCasRenterContractBasicExpensesValue + UpdateSettlementContract.CrCasRenterContractBasicCompensationValue;
                    CheckUpdateRenterBalance = await _contractSettlement.UpdateRenterLessor(UpdateSettlementContract.CrCasRenterContractBasicNo, (decimal)UpdateSettlementContract.CrCasRenterContractBasicActualAmountRequired,
                                                                                           (decimal)UpdateSettlementContract.CrCasRenterContractBasicAmountPaid, (decimal)UpdateSettlementContract.CrCasRenterContractBasicActualTotal, (decimal)TotalContractValue, (int)UpdateSettlementContract.CrCasRenterContractBasicActualDays);
                    // Account Contract Tax Owed 

                    var CheckAddAccountContractTaxOwed = true;
                    CheckAddAccountContractTaxOwed = await _contractSettlement.AddAccountContractTaxOwed(UpdateSettlementContract.CrCasRenterContractBasicNo, (decimal)UpdateSettlementContract.CrCasRenterContractBasicActualTotal);
                    // Alert Contract
                    var CheckUpdateAlert = true;
                    CheckUpdateAlert = await _contractSettlement.UpdateAlert(UpdateSettlementContract.CrCasRenterContractBasicNo);

                    // Authrization Contract
                    var CheckUpdateAuthrization = true;
                    CheckUpdateAuthrization = await _contractSettlement.UpdateAuthrization(UpdateSettlementContract.CrCasRenterContractBasicNo);

                    //Update Mas Renter Info Of Car Renter
                    var CheckMasRenter = true;
                    CheckMasRenter = await _contractSettlement.UpdateMasRenter(Renter.CrMasRenterInformationId);

                    //Update Driver and Private Driver and Add Driver 
                    var CheckPrivateDriver = true;
                    var CheckDriver = true;
                    var CheckAddDriver = true;
                    if (!string.IsNullOrEmpty(UpdateSettlementContract.CrCasRenterContractBasicPrivateDriverId)) CheckPrivateDriver = await _contractSettlement.UpdatePrivateDriverStatus(UpdateSettlementContract.CrCasRenterContractBasicPrivateDriverId, lessorCode);
                    else
                    {
                        //Update Driver
                        if (!string.IsNullOrEmpty(UpdateSettlementContract.CrCasRenterContractBasicDriverId) && UpdateSettlementContract.CrCasRenterContractBasicDriverId.Trim() != UpdateSettlementContract.CrCasRenterContractBasicRenterId) CheckDriver = await _contractSettlement.UpdateDriverStatus(UpdateSettlementContract.CrCasRenterContractBasicDriverId, lessorCode);
                        //Update Add Driver
                        if (!string.IsNullOrEmpty(UpdateSettlementContract.CrCasRenterContractBasicAdditionalDriverId)) CheckAddDriver = await _contractSettlement.UpdateDriverStatus(UpdateSettlementContract.CrCasRenterContractBasicAdditionalDriverId, lessorCode);
                    }

                    //Update DocAndMaintainance Of Car
                    var CheckDocAndMaintainance = await _contractSettlement.UpdateCarDocMaintainance(UpdateSettlementContract.CrCasRenterContractBasicCarSerailNo, lessorCode,
                                                                                                   Branch.CrCasBranchInformationCode, int.Parse(ContractInfo.CurrentMeter));

                    //Update Information Of Car
                    var CheckCarInfo = true;
                    CheckCarInfo = await _contractSettlement.UpdateCarInformation(UpdateSettlementContract.CrCasRenterContractBasicCarSerailNo, lessorCode, Branch.CrCasBranchInformationCode,
                                                                                                              int.Parse(ContractInfo.CurrentMeter), CheckDocAndMaintainance);

                    //CheckUp
                    var CheckCheckUpCar = true;
                    if (ReasonsCheckUp != null)
                    {
                        foreach (var item in ReasonsCheckUp)
                        {
                            string Code = item.Key;
                            string Reason = item.Value;
                            if (CheckCheckUpCar && !string.IsNullOrEmpty(Reason)) CheckCheckUpCar = await _contractSettlement.UpdateRenterContractCheckUp(lessorCode, UpdateSettlementContract.CrCasRenterContractBasicNo, UpdateSettlementContract.CrCasRenterContractBasicCarSerailNo, UpdateSettlementContract.CrCasRenterContractPriceReference, Code, Reason);
                        }
                    }

                    // add Account Contract Company Owed
                    var ChechAddAccountContractCompanyOwed = true;
                    var CompanyContract = await _unitOfWork.CrMasContractCompany.FindAsync(x => x.CrMasContractCompanyLessor == UpdateSettlementContract.CrCasRenterContractBasicLessor && x.CrMasContractCompanyProcedures == "112");//ForBnan Contract
                    if (CompanyContract.CrMasContractCompanyActivation != "1")
                    {
                        ChechAddAccountContractCompanyOwed = await _contractSettlement.AddAccountContractCompanyOwed(UpdateSettlementContract.CrCasRenterContractBasicNo, ContractInfo.ActualDaysNo, (decimal)UpdateSettlementContract.CrCasRenterContractBasicActualDailyRent);
                    }

                    // add Account Contract Company Owed
                    var ChechUpdateRenterStatistics = true;
                    ChechUpdateRenterStatistics = await _contractSettlement.UpdateRenterStatistics(UpdateSettlementContract);
                    if (UpdateSettlementContract!=null && CheckAccountReceipt&& CheckBranch&& CheckSalesPoint&& CheckBranchValidity&& CheckUserInformation&&
                        CheckMasRenter && CheckUpdateAuthrization&& CheckUpdateAlert&& CheckAddAccountContractTaxOwed && CheckUpdateRenterBalance && CheckAddDriver&&
                        CheckDriver&& CheckPrivateDriver&&!string.IsNullOrEmpty(CheckDocAndMaintainance)&& CheckCarInfo&& CheckCheckUpCar&& ChechAddAccountContractCompanyOwed &&ChechUpdateRenterStatistics )
                    {
                        try
                        {
                            if (await _unitOfWork.CompleteAsync() > 0)
                            {
                                _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        catch (Exception ex)
                        {
                            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                            return RedirectToAction("Index", "Home");
                            throw;
                        }
                    }
                }
            }
            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> GetSalesPoint(string PaymentMethod, string BranchCode)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            List<SalesPointsVM> SalesPointVMList = new List<SalesPointsVM>();
            List<AccountBankVM> AccountBankVMList = new List<AccountBankVM>();
            var Type = "0";
            if (PaymentMethod != null)
            {
                if (PaymentMethod == "10")
                {
                    var SalesPoints = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == lessorCode && x.CrCasAccountSalesPointBrn == BranchCode &&
                                                                           x.CrCasAccountSalesPointStatus == Status.Active && x.CrCasAccountSalesPointBankStatus == Status.Active &&
                                                                           x.CrCasAccountSalesPointBranchStatus == Status.Active && x.CrCasAccountSalesPointBank == "00").ToList();
                    Type = "1";

                    foreach (var item in SalesPoints)
                    {
                        SalesPointsVM SalesPointVM = new SalesPointsVM
                        {
                            CrCasAccountSalesPointNo = item.CrCasAccountSalesPointNo,
                            CrCasAccountSalesPointCode = item.CrCasAccountSalesPointCode,
                            CrCasAccountSalesPointArName = item.CrCasAccountSalesPointArName,
                            CrCasAccountSalesPointEnName = item.CrCasAccountSalesPointEnName,
                            CrCasAccountSalesPointBank = item.CrCasAccountSalesPointBank,
                            CrCasAccountSalesPointAccountBank = item.CrCasAccountSalesPointAccountBank
                        };
                        SalesPointVMList.Add(SalesPointVM);
                    }
                }
                else if (PaymentMethod == "20" || PaymentMethod == "22" || PaymentMethod == "21" || PaymentMethod == "23")
                {
                    var SalesPoints = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == lessorCode && x.CrCasAccountSalesPointBrn == BranchCode &&
                                                                           x.CrCasAccountSalesPointStatus == Status.Active && x.CrCasAccountSalesPointBankStatus == Status.Active &&
                                                                           x.CrCasAccountSalesPointBranchStatus == Status.Active && x.CrCasAccountSalesPointBank != "00").ToList();
                    Type = "1";
                    foreach (var item in SalesPoints)
                    {
                        SalesPointsVM SalesPointVM = new SalesPointsVM
                        {
                            CrCasAccountSalesPointNo = item.CrCasAccountSalesPointNo,
                            CrCasAccountSalesPointCode = item.CrCasAccountSalesPointCode,
                            CrCasAccountSalesPointArName = item.CrCasAccountSalesPointArName,
                            CrCasAccountSalesPointEnName = item.CrCasAccountSalesPointEnName,
                            CrCasAccountSalesPointBank = item.CrCasAccountSalesPointBank,
                            CrCasAccountSalesPointAccountBank = item.CrCasAccountSalesPointAccountBank
                        };
                        SalesPointVMList.Add(SalesPointVM);
                    }
                }
                else
                {
                    var AccountBanks = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankLessor == lessorCode && x.CrCasAccountBankStatus == Status.Active &&
                                                                         x.CrCasAccountBankNo != "00").ToList();
                    Type = "2";
                    foreach (var item in AccountBanks)
                    {
                        AccountBankVM AccountBankVM = new AccountBankVM
                        {
                            CrCasAccountBankNo = item.CrCasAccountBankNo,
                            CrCasAccountBankArName = item.CrCasAccountBankArName,
                            CrCasAccountBankEnName = item.CrCasAccountBankEnName,
                            CrCasAccountBankCode = item.CrCasAccountBankCode,
                        };
                        AccountBankVMList.Add(AccountBankVM);
                    }
                }
                return Json(new { SalesPoints = SalesPointVMList, AccountBank = AccountBankVMList, Type = Type });
            }
            return Json(null);
        }
         private CrCasAccountReceipt GetContractAccountReceipt(string LessorCode, string BranchCode)
        {
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var Lrecord = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptLessorCode == LessorCode &&
                                                                       x.CrCasAccountReceiptYear == y && x.CrCasAccountReceiptBranchCode == BranchCode)
                                                             .Max(x => x.CrCasAccountReceiptNo.Substring(x.CrCasAccountReceiptNo.Length - 6, 6));

            CrCasAccountReceipt c = new CrCasAccountReceipt();
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                c.CrCasAccountReceiptNo = val.ToString("000000");
            }
            else
            {
                c.CrCasAccountReceiptNo = "000001";
            }

            return c;
        }

    }
}
