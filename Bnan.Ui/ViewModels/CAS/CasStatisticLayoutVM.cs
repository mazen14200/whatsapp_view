using Bnan.Core.Models;
using Microsoft.CodeAnalysis.Options;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bnan.Ui.ViewModels.CAS
{
    public class CasStatisticLayoutVM
    {
        public List<CrCasBranchInformation>? CrCasBranchInformations { get; set; }

        public decimal? CreditorTotal { get; set; }
        public decimal? DebitTotal { get; set; }
        public decimal? TransaferTotal { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
  
        public List<ChartBranchDataVM>? ChartBranchDataVM { get; set; }
        public List<ChartBranchDataVM>? ChartBranchDataVM_2ForAll { get; set; }
        public List<ChartBranchDataVM>? PaymentMethodsUser { get; set; }
        public CrCasBranchInformation? CrCasBranchInformation { get; set; }
        public CrMasUserBranchValidity? CrMasUserBranchValidity { get; set; }

        public string? SelectedBranch { get; set; }


    }
}
