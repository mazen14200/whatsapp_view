using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupAccountReference
    {
        public string CrMasSupAccountReceiptReferenceCode { get; set; } = null!;
        public string? CrMasSupAccountReceiptReferenceArName { get; set; }
        public string? CrMasSupAccountReceiptReferenceEnName { get; set; }
        public string? CrMasSupAccountPaymentMethodStatus { get; set; }
        public string? CrMasSupAccountPaymentMethodReasons { get; set; }
    }
}
