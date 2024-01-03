using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupAccountBank
    {
        public CrMasSupAccountBank()
        {
            CrCasAccountBanks = new HashSet<CrCasAccountBank>();
            CrCasAccountReceipts = new HashSet<CrCasAccountReceipt>();
            CrCasAccountSalesPoints = new HashSet<CrCasAccountSalesPoint>();
            CrMasRenterInformations = new HashSet<CrMasRenterInformation>();
        }

        public string CrMasSupAccountBankCode { get; set; } = null!;
        public string? CrMasSupAccountBankArName { get; set; }
        public string? CrMasSupAccountBankEnName { get; set; }
        public string? CrMasSupAccountBankStatus { get; set; }
        public string? CrMasSupAccountBankReasons { get; set; }

        public virtual ICollection<CrCasAccountBank> CrCasAccountBanks { get; set; }
        public virtual ICollection<CrCasAccountReceipt> CrCasAccountReceipts { get; set; }
        public virtual ICollection<CrCasAccountSalesPoint> CrCasAccountSalesPoints { get; set; }
        public virtual ICollection<CrMasRenterInformation> CrMasRenterInformations { get; set; }
    }
}
