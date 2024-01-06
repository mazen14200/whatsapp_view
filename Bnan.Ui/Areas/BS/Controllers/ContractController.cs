using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
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
            var userLogin = await _userManager.GetUserAsync(User);
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
            var drivers = _unitOfWork.CrCasRenterPrivateDriverInformation.FindAll(x => x.CrCasRenterPrivateDriverInformationLessor == lessorCode && x.CrCasRenterPrivateDriverInformationStatus == Status.Active).ToList();
            var carsAvailable = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationBranch == selectBranch && x.CrCasCarInformationStatus == Status.Active &&
                                                                                 x.CrCasCarInformationPriceStatus == true && x.CrCasCarInformationOwnerStatus == Status.Active &&
                                                                                (x.CrCasCarInformationForSaleStatus == Status.Active || x.CrCasCarInformationForSaleStatus == Status.RendAndForSale),
                                                                                new[] { "CrCasCarInformationDistributionNavigation", "CrCasCarInformationDistributionNavigation.CrCasPriceCarBasics",
                                                                                        "CrCasCarInformationCategoryNavigation", "CrCasCarDocumentsMaintenances" }).ToList();
            var categoryCars = carsAvailable.Select(item => item.CrCasCarInformationCategoryNavigation).Distinct().OrderBy(item => item.CrMasSupCarCategoryCode).ToList();
            var CheckupCars = _unitOfWork.CrMasSupContractCarCheckup.FindAll(x=>x.CrMasSupContractCarCheckupStatus == Status.Active).ToList();
            ViewBag.StartDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                SelectedBranch = selectBranch,
                CrCasBranchInformation = branch,
                Drivers = drivers,
                CarCategories = categoryCars,
                CarsFilter = carsAvailable,
                CarsCheckUp= CheckupCars
            };
            return View(bSLayoutVM);
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
                    KeyCountry = BnanRenterInfo?.CrMasRenterInformationCountreyKey
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
                        KeyCountry = elmRenterInfo?.CrElmPersonalCountryKey
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
        public async Task<IActionResult> CheckAuthDriver(bool id, bool address,bool license,bool age,bool employer)
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
            var carPrice = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicDistributionCode == carInfo.CrCasCarInformationDistribution);
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
            var AdvantagesTotalValue = _unitOfWork.CrCasPriceCarAdvantage.FindAll(x => x.CrCasPriceCarAdvantagesNo == priceNumber).Select(x=>x.CrCasPriceCarAdvantagesValue).Sum();
            if (AdvantagesTotalValue!=null) return Json(AdvantagesTotalValue);
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
                Total=optionsList.Select(x=>x.OptionsValue).Sum()
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
        
    }
}
