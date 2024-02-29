using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupAccountPaymentMethod
    {
        public CrMasSupAccountPaymentMethod()
        {
            CrCasAccountReceipts = new HashSet<CrCasAccountReceipt>();
        }

        public string CrMasSupAccountPaymentMethodCode { get; set; } = null!;
        public string? CrMasSupAccountPaymentMethodClassification { get; set; }
        public string? CrMasSupAccountPaymentMethodArName { get; set; }
        public string? CrMasSupAccountPaymentMethodEnName { get; set; }
        public string? CrMasSupAccountPaymentMethodAcceptImage { get; set; }
        public string? CrMasSupAccountPaymentMethodRejectImage { get; set; }
        public string? CrMasSupAccountPaymentMethodStatus { get; set; }
        public string? CrMasSupAccountPaymentMethodReasons { get; set; }

        public virtual ICollection<CrCasAccountReceipt> CrCasAccountReceipts { get; set; }
    }
}
