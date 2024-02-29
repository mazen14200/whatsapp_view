using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasUserMainValidation
    {
        public string CrMasUserMainValidationUser { get; set; } = null!;
        public string CrMasUserMainValidationMainTasks { get; set; } = null!;
        public string? CrMasUserMainValidationMainSystem { get; set; }
        public bool? CrMasUserMainValidationAuthorization { get; set; }

        public virtual CrMasSysSystem? CrMasUserMainValidationMainSystemNavigation { get; set; }
        public virtual CrMasSysMainTask CrMasUserMainValidationMainTasksNavigation { get; set; } = null!;
        public virtual CrMasUserInformation CrMasUserMainValidationUserNavigation { get; set; } = null!;
    }
}
