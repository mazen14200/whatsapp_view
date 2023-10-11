using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasUserSubValidation
    {
        public string CrMasUserSubValidationUser { get; set; } = null!;
        public string CrMasUserSubValidationSubTasks { get; set; } = null!;
        public string? CrMasUserSubValidationSystem { get; set; }
        public string? CrMasUserSubValidationMain { get; set; }
        public bool? CrMasUserSubValidationAuthorization { get; set; }

        public virtual CrMasSysMainTask? CrMasUserSubValidationMainNavigation { get; set; }
        public virtual CrMasSysSubTask CrMasUserSubValidationSubTasksNavigation { get; set; } = null!;
        public virtual CrMasSysSystem? CrMasUserSubValidationSystemNavigation { get; set; }
        public virtual CrMasUserInformation CrMasUserSubValidationUserNavigation { get; set; } = null!;
    }
}
