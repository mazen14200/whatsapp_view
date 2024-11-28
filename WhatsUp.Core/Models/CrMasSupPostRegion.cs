using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupPostRegion
    {
        public CrMasSupPostRegion()
        {

        }

        public string CrMasSupPostRegionsCode { get; set; } = null!;
        public string? CrMasSupPostRegionsArName { get; set; }
        public string? CrMasSupPostRegionsEnName { get; set; }
        public string? CrMasSupPostRegionsLocation { get; set; }
        public decimal? CrMasSupPostRegionsLongitude { get; set; }
        public decimal? CrMasSupPostRegionsLatitude { get; set; }
        public string? CrMasSupPostRegionsStatus { get; set; }
        public string? CrMasSupPostRegionsReasons { get; set; }


    }
}
