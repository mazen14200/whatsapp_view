using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class CarRegistrationVM
    {
        public string CrMasSupCarRegistrationCode { get; set; } = null!;

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasSupCarRegistrationArName { get; set; }

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasSupCarRegistrationEnName { get; set; }
        public string? CrMasSupCarRegistrationStatus { get; set; }
        public string? CrMasSupCarRegistrationReasons { get; set; }
    }
}
