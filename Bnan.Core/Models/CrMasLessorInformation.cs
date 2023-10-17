using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasLessorInformation
    {
        public CrMasLessorInformation()
        {
            CrCasAccountBanks = new HashSet<CrCasAccountBank>();
            CrCasBeneficiaries = new HashSet<CrCasBeneficiary>();
            CrCasBranchDocuments = new HashSet<CrCasBranchDocument>();
            CrCasBranchInformations = new HashSet<CrCasBranchInformation>();
            CrCasCarAdvantages = new HashSet<CrCasCarAdvantage>();
            CrCasLessorMechanisms = new HashSet<CrCasLessorMechanism>();
            CrCasLessorMemberships = new HashSet<CrCasLessorMembership>();
            CrCasOwners = new HashSet<CrCasOwner>();
            CrCasRenterPrivateDriverInformations = new HashSet<CrCasRenterPrivateDriverInformation>();
            CrMasContractCompanies = new HashSet<CrMasContractCompany>();
            CrMasUserInformations = new HashSet<CrMasUserInformation>();
            CrMasUserLogins = new HashSet<CrMasUserLogin>();
        }

        public string CrMasLessorInformationCode { get; set; } = null!;
        public string? CrMasLessorInformationArLongName { get; set; }
        public string? CrMasLessorInformationArShortName { get; set; }
        public string? CrMasLessorInformationEnLongName { get; set; }
        public string? CrMasLessorInformationEnShortName { get; set; }
        public string? CrMasLessorInformationClassification { get; set; }
        public string? CrMasLessorInformationLocation { get; set; }
        public string? CrMasLessorInformationGovernmentNo { get; set; }
        public string? CrMasLessorInformationTaxNo { get; set; }
        public string? CrMasLessorInformationDirectorArName { get; set; }
        public string? CrMasLessorInformationDirectorEnName { get; set; }
        public string? CrMasLessorInformationCommunicationMobileKey { get; set; }
        public string? CrMasLessorInformationCommunicationMobile { get; set; }
        public string? CrMasLessorInformationCallFreeKey { get; set; }
        public string? CrMasLessorInformationCallFree { get; set; }
        public string? CrMasLessorInformationEmail { get; set; }
        public string? CrMasLessorInformationTwiter { get; set; }
        public string? CrMasLessorInformationFaceBook { get; set; }
        public string? CrMasLessorInformationInstagram { get; set; }
        public string? CrMasLessorInformationAccount { get; set; }
        public string? CrMasLessorInformationContWhatsappKey { get; set; }
        public string? CrMasLessorInformationContWhatsapp { get; set; }
        public string? CrMasLessorInformationContEmail { get; set; }
        public string? CrMasLessorInformationStatus { get; set; }
        public string? CrMasLessorInformationReasons { get; set; }

        public virtual CrCasLessorClassification? CrMasLessorInformationClassificationNavigation { get; set; }
        public virtual CrMasLessorImage CrMasLessorImage { get; set; } = null!;
        public virtual ICollection<CrCasAccountBank> CrCasAccountBanks { get; set; }
        public virtual ICollection<CrCasBeneficiary> CrCasBeneficiaries { get; set; }
        public virtual ICollection<CrCasBranchDocument> CrCasBranchDocuments { get; set; }
        public virtual ICollection<CrCasBranchInformation> CrCasBranchInformations { get; set; }
        public virtual ICollection<CrCasCarAdvantage> CrCasCarAdvantages { get; set; }
        public virtual ICollection<CrCasLessorMechanism> CrCasLessorMechanisms { get; set; }
        public virtual ICollection<CrCasLessorMembership> CrCasLessorMemberships { get; set; }
        public virtual ICollection<CrCasOwner> CrCasOwners { get; set; }
        public virtual ICollection<CrCasRenterPrivateDriverInformation> CrCasRenterPrivateDriverInformations { get; set; }
        public virtual ICollection<CrMasContractCompany> CrMasContractCompanies { get; set; }
        public virtual ICollection<CrMasUserInformation> CrMasUserInformations { get; set; }
        public virtual ICollection<CrMasUserLogin> CrMasUserLogins { get; set; }
    }
}
