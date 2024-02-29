using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasOwner
    {
        public CrCasOwner()
        {
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
        }

        public string CrCasOwnersCode { get; set; } = null!;
        public string CrCasOwnersLessorCode { get; set; } = null!;
        public string? CrCasOwnersCommercialNo { get; set; }
        public string? CrCasOwnersSector { get; set; }
        public string? CrCasOwnersCountryKey { get; set; }
        public string? CrCasOwnersMobile { get; set; }
        public bool? CrCasOwnersSendContractByWhatsUp { get; set; }
        public bool? CrCasOwnersSendContractByEmail { get; set; }
        public string? CrCasOwnersArName { get; set; }
        public string? CrCasOwnersEnName { get; set; }
        public string? CrCasOwnersStatus { get; set; }
        public string? CrCasOwnersReasons { get; set; }

        public virtual CrMasLessorInformation CrCasOwnersLessorCodeNavigation { get; set; } = null!;
        public virtual CrMasSupRenterSector? CrCasOwnersSectorNavigation { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
    }
}
