using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarRegistration
    {
        public CrMasSupCarRegistration()
        {
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
        }

        public string CrMasSupCarRegistrationCode { get; set; } = null!;
        public string? CrMasSupCarRegistrationArName { get; set; }
        public string? CrMasSupCarRegistrationEnName { get; set; }
        public string? CrMasSupCarRegistrationStatus { get; set; }
        public string? CrMasSupCarRegistrationReasons { get; set; }

        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
    }
}
