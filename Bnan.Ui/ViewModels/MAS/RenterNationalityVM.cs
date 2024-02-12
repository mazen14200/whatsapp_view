using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class RenterNationalityVM
    {
        public string CrMasSupRenterNationalitiesCode { get; set; } = null!;
        public string? CrMasSupRenterNationalitiesGroupCode { get; set; }

        [Required(ErrorMessage = "requiredFiled"), MaxLength(30)]
        public string? CrMasSupRenterNationalitiesArName { get; set; }
        [Required(ErrorMessage = "requiredFiled"), MaxLength(30)]
        public string? CrMasSupRenterNationalitiesEnName { get; set; }
        public string? CrMasSupRenterNationalitiesFlag { get; set; }
        public int? CrMasSupRenterNationalitiesCounter { get; set; }
        public string? CrMasSupRenterNationalitiesStatus { get; set; }
        public string? CrMasSupRenterNationalitiesReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupRenterNationalitiesGroupCodeNavigation { get; set; }
    }
}
