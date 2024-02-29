using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasRenterContractAdvantage
    {
        public string CrCasRenterContractAdvantagesNo { get; set; } = null!;
        public string CrCasRenterContractAdvantagesCode { get; set; } = null!;
        public decimal? CrCasContractAdvantagesValue { get; set; }

        public virtual CrMasSupCarAdvantage CrCasRenterContractAdvantagesCodeNavigation { get; set; } = null!;
    }
}
