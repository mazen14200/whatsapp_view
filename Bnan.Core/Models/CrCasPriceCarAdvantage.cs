using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasPriceCarAdvantage
    {
        public string CrCasPriceCarAdvantagesNo { get; set; } = null!;
        public string CrCasPriceCarAdvantagesCode { get; set; } = null!;
        public decimal? CrCasPriceCarAdvantagesValue { get; set; }

        public virtual CrMasSupCarAdvantage CrCasPriceCarAdvantagesCodeNavigation { get; set; } = null!;
        public virtual CrCasPriceCarBasic CrCasPriceCarAdvantagesNoNavigation { get; set; } = null!;
    }
}
