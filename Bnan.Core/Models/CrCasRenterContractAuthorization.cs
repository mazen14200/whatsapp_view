using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasRenterContractAuthorization
    {
        public string CrCasRenterContractAuthorizationContractNo { get; set; } = null!;
        public string? CrCasRenterContractAuthorizationLessor { get; set; }
        public bool? CrCasRenterContractAuthorizationType { get; set; }
        public string? CrCasRenterContractAuthorizationNo { get; set; }
        public DateTime? CrCasRenterContractAuthorizationStartDate { get; set; }
        public int? CrCasRenterContractAuthorizationDaysNo { get; set; }
        public decimal? CrCasRenterContractAuthorizationValue { get; set; }
        public DateTime? CrCasRenterContractAuthorizationEndDate { get; set; }
        public bool? CrCasRenterContractAuthorizationAction { get; set; }
        public string? CrCasRenterContractAuthorizationStatus { get; set; }

        public virtual CrMasLessorInformation? CrCasRenterContractAuthorizationLessorNavigation { get; set; }
    }
}
