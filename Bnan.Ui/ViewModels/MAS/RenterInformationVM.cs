using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.MAS
{
    public class RenterInformationVM
    {

        public string CrMasRenterInformationId { get; set; } = null!;
        public int? CrMasRenterInformationCopyId { get; set; }
        public string? CrMasRenterInformationIdtype { get; set; }
        public string? CrMasRenterInformationSector { get; set; }
        public string? CrMasRenterInformationTaxNo { get; set; }
       
        [Required(ErrorMessage = "requiredFiled"), MaxLength(110, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasRenterInformationArName { get; set; }

        [Required(ErrorMessage = "requiredFiled"), MaxLength(110, ErrorMessage = "requiredNoLengthFiled30")]
        public string? CrMasRenterInformationEnName { get; set; }
        public DateTime? CrMasRenterInformationBirthDate { get; set; }
        public DateTime? CrMasRenterInformationIssueIdDate { get; set; }
        public DateTime? CrMasRenterInformationExpiryIdDate { get; set; }
        public string? CrMasRenterInformationIssuePlace { get; set; }
        public string? CrMasRenterInformationDrivingLicenseNo { get; set; }
        public string? CrMasRenterInformationDrivingLicenseType { get; set; }
        public DateTime? CrMasRenterInformationDrivingLicenseDate { get; set; }
        public DateTime? CrMasRenterInformationExpiryDrivingLicenseDate { get; set; }
        public string? CrMasRenterInformationCommunicationLanguage { get; set; }
        public string? CrMasRenterInformationProfession { get; set; }
        public string? CrMasRenterInformationNationality { get; set; }
        public string? CrMasRenterInformationGender { get; set; }
        public string? CrMasRenterInformationEmployer { get; set; }
        public string? CrMasRenterInformationCountreyKey { get; set; }
        public string? CrMasRenterInformationMobile { get; set; }
        public string? CrMasRenterInformationEmail { get; set; }
        public string? CrMasRenterInformationBank { get; set; }
        public string? CrMasRenterInformationIban { get; set; }
        public DateTime? CrMasRenterInformationUpDatePersonalData { get; set; }
        public DateTime? CrMasRenterInformationUpDateWorkplaceData { get; set; }
        public DateTime? CrMasRenterInformationUpDateLicenseData { get; set; }
        public string? CrMasRenterInformationSignature { get; set; }
        public string? CrMasRenterInformationRenterIdImage { get; set; }
        public string? CrMasRenterInformationRenterLicenseImage { get; set; }
        public string? CrMasRenterInformationStatus { get; set; }
        public string? CrMasRenterInformationReasons { get; set; }

        public virtual CrMasSupAccountBank? CrMasRenterInformationBankNavigation { get; set; }
        public virtual CrMasSupRenterDrivingLicense? CrMasRenterInformationDrivingLicenseTypeNavigation { get; set; }
        public virtual CrMasSupRenterEmployer? CrMasRenterInformationEmployerNavigation { get; set; }
        public virtual CrMasSupRenterGender? CrMasRenterInformationGenderNavigation { get; set; }
        public virtual CrMasSupRenterIdtype? CrMasRenterInformationIdtypeNavigation { get; set; }
        public virtual CrMasSupRenterNationality? CrMasRenterInformationNationalityNavigation { get; set; }
        public virtual CrMasSupRenterProfession? CrMasRenterInformationProfessionNavigation { get; set; }
        public virtual CrMasSupRenterSector? CrMasRenterInformationSectorNavigation { get; set; }
        public virtual CrMasRenterPost CrMasRenterPost { get; set; } = null!;
    }
}
