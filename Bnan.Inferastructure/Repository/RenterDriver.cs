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
                    CrCasRenterPrivateDriverInformationStatus=Status.Acive
                };
                await _unitOfWork.CrCasRenterPrivateDriverInformation.AddAsync(crCasRenterPrivateDriverInformation);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
    }
}
