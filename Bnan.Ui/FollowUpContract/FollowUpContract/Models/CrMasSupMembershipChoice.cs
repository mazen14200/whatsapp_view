using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CRMasSysProbabilityMembership
    {
        public CRMasSysProbabilityMembership()
        {
            CrCasLessorMemberships = new HashSet<CrCasLessorMembership>();
        }

        public string CRMasSysProbabilityMembershipCode { get; set; } = null!;
        public string? CRMasSysProbabilityMembershipGroup { get; set; }
        public string? CRMasSysProbabilityMembershipStetment { get; set; }

        public virtual ICollection<CrCasLessorMembership> CrCasLessorMemberships { get; set; }
    }
}
