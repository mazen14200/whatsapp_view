using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bnan.Ui.ViewModels.CAS
{
    public class AccountBankVM
    {
        public string? CrCasAccountBankCode { get; set; }
        public string? CrCasAccountBankLessor { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasAccountBankNo { get; set; }
        public string? CrCasAccountBankSerail { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasAccountBankIban { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasAccountBankArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasAccountBankEnName { get; set; }
        public string? CrCasAccountBankStatus { get; set; }
        public string? CrCasAccountBankReasons { get; set; }
    }
}
