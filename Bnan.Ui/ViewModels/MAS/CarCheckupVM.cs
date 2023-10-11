using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class CarCheckupVM 
    {
        public string CrMasSupContractCarCheckupCode { get; set; } = null!;
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupContractCarCheckupArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupContractCarCheckupEnName { get; set; }
        public string? CrMasSupContractCarCheckupAcceptImage { get; set; }
        public string? CrMasSupContractCarCheckupRejectImage { get; set; }
        public string? CrMasSupContractCarCheckupBlockImage { get; set; }
        public string? CrMasSupContractCarCheckupStatus { get; set; }
        public string? CrMasSupContractCarCheckupReasons { get; set; }
    }
}
