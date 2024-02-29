using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasRenterPost
    {
        public string CrMasRenterPostCode { get; set; } = null!;
        public string? CrMasRenterPostShortCode { get; set; }
        public string? CrMasRenterPostRegions { get; set; }
        public string? CrMasRenterPostCity { get; set; }
        public string? CrMasRenterPostArDistrict { get; set; }
        public string? CrMasRenterPostEnDistrict { get; set; }
        public string? CrMasRenterPostArStreet { get; set; }
        public string? CrMasRenterPostEnStreet { get; set; }
        public string? CrMasRenterPostBuilding { get; set; }
        public string? CrMasRenterPostUnitNo { get; set; }
        public string? CrMasRenterPostZipCode { get; set; }
        public string? CrMasRenterPostAdditionalNumbers { get; set; }
        public string? CrMasRenterPostArConcatenate { get; set; }
        public string? CrMasRenterPostEnConcatenate { get; set; }
        public string? CrMasRenterPostArShortConcatenate { get; set; }
        public string? CrMasRenterPostEnShortConcatenate { get; set; }
        public string? CrMasRenterPostArMailManual { get; set; }
        public string? CrMasRenterPostEnMailManual { get; set; }
        public DateTime? CrMasRenterPostUpDatePost { get; set; }
        public string? CrMasRenterPostStatus { get; set; }
        public string? CrMasRenterPostReasons { get; set; }

        public virtual CrMasSupPostCity? CrMasRenterPostCityNavigation { get; set; }
        public virtual CrMasRenterInformation CrMasRenterPostCodeNavigation { get; set; } = null!;
        public virtual CrMasSupPostRegion? CrMasRenterPostRegionsNavigation { get; set; }
    }
}
