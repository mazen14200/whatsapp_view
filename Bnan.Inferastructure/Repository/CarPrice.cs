using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public async Task<bool> AddFeatures(string serialNumber, string id, string value)
        {

            CrCasPriceCarAdvantage crCasPriceCarAdvantage = new CrCasPriceCarAdvantage()
            {
                CrCasPriceCarAdvantagesNo = serialNumber,
                CrCasPriceCarAdvantagesCode = id,
                CrCasPriceCarAdvantagesValue = decimal.Parse(value, CultureInfo.InvariantCulture)
            };
            await _unitOfWork.CrCasPriceCarAdvantage.AddAsync(crCasPriceCarAdvantage);
            return true;
        }





        public async Task<bool> AddChoises(string serialNumber, string id, string value)
        {

            CrCasPriceCarOption crCasPriceCarOption = new CrCasPriceCarOption()
            {
                CrCasPriceCarOptionsNo = serialNumber,
                CrCasPriceCarOptionsCode = id,
                CrCasPriceCarOptionsValue = decimal.Parse(value, CultureInfo.InvariantCulture)
            };
            await _unitOfWork.CrCasPriceCarOption.AddAsync(crCasPriceCarOption);
            return true;
        }


        public async Task<bool> AddAdditionals(string serialNumber, string id, string value)
        {
            CrCasPriceCarAdditional crCasPriceCarAdditional = new CrCasPriceCarAdditional()
            {
                CrCasPriceCarAdditionalNo = serialNumber,
                CrCasPriceCarAdditionalCode = id,
                CrCasPriceCarAdditionalValue = decimal.Parse(value, CultureInfo.InvariantCulture)
            };
            await _unitOfWork.CrCasPriceCarAdditional.AddAsync(crCasPriceCarAdditional);
            return true;
        }

        public async Task<string> AddPriceCar(CrCasPriceCarBasic model)
        {
            var distribution = _unitOfWork.CrMasSupCarDistribution.Find(x => x.CrMasSupCarDistributionCode == model.CrCasPriceCarBasicDistributionCode);
            if (distribution != null)
            {
                DateTime year = DateTime.Now;
                var y = year.ToString("yy");
                var Lrecord = _unitOfWork.CrCasPriceCarBasic.FindAll(x => x.CrCasPriceCarBasicLessorCode == model.CrCasPriceCarBasicLessorCode).Max(x => x.CrCasPriceCarBasicNo.Substring(x.CrCasPriceCarBasicNo.Length - 6, 6));
                string Serial;
                if (Lrecord != null)
                {
                    Int64 val = Int64.Parse(Lrecord) + 1;
                    Serial = val.ToString("000000");
                }
                else
                {
                    Serial = "000001";
                }

                var AboutToExpire = _unitOfWork.CrCasLessorMechanism.FindAsync(l => l.CrCasLessorMechanismCode == model.CrCasPriceCarBasicLessorCode
                                                                               && l.CrCasLessorMechanismProcedures == "219"
                                                                               && l.CrCasLessorMechanismProceduresClassification == "20").Result.CrCasLessorMechanismDaysAlertAboutExpire;

                model.CrCasPriceCarBasicNo = y + "-" + "1" + "219" + "-" + model.CrCasPriceCarBasicLessorCode + "100" + "-" + Serial;
                model.CrCasPriceCarBasicLessorCode = model.CrCasPriceCarBasicLessorCode;
                model.CrCasPriceCarBasicYear = DateTime.Now.Year.ToString().Substring(2, 2);
                model.CrCasPriceCarBasicBrandCode = distribution.CrMasSupCarDistributionBrand;
                model.CrCasPriceCarBasicCarYear = distribution.CrMasSupCarDistributionYear;
                model.CrCasPriceCarBasicModelCode = distribution.CrMasSupCarDistributionModel;
                model.CrCasPriceCarBasicCategoryCode = distribution.CrMasSupCarDistributionCategory;
                model.CrCasPriceCarBasicDistributionCode = distribution.CrMasSupCarDistributionCode;
                model.CrCasPriceCarBasicDate = DateTime.Now.Date;
                if (AboutToExpire != null) model.CrCasPriceCarBasicDateAboutToFinish = model.CrCasPriceCarBasicEndDate?.AddDays(-(double)AboutToExpire);
                model.CrCasPriceCarBasicWeeklyDay = 7;
                model.CrCasPriceCarBasicMonthlyDay = 28;
                model.CrCasPriceCarBasicYearlyDay = 360;
                model.CrCasPriceCarBasicRequireFinancialCredit = false;
                if (model.CrCasPriceCarBasicIsAdditionalDriver == false) model.CrCasPriceCarBasicAdditionalDriverValue = 0;
                model.CrCasPriceCarBasicStatus = Status.Active;
                await _unitOfWork.CrCasPriceCarBasic.AddAsync(model);
                return model.CrCasPriceCarBasicNo;
            }
            return null;
        }
    }
}
