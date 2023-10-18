using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class CarInformation: ICarInformation
    {

        public IUnitOfWork _unitOfWork;

        public CarInformation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCarInformation(CrCasCarInformation model)
        {
            var lessor = _unitOfWork.CrMasLessorInformation.Find(x => x.CrMasLessorInformationCode == model.CrCasCarInformationLessor);
            var branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationLessor == model.CrCasCarInformationLessor && x.CrCasBranchInformationCode == "100", new[] { "CrCasBranchPost" });
            var owner = _unitOfWork.CrCasOwner.Find(x => x.CrCasOwnersCode == lessor.CrMasLessorInformationGovernmentNo&&x.CrCasOwnersLessorCode==lessor.CrMasLessorInformationCode);
            var benficty = _unitOfWork.CrCasBeneficiary.Find(x => x.CrCasBeneficiaryCode == lessor.CrMasLessorInformationGovernmentNo && x.CrCasBeneficiaryLessorCode == lessor.CrMasLessorInformationCode);
            var distribution = _unitOfWork.CrMasSupCarDistribution.Find(x => x.CrMasSupCarDistributionCode == model.CrCasCarInformationDistribution);
            CrCasCarInformation casCarInformation = new CrCasCarInformation()
            {
                CrCasCarInformationSerailNo=model.CrCasCarInformationSerailNo,
                CrCasCarInformationConcatenateArName= distribution.CrMasSupCarDistributionConcatenateArName,
                CrCasCarInformationConcatenateEnName= distribution.CrMasSupCarDistributionConcatenateEnName,
                CrCasCarDocumentsMaintenances =model.CrCasCarDocumentsMaintenances,
                CrCasCarAdvantages=model.CrCasCarAdvantages,
                CrCasCarInformationCategory= distribution.CrMasSupCarDistributionCategory,
                CrCasCarInformationBrand= distribution.CrMasSupCarDistributionBrand,
                CrCasCarInformationModel = distribution.CrMasSupCarDistributionModel,
                CrCasCarInformationDistribution = model.CrCasCarInformationDistribution,
                CrCasCarInformationYear = distribution.CrMasSupCarDistributionYear,
                CrCasCarInformationCity = branch.CrCasBranchPost?.CrCasBranchPostCity,
                CrCasCarInformationRegion= branch.CrCasBranchPost?.CrCasBranchPostRegions,
                CrCasCarInformationCurrentMeter= model.CrCasCarInformationCurrentMeter,
                CrCasCarInformationCvt= model.CrCasCarInformationCvt,
                CrCasCarInformationFuel= model.CrCasCarInformationFuel,
                CrCasCarInformationFloorColor= model.CrCasCarInformationFloorColor,
                CrCasCarInformationMainColor= model.CrCasCarInformationMainColor,
                CrCasCarInformationSeatColor = model.CrCasCarInformationSeatColor,
                CrCasCarInformationSecondaryColor= model.CrCasCarInformationSecondaryColor,
                CrCasCarInformationJoinedFleetDate= model.CrCasCarInformationJoinedFleetDate,
                CrCasCarInformationLessor=model.CrCasCarInformationLessor,
                CrCasCarInformationStructureNo= model.CrCasCarInformationStructureNo,
                CrCasCarInformationOwner= owner.CrCasOwnersCode,
                CrCasCarInformationBeneficiary = benficty.CrCasBeneficiaryCode,
                CrCasCarInformationBranch = "100",
                CrCasCarInformationLocation = "100",
                CrCasCarInformationPlateArNo = model.CrCasCarInformationPlateArNo,
                CrCasCarInformationPlateEnNo = model.CrCasCarInformationPlateEnNo,
                CrCasCarInformationRegistration = model.CrCasCarInformationRegistration,
                CrCasCarInformationReasons= model.CrCasCarInformationReasons,
                CrCasCarInformationDocumentationStatus=false,
                CrCasCarInformationMaintenanceStatus=true,
                CrCasCarInformationPriceStatus=false,
                CrCasCarInformationBranchStatus="A",
                CrCasCarInformationStatus="A",
                CrCasCarInformationOwnerStatus="A",
                CrCasCarInformationForSaleStatus="A",
                CrCasCarInformationOfferValueSale=0,
                CrCasCarInformationConractDaysNo=0,
                CrCasCarInformationConractCount=0,
            };



            await _unitOfWork.CrCasCarInformation.AddAsync(casCarInformation);
            return true;
            
            
            
        }
    }
}
