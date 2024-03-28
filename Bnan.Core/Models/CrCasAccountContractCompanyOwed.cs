using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasAccountContractCompanyOwed
    {
        public string CrCasAccountContractCompanyOwedNo { get; set; } = null!;
        public string? CrCasAccountContractCompanyOwedCompanyCode { get; set; }
        public string? CrCasAccountContractCompanyOwedContractCom { get; set; }
        public DateTime? CrCasAccountContractCompanyOwedDate { get; set; }
        public string? CrCasAccountContractCompanyOwedType { get; set; }
        public decimal? CrCasAccountContractCompanyOwedPercentage { get; set; }
        public decimal? CrCasAccountContractCompanyOwedValue { get; set; }
        public decimal? CrCasAccountContractCompanyOwedDiscountPercentage { get; set; }
        public decimal? CrCasAccountContractCompanyOwedDaliayValue { get; set; }
        public decimal? CrCasAccountContractCompanyOwedContractValue { get; set; }
        public decimal? CrCasAccountContractCompanyOwedBeforeAmount { get; set; }
        public decimal? CrCasAccountContractCompanyOwedAfterAmount { get; set; }
        public decimal? CrCasAccountContractCompanyOwedTaxPercentage { get; set; }
        public decimal? CrCasAccountContractCompanyOwedTaxValue { get; set; }
        public decimal? CrCasAccountContractCompanyOwedAmount { get; set; }
        public bool? CrCasAccountContractCompanyOwedAccrualStatus { get; set; }
        public DateTime? CrCasAccountContractCompanyOwedDatePayment { get; set; }
        public string? CrCasAccountContractCompanyOwedAccrualPaymentNo { get; set; }

        public virtual CrCasSysAdministrativeProcedure? CrCasAccountContractCompanyOwedAccrualPaymentNoNavigation { get; set; }
        public virtual CrMasLessorInformation? CrCasAccountContractCompanyOwedCompanyCodeNavigation { get; set; }
        public virtual CrMasContractCompany? CrCasAccountContractCompanyOwedContractComNavigation { get; set; }
    }
}
