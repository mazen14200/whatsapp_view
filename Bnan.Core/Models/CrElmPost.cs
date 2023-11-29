using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrElmPost
    {
        public string? CrElmPostCode { get; set; }
        public string? CrElmPostRegionsArName { get; set; }
        public string? CrElmPostRegionsEnName { get; set; }
        public string? CrElmPostCityArName { get; set; }
        public string? CrElmPostCityEnName { get; set; }
        public string? CrElmPostDistrictArName { get; set; }
        public string? CrElmPostDistrictEnName { get; set; }
        public string? CrElmPostStreetArName { get; set; }
        public string? CrElmPostStreetEnName { get; set; }
        public int? CrElmPostBuildingNo { get; set; }
        public string? CrElmPostUnitNo { get; set; }
        public int? CrElmPostZipCode { get; set; }
        public int? CrElmPostAdditionalNo { get; set; }
    }
}
