using Bnan.Core.Models;

namespace Bnan.Ui.ViewModels.CAS
{
    public class AdmintritiveCustodyVM
    {
        public string CrCasSysAdministrativeProceduresNo { get; set; } = null!;
        public string? CrCasSysAdministrativeProceduresCode { get; set; }
        public string? CrCasSysAdministrativeProceduresClassification { get; set; }
        public string? CrCasSysAdministrativeProceduresLessor { get; set; }
        public string? CrCasSysAdministrativeProceduresBranch { get; set; }
        public string? CrCasSysAdministrativeProceduresDate { get; set; }
        public string? CrCasSysAdministrativeProceduresTargeted { get; set; }
        public string? CrCasSysAdministrativeProceduresDebit { get; set; }
        public string? CrCasSysAdministrativeProceduresCreditor { get; set; }
        public string? TotalAmount { get; set; }
        public string? CrCasSysAdministrativeProceduresDocStartDate { get; set; }
        public string? CrCasSysAdministrativeProceduresDocEndDate { get; set; }
        public string? CrCasSysAdministrativeProceduresUserInsert { get; set; }
        public string? CrCasSysAdministrativeProceduresArDescription { get; set; }
        public string? CrCasSysAdministrativeProceduresEnDescription { get; set; }
        public string? CrCasSysAdministrativeProceduresStatus { get; set; }
        public string? CrCasSysAdministrativeProceduresReasons { get; set; }
        public string? DatePassing { get; set; }
        public string? NewReceiptNo { get; set; }
        public virtual CrMasUserInformation? UserInsertNavigation { get; set; }
        public virtual CrMasUserInformation? UserReceviedNavigation { get; set; }
        public virtual CrCasAccountSalesPoint? SalesPointNavigation { get; set; }
        public virtual List<CrCasAccountReceipt>? AccountReceipt { get; set; }
    }
}
