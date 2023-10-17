using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasBeneficiary
    {
        public CrCasBeneficiary()
        {
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
        }

        public string CrCasBeneficiaryCode { get; set; } = null!;
        public string CrCasBeneficiaryLessorCode { get; set; } = null!;
        public string? CrCasBeneficiaryCommercialNo { get; set; }
        public string? CrCasBeneficiarySector { get; set; }
        public string? CrCasBeneficiaryArName { get; set; }
        public string? CrCasBeneficiaryEnName { get; set; }
        public string? CrCasBeneficiaryStatus { get; set; }
        public string? CrCasBeneficiaryReasons { get; set; }

        public virtual CrMasLessorInformation CrCasBeneficiaryLessorCodeNavigation { get; set; } = null!;
        public virtual CrMasSupRenterSector? CrCasBeneficiarySectorNavigation { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
    }
}
