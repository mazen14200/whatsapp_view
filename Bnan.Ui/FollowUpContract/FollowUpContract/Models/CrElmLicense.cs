using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrElmLicense
    {
        public string CrElmLicensePersonId { get; set; } = null!;
        public string? CrElmLicenseNo { get; set; }
        public string? CrElmLicenseArName { get; set; }
        public string? CrElmLicenseEnName { get; set; }
        public DateTime? CrElmLicenseIssuedDate { get; set; }
        public DateTime? CrElmLicenseExpiryDate { get; set; }
    }
}
