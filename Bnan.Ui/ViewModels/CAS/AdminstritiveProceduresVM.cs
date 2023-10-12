using Bnan.Core.Models;

namespace Bnan.Ui.ViewModels.CAS
{
    public class AdminstritiveProceduresVM
    {
        public string CrCasSysAdministrativeProceduresNo { get; set; } = null!;
        public string? CrCasSysAdministrativeProceduresYear { get; set; }
        public string? CrCasSysAdministrativeProceduresSector { get; set; }
        public string? CrCasSysAdministrativeProceduresCode { get; set; }
        public string? CrCasSysAdministrativeProceduresClassification { get; set; }
        public string? CrCasSysAdministrativeProceduresLessor { get; set; }
        public string? CrCasSysAdministrativeProceduresBranch { get; set; }
        public DateTime? CrCasSysAdministrativeProceduresDate { get; set; }
        public TimeSpan? CrCasSysAdministrativeProceduresTime { get; set; }
        public string? CrCasSysAdministrativeProceduresTargeted { get; set; }
        public decimal? CrCasSysAdministrativeProceduresDebit { get; set; }
        public decimal? CrCasSysAdministrativeProceduresCreditor { get; set; }
        public string? CrCasSysAdministrativeProceduresDocNo { get; set; }
        public DateTime? CrCasSysAdministrativeProceduresDocDate { get; set; }
        public DateTime? CrCasSysAdministrativeProceduresDocStartDate { get; set; }
        public DateTime? CrCasSysAdministrativeProceduresDocEndDate { get; set; }
        public string? CrCasSysAdministrativeProceduresCarFrom { get; set; }
        public string? CrCasSysAdministrativeProceduresCarTo { get; set; }
        public string? CrCasSysAdministrativeProceduresUserInsert { get; set; }
        public string? CrCasSysAdministrativeProceduresArDescription { get; set; }
        public string? CrCasSysAdministrativeProceduresEnDescription { get; set; }
        public string? CrCasSysAdministrativeProceduresStatus { get; set; }
        public string? CrCasSysAdministrativeProceduresReasons { get; set; }
        public string? NameOfTargetAr { get; set; }
        public string? NameOfTargetEn { get; set; }
        public virtual CrCasBranchInformation? CrCasSysAdministrativeProcedures { get; set; }
        public virtual CrMasSysProcedure? CrCasSysAdministrativeProceduresCodeNavigation { get; set; }
        public virtual CrMasSupRenterSector? CrCasSysAdministrativeProceduresSectorNavigation { get; set; }
        public virtual CrMasUserInformation? CrCasSysAdministrativeProceduresUserInsertNavigation { get; set; }
        public virtual ICollection<CrMasUserContractValidity> CrMasUserContractValidities { get; set; }

    }
}
