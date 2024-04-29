using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasAccountInvoice
    {
        public string CrCasAccountInvoiceNo { get; set; } = null!;
        public string? CrCasAccountInvoiceYear { get; set; }
        public string? CrCasAccountInvoiceType { get; set; }
        public string? CrCasAccountInvoiceLessorCode { get; set; }
        public string? CrCasAccountInvoiceBranchCode { get; set; }
        public DateTime? CrCasAccountInvoiceDate { get; set; }
        public string? CrCasAccountInvoiceReferenceContract { get; set; }
        public string? CrCasAccountInvoiceReferenceReceipt { get; set; }
        public string? CrCasAccountInvoiceUserCode { get; set; }
        public string? CrCasAccountInvoiceArPdfFile { get; set; }
        public string? CrCasAccountInvoiceEnPdfFile { get; set; }

        public virtual CrCasBranchInformation? CrCasAccountInvoiceNavigation { get; set; }
        public virtual CrCasAccountReceipt? CrCasAccountInvoiceReferenceReceiptNavigation { get; set; }
        public virtual CrMasUserInformation? CrCasAccountInvoiceUserCodeNavigation { get; set; }
    }
}
