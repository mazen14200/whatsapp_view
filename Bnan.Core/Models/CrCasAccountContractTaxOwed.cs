using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasAccountContractTaxOwed
    {
        public string CrCasAccountContractTaxOwedContractNo { get; set; } = null!;
        public string? CrCasAccountContractTaxOwedLessor { get; set; }
        public decimal? CrCasAccountContractTaxOwedContractValue { get; set; }
        public decimal? CrCasAccountContractTaxOwedPercentage { get; set; }
        public decimal? CrCasAccountContractTaxOwedValue { get; set; }
        public DateTime? CrCasAccountContractTaxOwedDate { get; set; }
        public bool? CrCasAccountContractTaxOwedIsPaid { get; set; }
        public DateTime? CrCasAccountContractTaxOwedPayDate { get; set; }
        public string? CrCasAccountContractTaxOwedUserCode { get; set; }
        public string? CrCasAccountContractTaxOwedPayCode { get; set; }

        public virtual CrMasLessorInformation? CrCasAccountContractTaxOwedLessorNavigation { get; set; }
        public virtual CrCasSysAdministrativeProcedure? CrCasAccountContractTaxOwedPayCodeNavigation { get; set; }
        public virtual CrMasUserInformation? CrCasAccountContractTaxOwedUserCodeNavigation { get; set; }
    }
}
