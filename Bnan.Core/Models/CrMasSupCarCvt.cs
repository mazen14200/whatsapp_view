using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarCvt
    {
        public CrMasSupCarCvt()
        {
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
        }

        public string CrMasSupCarCvtCode { get; set; } = null!;
        public string? CrMasSupCarCvtArName { get; set; }
        public string? CrMasSupCarCvtEnName { get; set; }
        public string? CrMasSupCarCvtImage { get; set; }
        public string? CrMasSupCarCvtStatus { get; set; }
        public string? CrMasSupCarCvtReasons { get; set; }

        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
    }
}
