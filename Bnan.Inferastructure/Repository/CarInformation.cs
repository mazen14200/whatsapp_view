using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class CarInformation : ICarInformation
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
            var owner = _unitOfWork.CrCasOwner.Find(x => x.CrCasOwnersCode == lessor.CrMasLessorInformationGovernmentNo && x.CrCasOwnersLessorCode == lessor.CrMasLessorInformationCode);
            var benficty = _unitOfWork.CrCasBeneficiary.Find(x => x.CrCasBeneficiaryCode == lessor.CrMasLessorInformationGovernmentNo && x.CrCasBeneficiaryLessorCode == lessor.CrMasLessorInformationCode);
            var distribution = _unitOfWork.CrMasSupCarDistribution.Find(x => x.CrMasSupCarDistributionCode == model.CrCasCarInformationDistribution);
            var color = _unitOfWork.CrMasSupCarColor.Find(x => x.CrMasSupCarColorCode == model.CrCasCarInformationMainColor);
            CrCasCarInformation casCarInformation = new CrCasCarInformation()
            {
                CrCasCarInformationSerailNo = model.CrCasCarInformationSerailNo,
                CrCasCarInformationConcatenateArName = $"{distribution.CrMasSupCarDistributionConcatenateArName} - {color.CrMasSupCarColorArName}",
                CrCasCarInformationConcatenateEnName = $"{distribution.CrMasSupCarDistributionConcatenateEnName} - {color.CrMasSupCarColorEnName}",
                CrCasCarDocumentsMaintenances = model.CrCasCarDocumentsMaintenances,
                CrCasCarAdvantages = model.CrCasCarAdvantages,
                CrCasCarInformationCategory = distribution.CrMasSupCarDistributionCategory,
                CrCasCarInformationBrand = distribution.CrMasSupCarDistributionBrand,
                CrCasCarInformationModel = distribution.CrMasSupCarDistributionModel,
                CrCasCarInformationDistribution = model.CrCasCarInformationDistribution,
                CrCasCarInformationYear = distribution.CrMasSupCarDistributionYear,
                CrCasCarInformationCity = branch.CrCasBranchPost?.CrCasBranchPostCity,
                CrCasCarInformationRegion = branch.CrCasBranchPost?.CrCasBranchPostRegions,
                CrCasCarInformationCurrentMeter = model.CrCasCarInformationCurrentMeter,
                CrCasCarInformationCvt = model.CrCasCarInformationCvt,
                CrCasCarInformationFuel = model.CrCasCarInformationFuel,
                CrCasCarInformationFloorColor = model.CrCasCarInformationFloorColor,
                CrCasCarInformationMainColor = model.CrCasCarInformationMainColor,
                CrCasCarInformationSeatColor = model.CrCasCarInformationSeatColor,
                CrCasCarInformationSecondaryColor = model.CrCasCarInformationSecondaryColor,
                CrCasCarInformationJoinedFleetDate = model.CrCasCarInformationJoinedFleetDate,
                CrCasCarInformationLessor = model.CrCasCarInformationLessor,
                CrCasCarInformationStructureNo = model.CrCasCarInformationStructureNo,
                CrCasCarInformationOwner = owner.CrCasOwnersCode,
                CrCasCarInformationBeneficiary = benficty.CrCasBeneficiaryCode,
                CrCasCarInformationBranch = "100",
                CrCasCarInformationLocation = "100",
                CrCasCarInformationPlateArNo = model.CrCasCarInformationPlateArNo,
                CrCasCarInformationPlateEnNo = model.CrCasCarInformationPlateEnNo,
                CrCasCarInformationRegistration = model.CrCasCarInformationRegistration,
                CrCasCarInformationReasons = model.CrCasCarInformationReasons,
                CrCasCarInformationDocumentationStatus = false,
                CrCasCarInformationMaintenanceStatus = true,
                CrCasCarInformationPriceStatus = false,
                CrCasCarInformationBranchStatus = "A",
                CrCasCarInformationStatus = "A",
                CrCasCarInformationOwnerStatus = "A",
                CrCasCarInformationForSaleStatus = "A",
                CrCasCarInformationOfferValueSale = 0,
                CrCasCarInformationConractDaysNo = 0,
                CrCasCarInformationConractCount = 0,
            };
            await _unitOfWork.CrCasCarInformation.AddAsync(casCarInformation);
            return true;
        }
        public async Task<bool> AddAdvantagesToCar(string serialNumber, string advantageCode, string lessor, string distributionCode, string status)
        {
            var distribution = _unitOfWork.CrMasSupCarDistribution.Find(x => x.CrMasSupCarDistributionCode == distributionCode);
            var advantage = _unitOfWork.CrMasSupCarAdvantage.Find(x => x.CrMasSupCarAdvantagesCode == advantageCode);
            if (advantage != null)
            {
                CrCasCarAdvantage crCasCarAdvantage = new CrCasCarAdvantage()
                {
                    CrCasCarAdvantagesSerialNo = serialNumber,
                    CrCasCarAdvantagesLessor = lessor,
                    CrCasCarAdvantagesCode = advantage.CrMasSupCarAdvantagesCode,
                    CrCasCarAdvantagesBrand = distribution.CrMasSupCarDistributionBrand,
                    CrCasCarAdvantagesModel = distribution.CrMasSupCarDistributionModel,
                    CrCasCarAdvantagesCarYear = distribution.CrMasSupCarDistributionYear,
                    CrCasCarAdvantagesCategory = distribution.CrMasSupCarDistributionCategory,
                    CRCasCarAdvantagesStatus = status
                };
                await _unitOfWork.CrCasCarAdvantage.AddAsync(crCasCarAdvantage);
                return true;
            }
            return false;

        }

        public async Task<bool> UpdateCarInformation(CrCasCarInformation crCasCarInformation)
        {
            var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == crCasCarInformation.CrCasCarInformationSerailNo);
            if (car != null) {
                car.CrCasCarInformationRegistration = crCasCarInformation.CrCasCarInformationRegistration;
                car.CrCasCarInformationCvt = crCasCarInformation.CrCasCarInformationCvt;
                car.CrCasCarInformationFuel = crCasCarInformation.CrCasCarInformationFuel;
                car.CrCasCarInformationFloorColor = crCasCarInformation.CrCasCarInformationFloorColor;
                car.CrCasCarInformationSeatColor = crCasCarInformation.CrCasCarInformationSeatColor;
                car.CrCasCarInformationSecondaryColor = crCasCarInformation.CrCasCarInformationSecondaryColor;
                car.CrCasCarInformationReasons = crCasCarInformation.CrCasCarInformationReasons;
                _unitOfWork.CrCasCarInformation.Update(car);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAdvantagesToCar(string serialNumber, string advantageCode, string lessor, string status)
        {
            var advantageCar= _unitOfWork.CrCasCarAdvantage.Find(x=>x.CrCasCarAdvantagesSerialNo==serialNumber&&x.CrCasCarAdvantagesCode==advantageCode&&x.CrCasCarAdvantagesLessor==lessor);
            if (advantageCar != null)
            {
                advantageCar.CRCasCarAdvantagesStatus = status;
                _unitOfWork.CrCasCarAdvantage.Update(advantageCar);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCarToSale(CrCasCarInformation crCasCarInformation)
        {
            var car = await _unitOfWork.CrCasCarInformation.FindAsync(x => x.CrCasCarInformationSerailNo == crCasCarInformation.CrCasCarInformationSerailNo && x.CrCasCarInformationLessor == crCasCarInformation.CrCasCarInformationLessor,
                                                                                                                       new[] {"CrCasCarInformation1", "CrCasCarInformationDistributionNavigation",
                                                                                                                          "CrCasCarInformationCategoryNavigation", "CrCasCarInformation2"});
            if (car != null)
            {
                string status;
                if (crCasCarInformation.CrCasCarInformationForSaleStatus.ToLower() == "true") status = "A";
                else status = "H";

                car.CrCasCarInformationStatus = Status.ForSale;
                car.CrCasCarInformationOfferedSaleDate = crCasCarInformation.CrCasCarInformationOfferedSaleDate;
                car.CrCasCarInformationOfferValueSale = crCasCarInformation.CrCasCarInformationOfferValueSale;
                car.CrCasCarInformationReasons = crCasCarInformation.CrCasCarInformationReasons;
                car.CrCasCarInformationForSaleStatus = status;
                _unitOfWork.CrCasCarInformation.Update(car);
                return true;
            }
            return false;
        }
    }
}
