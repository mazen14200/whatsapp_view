using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupMembershipChoice
    {
        public CrMasSupMembershipChoice()
        {
            CrCasLessorMemberships = new HashSet<CrCasLessorMembership>();
        }

        public string CrMasSupMembershipChoiceCode { get; set; } = null!;
        public string? CrMasSupMembershipChoiceGroup { get; set; }
        public string? CrMasSupMembershipChoiceStetment { get; set; }

        public virtual ICollection<CrCasLessorMembership> CrCasLessorMemberships { get; set; }
    }
}
