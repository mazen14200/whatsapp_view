using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupRenterDrivingLicense
    {
        public CrMasSupRenterDrivingLicense()
        {
            CrCasRenterPrivateDriverInformations = new HashSet<CrCasRenterPrivateDriverInformation>();
        }

        public string CrMasSupRenterDrivingLicenseCode { get; set; } = null!;
        public string? CrMasSupRenterDrivingLicenseArName { get; set; }
        public string? CrMasSupRenterDrivingLicenseEnName { get; set; }
        public string? CrMasSupRenterDrivingLicenseStatus { get; set; }
        public string? CrMasSupRenterDrivingLicenseReasons { get; set; }

        public virtual ICollection<CrCasRenterPrivateDriverInformation> CrCasRenterPrivateDriverInformations { get; set; }
    }
}
