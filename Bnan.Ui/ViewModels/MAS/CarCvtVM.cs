using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class CarCvtVM
    {
        public string CrMasSupCarCvtCode { get; set; } = null!;

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasSupCarCvtArName { get; set; }

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasSupCarCvtEnName { get; set; }
        public string? CrMasSupCarCvtImage { get; set; }
        public string? CrMasSupCarCvtStatus { get; set; }
        public string? CrMasSupCarCvtReasons { get; set; }

    }
}
