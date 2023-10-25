using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasPriceCarAdditional
    {
        public string CrCasPriceCarAdditionalNo { get; set; } = null!;
        public string CrCasPriceCarAdditionalCode { get; set; } = null!;
        public decimal? CrCasPriceCarAdditionalValue { get; set; }

        public virtual CrMasSupContractAdditional CrCasPriceCarAdditionalCodeNavigation { get; set; } = null!;
        public virtual CrCasPriceCarBasic CrCasPriceCarAdditionalNoNavigation { get; set; } = null!;
    }
}
