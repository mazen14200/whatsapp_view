using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class CarCategoryVM
    {
        public string CrMasSupCarCategoryCode { get; set; } = null!;
        public string? CrMasSupCarCategoryGroup { get; set; }
        
        [Required(ErrorMessage = "requiredFiled"), MaxLength(50, ErrorMessage = "requiredNoLengthFiled50")]
        public string? CrMasSupCarCategoryArName { get; set; }

        [Required(ErrorMessage = "requiredFiled"), MaxLength(50, ErrorMessage = "requiredNoLengthFiled50")]
        public string? CrMasSupCarCategoryEnName { get; set; }
        public string? CrMasSupCarCategoryStatus { get; set; }
        public string? CrMasSupCarCategoryReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupCarCategoryGroupNavigation { get; set; }
    }
}
