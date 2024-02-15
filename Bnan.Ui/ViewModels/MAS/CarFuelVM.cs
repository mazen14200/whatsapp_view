using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class CarFuelVM
    {
        public string CrMasSupCarFuelCode { get; set; } = null!;

        [Required(ErrorMessage = "requiredFiled"), MaxLength(40, ErrorMessage = "requiredNoLengthFiled40")]
        public string CrMasSupCarFuelArName { get; set; }

        [Required(ErrorMessage = "requiredFiled"), MaxLength(40, ErrorMessage = "requiredNoLengthFiled40")]
        public string CrMasSupCarFuelEnName { get; set; }
        public string? CrMasSupCarFuelImage { get; set; }
        public string? CrMasSupCarFuelStatus { get; set; }
        public string? CrMasSupCarFuelReasons { get; set; }
    }
}
