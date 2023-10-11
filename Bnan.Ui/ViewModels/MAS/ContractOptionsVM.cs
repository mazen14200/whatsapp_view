using Bnan.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class ContractOptionsVM
    {
        public string CrMasSupContractOptionsCode { get; set; } = null!;
        public string? CrMasSupContractOptionsGroup { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupContractOptionsArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupContractOptionsEnName { get; set; }
        public string? CrMasSupContractOptionsAcceptImage { get; set; }
        public string? CrMasSupContractOptionsRejectImage { get; set; }
        public string? CrMasSupContractOptionsBlockImage { get; set; }
        public string? CrMasSupContractOptionsStatus { get; set; }
        public string? CrMasSupContractOptionsReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupContractOptionsGroupNavigation { get; set; }
    }
}
