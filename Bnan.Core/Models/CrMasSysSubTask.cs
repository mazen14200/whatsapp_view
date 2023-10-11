using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSysSubTask
    {
        public CrMasSysSubTask()
        {
            CrMasUserLogins = new HashSet<CrMasUserLogin>();
            CrMasUserProceduresValidations = new HashSet<CrMasUserProceduresValidation>();
            CrMasUserSubValidations = new HashSet<CrMasUserSubValidation>();
        }

        public string CrMasSysSubTasksCode { get; set; } = null!;
        public string? CrMasSysSubTasksSystemCode { get; set; }
        public string? CrMasSysSubTasksMainCode { get; set; }
        public string? CrMasSysSubTasksArName { get; set; }
        public string? CrMasSysSubTasksEnName { get; set; }
        public bool? CrMasSysSubTasksProceduresExpanded { get; set; }
        public string? CrMasSysSubTasksConcatenateArName { get; set; }
        public string? CrMasSysSubTasksConcatenateEnName { get; set; }
        public string? CrMasSysSubTasksStatus { get; set; }
        public string? CrMasSysSubTasksReasons { get; set; }

        public virtual CrMasSysMainTask? CrMasSysSubTasksMainCodeNavigation { get; set; }
        public virtual CrMasSysSystem? CrMasSysSubTasksSystemCodeNavigation { get; set; }
        public virtual ICollection<CrMasUserLogin> CrMasUserLogins { get; set; }
        public virtual ICollection<CrMasUserProceduresValidation> CrMasUserProceduresValidations { get; set; }
        public virtual ICollection<CrMasUserSubValidation> CrMasUserSubValidations { get; set; }
    }
}
