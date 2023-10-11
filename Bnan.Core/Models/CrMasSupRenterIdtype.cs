using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupRenterIdtype
    {
        public CrMasSupRenterIdtype()
        {
            CrCasRenterPrivateDriverInformations = new HashSet<CrCasRenterPrivateDriverInformation>();
        }

        public string CrMasSupRenterIdtypeCode { get; set; } = null!;
        public string? CrMasSupRenterIdtypeArName { get; set; }
        public string? CrMasSupRenterIdtypeEnName { get; set; }
        public string? CrMasSupRenterIdtypeStatus { get; set; }
        public string? CrMasSupRenterIdtypeReasons { get; set; }

        public virtual ICollection<CrCasRenterPrivateDriverInformation> CrCasRenterPrivateDriverInformations { get; set; }
    }
}
