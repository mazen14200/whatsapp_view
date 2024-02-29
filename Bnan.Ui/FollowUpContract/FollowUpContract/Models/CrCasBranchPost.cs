using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasBranchPost
    {
        public string CrCasBranchPostLessor { get; set; } = null!;
        public string CrCasBranchPostBranch { get; set; } = null!;
        public string? CrCasBranchPostShortCode { get; set; }
        public string? CrCasBranchPostRegions { get; set; }
        public string? CrCasBranchPostCity { get; set; }
        public string? CrCasBranchPostArDistrict { get; set; }
        public string? CrCasBranchPostEnDistrict { get; set; }
        public string? CrCasBranchPostArStreet { get; set; }
        public string? CrCasBranchPostEnStreet { get; set; }
        public string? CrCasBranchPostBuilding { get; set; }
        public string? CrCasBranchPostUnitNo { get; set; }
        public string? CrCasBranchPostZipCode { get; set; }
        public string? CrCasBranchPostAdditionalNumbers { get; set; }
        public string? CrCasBranchPostArConcatenate { get; set; }
        public string? CrCasBranchPostEnConcatenate { get; set; }
        public string? CrCasBranchPostArShortConcatenate { get; set; }
        public string? CrCasBranchPostEnShortConcatenate { get; set; }
        public string? CrCasBranchPostArMailManual { get; set; }
        public string? CrCasBranchPostEnMailManual { get; set; }
        public DateTime? CrCasBranchPostUpDateMail { get; set; }
        public string? CrCasBranchPostLocation { get; set; }
        public decimal? CrCasBranchPostLongitude { get; set; }
        public decimal? CrCasBranchPostLatitude { get; set; }
        public string? CrCasBranchPostStatus { get; set; }
        public string? CrCasBranchPostReasons { get; set; }

        public virtual CrMasSupPostCity? CrCasBranchPostCityNavigation { get; set; }
        public virtual CrCasBranchInformation CrCasBranchPostNavigation { get; set; } = null!;
        public virtual CrMasSupPostRegion? CrCasBranchPostRegionsNavigation { get; set; }
    }
}
