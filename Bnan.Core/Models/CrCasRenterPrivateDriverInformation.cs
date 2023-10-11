using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bnan.Core.Models
{
    public partial class CrCasRenterPrivateDriverInformation
    {
        [NotMapped]
        public string RenterDriverTypeEn { get; set; }
        [NotMapped]
        public string RenterDriverTypeAr { get; set; }

        [NotMapped]
        public string RenterDriverLicenseTypeEn { get; set; }
        [NotMapped]
        public string RenterDriverLicenseTypeAr { get; set; }

        [NotMapped]
        public string countryCode { get; set; }
        [NotMapped]
        public string callingKey { get; set; }

        [NotMapped]
        public string RenterDriverNationalityEn { get; set; }
        [NotMapped]
        public string RenterDriverNationalityAr { get; set; }

        [NotMapped]
        public string RenterDriverGenderEn { get; set; }
        [NotMapped]
        public string RenterDriverGenderAr { get; set; }
        public string CrCasRenterPrivateDriverInformationId { get; set; } = null!;
        public string CrCasRenterPrivateDriverInformationLessor { get; set; } = null!;
        public string? CrCasRenterPrivateDriverInformationIdtrype { get; set; }
        public string? CrCasRenterPrivateDriverInformationArName { get; set; }
        public string? CrCasRenterPrivateDriverInformationEnName { get; set; }
        public DateTime? CrCasRenterPrivateDriverInformationBirthDate { get; set; }
        public DateTime? CrCasRenterPrivateDriverInformationIssueIdDate { get; set; }
        public DateTime? CrCasRenterPrivateDriverInformationExpiryIdDate { get; set; }
        public string? CrCasRenterPrivateDriverInformationLicenseNo { get; set; }
        public string? CrCasRenterPrivateDriverInformationLicenseType { get; set; }
        public DateTime? CrCasRenterPrivateDriverInformationLicenseDate { get; set; }
        public DateTime? CrCasRenterPrivateDriverInformationLicenseExpiry { get; set; }
        public string? CrCasRenterPrivateDriverInformationNationality { get; set; }
        public string? CrCasRenterPrivateDriverInformationGender { get; set; }
        public string? CrCasRenterPrivateDriverInformationMobile { get; set; }
        public string? CrCasRenterPrivateDriverInformationEmail { get; set; }
        public DateTime? CrCasRenterPrivateDriverInformationLastContract { get; set; }
        public int? CrCasRenterPrivateDriverInformationContractCount { get; set; }
        public int? CrCasRenterPrivateDriverInformationDaysCount { get; set; }
        public int? CrCasRenterPrivateDriverInformationTraveledDistance { get; set; }
        public decimal? CrCasRenterPrivateDriverInformationEvaluationTotal { get; set; }
        public decimal? CrCasRenterPrivateDriverInformationEvaluationValue { get; set; }
        public string? CrCasRenterPrivateDriverInformationSignature { get; set; }
        public string? CrCasRenterPrivateDriverInformationIdImage { get; set; }
        public string? CrCasRenterPrivateDriverInformationLicenseImage { get; set; }
        public string? CrCasRenterPrivateDriverInformationStatus { get; set; }
        public string? CrCasRenterPrivateDriverInformationReasons { get; set; }

        public virtual CrMasSupRenterGender? CrCasRenterPrivateDriverInformationGenderNavigation { get; set; }
        public virtual CrMasSupRenterIdtype? CrCasRenterPrivateDriverInformationIdtrypeNavigation { get; set; }
        public virtual CrMasLessorInformation CrCasRenterPrivateDriverInformationLessorNavigation { get; set; } = null!;
        public virtual CrMasSupRenterDrivingLicense? CrCasRenterPrivateDriverInformationLicenseTypeNavigation { get; set; }
        public virtual CrMasSupRenterNationality? CrCasRenterPrivateDriverInformationNationalityNavigation { get; set; }
    }
}
