using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class CrMasSupCarDistributionVM
    {

        public string? CrMasSupCarDistributionCode { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public string CrMasSupCarDistributionModel { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string CrMasSupCarDistributionCategory { get; set; } 
        [Required(ErrorMessage = "requiredFiled")]
        public string CrMasSupCarDistributionYear { get; set; } 

        [Required(ErrorMessage = "requiredFiled")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "requiredNumber")]
        [Range(1, int.MaxValue, ErrorMessage = "requiredGreaterThan0")]
        public int? CrMasSupCarDistributionDoor { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "requiredNumber")]
        [Range(1, int.MaxValue, ErrorMessage = "requiredGreaterThan0")]
        public int? CrMasSupCarDistributionBagBags { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "requiredNumber")]
        [Range(1, int.MaxValue, ErrorMessage = "requiredGreaterThan0")]
        public int? CrMasSupCarDistributionSmallBags { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "requiredNumber")]
        [Range(1, int.MaxValue, ErrorMessage = "requiredGreaterThan0")]
        public int? CrMasSupCarDistributionPassengers { get; set; }

        public int? CrMasSupCarDistributionCount { get; set; }
        public string? CrMasSupCarDistributionConcatenateArName { get; set; }
        public string? CrMasSupCarDistributionConcatenateEnName { get; set; }
        public string? CrMasSupCarDistributionImage { get; set; }
        public string? CrMasSupCarDistributionStatus { get; set; }
        public string? CrMasSupCarDistributionReasons { get; set; }
    }
}
