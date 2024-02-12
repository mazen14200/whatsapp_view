using System.ComponentModel.DataAnnotations;


namespace Bnan.Ui.ViewModels.MAS
{
    public class CarBrandsVM
    {
        public string CrMasSupCarBrandCode { get; set; } = null!;

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30, ErrorMessage = "requiredNoLengthFiled30")]
        public string CrMasSupCarBrandArName { get; set; }

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasSupCarBrandEnName { get; set; }

        public string? CrMasSupCarBrandImage { get; set; }
        public string? CrMasSupCarBrandStatus { get; set; }
        public string? CrMasSupCarBrandReasons { get; set; }
    }
}
