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
        public string? AdditionalDriverId { get; set; }
        public string? PrivateDriverId { get; set; }
        public string? RenterReasons { get; set; }
        public string? DriverReasons { get; set; }
        public string? AddDriverReasons { get; set; }
        public string? SerialNo { get; set; }
        public string? PriceNo { get; set; }
        public string? CurrentMeter { get; set; }

    }
}
