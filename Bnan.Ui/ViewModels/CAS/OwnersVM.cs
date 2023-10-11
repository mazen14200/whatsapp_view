using Bnan.Core.Models;
using Bnan.Ui.ViewModels.MAS;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class OwnersVM
    {
        [RegularExpression(@"^\d{10}$", ErrorMessage = "requiredNoLengthFiled10")]
        [Required(ErrorMessage = "requiredFiled")]
        public string CrCasOwnersCode { get; set; } = null!;
        public string? CrCasOwnersLessorCode { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasOwnersCommercialNo { get; set; }
        public string? CrCasOwnersSector { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasOwnersArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasOwnersEnName { get; set; }
        public string? CrCasOwnersStatus { get; set; }
        public string? CrCasOwnersReasons { get; set; }
    }
}
