using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class PaymentMethodsVM
    {
        public string CrMasSupAccountPaymentMethodCode { get; set; } = null!;
        
        public string? CrMasSupAccountPaymentMethodClassification { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupAccountPaymentMethodArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupAccountPaymentMethodEnName { get; set; }
        public string? CrMasSupAccountPaymentMethodAcceptImage { get; set; }
        public string? CrMasSupAccountPaymentMethodRejectImage { get; set; }
        public string? CrMasSupAccountPaymentMethodStatus { get; set; }
        public string? CrMasSupAccountPaymentMethodReasons { get; set; }

    }
}
