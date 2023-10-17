using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasBranchInformation
    {
        public CrCasBranchInformation()
        {
            CrCasAccountSalesPoints = new HashSet<CrCasAccountSalesPoint>();
            CrCasBranchDocuments = new HashSet<CrCasBranchDocument>();
            CrCasCarDocumentsMaintenances = new HashSet<CrCasCarDocumentsMaintenance>();
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
            CrCasSysAdministrativeProcedures = new HashSet<CrCasSysAdministrativeProcedure>();
            CrMasUserBranchValidities = new HashSet<CrMasUserBranchValidity>();
        }

        public string CrCasBranchInformationLessor { get; set; } = null!;
        public string CrCasBranchInformationCode { get; set; } = null!;
        public string? CrCasBranchInformationArTga { get; set; }
        public string? CrCasBranchInformationEnTga { get; set; }
        public string? CrCasBranchInformationArName { get; set; }
        public string? CrCasBranchInformationArShortName { get; set; }
        public string? CrCasBranchInformationEnName { get; set; }
        public string? CrCasBranchInformationEnShortName { get; set; }
        public string? CrCasBranchInformationGovernmentNo { get; set; }
        public string? CrCasBranchInformationTaxNo { get; set; }
        public string? CrCasBranchInformationDirectorArName { get; set; }
        public string? CrCasBranchInformationDirectorEnName { get; set; }
        public string? CrMasBranchInformationTeleKey { get; set; }
        public string? CrCasBranchInformationTelephone { get; set; }
        public string? CrMasBranchInformationMobileKey { get; set; }
        public string? CrCasBranchInformationMobile { get; set; }
        public string? CrCasBranchInformationDirectorSignature { get; set; }
        public decimal? CrCasBranchInformationTotalBalance { get; set; }
        public decimal? CrCasBranchInformationReservedBalance { get; set; }
        public decimal? CrCasBranchInformationAvailableBalance { get; set; }
        public string? CrCasBranchInformationStatus { get; set; }
        public string? CrCasBranchInformationReasons { get; set; }

        public virtual CrMasLessorInformation CrCasBranchInformationLessorNavigation { get; set; } = null!;
        public virtual CrCasBranchPost CrCasBranchPost { get; set; } = null!;
        public virtual ICollection<CrCasAccountSalesPoint> CrCasAccountSalesPoints { get; set; }
        public virtual ICollection<CrCasBranchDocument> CrCasBranchDocuments { get; set; }
        public virtual ICollection<CrCasCarDocumentsMaintenance> CrCasCarDocumentsMaintenances { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
        public virtual ICollection<CrCasSysAdministrativeProcedure> CrCasSysAdministrativeProcedures { get; set; }
        public virtual ICollection<CrMasUserBranchValidity> CrMasUserBranchValidities { get; set; }
    }
}
