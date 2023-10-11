using Bnan.Inferastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Bnan.Core.Interfaces;

namespace Bnan.Ui.ViewModels.MAS
{
 
    
    public class BankVM 
    {
        public string CrMasSupAccountBankCode { get; set; } = null!;

        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupAccountBankArName { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupAccountBankEnName { get; set; }

        public string? CrMasSupAccountBankReasons { get; set; }

        public string? CrMasSupAccountBankStatus { get; set; }

    }
}
