using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.BS
{
    public class ContractSettlementVM
    {
        public string CrCasRenterContractBasicNo { get; set; } = null!;
        public int CrCasRenterContractBasicCopy { get; set; }
        public string? CrCasRenterContractBasicYear { get; set; }
        public string? CrCasRenterContractBasicSector { get; set; }
        public string? CrCasRenterContractBasicProcedures { get; set; }
        public string? CrCasRenterContractBasicLessor { get; set; }
        public string? CrCasRenterContractBasicBranch { get; set; }
        public DateTime? CrCasRenterContractBasicIssuedDate { get; set; }
        public DateTime? CrCasRenterContractBasicAllowCanceled { get; set; }
        public DateTime? CrCasRenterContractBasicExpectedStartDate { get; set; }
        public DateTime? CrCasRenterContractBasicExpectedEndDate { get; set; }
        public int? CrCasRenterContractBasicExpectedRentalDays { get; set; }
        public string? CrCasRenterContractBasicRenterId { get; set; }
        public string? CrCasRenterContractBasicDriverId { get; set; }
        public string? CrCasRenterContractBasicAdditionalDriverId { get; set; }
        public string? CrCasRenterContractBasicPrivateDriverId { get; set; }
        public string? CrCasRenterContractBasicCarSerailNo { get; set; }
        public int? CrCasRenterContractBasicFreeHours { get; set; }
        public int? CrCasRenterContractBasicUserFreeHours { get; set; }
        public int? CrCasRenterContractBasicTotalFreeHours { get; set; }
        public int? CrCasRenterContractBasicHourMax { get; set; }
        public decimal? CrCasRenterContractBasicHourValue { get; set; }
        public int? CrCasRenterContractBasicDailyFreeKm { get; set; }
        public int? CrCasRenterContractBasicDailyFreeKmUser { get; set; }
        public int? CrCasRenterContractBasicTotalDailyFreeKm { get; set; }
        public decimal? CrCasRenterContractBasicKmValue { get; set; }
        public decimal? CrCasRenterContractBasicDailyRent { get; set; }
        public decimal? CrCasRenterContractBasicWeeklyRent { get; set; }
        public decimal? CrCasRenterContractBasicMonthlyRent { get; set; }
        public decimal? CrCasRenterContractBasicYearlyRent { get; set; }
        public decimal? CrCasRenterContractBasicAdditionalDriverValue { get; set; }
        public decimal? CrCasRenterContractBasicPrivateDriverValue { get; set; }
        public decimal? CrCasRenterContractBasicAuthorizationValue { get; set; }
        public decimal? CrCasRenterContractBasicTaxRate { get; set; }
        public decimal? CrCasRenterContractBasicUserDiscountRate { get; set; }
        public int? CrCasRenterContractBasicCurrentReadingMeter { get; set; }
        public decimal? CrCasRenterContractBasicExpectedRentValue { get; set; }
        public decimal? CrCasRenterContractBasicExpectedOptionsValue { get; set; }
        public decimal? CrCasRenterContractBasicAdditionalValue { get; set; }
        public decimal? CrCasRenterContractBasicExpectedPrivateDriverValue { get; set; }
        public decimal? CrCasRenterContractBasicExpectedValueBeforDiscount { get; set; }
        public decimal? CrCasRenterContractBasicExpectedDiscountValue { get; set; }
        public decimal? CrCasRenterContractBasicExpectedValueAfterDiscount { get; set; }
        public decimal? CrCasRenterContractBasicExpectedTaxValue { get; set; }
        public decimal? CrCasRenterContractBasicExpectedTotal { get; set; }
        public decimal? CrCasRenterContractBasicPreviousBalance { get; set; }
        public decimal? CrCasRenterContractBasicAmountRequired { get; set; }
        public decimal? CrCasRenterContractBasicAmountPaidAdvance { get; set; }
        public string? CrCasRenterContractBasicArPdfFile { get; set; }
        public string? CrCasRenterContractBasicEnPdfFile { get; set; }
        public string? CrCasRenterContractBasicArTga { get; set; }
        public string? CrCasRenterContractBasicEnTga { get; set; }
        public string? CrCasRenterContractPriceReference { get; set; }
        public string? CrCasRenterContractOffersReference { get; set; }
        public string? CrCasRenterContractUserReference { get; set; }
        public string? CrCasRenterContractBasicUserInsert { get; set; }
        public string? CrCasRenterContractBasicStatus { get; set; }
        public string? CrCasRenterContractBasicReasons { get; set; }
        public DateTime? AuthEndDate { get; set; }
        public bool? AuthType { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? SalesPoint { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? PaymentMethod { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? DaysNo { get; set; }
        public string? AmountPayed { get; set; }
        public decimal? CasRenterPreviousBalance { get; set; }
        public string? RentValue { get; set; }
        public string? DiscountValue { get; set; }
        public string? TaxValue { get; set; }
        public string? TotalContract { get; set; }
        public string? TotalAmount { get; set; }
        public string? AccountNo { get; set; }
        public string? AdvantagesValue { get; set; }


        public virtual CrCasRenterLessor? CrCasRenterContractBasic5 { get; set; }
        public virtual CrCasCarInformation? CrCasRenterContractBasicCarSerailNoNavigation { get; set; }
        public virtual CrCasBranchInformation? CrCasRenterContractBasic1 { get; set; }

    }
}
