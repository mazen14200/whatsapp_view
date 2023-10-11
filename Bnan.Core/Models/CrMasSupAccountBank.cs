using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupAccountBank
    {
        public CrMasSupAccountBank()
        {
            CrCasAccountBanks = new HashSet<CrCasAccountBank>();
            CrCasAccountSalesPoints = new HashSet<CrCasAccountSalesPoint>();
        }

        public string CrMasSupAccountBankCode { get; set; } = null!;
        public string? CrMasSupAccountBankArName { get; set; }
        public string? CrMasSupAccountBankEnName { get; set; }
        public string? CrMasSupAccountBankStatus { get; set; }
        public string? CrMasSupAccountBankReasons { get; set; }

        public virtual ICollection<CrCasAccountBank> CrCasAccountBanks { get; set; }
        public virtual ICollection<CrCasAccountSalesPoint> CrCasAccountSalesPoints { get; set; }
    }
}
