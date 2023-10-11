using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSysSystem
    {
        public CrMasSysSystem()
        {
            CrMasSysMainTasks = new HashSet<CrMasSysMainTask>();
            CrMasSysProceduresTasks = new HashSet<CrMasSysProceduresTask>();
            CrMasSysSubTasks = new HashSet<CrMasSysSubTask>();
            CrMasUserLogins = new HashSet<CrMasUserLogin>();
            CrMasUserMainValidations = new HashSet<CrMasUserMainValidation>();
            CrMasUserProceduresValidations = new HashSet<CrMasUserProceduresValidation>();
            CrMasUserSubValidations = new HashSet<CrMasUserSubValidation>();
        }

        public string CrMasSysSystemCode { get; set; } = null!;
        public string? CrMasSysSystemArName { get; set; }
        public string? CrMasSysSystemEnName { get; set; }
        public string? CrMasSysSystemStatus { get; set; }
        public string? CrMasSysSystemReasons { get; set; }

        public virtual ICollection<CrMasSysMainTask> CrMasSysMainTasks { get; set; }
        public virtual ICollection<CrMasSysProceduresTask> CrMasSysProceduresTasks { get; set; }
        public virtual ICollection<CrMasSysSubTask> CrMasSysSubTasks { get; set; }
        public virtual ICollection<CrMasUserLogin> CrMasUserLogins { get; set; }
        public virtual ICollection<CrMasUserMainValidation> CrMasUserMainValidations { get; set; }
        public virtual ICollection<CrMasUserProceduresValidation> CrMasUserProceduresValidations { get; set; }
        public virtual ICollection<CrMasUserSubValidation> CrMasUserSubValidations { get; set; }
    }
}
