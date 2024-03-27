using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class CarPriceVM
    {
        public string? CrCasPriceCarBasicNo { get; set; }
        public string? CrCasPriceCarBasicYear { get; set; }
        public string? CrCasPriceCarBasicType { get; set; }
        public string? CrCasPriceCarBasicLessorCode { get; set; }
        public string? CrCasPriceCarBasicBrandCode { get; set; }
        public string? CrCasPriceCarBasicModelCode { get; set; }
        public string? CrCasPriceCarBasicCategoryCode { get; set; }
        public string? CrCasPriceCarBasicCarYear { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasPriceCarBasicDistributionCode { get; set; }
        public DateTime? CrCasPriceCarBasicDate { get; set; }
        public DateTime? CrCasPriceCarBasicStartDate { get; set; }
        public DateTime? CrCasPriceCarBasicEndDate { get; set; }
        
        public int? CrCasPriceCarBasicWeeklyDay { get; set; }
        public int? CrCasPriceCarBasicMonthlyDay { get; set; }
        public int? CrCasPriceCarBasicYearlyDay { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public int? CrCasPriceCarBasicNoDailyFreeKm { get; set; }
        
        [Required(ErrorMessage = "requiredFiled")]
        public int? CrCasPriceCarBasicFreeAdditionalHours { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public int? CrCasPriceCarBasicHourMax { get; set; }
       
        public bool? CrCasPriceCarBasicIsAdditionalDriver { get; set; }
        
        [Required(ErrorMessage = "requiredFiled")]
        public int? CrCasPriceCarBasicMinAge { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public int? CrCasPriceCarBasicMaxAge { get; set; }
      
        public bool? CrCasPriceCarBasicRequireFinancialCredit { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public int? CrCasPriceCarBasicCancelHour { get; set; }
        public int? CrCasPriceCarBasicAlertHour { get; set; }
        public DateTime? CrCasPriceCarBasicDateAboutToFinish { get; set; }
        public string? CrCasPriceCarBasicStatus { get; set; }
        public string? CrCasPriceCarBasicReasons { get; set; }

        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicDailyRent { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicWeeklyRent { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicMonthlyRent { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicYearlyRent { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicAdditionalKmValue { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicAdditionalDriverValue { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicPrivateDriverValue { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasCarPriceBasicInFeesTamm { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasCarPriceBasicOutFeesTamm { get; set; }
        public decimal? CrCasCarPriceBasicInFeesTga { get; set; }
        public decimal? CrCasCarPriceBasicOutFeesTga { get; set; }

        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicExtraHourValue { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicRentalTaxRate { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicCompensationAccident { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicCompensationFire { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicCompensationTheft { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicCompensationDrowning { get; set; }

        //Strings 
        [Required(ErrorMessage = "requiredFiled")]
        public string? DailyRent { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? WeeklyRent { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? MonthlyRent { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? YearlyRent { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? AdditionalKmValue { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? AdditionalDriverValue { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? PrivateDriverValue { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? InFeesTamm { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? OutFeesTamm { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        //public string? InFeesTga { get; set; }
        //[Required(ErrorMessage = "requiredFiled")]
        //public string? OutFeesTga { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? ExtraHourValue { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? RentalTaxRate { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CompensationAccident { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CompensationFire { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CompensationTheft { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CompensationDrowning { get; set; }

        public virtual CrMasSupCarBrand? CrCasPriceCarBasicBrandCodeNavigation { get; set; }
        public virtual CrMasSupCarYear? CrCasPriceCarBasicCarYearNavigation { get; set; }
        public virtual CrMasSupCarCategory? CrCasPriceCarBasicCategoryCodeNavigation { get; set; }
        public virtual CrMasSupCarDistribution? CrCasPriceCarBasicDistributionCodeNavigation { get; set; }
        public virtual CrMasLessorInformation? CrCasPriceCarBasicLessorCodeNavigation { get; set; }
        public virtual CrMasSupCarModel? CrCasPriceCarBasicModelCodeNavigation { get; set; }
        public virtual ICollection<CrCasPriceCarAdditional>? CrCasPriceCarAdditionals { get; set; }
        public virtual ICollection<CrCasPriceCarAdvantage>? CrCasPriceCarAdvantages { get; set; }
        public virtual ICollection<CrCasPriceCarOption>? CrCasPriceCarOptions { get; set; }
    }
}
