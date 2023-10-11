using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasUserProceduresValidation
    {
        public string CrMasUserProceduresValidationCode { get; set; } = null!;
        public string CrMasUserProceduresValidationSubTasks { get; set; } = null!;
        public string? CrMasUserProceduresValidationSystem { get; set; }
        public string? CrMasUserProceduresValidationMainTask { get; set; }
        public bool? CrMasUserProceduresValidationInsertAuthorization { get; set; }
        public bool? CrMasUserProceduresValidationUpDateAuthorization { get; set; }
        public bool? CrMasUserProceduresValidationHoldAuthorization { get; set; }
        public bool? CrMasUserProceduresValidationUnHoldAuthorization { get; set; }
        public bool? CrMasUserProceduresValidationDeleteAuthorization { get; set; }
        public bool? CrMasUserProceduresValidationUnDeleteAuthorization { get; set; }

        public virtual CrMasUserInformation CrMasUserProceduresValidationCodeNavigation { get; set; } = null!;
        public virtual CrMasSysMainTask? CrMasUserProceduresValidationMainTaskNavigation { get; set; }
        public virtual CrMasSysSubTask CrMasUserProceduresValidationSubTasksNavigation { get; set; } = null!;
        public virtual CrMasSysSystem? CrMasUserProceduresValidationSystemNavigation { get; set; }
    }
}
