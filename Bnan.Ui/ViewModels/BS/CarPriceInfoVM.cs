using Bnan.Core.Models;

namespace Bnan.Ui.ViewModels.BS
{
    public class CarPriceInfoVM
    {
        public string CrCasPriceCarBasicNo { get; set; } = null!;
        public string? CrCasPriceCarBasicYear { get; set; }
        public string? CrCasPriceCarBasicType { get; set; }
        public string? CrCasPriceCarBasicLessorCode { get; set; }
        public string? CrCasPriceCarBasicBrandCode { get; set; }
        public string? CrCasPriceCarBasicModelCode { get; set; }
        public string? CrCasPriceCarBasicCategoryCode { get; set; }
        public string? CrCasPriceCarBasicCarYear { get; set; }
        public string? CrCasPriceCarBasicDistributionCode { get; set; }
        public DateTime? CrCasPriceCarBasicDate { get; set; }
        public DateTime? CrCasPriceCarBasicStartDate { get; set; }
        public DateTime? CrCasPriceCarBasicEndDate { get; set; }
        public decimal? CrCasPriceCarBasicDailyRent { get; set; }
        public decimal? CrCasPriceCarBasicWeeklyRent { get; set; }
        public decimal? CrCasPriceCarBasicMonthlyRent { get; set; }
        public decimal? CrCasPriceCarBasicYearlyRent { get; set; }
        public int? CrCasPriceCarBasicWeeklyDay { get; set; }
        public int? CrCasPriceCarBasicMonthlyDay { get; set; }
        public int? CrCasPriceCarBasicYearlyDay { get; set; }
        public int? CrCasPriceCarBasicNoDailyFreeKm { get; set; }
        public decimal? CrCasPriceCarBasicAdditionalKmValue { get; set; }
        public int? CrCasPriceCarBasicFreeAdditionalHours { get; set; }
        public int? CrCasPriceCarBasicHourMax { get; set; }
        public decimal? CrCasPriceCarBasicExtraHourValue { get; set; }
        public decimal? CrCasPriceCarBasicRentalTaxRate { get; set; }
        public bool? CrCasPriceCarBasicIsAdditionalDriver { get; set; }
        public decimal? CrCasPriceCarBasicAdditionalDriverValue { get; set; }
        public decimal? CrCasCarPriceBasicInFeesTamm { get; set; }
        public decimal? CrCasCarPriceBasicOutFeesTamm { get; set; }
        public decimal? CrCasCarPriceBasicInFeesTga { get; set; }
        public decimal? CrCasCarPriceBasicOutFeesTga { get; set; }
        public int? CrCasPriceCarBasicMinAge { get; set; }
        public int? CrCasPriceCarBasicMaxAge { get; set; }
        public decimal? CrCasPriceCarBasicCompensationAccident { get; set; }
        public decimal? CrCasPriceCarBasicCompensationFire { get; set; }
        public decimal? CrCasPriceCarBasicCompensationTheft { get; set; }
        public decimal? CrCasPriceCarBasicCompensationDrowning { get; set; }
        public bool? CrCasPriceCarBasicRequireFinancialCredit { get; set; }
        public int? CrCasPriceCarBasicCancelHour { get; set; }
        public int? CrCasPriceCarBasicAlertHour { get; set; }
        public DateTime? CrCasPriceCarBasicDateAboutToFinish { get; set; }
        public string? CrCasPriceCarBasicStatus { get; set; }
        public string? CrCasPriceCarBasicReasons { get; set; }

        public virtual CrMasSupCarBrand? CrCasPriceCarBasicBrandCodeNavigation { get; set; }
        public virtual CrMasSupCarCategory? CrCasPriceCarBasicCategoryCodeNavigation { get; set; }
        public virtual CrMasSupCarDistribution? CrCasPriceCarBasicDistributionCodeNavigation { get; set; }
        public virtual CrMasLessorInformation? CrCasPriceCarBasicLessorCodeNavigation { get; set; }
        public virtual CrMasSupCarModel? CrCasPriceCarBasicModelCodeNavigation { get; set; }
        public virtual ICollection<CrCasPriceCarAdditional>? CrCasPriceCarAdditionals { get; set; }
        public virtual ICollection<CrCasPriceCarAdvantage>? CrCasPriceCarAdvantages { get; set; }
        public virtual ICollection<CrCasPriceCarOption>? CrCasPriceCarOptions { get; set; }





    }
}
