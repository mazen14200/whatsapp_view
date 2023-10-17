using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarColor
    {
        public CrMasSupCarColor()
        {
            CrCasCarInformationCrCasCarInformationFloorColorNavigations = new HashSet<CrCasCarInformation>();
            CrCasCarInformationCrCasCarInformationMainColorNavigations = new HashSet<CrCasCarInformation>();
            CrCasCarInformationCrCasCarInformationSeatColorNavigations = new HashSet<CrCasCarInformation>();
            CrCasCarInformationCrCasCarInformationSecondaryColorNavigations = new HashSet<CrCasCarInformation>();
        }

        public string CrMasSupCarColorCode { get; set; } = null!;
        public string? CrMasSupCarColorArName { get; set; }
        public string? CrMasSupCarColorEnName { get; set; }
        public int? CrMasSupCarColorCounter { get; set; }
        public string? CrMasSupCarColorImage { get; set; }
        public string? CrMasSupCarColorStatus { get; set; }
        public string? CrMasSupCarColorReasons { get; set; }

        public virtual ICollection<CrCasCarInformation> CrCasCarInformationCrCasCarInformationFloorColorNavigations { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformationCrCasCarInformationMainColorNavigations { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformationCrCasCarInformationSeatColorNavigations { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformationCrCasCarInformationSecondaryColorNavigations { get; set; }
    }
}
