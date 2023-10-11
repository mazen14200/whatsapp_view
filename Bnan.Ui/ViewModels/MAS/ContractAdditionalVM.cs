using Bnan.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class ContractAdditionalVM 
    {
        public string CrMasSupContractAdditionalCode { get; set; } = null!;
        public string? CrMasSupContractAdditionalGroup { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupContractAdditionalArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupContractAdditionalEnName { get; set; }
        public string? CrMasSupContractAdditionalAcceptImage { get; set; }
        public string? CrMasSupContractAdditionalRejectImage { get; set; }
        public string? CrMasSupContractAdditionalBlockImage { get; set; }
        public string? CrMasSupContractAdditionalStatus { get; set; }
        public string? CrMasSupContractAdditionalReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupContractAdditionalGroupNavigation { get; set; }
    }
}
