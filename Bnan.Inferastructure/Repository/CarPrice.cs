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
    public class CarPrice : ICarPrice
    {
        private IUnitOfWork _unitOfWork;

        public CarPrice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public class VmModel
        {
            public string Id { get; set; }
            public string Value { get; set; }
        }
        public async Task<bool> AddFeatures(string serialNumber, string id, string value)
        {
            var car = _unitOfWork.CrCasCarInformation.Find(x=>x.CrCasCarInformationSerailNo== serialNumber);
            if (car != null)
            {
                CrCasPriceCarAdvantage crCasPriceCarAdvantage = new CrCasPriceCarAdvantage()
                {
                    CrCasPriceCarAdvantagesNo = serialNumber,
                    CrCasPriceCarAdvantagesCode = id,
                    CrCasPriceCarAdvantagesValue = decimal.Parse(value)
                };
                await _unitOfWork.CrCasPriceCarAdvantage.AddAsync(crCasPriceCarAdvantage);
                return true;
            }
            return false;
        }

       



        public async Task<bool> AddChoises(string serialNumber, string id, string value)
        {
            var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == serialNumber);
            if (car != null)
            {
                CrCasPriceCarOption crCasPriceCarOption = new CrCasPriceCarOption()
                {
                    CrCasPriceCarOptionsNo = serialNumber,
                    CrCasPriceCarOptionsCode = id,
                    CrCasPriceCarOptionsValue = decimal.Parse(value)
                };
                await _unitOfWork.CrCasPriceCarOption.AddAsync(crCasPriceCarOption);
                return true;
            }
            return false;
        }

       
        public async Task<bool> AddAdditionals(string serialNumber, string id, string value)
        {
            var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == serialNumber);
            if (car != null)
            {
                CrCasPriceCarAdditional crCasPriceCarAdditional = new CrCasPriceCarAdditional()
                {
                    CrCasPriceCarAdditionalNo = serialNumber,
                    CrCasPriceCarAdditionalCode = id,
                    CrCasPriceCarAdditionalValue = decimal.Parse(value)
                };
                await _unitOfWork.CrCasPriceCarAdditional.AddAsync(crCasPriceCarAdditional);
                return true;
            }
            return false;
        }

        public async Task<bool> AddPriceCar(CrCasPriceCarBasic model)
        {
            var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == model.CrCasPriceCarBasicNo);
            var distribution = _unitOfWork.CrMasSupCarDistribution.Find(x => x.CrMasSupCarDistributionCode == model.CrCasPriceCarBasicDistributionCode);
            if (car != null && distribution!=null)
            {
                var AboutToExpire = _unitOfWork.CrCasLessorMechanism.FindAsync(l => l.CrCasLessorMechanismCode == car.CrCasCarInformationLessor
                                                                               && l.CrCasLessorMechanismProcedures == "219"
                                                                               && l.CrCasLessorMechanismProceduresClassification == "20").Result.CrCasLessorMechanismDaysAlertAboutExpire;
                model.CrCasPriceCarBasicLessorCode = car.CrCasCarInformationLessor;
                model.CrCasPriceCarBasicYear = DateTime.Now.Year.ToString().Substring(2, 2);
                model.CrCasPriceCarBasicBrandCode = distribution.CrMasSupCarDistributionBrand;
                model.CrCasPriceCarBasicCarYear = car.CrCasCarInformationYear;
                model.CrCasPriceCarBasicModelCode = distribution.CrMasSupCarDistributionModel;
                model.CrCasPriceCarBasicCategoryCode = distribution.CrMasSupCarDistributionCategory;
                model.CrCasPriceCarBasicDistributionCode = distribution.CrMasSupCarDistributionCode;
                model.CrCasPriceCarBasicDate = DateTime.Now.Date;
                if (AboutToExpire!=null) model.CrCasPriceCarBasicDateAboutToFinish = model.CrCasPriceCarBasicEndDate?.AddDays(-(double)AboutToExpire);
                model.CrCasPriceCarBasicWeeklyDay = 7;
                model.CrCasPriceCarBasicMonthlyDay = 28;
                model.CrCasPriceCarBasicYearlyDay = 360;
                model.CrCasPriceCarBasicRequireFinancialCredit = false;
                model.CrCasPriceCarBasicStatus = Status.Active;

                await _unitOfWork.CrCasPriceCarBasic.AddAsync(model);
                return true;
            }
            return false;
        }
    }
}


//CrCasPriceCarBasic crCasPriceCarBasic = new CrCasPriceCarBasic()
//{
//    CrCasPriceCarBasicNo = model.CrCasPriceCarBasicNo,
//    CrCasPriceCarBasicCarYear = distribution.CrMasSupCarDistributionYear,
//    CrCasPriceCarBasicType = null,
//    CrCasPriceCarBasicLessorCode = car.CrCasCarInformationLessor,
//    CrCasPriceCarBasicBrandCode = distribution.CrMasSupCarDistributionBrand,
//    CrCasPriceCarBasicModelCode = distribution.CrMasSupCarDistributionModel,
//    CrCasPriceCarBasicCategoryCode = distribution.CrMasSupCarDistributionCategory,
//    CrCasPriceCarBasicDistributionCode = distribution.CrMasSupCarDistributionCode,
//    CrCasPriceCarBasicDate = DateTime.Now.Date,
//    CrCasPriceCarBasicStartDate = model.CrCasPriceCarBasicStartDate,
//    CrCasPriceCarBasicEndDate = model.CrCasPriceCarBasicEndDate,
//    CrCasPriceCarBasicDailyRent = model.CrCasPriceCarBasicDailyRent,
//    CrCasPriceCarBasicWeeklyRent = model.CrCasPriceCarBasicWeeklyRent,
//    CrCasPriceCarBasicMonthlyRent = model.CrCasPriceCarBasicMonthlyRent,
//    CrCasPriceCarBasicYearlyRent = model.CrCasPriceCarBasicYearlyRent,
//    CrCasPriceCarBasicWeeklyDay = 7,
//    CrCasPriceCarBasicMonthlyDay = 28,
//    CrCasPriceCarBasicYearlyDay = 360,
//    CrCasPriceCarBasicNoDailyFreeKm = model.CrCasPriceCarBasicNoDailyFreeKm,
//    CrCasPriceCarBasicFreeAdditionalHours = model.CrCasPriceCarBasicFreeAdditionalHours,
//    CrCasPriceCarBasicExtraHourValue = model.CrCasPriceCarBasicExtraHourValue,
//    CrCasPriceCarBasicHourMax = model.CrCasPriceCarBasicHourMax,
//    CrCasPriceCarBasicAlertHour = null,
//    CrCasPriceCarBasicCancelHour = model.CrCasPriceCarBasicCancelHour,
//    CrCasPriceCarBasicRequireFinancialCredit = false,
//    CrCasPriceCarBasicCompensationAccident = model.CrCasPriceCarBasicCompensationAccident,
//    CrCasPriceCarBasicCompensationDrowning = model.CrCasPriceCarBasicCompensationDrowning,
//    CrCasPriceCarBasicCompensationFire = model.CrCasPriceCarBasicCompensationFire,
//    CrCasPriceCarBasicCompensationTheft = model.CrCasPriceCarBasicCompensationTheft,
//    CrCasCarPriceBasicInFeesTga = null,
//    CrCasCarPriceBasicOutFeesTga = null,
//    CrCasCarPriceBasicInFeesTamm = model.CrCasCarPriceBasicInFeesTamm,
//    CrCasCarPriceBasicOutFeesTamm = model.CrCasCarPriceBasicOutFeesTamm
//};