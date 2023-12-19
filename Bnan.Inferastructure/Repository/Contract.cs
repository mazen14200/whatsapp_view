using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
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
        public async Task<bool> AddRenterFromElmToMasRenter(string RenterID, CrElmEmployer crElmEmployer, CrElmPersonal crElmPersonal, CrElmLicense crElmLicense)
        {
            var sectorCode = GetSector(RenterID, crElmPersonal.CrElmPersonalSector);
            var nationalityCode = GetNationality(sectorCode, crElmPersonal.CrElmPersonalArNationality, crElmPersonal.CrElmPersonalEnNationality);
            var genderCode = GetGender(sectorCode, crElmPersonal.CrElmPersonalArGender, crElmPersonal.CrElmPersonalEnGender);
            var licenceCode = GetLicence(sectorCode, crElmLicense.CrElmLicenseArName, crElmLicense.CrElmLicenseEnName);
            var employerCode = GetEmployer(sectorCode, crElmEmployer.CrElmEmployerArName, crElmEmployer.CrElmEmployerEnName);
            var professionsCode = GetProfessions(sectorCode, crElmPersonal.CrElmPersonalArProfessions, crElmPersonal.CrElmPersonalEnProfessions);
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
                CrMasRenterInformationIssuePlace = null,
                CrMasRenterInformationRenterIdImage = null,
                CrMasRenterInformationRenterLicenseImage = null,
                CrMasRenterInformationBank = null,
                CrMasRenterInformationIban = null,
                CrMasRenterInformationSector = sectorCode,
                CrMasRenterInformationNationality = nationalityCode,
                CrMasRenterInformationGender = genderCode,
                CrMasRenterInformationDrivingLicenseType=licenceCode,
                CrMasRenterInformationJobs=employerCode,
                CrMasRenterInformationWorkplaceSubscription = professionsCode,
                CrMasRenterInformationStatus = "A",
                CrMasRenterInformationIdtype=null,
                CrMasRenterInformationDrivingLicenseNo = crElmLicense.CrElmLicenseNo,
                CrMasRenterInformationDrivingLicenseDate = crElmLicense.CrElmLicenseIssuedDate,
                CrMasRenterInformationExpiryDrivingLicenseDate= crElmLicense.CrElmLicenseExpiryDate,
                CrMasRenterInformationSignature=null,
                CrMasRenterInformationTaxNo= null,
                CrMasRenterInformationReasons= null
            };
            if (await _unitOfWork.CrMasRenterInformation.AddAsync(renterInformation)==null) return false;
            return true;
        }

        private string GetSector(string renterID, string sectorType)
        {
            var sectorCode = "";
            var sector = _unitOfWork.CrMasSupRenterSector.Find(x => x.CrMasSupRenterSectorArName.Trim() == sectorType.Trim());
            if (sector != null) sectorCode = sector.CrMasSupRenterSectorCode;
            else
            {
                var SubSector = renterID.Substring(0, 1);
                var OurSectorCode = "";
                if (SubSector == "1" || SubSector == "2")
                {
                    sectorCode = "1";
                }
                else if (SubSector == "7")
                {
                    sectorCode = "2";
                }
            }
            return sectorCode;
        }

        private string GetNationality(string sectorCode, string nationalityAr, string nationalityEn)
        {
            var nationalityCode = "";
            if (sectorCode != "1") nationalityCode = "1000000001"; // None becasue its not human
            else
            {
                var nationlity = _unitOfWork.CrMasSupRenterNationality.Find(x => x.CrMasSupRenterNationalitiesArName == nationalityAr.Trim() || x.CrMasSupRenterNationalitiesEnName == nationalityEn.Trim());
                if (nationlity != null) nationalityCode = nationlity.CrMasSupRenterNationalitiesCode;
                else // If == Null should be enter the new value in RenterNationlity table ;
                {
                    var lastRecordInNationality = _unitOfWork.CrMasSupRenterNationality.GetAll().LastOrDefault();
                    int intValue = int.Parse(lastRecordInNationality.CrMasSupRenterNationalitiesCode);
                    int newIntValue = intValue + 1;
                    nationalityCode = newIntValue.ToString();

                    CrMasSupRenterNationality crMasSupRenterNationality = new CrMasSupRenterNationality
                    {
                        CrMasSupRenterNationalitiesCode = nationalityCode,
                        CrMasSupRenterNationalitiesArName = nationalityAr.Trim(),
                        CrMasSupRenterNationalitiesEnName = nationalityEn.Trim(),
                        CrMasSupRenterNationalitiesFlag = "",
                        CrMasSupRenterNationalitiesCounter = 0,
                        CrMasSupRenterNationalitiesStatus = Status.Active,
                        CrMasSupRenterNationalitiesReasons = ""
                    };
                    _unitOfWork.CrMasSupRenterNationality.Add(crMasSupRenterNationality);
                    _unitOfWork.Complete();
                }
            }
            return nationalityCode;
        }

        private string GetGender(string sectorCode, string GenderAr, string GenderEn)
        {
            var GenderCode = "";
            if (sectorCode != "1") GenderCode = "1100000001"; // None becasue its not human
            else
            {
                var gender = _unitOfWork.CrMasSupRenterGender.Find(x => x.CrMasSupRenterGenderArName == GenderAr.Trim() || x.CrMasSupRenterGenderEnName == GenderEn.Trim());
                if (gender != null) GenderCode = gender.CrMasSupRenterGenderCode;
                else GenderCode = "1100000002"; // Not Avaliable Value 
            }
            return GenderCode;
        }

        private string GetProfessions(string sectorCode, string ProfessionsAr, string ProfessionsEn)
        {
            var ProfessionsCode = "";
            if (sectorCode != "1") ProfessionsCode = "1400000001"; // None becasue its not human
            else
            {
                var Professions = _unitOfWork.CrMasSupRenterProfession.Find(x => x.CrMasSupRenterProfessionsArName == ProfessionsAr.Trim() || x.CrMasSupRenterProfessionsEnName == ProfessionsEn.Trim());
                if (Professions != null) ProfessionsCode = Professions.CrMasSupRenterProfessionsCode;
                else ProfessionsCode = "1100000002"; // Not Avaliable Value 
            }
            return ProfessionsCode;
        }

        private string GetLicence(string sectorCode, string LicenceAr, string LicenceEn)
        {
            var LicenceCode = "";
            if (sectorCode != "1") LicenceCode = "1"; // None becasue its not human
            else
            {
                var Licence = _unitOfWork.CrMasSupRenterDrivingLicense.Find(x => x.CrMasSupRenterDrivingLicenseArName == LicenceAr.Trim() || x.CrMasSupRenterDrivingLicenseEnName == LicenceEn.Trim());
                if (Licence != null) LicenceCode = Licence.CrMasSupRenterDrivingLicenseCode;
                else LicenceCode = "2"; // Not Avaliable Value 
            }
            return LicenceCode;
        }

        private string GetEmployer(string sectorCode, string EmployerAr, string EmployerEn)
        {
            var EmployerCode = "";
            if (sectorCode != "1") EmployerCode = "1800000001"; // None becasue its not human
            else
            {
                var Employer = _unitOfWork.CrMasSupRenterEmployer.Find(x => x.CrMasSupRenterEmployerArName == EmployerAr.Trim() || x.CrMasSupRenterEmployerEnName == EmployerEn.Trim());
                if (Employer != null) EmployerCode = Employer.CrMasSupRenterEmployerCode;
                else EmployerCode = "1800000002"; // Not Avaliable Value 
            }
            return EmployerCode;
        }
        private string GetRegion(string sectorCode, string RegionAr, string RegionEn)
        {
            var RegionCode = "";
            if (string.IsNullOrEmpty(RegionAr) && string.IsNullOrEmpty(RegionEn)) RegionCode = "11"; // None becasue its not human
            else
            {
                var region = _unitOfWork.CrMasSupPostRegion.Find(x => x.CrMasSupPostRegionsArName == RegionAr.Trim() || x.CrMasSupPostRegionsEnName == RegionEn.Trim());
                if (region != null) RegionCode = region.CrMasSupPostRegionsCode;
                else // If == Null should be enter the new value in PostCity table ;
                {
                    var lastRecordInRegion = _unitOfWork.CrMasSupPostRegion.GetAll().LastOrDefault();
                    int intValue = int.Parse(lastRecordInRegion.CrMasSupPostRegionsCode);
                    int newIntValue = intValue + 1;
                    RegionCode = newIntValue.ToString();

                    CrMasSupPostRegion crMasSupPostRegion = new CrMasSupPostRegion
                    {
                        CrMasSupPostRegionsCode = RegionCode,
                        CrMasSupPostRegionsArName = RegionAr.Trim(),
                        CrMasSupPostRegionsEnName = RegionEn.Trim(),
                        CrMasSupPostRegionsLatitude =0,
                        CrMasSupPostRegionsLongitude = 0,
                        CrMasSupPostRegionsStatus = Status.Active,
                        CrMasSupPostRegionsLocation="",
                        CrMasSupPostRegionsReasons = ""
                    };
                    _unitOfWork.CrMasSupPostRegion.Add(crMasSupPostRegion);
                    _unitOfWork.Complete();
                }
            }
            return RegionCode;
        }

        private string GetCity(string sectorCode, string CityAr, string CityEn,string regionCode)
        {
            var CityCode = "";
            if (string.IsNullOrEmpty(CityAr) && string.IsNullOrEmpty(CityEn)) CityCode = "1700000002"; // None becasue its not human
            else
            {
                var city = _unitOfWork.CrMasSupPostCity.Find(x => x.CrMasSupPostCityArName == CityAr.Trim() || x.CrMasSupPostCityArName == CityEn.Trim());
                var region = _unitOfWork.CrMasSupPostRegion.Find(x => x.CrMasSupPostRegionsCode == regionCode);
                if (city != null) CityCode = city.CrMasSupPostCityCode;
                else // If == Null should be enter the new value in PostCity table ;
                {
                    var lastRecordInCity = _unitOfWork.CrMasSupPostCity.GetAll().LastOrDefault();
                    int intValue = int.Parse(lastRecordInCity.CrMasSupPostCityCode);
                    int newIntValue = intValue + 1;
                    CityCode = newIntValue.ToString();

                    CrMasSupPostCity crMasSupPostCity = new CrMasSupPostCity
                    {
                        CrMasSupPostCityCode = CityCode,
                        CrMasSupPostCityArName = CityAr.Trim(),
                        CrMasSupPostCityEnName = CityEn.Trim(),
                        CrMasSupPostCityRegionsCode = regionCode,
                        CrMasSupPostCityGroupCode = "17",
                        CrMasSupPostCityLongitude=null,
                        CrMasSupPostCityLatitude=null,
                        CrMasSupPostCityRegionsStatus = null,
                        CrMasSupPostCityLocation = "",
                        CrMasSupPostCityCounter = 0,
                        CrMasSupPostCityConcatenateArName= region.CrMasSupPostRegionsArName+"-"+CityAr.Trim(),
                        CrMasSupPostCityConcatenateEnName= region.CrMasSupPostRegionsEnName+"-"+CityEn.Trim(),
                        CrMasSupPostCityStatus=Status.Active,
                        CrMasSupPostCityReasons=""
                    };
                    _unitOfWork.CrMasSupPostCity.Add(crMasSupPostCity);
                    _unitOfWork.Complete();
                }
            }
            return CityCode;
        }
    }
}
