using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupPostCity
    {
        public CrMasSupPostCity()
        {
            CrCasBranchPosts = new HashSet<CrCasBranchPost>();
        }

        public string CrMasSupPostCityCode { get; set; } = null!;
        public string? CrMasSupPostCityGroupCode { get; set; }
        public string? CrMasSupPostCityRegionsCode { get; set; }
        public string? CrMasSupPostCityArName { get; set; }
        public string? CrMasSupPostCityEnName { get; set; }
        public string? CrMasSupPostCityConcatenateArName { get; set; }
        public string? CrMasSupPostCityConcatenateEnName { get; set; }
        public string? CrMasSupPostCityLocation { get; set; }
        public string? CrMasSupPostCityLongitude { get; set; }
        public string? CrMasSupPostCityLatitude { get; set; }
        public int? CrMasSupPostCityCounter { get; set; }
        public string? CrMasSupPostCityRegionsStatus { get; set; }
        public string? CrMasSupPostCityStatus { get; set; }
        public string? CrMasSupPostCityReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupPostCityGroupCodeNavigation { get; set; }
        public virtual CrMasSupPostRegion? CrMasSupPostCityRegionsCodeNavigation { get; set; }
        public virtual ICollection<CrCasBranchPost> CrCasBranchPosts { get; set; }
    }
}
