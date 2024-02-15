using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class CarAdvantageVM
    {
        public string CrMasSupCarAdvantagesCode { get; set; } = null!;

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasSupCarAdvantagesArName { get; set; }

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasSupCarAdvantagesEnName { get; set; }
        public string? CrMasSupCarAdvantagesImage { get; set; }
        public string? CrMasSupCarAdvantagesStatus { get; set; }
        public string? CrMasSupCarAdvantagesReasons { get; set; }
    }
}
