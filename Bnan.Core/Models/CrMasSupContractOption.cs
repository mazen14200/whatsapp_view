using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupContractOption
    {
        public string CrMasSupContractOptionsCode { get; set; } = null!;
        public string? CrMasSupContractOptionsGroup { get; set; }
        public string? CrMasSupContractOptionsArName { get; set; }
        public string? CrMasSupContractOptionsEnName { get; set; }
        public string? CrMasSupContractOptionsAcceptImage { get; set; }
        public string? CrMasSupContractOptionsRejectImage { get; set; }
        public string? CrMasSupContractOptionsBlockImage { get; set; }
        public string? CrMasSupContractOptionsStatus { get; set; }
        public string? CrMasSupContractOptionsReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupContractOptionsGroupNavigation { get; set; }
    }
}
