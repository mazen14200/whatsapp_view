using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupContractAdditional
    {
        public CrMasSupContractAdditional()
        {
            CrCasPriceCarAdditionals = new HashSet<CrCasPriceCarAdditional>();
        }

        public string CrMasSupContractAdditionalCode { get; set; } = null!;
        public string? CrMasSupContractAdditionalGroup { get; set; }
        public string? CrMasSupContractAdditionalArName { get; set; }
        public string? CrMasSupContractAdditionalEnName { get; set; }
        public string? CrMasSupContractAdditionalAcceptImage { get; set; }
        public string? CrMasSupContractAdditionalRejectImage { get; set; }
        public string? CrMasSupContractAdditionalBlockImage { get; set; }
        public string? CrMasSupContractAdditionalStatus { get; set; }
        public string? CrMasSupContractAdditionalReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupContractAdditionalGroupNavigation { get; set; }
        public virtual ICollection<CrCasPriceCarAdditional> CrCasPriceCarAdditionals { get; set; }
    }
}
