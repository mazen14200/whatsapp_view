using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSysMainTask
    {
        public CrMasSysMainTask()
        {
            CrMasSysProceduresTasks = new HashSet<CrMasSysProceduresTask>();
            CrMasSysSubTasks = new HashSet<CrMasSysSubTask>();
            CrMasUserLogins = new HashSet<CrMasUserLogin>();
            CrMasUserMainValidations = new HashSet<CrMasUserMainValidation>();
            CrMasUserProceduresValidations = new HashSet<CrMasUserProceduresValidation>();
            CrMasUserSubValidations = new HashSet<CrMasUserSubValidation>();
        }

        public string CrMasSysMainTasksCode { get; set; } = null!;
        public string? CrMasSysMainTasksSystem { get; set; }
        public string? CrMasSysMainTasksArName { get; set; }
        public string? CrMasSysMainTasksEnName { get; set; }
        public string? CrMasSysMainTasksConcatenateArName { get; set; }
        public string? CrMasSysMainTasksConcatenateEnName { get; set; }
        public string? CrMasSysMainTasksStatus { get; set; }
        public string? CrMasSysMainTasksReasons { get; set; }

        public virtual CrMasSysSystem? CrMasSysMainTasksSystemNavigation { get; set; }
        public virtual ICollection<CrMasSysProceduresTask> CrMasSysProceduresTasks { get; set; }
        public virtual ICollection<CrMasSysSubTask> CrMasSysSubTasks { get; set; }
        public virtual ICollection<CrMasUserLogin> CrMasUserLogins { get; set; }
        public virtual ICollection<CrMasUserMainValidation> CrMasUserMainValidations { get; set; }
        public virtual ICollection<CrMasUserProceduresValidation> CrMasUserProceduresValidations { get; set; }
        public virtual ICollection<CrMasUserSubValidation> CrMasUserSubValidations { get; set; }
    }
}
