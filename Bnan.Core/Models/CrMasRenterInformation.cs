using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasRenterInformation
    {
        public string CrMasRenterInformationId { get; set; } = null!;
        public int? CrMasRenterInformationCopyId { get; set; }
        public string? CrMasRenterInformationIdtrype { get; set; }
        public bool? CrMasRenterInformationIdstatus { get; set; }
        public string? CrMasRenterInformationSector { get; set; }
        public string? CrMasRenterInformationTaxNo { get; set; }
        public string? CrMasRenterInformationArName { get; set; }
        public string? CrMasRenterInformationEnName { get; set; }
        public DateTime? CrMasRenterInformationBirthDate { get; set; }
        public DateTime? CrMasRenterInformationIssueIdDate { get; set; }
        public DateTime? CrMasRenterInformationExpiryIdDate { get; set; }
        public string? CrMasRenterInformationDrivingLicenseType { get; set; }
        public DateTime? CrMasRenterInformationDrivingLicenseDate { get; set; }
        public DateTime? CrMasRenterInformationExpiryDrivingLicenseDate { get; set; }
        public string? CrMasRenterInformationLangue { get; set; }
        public string? CrMasRenterInformationWorkplaceSubscription { get; set; }
        public string? CrMasRenterInformationNationality { get; set; }
        public string? CrMasRenterInformationGender { get; set; }
        public string? CrMasRenterInformationJobs { get; set; }
        public string? CrMasRenterInformationMobile { get; set; }
        public string? CrMasRenterInformationEmail { get; set; }
        public string? CrMasRenterInformationIban { get; set; }
        public string? CrMasRenterInformationBank { get; set; }
        public DateTime? CrMasRenterInformationUpDatePersonalData { get; set; }
        public DateTime? CrMasRenterInformationUpDateWorkplaceData { get; set; }
        public DateTime? CrMasRenterInformationUpDateLicenseData { get; set; }
        public DateTime? CrMasRenterInformationDateFirstInteraction { get; set; }
        public DateTime? CrMasRenterInformationDateLastInteraction { get; set; }
        public DateTime? CrMasRenterInformationDateLastContract { get; set; }
        public int? CrMasRenterInformationEvaluationCount { get; set; }
        public int? CrMasRenterInformationDays { get; set; }
        public int? CrMasRenterInformationTraveledDistance { get; set; }
        public decimal? CrMasRenterInformationValue { get; set; }
        public decimal? CrMasRenterInformationEvaluationTotal { get; set; }
        public decimal? CrMasRenterInformationEvaluationValue { get; set; }
        public string? CrMasRenterInformationSignature { get; set; }
        public string? CrMasRenterInformationRenterIdImage { get; set; }
        public string? CrMasRenterInformationRenterLicenseImage { get; set; }
        public string? CrMasRenterInformationStatus { get; set; }
        public string? CrMasRenterInformationReasons { get; set; }

        public virtual CrMasSupAccountBank? CrMasRenterInformationBankNavigation { get; set; }
        public virtual CrMasSupRenterDrivingLicense? CrMasRenterInformationDrivingLicenseTypeNavigation { get; set; }
        public virtual CrMasSupRenterGender? CrMasRenterInformationGenderNavigation { get; set; }
        public virtual CrMasSupRenterIdtype? CrMasRenterInformationIdtrypeNavigation { get; set; }
        public virtual CrMasSupRenterProfession? CrMasRenterInformationJobsNavigation { get; set; }
        public virtual CrMasSupRenterNationality? CrMasRenterInformationNationalityNavigation { get; set; }
        public virtual CrMasSupRenterSector? CrMasRenterInformationSectorNavigation { get; set; }
        public virtual CrMasSupRenterEmployer? CrMasRenterInformationWorkplaceSubscriptionNavigation { get; set; }
    }
}
