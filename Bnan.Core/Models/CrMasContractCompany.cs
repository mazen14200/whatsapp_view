using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasContractCompany
    {
        public CrMasContractCompany()
        {
            CrCasAccountContractCompanyOweds = new HashSet<CrCasAccountContractCompanyOwed>();
            CrMasContractCompanyDetaileds = new HashSet<CrMasContractCompanyDetailed>();
        }

        public string CrMasContractCompanyNo { get; set; } = null!;
        public string? CrMasContractCompanyYear { get; set; }
        public string? CrMasContractCompanySector { get; set; }
        public string? CrMasContractCompanyProcedures { get; set; }
        public string? CrMasContractCompanyProceduresClassification { get; set; }
        public string? CrMasContractCompanyLessor { get; set; }
        public string? CrMasContractCompanyNumber { get; set; }
        public DateTime? CrMasContractCompanyDate { get; set; }
        public DateTime? CrMasContractCompanyStartDate { get; set; }
        public DateTime? CrMasContractCompanyEndDate { get; set; }
        public DateTime? CrMasContractCompanyAboutToExpire { get; set; }
        public string? CrMasContractCompanyActivation { get; set; }
        public decimal? CrMasContractCompanyAnnualFees { get; set; }
        public decimal? CrMasContractCompanyDiscountRate { get; set; }
        public decimal? CrMasContractCompanyTaxRate { get; set; }
        public string? CrMasContractCompanyUserId { get; set; }
        public string? CrMasContractCompanyUserPassword { get; set; }
        public string? CrMasContractCompanyImage { get; set; }
        public string? CrMasContractCompanyStatus { get; set; }
        public string? CrMasContractCompanyReasons { get; set; }

        public virtual CrMasLessorInformation? CrMasContractCompanyLessorNavigation { get; set; }
        public virtual CrMasSysProcedure? CrMasContractCompanyProceduresNavigation { get; set; }
        public virtual CrMasSupRenterSector? CrMasContractCompanySectorNavigation { get; set; }
        public virtual ICollection<CrCasAccountContractCompanyOwed> CrCasAccountContractCompanyOweds { get; set; }
        public virtual ICollection<CrMasContractCompanyDetailed> CrMasContractCompanyDetaileds { get; set; }
    }
}
