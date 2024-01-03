using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarAdvantage
    {
        public CrMasSupCarAdvantage()
        {
            CrCasCarAdvantages = new HashSet<CrCasCarAdvantage>();
            CrCasPriceCarAdvantages = new HashSet<CrCasPriceCarAdvantage>();
            CrCasRenterContractAdvantages = new HashSet<CrCasRenterContractAdvantage>();
        }

        public string CrMasSupCarAdvantagesCode { get; set; } = null!;
        public string? CrMasSupCarAdvantagesArName { get; set; }
        public string? CrMasSupCarAdvantagesEnName { get; set; }
        public string? CrMasSupCarAdvantagesImage { get; set; }
        public string? CrMasSupCarAdvantagesStatus { get; set; }
        public string? CrMasSupCarAdvantagesReasons { get; set; }

        public virtual ICollection<CrCasCarAdvantage> CrCasCarAdvantages { get; set; }
        public virtual ICollection<CrCasPriceCarAdvantage> CrCasPriceCarAdvantages { get; set; }
        public virtual ICollection<CrCasRenterContractAdvantage> CrCasRenterContractAdvantages { get; set; }
    }
}
