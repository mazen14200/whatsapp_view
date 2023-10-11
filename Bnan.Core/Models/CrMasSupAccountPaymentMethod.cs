using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupAccountPaymentMethod
    {
        public string CrMasSupAccountPaymentMethodCode { get; set; } = null!;
        public string? CrMasSupAccountPaymentMethodClassification { get; set; }
        public string? CrMasSupAccountPaymentMethodArName { get; set; }
        public string? CrMasSupAccountPaymentMethodEnName { get; set; }
        public string? CrMasSupAccountPaymentMethodAcceptImage { get; set; }
        public string? CrMasSupAccountPaymentMethodRejectImage { get; set; }
        public string? CrMasSupAccountPaymentMethodStatus { get; set; }
        public string? CrMasSupAccountPaymentMethodReasons { get; set; }
    }
}
