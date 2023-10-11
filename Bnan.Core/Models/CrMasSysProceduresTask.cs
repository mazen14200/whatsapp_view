using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSysProceduresTask
    {
        public string CrMasSysProceduresTasksSubTasks { get; set; } = null!;
        public string? CrMasSysProceduresTasksSystem { get; set; }
        public string? CrMasSysProceduresTasksMainTasks { get; set; }
        public bool? CrMasSysProceduresTasksInsertAvailable { get; set; }
        public string? CrMasSysProceduresTasksInsertArName { get; set; }
        public string? CrMasSysProceduresTasksInsertEnName { get; set; }
        public bool? CrMasSysProceduresTasksUpDateAvailable { get; set; }
        public string? CrMasSysProceduresTasksUpDateArName { get; set; }
        public string? CrMasSysProceduresTasksUpDateEnName { get; set; }
        public bool? CrMasSysProceduresTasksHoldAvailable { get; set; }
        public string? CrMasSysProceduresTasksHoldArName { get; set; }
        public string? CrMasSysProceduresTasksHoldEnName { get; set; }
        public bool? CrMasSysProceduresTasksUnHoldAvailable { get; set; }
        public string? CrMasSysProceduresTasksUnHoldArName { get; set; }
        public string? CrMasSysProceduresTasksUnHoldEnName { get; set; }
        public bool? CrMasSysProceduresTasksDeleteAvailable { get; set; }
        public string? CrMasSysProceduresTasksDeleteArName { get; set; }
        public string? CrMasSysProceduresTasksDeleteEnName { get; set; }
        public bool? CrMasSysProceduresTasksUnDeleteAvailable { get; set; }
        public string? CrMasSysProceduresTasksUnDeleteArName { get; set; }
        public string? CrMasSysProceduresTasksUnDeleteEnName { get; set; }

        public virtual CrMasSysMainTask? CrMasSysProceduresTasksMainTasksNavigation { get; set; }
        public virtual CrMasSysSystem? CrMasSysProceduresTasksSystemNavigation { get; set; }
    }
}
