using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasLessorMembership
    {
        public string CrCasLessorMembershipConditions { get; set; } = null!;
        public string CrCasLessorMembershipConditionsLessor { get; set; } = null!;
        public bool? CrCasLessorMembershipConditionsActivate { get; set; }
        public decimal? CrCasLessorMembershipConditionsAmount { get; set; }
        public string? CrCasLessorMembershipConditionsLink1 { get; set; }
        public int? CrCasLessorMembershipConditionsKm { get; set; }
        public string? CrCasLessorMembershipConditionsLink2 { get; set; }
        public int? CrCasLessorMembershipConditionsContractNo { get; set; }
        public bool? CrCasLessorMembershipConditionsIsCorrecte { get; set; }
        public string? CrCasLessorMembershipConditionsGroup { get; set; }
        public string? CrCasLessorMembershipConditionsPicture { get; set; }

        public virtual CrMasLessorInformation CrCasLessorMembershipConditionsLessorNavigation { get; set; } = null!;
        public virtual CrMasSupRenterMembership CrCasLessorMembershipConditionsNavigation { get; set; } = null!;
    }
}
