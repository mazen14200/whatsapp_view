using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupRenterMembership
    {
        public CrMasSupRenterMembership()
        {
            CrCasLessorMemberships = new HashSet<CrCasLessorMembership>();
            CrCasRenterContractStatistics = new HashSet<CrCasRenterContractStatistic>();
            CrCasRenterLessors = new HashSet<CrCasRenterLessor>();
        }

        public string CrMasSupRenterMembershipCode { get; set; } = null!;
        public string? CrMasSupRenterMembershipGroupCode { get; set; }
        public string? CrMasSupRenterMembershipArName { get; set; }
        public string? CrMasSupRenterMembershipEnName { get; set; }
        public string? CrMasSupRenterMembershipAcceptPicture { get; set; }
        public string? CrMasSupRenterMembershipRejectPicture { get; set; }
        public string? CrMasSupRenterMembershipStatus { get; set; }
        public string? CrMasSupRenterMembershipReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupRenterMembershipGroupCodeNavigation { get; set; }
        public virtual ICollection<CrCasLessorMembership> CrCasLessorMemberships { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatistics { get; set; }
        public virtual ICollection<CrCasRenterLessor> CrCasRenterLessors { get; set; }
    }
}
