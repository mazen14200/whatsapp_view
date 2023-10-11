using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupPostRegion
    {
        public CrMasSupPostRegion()
        {
            CrCasBranchPosts = new HashSet<CrCasBranchPost>();
            CrMasSupPostCities = new HashSet<CrMasSupPostCity>();
        }

        public string CrMasSupPostRegionsCode { get; set; } = null!;
        public string? CrMasSupPostRegionsArName { get; set; }
        public string? CrMasSupPostRegionsEnName { get; set; }
        public string? CrMasSupPostRegionsLocation { get; set; }
        public string? CrMasSupPostRegionsLongitude { get; set; }
        public string? CrMasSupPostRegionsLatitude { get; set; }
        public string? CrMasSupPostRegionsStatus { get; set; }
        public string? CrMasSupPostRegionsReasons { get; set; }

        public virtual ICollection<CrCasBranchPost> CrCasBranchPosts { get; set; }
        public virtual ICollection<CrMasSupPostCity> CrMasSupPostCities { get; set; }
    }
}
