using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSysProcedure
    {
        public CrMasSysProcedure()
        {
            CrCasBranchDocuments = new HashSet<CrCasBranchDocument>();
            CrCasCarDocumentsMaintenances = new HashSet<CrCasCarDocumentsMaintenance>();
            CrCasLessorMechanisms = new HashSet<CrCasLessorMechanism>();
            CrCasSysAdministrativeProcedures = new HashSet<CrCasSysAdministrativeProcedure>();
            CrMasContractCompanies = new HashSet<CrMasContractCompany>();
        }

        public string CrMasSysProceduresCode { get; set; } = null!;
        public string? CrMasSysProceduresClassification { get; set; }
        public bool? CrMasSysProceduresSubjectAlert { get; set; }
        public string? CrMasSysProceduresArName { get; set; }
        public string? CrMasSysProceduresEnName { get; set; }
        public long? CrMasSysProceduresDaysAlertAboutExpire { get; set; }
        public long? CrMasSysProceduresKmAlertAboutExpire { get; set; }
        public string? CrMasSysProceduresStatus { get; set; }
        public string? CrMasSysProceduresReasons { get; set; }

        public virtual ICollection<CrCasBranchDocument> CrCasBranchDocuments { get; set; }
        public virtual ICollection<CrCasCarDocumentsMaintenance> CrCasCarDocumentsMaintenances { get; set; }
        public virtual ICollection<CrCasLessorMechanism> CrCasLessorMechanisms { get; set; }
        public virtual ICollection<CrCasSysAdministrativeProcedure> CrCasSysAdministrativeProcedures { get; set; }
        public virtual ICollection<CrMasContractCompany> CrMasContractCompanies { get; set; }
    }
}
