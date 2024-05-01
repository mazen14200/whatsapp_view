using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasAccountReceipt
    {
        public CrCasAccountReceipt()
        {
            CrCasAccountInvoices = new HashSet<CrCasAccountInvoice>();
        }

        public string CrCasAccountReceiptNo { get; set; } = null!;
        public string? CrCasAccountReceiptYear { get; set; }
        public string? CrCasAccountReceiptType { get; set; }
        public string? CrCasAccountReceiptLessorCode { get; set; }
        public string? CrCasAccountReceiptBranchCode { get; set; }
        public DateTime? CrCasAccountReceiptDate { get; set; }
        public string? CrCasAccountReceiptPaymentMethod { get; set; }
        public string? CrCasAccountReceiptReferenceType { get; set; }
        public string? CrCasAccountReceiptReferenceNo { get; set; }
        public decimal? CrCasAccountReceiptPayment { get; set; }
        public decimal? CrCasAccountReceiptReceipt { get; set; }
        public string? CrCasAccountReceiptBank { get; set; }
        public string? CrCasAccountReceiptAccount { get; set; }
        public string? CrCasAccountReceiptCar { get; set; }
        public string? CrCasAccountReceiptSalesPoint { get; set; }
        public decimal? CrCasAccountReceiptSalesPointPreviousBalance { get; set; }
        public string? CrCasAccountReceiptRenterId { get; set; }
        public decimal? CrCasAccountReceiptRenterPreviousBalance { get; set; }
        public string? CrCasAccountReceiptUser { get; set; }
        public decimal? CrCasAccountReceiptUserPreviousBalance { get; set; }
        public decimal? CrCasAccountReceiptBranchUserPreviousBalance { get; set; }
        public string? CrCasAccountReceiptIsPassing { get; set; }
        public DateTime? CrCasAccountReceiptPassingDate { get; set; }
        public string? CrCasAccountReceiptPassingUser { get; set; }
        public string? CrCasAccountReceiptPassingReference { get; set; }
        public string? CrCasAccountReceiptArPdfFile { get; set; }
        public string? CrCasAccountReceiptEnPdfFile { get; set; }
        public string? CrCasAccountReceiptReasons { get; set; }

        public virtual CrCasAccountBank? CrCasAccountReceiptAccountNavigation { get; set; }
        public virtual CrMasSupAccountBank? CrCasAccountReceiptBankNavigation { get; set; }
        public virtual CrCasCarInformation? CrCasAccountReceiptCarNavigation { get; set; }
        public virtual CrCasBranchInformation? CrCasAccountReceiptNavigation { get; set; }
        public virtual CrMasSupAccountPaymentMethod? CrCasAccountReceiptPaymentMethodNavigation { get; set; }
        public virtual CrMasSupAccountReference? CrCasAccountReceiptReferenceTypeNavigation { get; set; }
        public virtual CrMasRenterInformation? CrCasAccountReceiptRenter { get; set; }
        public virtual CrCasAccountSalesPoint? CrCasAccountReceiptSalesPointNavigation { get; set; }
        public virtual CrMasUserInformation? CrCasAccountReceiptUserNavigation { get; set; }
        public virtual ICollection<CrCasAccountInvoice> CrCasAccountInvoices { get; set; }
    }
}
