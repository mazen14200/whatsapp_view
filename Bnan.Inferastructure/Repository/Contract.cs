using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class Contract : IContract
    {
        private IUnitOfWork _unitOfWork;

        public Contract(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CrMasRenterInformation> AddRenterFromElmToMasRenter(string RenterID, CrElmEmployer crElmEmployer, CrElmPersonal crElmPersonal, CrElmLicense crElmLicense)
        {
            var firstChar = RenterID.Substring(0, 1);

            var sectorCode = "";
            var nationalityCode = "";
            var genderCode = "";
            var professionsCode = "";
            var licenceCode = "";
            var employerCode = "";

            if (crElmPersonal != null)
            {
                sectorCode = GetSector(firstChar, crElmPersonal.CrElmPersonalSector);
                nationalityCode = GetNationality(firstChar, crElmPersonal.CrElmPersonalArNationality, crElmPersonal.CrElmPersonalEnNationality);
                genderCode = GetGender(firstChar, crElmPersonal.CrElmPersonalArGender, crElmPersonal.CrElmPersonalEnGender);
                professionsCode = GetProfessions(firstChar, crElmPersonal.CrElmPersonalArProfessions, crElmPersonal.CrElmPersonalEnProfessions);
            }
            else
            {
                nationalityCode = "1000000002";
                genderCode = "1100000002";
                professionsCode = "1400000002";
            }
            if (crElmLicense != null) licenceCode = GetLicence(firstChar, crElmLicense.CrElmLicenseArName, crElmLicense.CrElmLicenseEnName);
            else licenceCode = "2";
            if (crElmEmployer != null) employerCode = GetEmployer(firstChar, crElmEmployer.CrElmEmployerArName, crElmEmployer.CrElmEmployerEnName);
            else employerCode = "1800000002";
            var typeID = GetTypeID(firstChar, sectorCode);
            CrMasRenterInformation renterInformation = new CrMasRenterInformation
            {
                CrMasRenterInformationId = RenterID,
                CrMasRenterInformationArName = crElmPersonal.CrElmPersonalArName,
                CrMasRenterInformationEnName = crElmPersonal.CrElmPersonalEnName,
                CrMasRenterInformationCopyId = crElmPersonal.CrElmPersonalIdCopy,
                CrMasRenterInformationBirthDate = crElmPersonal.CrElmPersonalBirthDate,
                CrMasRenterInformationIssueIdDate = crElmPersonal.CrElmPersonalIssuedIdDate,
                CrMasRenterInformationExpiryIdDate = crElmPersonal.CrElmPersonalExpiryIdDate,
                CrMasRenterInformationCountreyKey = crElmPersonal.CrElmPersonalCountryKey,
                CrMasRenterInformationMobile = crElmPersonal.CrElmPersonalMobile,
                CrMasRenterInformationEmail = crElmPersonal.CrElmPersonalEmail,
                CrMasRenterInformationUpDateLicenseData = DateTime.Now.Date,
                CrMasRenterInformationUpDatePersonalData = DateTime.Now.Date,
                CrMasRenterInformationUpDateWorkplaceData = DateTime.Now.Date,
                CrMasRenterInformationCommunicationLanguage = "1",
                CrMasRenterInformationIssuePlace = crElmPersonal.CrElmPersonalIssuedPlace,
                CrMasRenterInformationRenterIdImage = null,
                CrMasRenterInformationRenterLicenseImage = null,
                CrMasRenterInformationBank = null,
                CrMasRenterInformationIban = null,
                CrMasRenterInformationSector = sectorCode,
                CrMasRenterInformationNationality = nationalityCode,
                CrMasRenterInformationGender = genderCode,
                CrMasRenterInformationDrivingLicenseType = licenceCode,
                CrMasRenterInformationEmployer = employerCode,
                CrMasRenterInformationProfession = professionsCode,
                CrMasRenterInformationStatus = "A",
                CrMasRenterInformationIdtype = typeID,
                CrMasRenterInformationDrivingLicenseNo = crElmLicense?.CrElmLicenseNo,
                CrMasRenterInformationDrivingLicenseDate = crElmLicense?.CrElmLicenseIssuedDate,
                CrMasRenterInformationExpiryDrivingLicenseDate = crElmLicense?.CrElmLicenseExpiryDate,
                CrMasRenterInformationSignature = null,
                CrMasRenterInformationTaxNo = null,
                CrMasRenterInformationReasons = null
            };
            if (await _unitOfWork.CrMasRenterInformation.AddAsync(renterInformation) == null) return null;
            return renterInformation;
        }
        public async Task<CrMasRenterPost> AddRenterFromElmToMasRenterPost(string RenterID, CrElmPost crElmPost)
        {
            if (crElmPost != null)
            {
                var RegionCode = "";
                var CityCode = "";
                var concatenatedArAddress = "";
                var concatenatedEnAddress = "";
                var concatenatedArAddressShort = "";
                var concatenatedEnAddressShort = "";
                var buildingInfoAr = "";
                var unitInfoAr = "";
                var zipCodeAr = "";
                var additionalNoAr = "";
                var buildingInfoEn = "";
                var unitInfoEn = "";
                var zipCodeEn = "";
                var additionalNoEn = "";


                RegionCode = GetRegion(crElmPost.CrElmPostRegionsArName, crElmPost.CrElmPostRegionsEnName);
                CityCode = GetCity(crElmPost.CrElmPostCityArName, crElmPost.CrElmPostCityEnName, RegionCode);
                buildingInfoAr = $"مبنى ({crElmPost.CrElmPostBuildingNo}) ";
                unitInfoAr = $"وحدة ({crElmPost.CrElmPostUnitNo}) ";
                zipCodeAr = $"الرمز البريدي ({crElmPost.CrElmPostZipCode}) ";
                additionalNoAr = $"الرقم الاضافي ({crElmPost.CrElmPostAdditionalNo}) ";
                buildingInfoEn = $"Building ({crElmPost.CrElmPostBuildingNo}) ";
                unitInfoEn = $"Unit ({crElmPost.CrElmPostUnitNo}) ";
                zipCodeEn = $"ZipCode ({crElmPost.CrElmPostZipCode}) ";
                additionalNoEn = $"additionalNo ({crElmPost.CrElmPostAdditionalNo}) ";

                concatenatedArAddress = string.Join(" - ", crElmPost.CrElmPostRegionsArName, crElmPost.CrElmPostCityArName, crElmPost.CrElmPostDistrictArName,
                                                           crElmPost.CrElmPostStreetArName, buildingInfoAr, unitInfoAr, zipCodeAr, additionalNoAr);

                concatenatedEnAddress = string.Join(" - ", crElmPost.CrElmPostRegionsEnName, crElmPost.CrElmPostCityEnName, crElmPost.CrElmPostDistrictEnName,
                                                           crElmPost.CrElmPostStreetEnName, buildingInfoEn, unitInfoEn, zipCodeEn, additionalNoEn);

                concatenatedArAddressShort = string.Join(" - ", crElmPost.CrElmPostRegionsArName, crElmPost.CrElmPostCityArName, crElmPost.CrElmPostDistrictArName,
                                                           crElmPost.CrElmPostStreetArName);

                concatenatedEnAddressShort = string.Join(" - ", crElmPost.CrElmPostRegionsEnName, crElmPost.CrElmPostCityEnName, crElmPost.CrElmPostDistrictEnName,
                                                           crElmPost.CrElmPostStreetEnName);

                CrMasRenterPost crMasRenterPost = new CrMasRenterPost
                {
                    CrMasRenterPostCode = RenterID,
                    CrMasRenterPostShortCode = null,
                    CrMasRenterPostAdditionalNumbers = crElmPost.CrElmPostAdditionalNo.ToString(),
                    CrMasRenterPostCity = CityCode,
                    CrMasRenterPostRegions = RegionCode,
                    CrMasRenterPostArStreet = crElmPost.CrElmPostStreetArName,
                    CrMasRenterPostEnStreet = crElmPost.CrElmPostStreetEnName,
                    CrMasRenterPostArDistrict = crElmPost.CrElmPostDistrictArName,
                    CrMasRenterPostEnDistrict = crElmPost.CrElmPostDistrictEnName,
                    CrMasRenterPostZipCode = crElmPost.CrElmPostZipCode.ToString(),
                    CrMasRenterPostBuilding = crElmPost.CrElmPostBuildingNo.ToString(),
                    CrMasRenterPostUnitNo = crElmPost.CrElmPostUnitNo,
                    CrMasRenterPostArConcatenate = concatenatedArAddress,
                    CrMasRenterPostArShortConcatenate = concatenatedArAddressShort,
                    CrMasRenterPostEnConcatenate = concatenatedEnAddress,
                    CrMasRenterPostEnShortConcatenate = concatenatedEnAddressShort,
                    CrMasRenterPostStatus = Status.Active,
                    CrMasRenterPostUpDatePost = DateTime.Now.Date,
                    CrMasRenterPostReasons = ""
                };
                if (await _unitOfWork.CrMasRenterPost.AddAsync(crMasRenterPost) == null) return null;
                return crMasRenterPost;
            }
            return null;
        }
        public async Task<CrCasRenterLessor> AddRenterFromElmToCasRenterLessor(string LessorCode, CrMasRenterInformation crMasRenterInformation, CrMasRenterPost crMasRenterPost)
        {
            var ageCode = "";
            var RegionCode = "";
            var CityCode = "";
            if (crMasRenterPost != null)
            {
                RegionCode = crMasRenterPost.CrMasRenterPostRegions;
                CityCode = crMasRenterPost.CrMasRenterPostCity;
            }
            else
            {
                RegionCode = "11";
                CityCode = "1700000002";
            }

            if (crMasRenterInformation != null) ageCode = GetAge(crMasRenterInformation.CrMasRenterInformationBirthDate?.ToString("yyyy/MM/dd"));
            CrCasRenterLessor casRenterLessor = new CrCasRenterLessor
            {
                CrCasRenterLessorId = crMasRenterInformation.CrMasRenterInformationId,
                CrCasRenterLessorCode = LessorCode,
                CrCasRenterLessorSector = crMasRenterInformation?.CrMasRenterInformationSector,
                CrCasRenterLessorCopyId = crMasRenterInformation?.CrMasRenterInformationCopyId,
                CrCasRenterLessorIdtrype = crMasRenterInformation?.CrMasRenterInformationIdtype,
                CrCasRenterLessorMembership = Membership.Mutual, // This The first memeber ship for every new renter 1600000006
                CrCasRenterLessorDateFirstInteraction = DateTime.Now.Date,
                CrCasRenterLessorDateLastContractual = null,
                CrCasRenterLessorContractCount = 0,
                CrCasRenterLessorContractDays = 0,
                CrCasRenterLessorContractKm = 0,
                CrCasRenterLessorContractTradedAmount = 0,
                CrCasRenterLessorEvaluationNumber = 0,
                CrCasRenterLessorEvaluationTotal = 0,
                CrCasRenterLessorEvaluationValue = 0,
                CrCasRenterLessorBalance = 0,
                CrCasRenterLessorStatisticsNationalities = crMasRenterInformation?.CrMasRenterInformationNationality,
                CrCasRenterLessorStatisticsGender = crMasRenterInformation?.CrMasRenterInformationGender,
                CrCasRenterLessorStatisticsJobs = crMasRenterInformation?.CrMasRenterInformationProfession,
                CrCasRenterLessorStatisticsRegions = RegionCode,
                CrCasRenterLessorStatisticsCity = CityCode,
                CrCasRenterLessorStatisticsAge = ageCode,
                CrCasRenterLessorStatisticsTraded = "1",
                CrCasRenterLessorDealingMechanism = "10", //I Dont Know why put this 10 
                CrCasRenterLessorStatus = Status.Active,
                CrCasRenterLessorReasons = "",
            };
            if (await _unitOfWork.CrCasRenterLessor.AddAsync(casRenterLessor) == null) return null;
            return casRenterLessor;
        }

        public async Task<bool> AddRenterContractChoice(string LessorCode, string SerialNo, string PriceNo, string Choice)
        {
            CrCasRenterContractChoice renterContractChoice = new CrCasRenterContractChoice();

            var carInfo = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == SerialNo);
            var carPrice = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicNo == PriceNo);
            if (carInfo != null && carPrice != null)
            {
                var carChoice = _unitOfWork.CrCasPriceCarOption.Find(x => x.CrCasPriceCarOptionsNo == carPrice.CrCasPriceCarBasicNo && x.CrCasPriceCarOptionsCode == Choice);
                if (carChoice != null)
                {
                    renterContractChoice.CrCasRenterContractChoiceNo = carChoice.CrCasPriceCarOptionsNo;
                    renterContractChoice.CrCasRenterContractChoiceCode = carChoice.CrCasPriceCarOptionsCode;
                    renterContractChoice.CrCasContractChoiceValue = carChoice.CrCasPriceCarOptionsValue;
                }
            }
            if (renterContractChoice != null && _unitOfWork.CrCasRenterContractChoice.Add(renterContractChoice) != null) return true;
            return false;
        }

        public async Task<bool> AddRenterContractAdditional(string LessorCode, string SerialNo, string PriceNo, string Additional)
        {

            CrCasRenterContractAdditional renterContractAdditional = new CrCasRenterContractAdditional();

            var carInfo = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == SerialNo);
            var carPrice = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicNo == PriceNo);
            if (carInfo != null && carPrice != null)
            {
                var carAdditional = _unitOfWork.CrCasPriceCarAdditional.Find(x => x.CrCasPriceCarAdditionalNo == carPrice.CrCasPriceCarBasicNo && x.CrCasPriceCarAdditionalCode == Additional);
                if (carAdditional != null)
                {
                    renterContractAdditional.CrCasRenterContractAdditionalNo = carAdditional.CrCasPriceCarAdditionalNo;
                    renterContractAdditional.CrCasRenterContractAdditionalCode = carAdditional.CrCasPriceCarAdditionalCode;
                    renterContractAdditional.CrCasContractAdditionalValue = carAdditional.CrCasPriceCarAdditionalValue;
                }
            }
            if (renterContractAdditional != null && _unitOfWork.CrCasRenterContractAdditional.Add(renterContractAdditional) != null) return true;
            return false;
        }


        public async Task<bool> AddRenterContractCheckUp(string LessorCode, string SerialNo, string PriceNo, string CheckUpCode,string Reasons)
        {
            CrCasRenterContractCarCheckup renterContractCarCheckup = new CrCasRenterContractCarCheckup();

            var carInfo = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == SerialNo);
            var carPrice = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicNo == PriceNo);
            if (carInfo != null && carPrice != null)
            {
                var carCheckUp = _unitOfWork.CrMasSupContractCarCheckup.Find(x => x.CrMasSupContractCarCheckupCode == CheckUpCode);
                if (carCheckUp != null)
                {
                    renterContractCarCheckup.CrCasRenterContractCarCheckupNo = carPrice.CrCasPriceCarBasicNo;
                    renterContractCarCheckup.CrCasRenterContractCarCheckupCode = carCheckUp.CrMasSupContractCarCheckupCode;
                    renterContractCarCheckup.CrCasRenterContractCarCheckupType = "1";
                    renterContractCarCheckup.CrCasRenterContractCarCheckupReasons = Reasons;
                }
            }

            if (renterContractCarCheckup != null && _unitOfWork.CrCasRenterContractCarCheckup.Add(renterContractCarCheckup) != null) return true;
            return false;
        }


        private string GetSector(string firstChar, string sectorType)
        {
            var sectorCode = "";
            var sector = _unitOfWork.CrMasSupRenterSector.Find(x => x.CrMasSupRenterSectorArName.Trim() == sectorType.Trim());
            if (sector != null) sectorCode = sector.CrMasSupRenterSectorCode;
            else
            {
                if (firstChar == "7") sectorCode = "0";
                else sectorCode = "1";
            }
            return sectorCode;
        }
        private string GetNationality(string firstChar, string nationalityAr, string nationalityEn)
        {
            var nationalityCode = "";
            if (firstChar == "7") nationalityCode = "1000000001"; // None becasue its not human
            else if (string.IsNullOrEmpty(nationalityAr) && string.IsNullOrEmpty(nationalityEn)) nationalityCode = "1000000002";
            else
            {
                var nationlity = _unitOfWork.CrMasSupRenterNationality.Find(x => x.CrMasSupRenterNationalitiesArName == nationalityAr.Trim() || x.CrMasSupRenterNationalitiesEnName == nationalityEn.Trim());
                if (nationlity != null) nationalityCode = nationlity.CrMasSupRenterNationalitiesCode;
                else // If == Null should be enter the new value in RenterNationlity table ;
                {
                    var lastRecordInNationality = _unitOfWork.CrMasSupRenterNationality.GetAll().LastOrDefault();
                    if (lastRecordInNationality == null) nationalityCode = "1000000003";
                    else
                    {
                        int intValue = int.Parse(lastRecordInNationality.CrMasSupRenterNationalitiesCode);
                        int newIntValue = intValue + 1;
                        nationalityCode = newIntValue.ToString();
                    }
                    CrMasSupRenterNationality crMasSupRenterNationality = new CrMasSupRenterNationality
                    {
                        CrMasSupRenterNationalitiesCode = nationalityCode,
                        CrMasSupRenterNationalitiesArName = nationalityAr.Trim(),
                        CrMasSupRenterNationalitiesEnName = nationalityEn.Trim(),
                        CrMasSupRenterNationalitiesFlag = "",
                        CrMasSupRenterNationalitiesCounter = 0,
                        CrMasSupRenterNationalitiesGroupCode = "10",
                        CrMasSupRenterNationalitiesStatus = Status.Active,
                        CrMasSupRenterNationalitiesReasons = ""
                    };
                    _unitOfWork.CrMasSupRenterNationality.Add(crMasSupRenterNationality);
                    //_unitOfWork.Complete();
                }
            }
            return nationalityCode;
        }
        private string GetGender(string firstChar, string GenderAr, string GenderEn)
        {
            var GenderCode = "";
            if (firstChar == "7") GenderCode = "1100000001"; // None becasue its not human
            else if (string.IsNullOrEmpty(GenderAr) && string.IsNullOrEmpty(GenderEn)) GenderCode = "1100000002"; // Not Avaliable Value
            else
            {
                var gender = _unitOfWork.CrMasSupRenterGender.Find(x => x.CrMasSupRenterGenderArName == GenderAr.Trim() || x.CrMasSupRenterGenderEnName == GenderEn.Trim());
                if (gender != null) GenderCode = gender.CrMasSupRenterGenderCode;
                else
                {
                    var lastRecordInGender = _unitOfWork.CrMasSupRenterGender.GetAll().LastOrDefault();
                    if (lastRecordInGender == null) GenderCode = "1100000003";
                    else
                    {
                        int intValue = int.Parse(lastRecordInGender.CrMasSupRenterGenderCode);
                        int newIntValue = intValue + 1;
                        GenderCode = newIntValue.ToString();
                    }

                    CrMasSupRenterGender crMasSupRenterGender = new CrMasSupRenterGender
                    {
                        CrMasSupRenterGenderCode = GenderCode,
                        CrMasSupRenterGenderArName = GenderAr.Trim(),
                        CrMasSupRenterGenderEnName = GenderEn.Trim(),
                        CrMasSupRenterGenderGroupCode = "11",
                        CrMasSupRenterGenderStatus = Status.Active,
                        CrMasSupRenterGenderReasons = ""
                    };
                    _unitOfWork.CrMasSupRenterGender.Add(crMasSupRenterGender);
                    //_unitOfWork.Complete();
                }
            }
            return GenderCode;
        }
        private string GetProfessions(string firstChar, string ProfessionsAr, string ProfessionsEn)
        {
            var ProfessionsCode = "";
            if (firstChar == "7") ProfessionsCode = "1400000001"; // None becasue its not human
            else if (string.IsNullOrEmpty(ProfessionsAr) && string.IsNullOrEmpty(ProfessionsEn)) ProfessionsCode = "1400000002";
            else
            {
                var Professions = _unitOfWork.CrMasSupRenterProfession.Find(x => x.CrMasSupRenterProfessionsArName == ProfessionsAr.Trim() || x.CrMasSupRenterProfessionsEnName == ProfessionsEn.Trim());
                if (Professions != null) ProfessionsCode = Professions.CrMasSupRenterProfessionsCode;
                else
                {
                    var lastRecordInProfessions = _unitOfWork.CrMasSupRenterProfession.GetAll().LastOrDefault();
                    if (lastRecordInProfessions == null) ProfessionsCode = "1400000003";
                    else
                    {
                        int intValue = int.Parse(lastRecordInProfessions.CrMasSupRenterProfessionsCode);
                        int newIntValue = intValue + 1;
                        ProfessionsCode = newIntValue.ToString();
                    }
                    CrMasSupRenterProfession crMasSupRenterProfession = new CrMasSupRenterProfession
                    {
                        CrMasSupRenterProfessionsCode = ProfessionsCode,
                        CrMasSupRenterProfessionsArName = ProfessionsAr.Trim(),
                        CrMasSupRenterProfessionsEnName = ProfessionsEn.Trim(),
                        CrMasSupRenterProfessionsGroupCode = "14",
                        CrMasSupRenterProfessionsStatus = Status.Active,
                        CrMasSupRenterProfessionsReasons = ""
                    };
                    _unitOfWork.CrMasSupRenterProfession.Add(crMasSupRenterProfession);
                    //_unitOfWork.Complete();
                }
            }
            return ProfessionsCode;
        }
        private string GetLicence(string firstChar, string LicenceAr, string LicenceEn)
        {
            var LicenceCode = "";
            if (firstChar == "7") LicenceCode = "1"; // None becasue its not human
            else if (string.IsNullOrEmpty(LicenceAr) && string.IsNullOrEmpty(LicenceEn)) LicenceCode = "2";
            else
            {
                var Licence = _unitOfWork.CrMasSupRenterDrivingLicense.Find(x => x.CrMasSupRenterDrivingLicenseArName == LicenceAr.Trim() || x.CrMasSupRenterDrivingLicenseEnName == LicenceEn.Trim());
                if (Licence != null) LicenceCode = Licence.CrMasSupRenterDrivingLicenseCode;
                else
                {

                    var lastRecordInLicence = _unitOfWork.CrMasSupRenterDrivingLicense.GetAll().LastOrDefault();
                    if (lastRecordInLicence == null) LicenceCode = "3";
                    else
                    {
                        int intValue = int.Parse(lastRecordInLicence.CrMasSupRenterDrivingLicenseCode);
                        int newIntValue = intValue + 1;
                        LicenceCode = newIntValue.ToString();
                    }
                    CrMasSupRenterDrivingLicense crMasSupRenterDrivingLicense = new CrMasSupRenterDrivingLicense
                    {
                        CrMasSupRenterDrivingLicenseCode = LicenceCode,
                        CrMasSupRenterDrivingLicenseArName = LicenceAr.Trim(),
                        CrMasSupRenterDrivingLicenseEnName = LicenceEn.Trim(),
                        CrMasSupRenterDrivingLicenseStatus = Status.Active,
                        CrMasSupRenterDrivingLicenseReasons = ""
                    };
                    _unitOfWork.CrMasSupRenterDrivingLicense.Add(crMasSupRenterDrivingLicense);
                }
            }
            return LicenceCode;
        }
        private string GetEmployer(string firstChar, string EmployerAr, string EmployerEn)
        {
            var EmployerCode = "";
            if (firstChar == "7") EmployerCode = "1800000001"; // None becasue its not human
            else if (string.IsNullOrEmpty(EmployerAr) && string.IsNullOrEmpty(EmployerEn)) EmployerCode = "1800000002";
            else
            {
                var Employer = _unitOfWork.CrMasSupRenterEmployer.Find(x => x.CrMasSupRenterEmployerArName == EmployerAr.Trim() || x.CrMasSupRenterEmployerEnName == EmployerEn.Trim());
                if (Employer != null) EmployerCode = Employer.CrMasSupRenterEmployerCode;
                else
                {

                    var lastRecordInEmployer = _unitOfWork.CrMasSupRenterEmployer.GetAll().LastOrDefault();
                    if (lastRecordInEmployer == null) EmployerCode = "1800000003";
                    else
                    {
                        int intValue = int.Parse(lastRecordInEmployer.CrMasSupRenterEmployerCode);
                        int newIntValue = intValue + 1;
                        EmployerCode = newIntValue.ToString();
                    }
                    CrMasSupRenterEmployer masSupRenterEmployer = new CrMasSupRenterEmployer
                    {
                        CrMasSupRenterEmployerCode = EmployerCode,
                        CrMasSupRenterEmployerArName = EmployerAr.Trim(),
                        CrMasSupRenterEmployerEnName = EmployerEn.Trim(),
                        CrMasSupRenterEmployerGroupCode = "18",
                        CrMasSupRenterEmployerCounter = 0,
                        CrMasSupRenterEmployerStatus = Status.Active,
                        CrMasSupRenterEmployerReasons = "",

                    };
                    _unitOfWork.CrMasSupRenterEmployer.Add(masSupRenterEmployer);
                    //_unitOfWork.Complete();
                }
            }
            return EmployerCode;
        }
        private string GetRegion(string RegionAr, string RegionEn)
        {
            var RegionCode = "";
            if (string.IsNullOrEmpty(RegionAr) && string.IsNullOrEmpty(RegionEn)) RegionCode = "11"; // Not Avaliable
            else
            {
                var region = _unitOfWork.CrMasSupPostRegion.Find(x => x.CrMasSupPostRegionsArName == RegionAr.Trim() || x.CrMasSupPostRegionsEnName == RegionEn.Trim());
                if (region != null) RegionCode = region.CrMasSupPostRegionsCode;
                else // If == Null should be enter the new value in PostCity table ;
                {
                    var lastRecordInRegion = _unitOfWork.CrMasSupPostRegion.GetAll().LastOrDefault();
                    if (lastRecordInRegion == null) RegionCode = "12";
                    else
                    {
                        int intValue = int.Parse(lastRecordInRegion.CrMasSupPostRegionsCode);
                        int newIntValue = intValue + 1;
                        RegionCode = newIntValue.ToString();
                    }
                    CrMasSupPostRegion crMasSupPostRegion = new CrMasSupPostRegion
                    {
                        CrMasSupPostRegionsCode = RegionCode,
                        CrMasSupPostRegionsArName = RegionAr.Trim(),
                        CrMasSupPostRegionsEnName = RegionEn.Trim(),
                        CrMasSupPostRegionsLatitude = 0,
                        CrMasSupPostRegionsLongitude = 0,
                        CrMasSupPostRegionsStatus = Status.Active,
                        CrMasSupPostRegionsLocation = "",
                        CrMasSupPostRegionsReasons = ""
                    };
                    _unitOfWork.CrMasSupPostRegion.Add(crMasSupPostRegion);
                    //_unitOfWork.Complete();
                }
            }
            return RegionCode;
        }
        private string GetCity(string CityAr, string CityEn, string regionCode)
        {
            var CityCode = "";
            if (string.IsNullOrEmpty(CityAr) && string.IsNullOrEmpty(CityEn)) CityCode = "1700000002"; // Not Avaliable
            else
            {
                var city = _unitOfWork.CrMasSupPostCity.Find(x => x.CrMasSupPostCityArName == CityAr.Trim() || x.CrMasSupPostCityArName == CityEn.Trim());
                var region = _unitOfWork.CrMasSupPostRegion.Find(x => x.CrMasSupPostRegionsCode == regionCode);
                if (city != null) CityCode = city.CrMasSupPostCityCode;
                else // If == Null should be enter the new value in PostCity table ;
                {
                    var lastRecordInCity = _unitOfWork.CrMasSupPostCity.GetAll().LastOrDefault();
                    if (lastRecordInCity == null) CityCode = "1700000003";
                    else
                    {
                        int intValue = int.Parse(lastRecordInCity.CrMasSupPostCityCode);
                        int newIntValue = intValue + 1;
                        CityCode = newIntValue.ToString();
                    }
                    CrMasSupPostCity crMasSupPostCity = new CrMasSupPostCity
                    {
                        CrMasSupPostCityCode = CityCode,
                        CrMasSupPostCityArName = CityAr.Trim(),
                        CrMasSupPostCityEnName = CityEn.Trim(),
                        CrMasSupPostCityRegionsCode = regionCode,
                        CrMasSupPostCityGroupCode = "17",
                        CrMasSupPostCityLongitude = null,
                        CrMasSupPostCityLatitude = null,
                        CrMasSupPostCityRegionsStatus = null,
                        CrMasSupPostCityLocation = "",
                        CrMasSupPostCityCounter = 0,
                        CrMasSupPostCityConcatenateArName = region.CrMasSupPostRegionsArName + "-" + CityAr.Trim(),
                        CrMasSupPostCityConcatenateEnName = region.CrMasSupPostRegionsEnName + "-" + CityEn.Trim(),
                        CrMasSupPostCityStatus = Status.Active,
                        CrMasSupPostCityReasons = ""
                    };
                    _unitOfWork.CrMasSupPostCity.Add(crMasSupPostCity);
                    //_unitOfWork.Complete();
                }
            }
            return CityCode;
        }
        private string GetAge(string date)
        {
            string ageCode = "";
            DateTime currentDate = DateTime.Now;
            DateTime birthDate = DateTime.Parse(date);
            int age = currentDate.Year - birthDate.Year;
            switch (age)
            {
                case int a when a < 20:
                    ageCode = "1";
                    break;
                case int a when a <= 30:
                    ageCode = "2";
                    break;
                case int a when a <= 40:
                    ageCode = "3";
                    break;
                case int a when a <= 50:
                    ageCode = "4";
                    break;
                case int a when a <= 60:
                    ageCode = "5";
                    break;
                default:
                    ageCode = "6";
                    break;
            }
            return ageCode;
        }
        private string GetTypeID(string firstChar, string sector)
        {
            string typeID = "";
            if (firstChar == "1") typeID = "1";
            else if (firstChar == "2") typeID = "2";
            else
            {
                if (sector == "2") typeID = "5";
                else typeID = "6";
            }
            return typeID;
        }


    }
}
