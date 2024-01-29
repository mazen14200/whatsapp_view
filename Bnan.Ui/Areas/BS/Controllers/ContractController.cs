using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Bnan.Ui.ViewModels.CAS.MecanismInputs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NToastNotify;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class ContractController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<ContractController> _localizer;
        private readonly IContract _ContractServices;
        public ContractController(IStringLocalizer<ContractController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IContract contractServices) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _ContractServices = contractServices;
        }
        public async Task<IActionResult> CreateContract()
        {
            //To Set Title 
            var titles = await setTitle("501", "5501001", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            
            var bSLayoutVM = await GetBranchesAndLayout();

            var drivers = _unitOfWork.CrCasRenterPrivateDriverInformation.FindAll(x => x.CrCasRenterPrivateDriverInformationLessor == lessorCode && x.CrCasRenterPrivateDriverInformationStatus == Status.Active).ToList();

            var carsAvailable = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == bSLayoutVM.SelectedBranch && x.CrCasCarInformationStatus == Status.Active &&
                                                                                 x.CrCasCarInformationPriceStatus == true && x.CrCasCarInformationOwnerStatus == Status.Active &&
                                                                                (x.CrCasCarInformationForSaleStatus == Status.Active || x.CrCasCarInformationForSaleStatus == Status.RendAndForSale),
                                                                                new[] { "CrCasCarInformationDistributionNavigation", "CrCasCarInformationDistributionNavigation.CrCasPriceCarBasics",
                                                                                        "CrCasCarInformationCategoryNavigation", "CrCasCarDocumentsMaintenances" }).ToList();

            var categoryCars = carsAvailable.Select(item => item.CrCasCarInformationCategoryNavigation).Distinct().OrderBy(item => item.CrMasSupCarCategoryCode).ToList();
            var CheckupCars = _unitOfWork.CrMasSupContractCarCheckup.FindAll(x => x.CrMasSupContractCarCheckupStatus == Status.Active).ToList();
            ViewBag.StartDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            //Check Account Bank And Sales Point and payment method
            var AccountBanks = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankLessor == lessorCode && x.CrCasAccountBankStatus == Status.Active, new[] { "CrCasAccountSalesPoints" }).ToList();
            var PaymentMethod = _unitOfWork.CrMasSupAccountPaymentMethod.FindAll(x => x.CrMasSupAccountPaymentMethodStatus == Status.Active).ToList();
            var SalesPoint = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == lessorCode &&
                                                                             x.CrCasAccountSalesPointBrn == bSLayoutVM.SelectedBranch &&
                                                                             x.CrCasAccountSalesPointBankStatus == Status.Active &&
                                                                             x.CrCasAccountSalesPointStatus == Status.Active &&
                                                                             x.CrCasAccountSalesPointBranchStatus == Status.Active).ToList();
            //Get ContractCode
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var sector = "1";
            var autoinc = GetContractLastRecord("1", lessorCode, bSLayoutVM.SelectedBranch).CrCasRenterContractBasicNo;
            var BasicContractNo = y + "-" + sector + "401" + "-" + lessorCode + bSLayoutVM.SelectedBranch + "-" + autoinc;
            ViewBag.ContractNo = BasicContractNo;

            bSLayoutVM.Drivers = drivers;
            bSLayoutVM.CarCategories = categoryCars;
            bSLayoutVM.CarsFilter = carsAvailable;
            bSLayoutVM.CarsCheckUp = CheckupCars;
            bSLayoutVM.SalesPoint = SalesPoint;
            bSLayoutVM.AccountBanks = AccountBanks;
            bSLayoutVM.PaymentMethods = PaymentMethod;

            return View(bSLayoutVM);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContract(BSLayoutVM bSLayoutVM, string ChoicesList, string AdditionalsList, Dictionary<string, string> Reasons, bool Contract_OutFeesTmm)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var ContractInfo = bSLayoutVM.Contract;
            var Renter = _unitOfWork.CrMasRenterInformation.Find(x => x.CrMasRenterInformationId == ContractInfo.RenterId);
            var RenterLessor = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == ContractInfo.RenterId);
            var Car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == ContractInfo.SerialNo);
            var CarPrice = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicNo == ContractInfo.PriceNo);
            var Branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == bSLayoutVM.SelectedBranch&&x.CrCasBranchInformationLessor==lessorCode);
            
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var sector = Renter.CrMasRenterInformationSector;
            var autoinc = GetContractLastRecord("1", lessorCode, Branch.CrCasBranchInformationCode).CrCasRenterContractBasicNo;
            var BasicContractNo = y + "-" + sector + "401" + "-" + lessorCode + Branch.CrCasBranchInformationCode + "-" + autoinc;
            if (userLogin!=null&& Renter != null && Car != null && CarPrice != null&&Branch!=null)
            {
                //Add Row in BasicContract Table 
                var BasicContract = await _ContractServices.AddRenterContractBasic(lessorCode, Branch.CrCasBranchInformationCode, BasicContractNo, ContractInfo.RenterId, ContractInfo.DriverId,
                                                                                  ContractInfo.PrivateDriverId, ContractInfo.AdditionalDriverId, ContractInfo.SerialNo, ContractInfo.PriceNo,
                                                                                  ContractInfo.DaysNo, ContractInfo.UserAddHours, ContractInfo.UserAddKm, ContractInfo.CurrentMeter, ContractInfo.OptionTotal,
                                                                                  ContractInfo.AdditionalTotal, ContractInfo.ContractValueAfterDiscount, ContractInfo.DiscountValue, ContractInfo.ContractValueBeforeDiscount,
                                                                                  ContractInfo.TaxValue, ContractInfo.TotalContractAmount, userLogin.CrMasUserInformationCode, ContractInfo.OutFeesTmm,
                                                                                  ContractInfo.UserDiscount, ContractInfo.AmountPayed, ContractInfo.RenterReasons);


                //Account Receipt
                var CheckAccountReceipt = true;
                var passing = "";
                if (BasicContract.CrCasRenterContractBasicAmountPaidAdvance > 0)
                {
                   
                    if (ContractInfo.PaymentMethod == "30")
                    {
                        passing = "4";
                        ContractInfo.AccountNo = ContractInfo.SalesPoint;
                    }
                    else {
                        passing = "1";
                    }

                    CheckAccountReceipt = await _ContractServices.AddAccountReceipt(BasicContract.CrCasRenterContractBasicNo, lessorCode, BasicContract.CrCasRenterContractBasicBranch,
                                                                                  ContractInfo.PaymentMethod, ContractInfo.AccountNo, BasicContract.CrCasRenterContractBasicCarSerailNo,
                                                                                  ContractInfo.SalesPoint,(decimal)BasicContract.CrCasRenterContractBasicAmountPaidAdvance,
                                                                                  BasicContract.CrCasRenterContractBasicRenterId,userLogin.CrMasUserInformationCode, passing, ContractInfo.PaymentReasons);
                }

                //Choices
                var CheckChoices = true;
                if (ChoicesList != null && ChoicesList.Length > 0)
                {
                    List<string> choiceCodes = ChoicesList.Split(',').ToList();
                    foreach (var item in choiceCodes) if (CheckChoices) CheckChoices = await _ContractServices.AddRenterContractChoice(lessorCode, BasicContract.CrCasRenterContractBasicNo, ContractInfo.SerialNo, ContractInfo.PriceNo, item.Trim());
                }

                //Additional
                var CheckAddditional = true;
                if (AdditionalsList != null && AdditionalsList.Length > 0)
                {
                    List<string> AdditionalsCode = AdditionalsList.Split(',').ToList();
                    foreach (var item in AdditionalsCode) if (CheckAddditional) CheckAddditional = await _ContractServices.AddRenterContractAdditional(lessorCode, BasicContract.CrCasRenterContractBasicNo, ContractInfo.SerialNo, ContractInfo.PriceNo, item.Trim());
                }

                //Advantages
                var CheckAdvantages = true;
                if (decimal.Parse(ContractInfo.AdvantagesTotalValue) > 0)
                {
                    var AdavntagesCar = _unitOfWork.CrCasPriceCarAdvantage.FindAll(x => x.CrCasPriceCarAdvantagesNo == ContractInfo.PriceNo);
                    foreach (var item in AdavntagesCar) if (CheckAdvantages) CheckAdvantages = await _ContractServices.AddRenterContractAdvantages(item, BasicContract.CrCasRenterContractBasicNo);
                }

                //CheckUp
                var CheckCheckUpCar = true;
                if (Reasons != null)
                {
                    foreach (var item in Reasons)
                    {
                        string Code = item.Key;
                        string Reason = item.Value;
                        if (CheckCheckUpCar) CheckCheckUpCar = await _ContractServices.AddRenterContractCheckUp(lessorCode, BasicContract.CrCasRenterContractBasicNo, ContractInfo.SerialNo, ContractInfo.PriceNo, Code, Reason);
                    }
                }


                //Authrization
                var CheckAuthrization = true;
                CheckAuthrization = await _ContractServices.AddRenterContractAuthrization(BasicContract.CrCasRenterContractBasicNo, lessorCode,
                                                                                          ContractInfo.OutFeesTmm, ContractInfo.FeesTmmValue);


                //Update DocAndMaintainance Of Car
                var CheckDocAndMaintainance = await _ContractServices.UpdateCarDocMaintainance(BasicContract.CrCasRenterContractBasicCarSerailNo, lessorCode,
                                                                                               Branch.CrCasBranchInformationCode, int.Parse(ContractInfo.CurrentMeter));

                //Update Information Of Car
                var CheckCarInfo = true;
                CheckCarInfo = await _ContractServices.UpdateCarInformation(BasicContract.CrCasRenterContractBasicCarSerailNo, lessorCode, Branch.CrCasBranchInformationCode,
                                                                            (DateTime)BasicContract.CrCasRenterContractBasicIssuedDate, (int)(BasicContract.CrCasRenterContractBasicExpectedRentalDays),
                                                                            int.Parse(ContractInfo.CurrentMeter), CheckDocAndMaintainance);

                //Update RenterLessor Of Car Renter
                var CheckRenterLessor = true;
                CheckRenterLessor = await _ContractServices.UpdateRenterLessor(Renter.CrMasRenterInformationId, lessorCode, (DateTime)BasicContract.CrCasRenterContractBasicIssuedDate,
                                                                              (decimal)BasicContract.CrCasRenterContractBasicAmountPaidAdvance, ContractInfo.RenterReasons);
                
                //Update Mas Renter Info Of Car Renter
                var CheckMasRenter = true;
                CheckMasRenter = await _ContractServices.UpdateMasRenter(Renter.CrMasRenterInformationId);

                //Update Driver and Private Driver and Add Driver 
                var CheckPrivateDriver = true;
                var CheckDriver = true;
                var CheckAddDriver = true;
                if (!string.IsNullOrEmpty(ContractInfo.PrivateDriverId)) CheckPrivateDriver = await _ContractServices.UpdatePrivateDriverStatus(ContractInfo.PrivateDriverId, lessorCode);
                else
                {
                    //Update Driver
                    if (!string.IsNullOrEmpty(ContractInfo.DriverId) && ContractInfo.DriverId.Trim() != ContractInfo.RenterId) CheckDriver = await _ContractServices.UpdateDriverStatus(ContractInfo.DriverId, lessorCode,ContractInfo.DriverReasons);
                    //Update Add Driver
                    if (!string.IsNullOrEmpty(ContractInfo.AdditionalDriverId)) CheckAddDriver = await _ContractServices.UpdateDriverStatus(ContractInfo.DriverId, lessorCode, ContractInfo.AddDriverReasons);
                }

                //Update Branch Balance , But first Check if passing equal 4 or not 
                var CheckBranch = true;
                if (passing!="4") CheckBranch = await _ContractServices.UpdateBranchBalance(Branch.CrCasBranchInformationCode, lessorCode, (decimal)BasicContract.CrCasRenterContractBasicAmountPaidAdvance);
               
                //Update SalesPoint Balance , But first Check if passing equal 4 or not 
                var CheckSalesPoint = true;
                if (!string.IsNullOrEmpty(ContractInfo.SalesPoint) && passing != "4") CheckSalesPoint = await _ContractServices.UpdateSalesPointBalance(Branch.CrCasBranchInformationCode, lessorCode, ContractInfo.SalesPoint, (decimal)BasicContract.CrCasRenterContractBasicAmountPaidAdvance);

                // UpdateBranchValidity
                var CheckBranchValidity = true;
                if (passing != "4") CheckBranchValidity = await _ContractServices.UpdateBranchValidity(Branch.CrCasBranchInformationCode, lessorCode, userLogin.CrMasUserInformationCode, ContractInfo.PaymentMethod, (decimal)BasicContract.CrCasRenterContractBasicAmountPaidAdvance);


                // UpdateUserBalance
                var CheckUserInformation = true;
                if (passing != "4") CheckUserInformation = await _ContractServices.UpdateUserBalance(Branch.CrCasBranchInformationCode, lessorCode, userLogin.CrMasUserInformationCode, ContractInfo.PaymentMethod, (decimal)BasicContract.CrCasRenterContractBasicAmountPaidAdvance);

                // Add Renter Alert
                var CheckRenterAlert = true;

                CheckRenterAlert = await _ContractServices.AddRenterAlert(BasicContract.CrCasRenterContractBasicNo, lessorCode, Branch.CrCasBranchInformationCode,
                                                                        (int)BasicContract.CrCasRenterContractBasicExpectedRentalDays, (DateTime)BasicContract.CrCasRenterContractBasicExpectedEndDate,
                                                                        BasicContract.CrCasRenterContractBasicCarSerailNo, BasicContract.CrCasRenterContractPriceReference);








                if (BasicContract != null && CheckChoices && CheckAddditional && CheckAdvantages &&
                    CheckCheckUpCar && CheckAuthrization && CheckCarInfo && CheckDocAndMaintainance!=null &&
                    CheckRenterLessor&& CheckAccountReceipt&& CheckBranch&& CheckSalesPoint&&
                    CheckUserInformation&& CheckBranchValidity&& CheckMasRenter&& CheckAddDriver&& 
                    CheckDriver&& CheckPrivateDriver&& CheckRenterAlert)
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
            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> GetRenter(string RenterId)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            //First check if bnan have this renter id or not 
            var BnanRenterInfo = _unitOfWork.CrMasRenterInformation.Find(x => x.CrMasRenterInformationId == RenterId, new[] { "CrMasRenterInformationGenderNavigation",
                                                                                                                           "CrMasRenterInformationNationalityNavigation",
                                                                                                                           "CrMasRenterInformationProfessionNavigation",
                                                                                                                           "CrMasRenterInformationEmployerNavigation",
                                                                                                                           "CrMasRenterInformationDrivingLicenseTypeNavigation"});

            if (BnanRenterInfo != null)
            {
                var LessorRenterInfo = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == RenterId && x.CrCasRenterLessorId == lessorCode);
                var RenterPost = _unitOfWork.CrMasRenterPost.Find(x => x.CrMasRenterPostCode == RenterId);
                //this model for View Only 
                RenterInformationsVM renterInformationsVM = new RenterInformationsVM
                {
                    RenterID = BnanRenterInfo?.CrMasRenterInformationId,
                    PersonalArName = BnanRenterInfo?.CrMasRenterInformationArName,
                    PersonalEnName = BnanRenterInfo?.CrMasRenterInformationEnName,
                    PersonalArGender = BnanRenterInfo?.CrMasRenterInformationGenderNavigation?.CrMasSupRenterGenderArName,
                    PersonalEnGender = BnanRenterInfo?.CrMasRenterInformationGenderNavigation?.CrMasSupRenterGenderEnName,
                    PersonalArNationality = BnanRenterInfo?.CrMasRenterInformationNationalityNavigation?.CrMasSupRenterNationalitiesArName,
                    PersonalEnNationality = BnanRenterInfo?.CrMasRenterInformationNationalityNavigation?.CrMasSupRenterNationalitiesEnName,
                    PersonalArProfessions = BnanRenterInfo?.CrMasRenterInformationProfessionNavigation?.CrMasSupRenterProfessionsArName,
                    PersonalEnProfessions = BnanRenterInfo?.CrMasRenterInformationProfessionNavigation?.CrMasSupRenterProfessionsEnName,
                    PersonalEmail = BnanRenterInfo?.CrMasRenterInformationEmail,
                    EmployerCode = BnanRenterInfo?.CrMasRenterInformationEmployerNavigation?.CrMasSupRenterEmployerCode,
                    EmployerArName = BnanRenterInfo?.CrMasRenterInformationEmployerNavigation?.CrMasSupRenterEmployerArName,
                    EmployerEnName = BnanRenterInfo?.CrMasRenterInformationEmployerNavigation?.CrMasSupRenterEmployerEnName,
                    LicenseCode = BnanRenterInfo?.CrMasRenterInformationDrivingLicenseTypeNavigation?.CrMasSupRenterDrivingLicenseCode,
                    LicenseArName = BnanRenterInfo?.CrMasRenterInformationDrivingLicenseTypeNavigation?.CrMasSupRenterDrivingLicenseArName,
                    LicenseEnName = BnanRenterInfo?.CrMasRenterInformationDrivingLicenseTypeNavigation?.CrMasSupRenterDrivingLicenseEnName,
                    LicenseExpiryDate = BnanRenterInfo?.CrMasRenterInformationExpiryDrivingLicenseDate,
                    LicenseIssuedDate = BnanRenterInfo?.CrMasRenterInformationIssueIdDate,
                    PostArNameConcenate = RenterPost?.CrMasRenterPostArConcatenate,
                    PostEnNameConcenate = RenterPost?.CrMasRenterPostEnConcatenate,
                    MobileNumber = BnanRenterInfo?.CrMasRenterInformationMobile,
                    BirthDate = BnanRenterInfo?.CrMasRenterInformationBirthDate,
                    ExpiryIdDate = BnanRenterInfo?.CrMasRenterInformationExpiryIdDate,
                    KeyCountry = BnanRenterInfo?.CrMasRenterInformationCountreyKey,
                    Balance = LessorRenterInfo?.CrCasRenterLessorBalance
                };
                return Json(renterInformationsVM);
            }
            else
            {
                var elmRenterInfo = _unitOfWork.CrElmPersonal.Find(x => x.CrElmPersonalCode == RenterId);
                if (elmRenterInfo != null)
                {
                    var elmRenterLicince = _unitOfWork.CrElmLicense.Find(x => x.CrElmLicensePersonId == RenterId);
                    var elmRenterEmployeer = _unitOfWork.CrElmEmployer.Find(x => x.CrElmEmployerCode == RenterId);
                    var elmRenterPost = _unitOfWork.CrElmPost.Find(x => x.CrElmPostCode == RenterId);

                    var masRenterInformation = await _ContractServices.AddRenterFromElmToMasRenter(RenterId, elmRenterEmployeer, elmRenterInfo, elmRenterLicince);
                    var masRenterPost = await _ContractServices.AddRenterFromElmToMasRenterPost(RenterId, elmRenterPost);
                    var casRenterLessor = await _ContractServices.AddRenterFromElmToCasRenterLessor(lessorCode, masRenterInformation, masRenterPost);
                    if (masRenterInformation != null && casRenterLessor != null)
                    {
                        //try for test
                        try
                        {
                            await _unitOfWork.CompleteAsync();
                        }
                        catch (Exception ex)
                        {
                            return Json(null);
                            throw;
                        }
                    }
                    else
                    {
                        return Json(null);
                    }

                    var buildingInfoAr = "";
                    var unitInfoAr = "";
                    var zipCodeAr = "";
                    var additionalNoAr = "";
                    var buildingInfoEn = "";
                    var unitInfoEn = "";
                    var zipCodeEn = "";
                    var additionalNoEn = "";
                    var concatenatedArAddress = "";
                    var concatenatedEnAddress = "";

                    if (elmRenterPost != null)
                    {
                        buildingInfoAr = $"مبنى ({elmRenterPost.CrElmPostBuildingNo}) ";
                        unitInfoAr = $"وحدة ({elmRenterPost.CrElmPostUnitNo}) ";
                        zipCodeAr = $"الرمز البريدي ({elmRenterPost.CrElmPostZipCode}) ";
                        additionalNoAr = $"الرقم الاضافي ({elmRenterPost.CrElmPostAdditionalNo}) ";
                        buildingInfoEn = $"Building ({elmRenterPost.CrElmPostBuildingNo}) ";
                        unitInfoEn = $"Unit ({elmRenterPost.CrElmPostUnitNo}) ";
                        zipCodeEn = $"ZipCode ({elmRenterPost.CrElmPostZipCode}) ";
                        additionalNoEn = $"additionalNo ({elmRenterPost.CrElmPostAdditionalNo}) ";
                        concatenatedArAddress = string.Join(" - ", elmRenterPost.CrElmPostRegionsArName, elmRenterPost.CrElmPostCityArName, elmRenterPost.CrElmPostDistrictArName,
                                                          elmRenterPost.CrElmPostStreetArName, buildingInfoAr, unitInfoAr, zipCodeAr, additionalNoAr);

                        concatenatedEnAddress = string.Join(" - ", elmRenterPost.CrElmPostRegionsEnName, elmRenterPost.CrElmPostCityEnName, elmRenterPost.CrElmPostDistrictEnName,
                                                                   elmRenterPost.CrElmPostStreetEnName, buildingInfoEn, unitInfoEn, zipCodeEn, additionalNoEn);
                    }

                    //this model for View Only 
                    RenterInformationsVM renterInformationsVM = new RenterInformationsVM
                    {
                        RenterID = elmRenterInfo?.CrElmPersonalCode,
                        PersonalArName = elmRenterInfo?.CrElmPersonalArName,
                        PersonalEnName = elmRenterInfo?.CrElmPersonalEnName,
                        PersonalArGender = elmRenterInfo?.CrElmPersonalArGender,
                        PersonalEnGender = elmRenterInfo?.CrElmPersonalEnGender,
                        PersonalArNationality = elmRenterInfo?.CrElmPersonalArNationality,
                        PersonalEnNationality = elmRenterInfo?.CrElmPersonalEnNationality,
                        PersonalArProfessions = elmRenterInfo?.CrElmPersonalArProfessions,
                        PersonalEnProfessions = elmRenterInfo?.CrElmPersonalEnProfessions,
                        PersonalEmail = elmRenterInfo?.CrElmPersonalEmail,
                        EmployerArName = elmRenterEmployeer?.CrElmEmployerArName,
                        EmployerEnName = elmRenterEmployeer?.CrElmEmployerEnName,
                        LicenseArName = elmRenterLicince?.CrElmLicenseArName,
                        LicenseEnName = elmRenterLicince?.CrElmLicenseEnName,
                        LicenseExpiryDate = elmRenterLicince?.CrElmLicenseExpiryDate,
                        LicenseIssuedDate = elmRenterLicince?.CrElmLicenseIssuedDate,
                        PostArNameConcenate = concatenatedArAddress,
                        PostEnNameConcenate = concatenatedEnAddress,
                        MobileNumber = elmRenterInfo?.CrElmPersonalMobile,
                        BirthDate = elmRenterInfo?.CrElmPersonalBirthDate,
                        ExpiryIdDate = elmRenterInfo?.CrElmPersonalExpiryIdDate,
                        KeyCountry = elmRenterInfo?.CrElmPersonalCountryKey,
                        Balance = 0
                    };
                    return Json(renterInformationsVM);
                }
            }
            return Json(null);
        }
        [HttpGet]
        public async Task<IActionResult> CheckAuthUser(bool id, bool address)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var userContractValidity = _unitOfWork.CrMasUserContractValidity.Find(x => x.CrMasUserContractValidityUserId == userLogin.Id);
            var check = "true";
            if (id == false && userContractValidity.CrMasUserContractValidityId == false)
            {
                check = "id";
                return Json(check);
            }
            else if (address == false && userContractValidity.CrMasUserContractValidityRenterAddress == false)
            {
                check = "address";
                return Json(check);
            }
            else { return Json(check); }

        }


        [HttpGet]
        public async Task<IActionResult> CheckAuthDriver(bool id, bool address, bool license, bool age, bool employer)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var userContractValidity = _unitOfWork.CrMasUserContractValidity.Find(x => x.CrMasUserContractValidityUserId == userLogin.Id);
            var check = "true";
            if (id == false && userContractValidity.CrMasUserContractValidityId == false)
            {
                check = "id";
                return Json(check);
            }
            else if (address == false && userContractValidity.CrMasUserContractValidityRenterAddress == false)
            {
                check = "address";
                return Json(check);
            }
            else if (license == false && userContractValidity.CrMasUserContractValidityDrivingLicense == false)
            {
                check = "license";
                return Json(check);
            }
            else if (age == false && userContractValidity.CrMasUserContractValidityAge == false)
            {
                check = "age";
                return Json(check);
            }
            else if (employer == false && userContractValidity.CrMasUserContractValidityEmployer == false)
            {
                check = "employer";
                return Json(check);
            }
            else { return Json(check); }
        }
        [HttpGet]
        public async Task<PartialViewResult> GetCarsByCategory(string selectedCategory, string selectedBranch)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var cars = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == selectedBranch && x.CrCasCarInformationStatus == Status.Active &&
                                                                                 x.CrCasCarInformationPriceStatus == true && x.CrCasCarInformationOwnerStatus == Status.Active &&
                                                                                (x.CrCasCarInformationForSaleStatus == Status.Active || x.CrCasCarInformationForSaleStatus == Status.RendAndForSale),
                                                                                new[] { "CrCasCarInformationDistributionNavigation", "CrCasCarInformationDistributionNavigation.CrCasPriceCarBasics",
                                                                                        "CrCasCarInformationCategoryNavigation", "CrCasCarDocumentsMaintenances" }).ToList();
            if (selectedCategory == "3400000000")
            {
                BSLayoutVM bSLayoutVM = new BSLayoutVM()
                {
                    CarsFilter = cars
                };
                return PartialView("_CarsList", bSLayoutVM);
            }
            BSLayoutVM bSLayout = new BSLayoutVM()
            {
                CarsFilter = cars.Where(x => x.CrCasCarInformationCategory == selectedCategory).ToList(),
            };
            return PartialView("_CarsList", bSLayout);


        }

        [HttpGet]
        public async Task<IActionResult> GetCarInformation(string serialNumber)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            BSCarsInformationVM carVM = new BSCarsInformationVM();
            var carInfo = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationSerailNo == serialNumber);

            carVM.CarInformation = carInfo;
            var carPrice = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicNo == carInfo.CrCasCarInformationPriceNo);
            if (carPrice != null)
            {
                carVM.CarPrice = carPrice;
                return Json(carVM);
            }
            return Json(null);

        }
        [HttpGet]
        public async Task<IActionResult> GetAdvantages(string priceNumber)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            BSCarsInformationVM carVM = new BSCarsInformationVM();
            var AdvantagesTotalValue = _unitOfWork.CrCasPriceCarAdvantage.FindAll(x => x.CrCasPriceCarAdvantagesNo == priceNumber).Select(x => x.CrCasPriceCarAdvantagesValue).Sum();
            if (AdvantagesTotalValue != null) return Json(AdvantagesTotalValue);
            return Json(null);
        }

        [HttpGet]
        public async Task<IActionResult> GetCarChoices(string priceNumber)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;

            List<OptionsVM> optionsList = new List<OptionsVM>();

            var carOptions = _unitOfWork.CrCasPriceCarOption.FindAll(x => x.CrCasPriceCarOptionsNo == priceNumber);

            foreach (var option in carOptions)
            {
                var supCarOptions = _unitOfWork.CrMasSupContractOption.Find(x => x.CrMasSupContractOptionsCode == option.CrCasPriceCarOptionsCode);

                OptionsVM optionsVM = new OptionsVM
                {
                    OptionsNo = option.CrCasPriceCarOptionsNo,
                    OptionsCode = option.CrCasPriceCarOptionsCode,
                    OptionsValue = option.CrCasPriceCarOptionsValue,
                    ArName = supCarOptions.CrMasSupContractOptionsArName,
                    EnName = supCarOptions.CrMasSupContractOptionsEnName
                };

                optionsList.Add(optionsVM);
            }
            var result = new
            {
                OptionsList = optionsList,
                Count = carOptions.Count(),
                Total = optionsList.Select(x => x.OptionsValue).Sum()
            };

            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCarAdditional(string priceNumber)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            List<AdditionalVM> additionalList = new List<AdditionalVM>();

            var carAdditionals = _unitOfWork.CrCasPriceCarAdditional.FindAll(x => x.CrCasPriceCarAdditionalNo == priceNumber);

            foreach (var additional in carAdditionals)
            {
                var supCarAdditional = _unitOfWork.CrMasSupContractAdditional.Find(x => x.CrMasSupContractAdditionalCode == additional.CrCasPriceCarAdditionalCode);

                AdditionalVM additionalVM = new AdditionalVM
                {
                    AddNo = additional.CrCasPriceCarAdditionalNo,
                    AddCode = additional.CrCasPriceCarAdditionalCode,
                    AddValue = additional.CrCasPriceCarAdditionalValue,
                    ArName = supCarAdditional.CrMasSupContractAdditionalArName,
                    EnName = supCarAdditional.CrMasSupContractAdditionalEnName
                };
                additionalList.Add(additionalVM);

            }
            var result = new
            {
                addList = additionalList,
                Count = carAdditionals.Count(),
                Total = additionalList.Select(x => x.AddValue).Sum()
            };
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCarCheckUp()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            List<CheckUpVM> checkUpList = new List<CheckUpVM>();
            var CheckUps = _unitOfWork.CrMasSupContractCarCheckup.FindAll(x => x.CrMasSupContractCarCheckupStatus == Status.Active).ToList();
            foreach (var carCheckup in CheckUps)
            {

                CheckUpVM carCheckupVM = new CheckUpVM
                {
                    Code = carCheckup.CrMasSupContractCarCheckupCode,
                    ArName = carCheckup.CrMasSupContractCarCheckupArName,
                    EnName = carCheckup.CrMasSupContractCarCheckupEnName
                };
                checkUpList.Add(carCheckupVM);
            }
            return Json(checkUpList);
        }

        public CrCasRenterContractBasic GetContractLastRecord(string Sector, string LessorCode, string BranchCode)
        {
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var SectorCode = Sector;
            var Lrecord = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicLessor == LessorCode &&
                x.CrCasRenterContractBasicSector == SectorCode
                && x.CrCasRenterContractBasicYear == y && x.CrCasRenterContractBasicBranch == BranchCode)
                .Max(x => x.CrCasRenterContractBasicNo.Substring(x.CrCasRenterContractBasicNo.Length - 6, 6));

            CrCasRenterContractBasic c = new CrCasRenterContractBasic();
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                c.CrCasRenterContractBasicNo = val.ToString("000000");
            }
            else
            {
                c.CrCasRenterContractBasicNo = "000001";
            }

            return c;
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
                                                                         x.CrCasAccountSalesPointBranchStatus == Status.Active &&x.CrCasAccountSalesPointBank=="00").ToList();
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
                else if (PaymentMethod=="20" || PaymentMethod=="22" || PaymentMethod=="21" || PaymentMethod=="23")
                {
                  var  SalesPoints = _unitOfWork.CrCasAccountSalesPoint.FindAll(x=> x.CrCasAccountSalesPointLessor == lessorCode && x.CrCasAccountSalesPointBrn == BranchCode &&
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
                  var  AccountBanks = _unitOfWork.CrCasAccountBank.FindAll(x=>x.CrCasAccountBankLessor==lessorCode&&x.CrCasAccountBankStatus==Status.Active&&
                                                                       x.CrCasAccountBankNo!="00").ToList();
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

    }
}
