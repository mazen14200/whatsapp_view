using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class SalesPointsVM
    {
        public string? CrCasAccountSalesPointCode { get; set; }
        public string? CrCasAccountSalesPointLessor { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasAccountSalesPointBrn { get; set; }
        public string? CrCasAccountSalesPointBank { get; set; }
        public string? CrCasAccountSalesPointSerial { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasAccountSalesPointAccountBank { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasAccountSalesPointNo { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasAccountSalesPointArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasAccountSalesPointEnName { get; set; }
        public decimal? CrCasAccountSalesPointTotalBalance { get; set; }
        public decimal? CrCasAccountSalesPointTotalReserved { get; set; }
        public decimal? CrCasAccountSalesPointTotalAvailable { get; set; }
        public string? CrCasAccountSalesPointBranchStatus { get; set; }
        public string? CrCasAccountSalesPointBankStatus { get; set; }
        public string? CrCasAccountSalesPointStatus { get; set; }
        public string? CrCasAccountSalesPointReasons { get; set; }

        public virtual CrCasAccountBank? CrCasAccountSalesPointAccountBankNavigation { get; set; }
        public virtual CrMasSupAccountBank? CrCasAccountSalesPointBankNavigation { get; set; }
        public virtual CrCasBranchInformation? CrCasAccountSalesPointNavigation { get; set; }
    }
}
