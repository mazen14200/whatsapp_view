using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasLessorMechanism
    {
        public string CrCasLessorMechanismCode { get; set; } = null!;
        public string CrCasLessorMechanismProcedures { get; set; } = null!;
        public string? CrCasLessorMechanismProceduresClassification { get; set; }
        public bool? CrCasLessorMechanismActivate { get; set; }
        public int? CrCasLessorMechanismDaysAlertAboutExpire { get; set; }
        public int? CrCasLessorMechanismKmAlertAboutExpire { get; set; }

        public virtual CrMasLessorInformation CrCasLessorMechanismCodeNavigation { get; set; } = null!;
        public virtual CrMasSysProcedure CrCasLessorMechanismProceduresNavigation { get; set; } = null!;
    }
}
