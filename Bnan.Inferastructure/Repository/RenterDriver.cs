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
    public class RenterDriver: IRenterDriver
    {
        public IUnitOfWork _unitOfWork;

        public RenterDriver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddRenterDriver(CrCasRenterPrivateDriverInformation model)
        {
            if (model!=null)
            {
                CrCasRenterPrivateDriverInformation crCasRenterPrivateDriverInformation = new CrCasRenterPrivateDriverInformation()
                {
                    CrCasRenterPrivateDriverInformationId=model.CrCasRenterPrivateDriverInformationId,
                    CrCasRenterPrivateDriverInformationArName=model.CrCasRenterPrivateDriverInformationArName,
                    CrCasRenterPrivateDriverInformationEnName=model.CrCasRenterPrivateDriverInformationEnName,
                    CrCasRenterPrivateDriverInformationEmail=model.CrCasRenterPrivateDriverInformationEmail,
                    CrCasRenterPrivateDriverInformationLicenseType=model.CrCasRenterPrivateDriverInformationLicenseType,
                    CrCasRenterPrivateDriverInformationIdtrype=model.CrCasRenterPrivateDriverInformationIdtrype,
                    CrCasRenterPrivateDriverInformationExpiryIdDate=model.CrCasRenterPrivateDriverInformationExpiryIdDate,
                    CrCasRenterPrivateDriverInformationIdImage=model.CrCasRenterPrivateDriverInformationIdImage,
                    CrCasRenterPrivateDriverInformationIssueIdDate=model.CrCasRenterPrivateDriverInformationIssueIdDate,
                    CrCasRenterPrivateDriverInformationBirthDate= model.CrCasRenterPrivateDriverInformationBirthDate,
                    CrCasRenterPrivateDriverInformationLicenseDate= model.CrCasRenterPrivateDriverInformationLicenseDate,
                    CrCasRenterPrivateDriverInformationContractCount=0,
                    CrCasRenterPrivateDriverInformationDaysCount = 0,
                    CrCasRenterPrivateDriverInformationLessor=model.CrCasRenterPrivateDriverInformationLessor,
                    CrCasRenterPrivateDriverInformationKeyMobile=model.CrCasRenterPrivateDriverInformationKeyMobile,
                    CrCasRenterPrivateDriverInformationMobile =model.CrCasRenterPrivateDriverInformationMobile,
                    CrCasRenterPrivateDriverInformationLicenseExpiry =model.CrCasRenterPrivateDriverInformationLicenseExpiry,
                    CrCasRenterPrivateDriverInformationLicenseImage =model.CrCasRenterPrivateDriverInformationLicenseImage,
                    CrCasRenterPrivateDriverInformationNationality =model.CrCasRenterPrivateDriverInformationNationality,
                    CrCasRenterPrivateDriverInformationSignature =model.CrCasRenterPrivateDriverInformationSignature,
                    CrCasRenterPrivateDriverInformationGender = model.CrCasRenterPrivateDriverInformationGender,
                    CrCasRenterPrivateDriverInformationReasons= model.CrCasRenterPrivateDriverInformationReasons,
                    CrCasRenterPrivateDriverInformationLicenseNo= model.CrCasRenterPrivateDriverInformationLicenseNo,
                    CrCasRenterPrivateDriverInformationEvaluationTotal= 0,
                    CrCasRenterPrivateDriverInformationTraveledDistance=0,
                    CrCasRenterPrivateDriverInformationEvaluationValue= 0,
                    CrCasRenterPrivateDriverInformationStatus=Status.Active
                };
                await _unitOfWork.CrCasRenterPrivateDriverInformation.AddAsync(crCasRenterPrivateDriverInformation);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateRenterDriver(CrCasRenterPrivateDriverInformation model)
        {
            var driver = _unitOfWork.CrCasRenterPrivateDriverInformation.Find(x=>x.CrCasRenterPrivateDriverInformationId==model.CrCasRenterPrivateDriverInformationId&&
                                                                                 x.CrCasRenterPrivateDriverInformationLessor==model.CrCasRenterPrivateDriverInformationLessor);

            if (driver!=null)
            {
                driver.CrCasRenterPrivateDriverInformationArName = model.CrCasRenterPrivateDriverInformationArName;
                driver.CrCasRenterPrivateDriverInformationEnName = model.CrCasRenterPrivateDriverInformationEnName;
                driver.CrCasRenterPrivateDriverInformationLicenseNo = model.CrCasRenterPrivateDriverInformationLicenseNo;
                driver.CrCasRenterPrivateDriverInformationMobile = model.CrCasRenterPrivateDriverInformationMobile;
                driver.CrCasRenterPrivateDriverInformationExpiryIdDate = model.CrCasRenterPrivateDriverInformationExpiryIdDate;
                driver.CrCasRenterPrivateDriverInformationIdImage = model.CrCasRenterPrivateDriverInformationIdImage;
                driver.CrCasRenterPrivateDriverInformationIssueIdDate = model.CrCasRenterPrivateDriverInformationIssueIdDate;
                driver.CrCasRenterPrivateDriverInformationLicenseDate = model.CrCasRenterPrivateDriverInformationLicenseDate;
                driver.CrCasRenterPrivateDriverInformationKeyMobile = model.CrCasRenterPrivateDriverInformationKeyMobile;
                driver.CrCasRenterPrivateDriverInformationLicenseExpiry = model.CrCasRenterPrivateDriverInformationLicenseExpiry;
                driver.CrCasRenterPrivateDriverInformationLicenseImage = model.CrCasRenterPrivateDriverInformationLicenseImage;
                driver.CrCasRenterPrivateDriverInformationSignature = model.CrCasRenterPrivateDriverInformationSignature;
                driver.CrCasRenterPrivateDriverInformationReasons = model.CrCasRenterPrivateDriverInformationReasons;
                _unitOfWork.CrCasRenterPrivateDriverInformation.Update(driver);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
    }
}
