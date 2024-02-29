using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasContractCompanyDetailed
    {
        public string CrMasContractCompanyDetailedNo { get; set; } = null!;
        public int CrMasContractCompanyDetailedSer { get; set; }
        public decimal? CrMasContractCompanyDetailedFromPrice { get; set; }
        public decimal? CrMasContractCompanyDetailedToPrice { get; set; }
        public decimal? CrMasContractCompanyDetailedValue { get; set; }

        public virtual CrMasContractCompany CrMasContractCompanyDetailedNoNavigation { get; set; } = null!;
    }
}
