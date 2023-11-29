using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasCarDocumentsMaintenance
    {
        public string CrCasCarDocumentsMaintenanceSerailNo { get; set; } = null!;
        public string CrCasCarDocumentsMaintenanceProcedures { get; set; } = null!;
        public string? CrCasCarDocumentsMaintenanceProceduresClassification { get; set; }
        public bool? CrCasCarDocumentsMaintenanceIsActivation { get; set; }
        public string? CrCasCarDocumentsMaintenanceLessor { get; set; }
        public string? CrCasCarDocumentsMaintenanceBranch { get; set; }
        public string? CrCasCarDocumentsMaintenanceNo { get; set; }
        public DateTime? CrCasCarDocumentsMaintenanceDate { get; set; }
        public DateTime? CrCasCarDocumentsMaintenanceStartDate { get; set; }
        public DateTime? CrCasCarDocumentsMaintenanceEndDate { get; set; }
        public DateTime? CrCasCarDocumentsMaintenanceDateAboutToFinish { get; set; }
        public int? CrCasCarDocumentsMaintenanceReadKm { get; set; }
        public int? CrCasCarDocumentsMaintenanceConsumptionKm { get; set; }
        public int? CrCasCarDocumentsMaintenanceCurrentMeter { get; set; }
        public int? CrCasCarDocumentsMaintenanceKmAboutToFinish { get; set; }
        public int? CrCasCarDocumentsMaintenanceKmEndsAt { get; set; }
        public string? CrCasCarDocumentsMaintenanceImage { get; set; }
        public string? CrCasCarDocumentsMaintenanceCarStatus { get; set; }
        public string? CrCasCarDocumentsMaintenanceStatus { get; set; }
        public string? CrCasCarDocumentsMaintenanceReasons { get; set; }

        public virtual CrCasBranchInformation? CrCasCarDocumentsMaintenanceNavigation { get; set; }
        public virtual CrMasSysProcedure CrCasCarDocumentsMaintenanceProceduresNavigation { get; set; } = null!;
        public virtual CrCasCarInformation CrCasCarDocumentsMaintenanceSerailNoNavigation { get; set; } = null!;
    }
}
