using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class AccountRefrenceVM
    {
        public string CrMasSupAccountReceiptReferenceCode { get; set; } = null!;
        [Required(ErrorMessage = "requiredFiled")]

        public string? CrMasSupAccountReceiptReferenceArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]

        public string? CrMasSupAccountReceiptReferenceEnName { get; set; }
        public string? CrMasSupAccountPaymentMethodStatus { get; set; }
        public string? CrMasSupAccountPaymentMethodReasons { get; set; }
    }
}
