using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.BS
{
    public class Contract
    {
        [Required(ErrorMessage = "requiredFiled")]
        public string? RenterId { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        [RegularExpression(@"^[0-689]\d{9}$", ErrorMessage = "NotStartWith7")]
        public string? DriverId { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        [RegularExpression(@"^[0-689]\d{9}$", ErrorMessage = "NotStartWith7")]
        public string? AdditionalDriverId { get; set; }
        public string? PrivateDriverId { get; set; }
        public string? RenterReasons { get; set; }
        public string? DriverReasons { get; set; }
        public string? AddDriverReasons { get; set; }
        public string? PaymentReasons { get; set; }
        public string? SerialNo { get; set; }
        public string? PriceNo { get; set; }
        public string? CurrentMeter { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? DaysNo { get; set; }
        public string? OutFeesTmm { get; set; }
        public string? FeesTmmValue { get; set; }
        public string? UserDiscount { get; set; }
        public string? UserAddHours { get; set; }
        public string? UserAddKm { get; set; }
        public string? AmountPayed { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? PaymentMethod { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? SalesPoint { get; set; }
        public string? AccountNo { get; set; }
        public string? OptionTotal { get; set; }
        public string? AdditionalTotal { get; set; }
        public string? ContractValueBeforeDiscount { get; set; }
        public string? DiscountValue { get; set; }
        public string? ContractValueAfterDiscount { get; set; }
        public string? TaxValue { get; set; }
        public string? TotalContractAmount { get; set; }
        public string? AdvantagesTotalValue { get; set; }

    }
}
