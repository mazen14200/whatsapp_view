using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasAccountSalesPoint
    {
        public CrCasAccountSalesPoint()
        {
            CrCasAccountReceipts = new HashSet<CrCasAccountReceipt>();
        }

        public string CrCasAccountSalesPointCode { get; set; } = null!;
        public string? CrCasAccountSalesPointLessor { get; set; }
        public string? CrCasAccountSalesPointBrn { get; set; }
        public string? CrCasAccountSalesPointBank { get; set; }
        public string? CrCasAccountSalesPointSerial { get; set; }
        public string? CrCasAccountSalesPointAccountBank { get; set; }
        public string? CrCasAccountSalesPointNo { get; set; }
        public string? CrCasAccountSalesPointArName { get; set; }
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
        public virtual ICollection<CrCasAccountReceipt> CrCasAccountReceipts { get; set; }
    }
}
