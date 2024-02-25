using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class RenterIdTypeVM
    {
        public string CrMasSupRenterIdtypeCode { get; set; } = null!;

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasSupRenterIdtypeArName { get; set; }

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasSupRenterIdtypeEnName { get; set; }
        public string? CrMasSupRenterIdtypeStatus { get; set; }
        public string? CrMasSupRenterIdtypeReasons { get; set; }
    }
}
