using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class CarsInforamtionVM
    {
        [Required(ErrorMessage = "requiredFiled")]
        public string CrCasCarInformationSerailNo { get; set; } = null!;
        public string? CrCasCarInformationLessor { get; set; }
        public string? CrCasCarInformationBranch { get; set; }
        public string? CrCasCarInformationLocation { get; set; }
        public string? CrCasCarInformationRegion { get; set; }
        public string? CrCasCarInformationCity { get; set; }
        public string? CrCasCarInformationOwner { get; set; }
        public string? CrCasCarInformationBeneficiary { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasCarInformationRegistration { get; set; }
        public string? CrCasCarInformationBrand { get; set; }
        public string? CrCasCarInformationModel { get; set; }
        public string? CrCasCarInformationCategory { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasCarInformationDistribution { get; set; }
        public string? CrCasCarInformationYear { get; set; }
        public string? CrCasCarInformationCustomsNo { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasCarInformationStructureNo { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasCarInformationMainColor { get; set; }
        public string? CrCasCarInformationSecondaryColor { get; set; }
        public string? CrCasCarInformationSeatColor { get; set; }
        public string? CrCasCarInformationFloorColor { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasCarInformationFuel { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasCarInformationCvt { get; set; }
        public string? CrCasCarInformationPlateArNo { get; set; }
        public string? CrCasCarInformationPlateEnNo { get; set; }
        public string? CrCasCarInformationConcatenateArName { get; set; }
        public string? CrCasCarInformationConcatenateEnName { get; set; }
        public DateTime? CrCasCarInformationJoinedFleetDate { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public int? CrCasCarInformationCurrentMeter { get; set; }
        public DateTime? CrCasCarInformationLastContractDate { get; set; }
        public int? CrCasCarInformationConractCount { get; set; }
        public int? CrCasCarInformationConractDaysNo { get; set; }
        public DateTime? CrCasCarInformationOfferedSaleDate { get; set; }
        public decimal? CrCasCarInformationOfferValueSale { get; set; }
        public string? CrCasCarInformationLastPictures { get; set; }
        public DateTime? CrCasCarInformationSoldDate { get; set; }
        public string? CrCasCarInformationPriceNo { get; set; }
        public bool? CrCasCarInformationDocumentationStatus { get; set; }
        public bool? CrCasCarInformationMaintenanceStatus { get; set; }
        public bool? CrCasCarInformationPriceStatus { get; set; }
        public string? CrCasCarInformationBranchStatus { get; set; }
        public string? CrCasCarInformationOwnerStatus { get; set; }
        public string? CrCasCarInformationForSaleStatus { get; set; }
        public string? CrCasCarInformationStatus { get; set; }
        public string? CrCasCarInformationReasons { get; set; }

        public virtual CrCasBranchInformation? CrCasCarInformation1 { get; set; }
        public virtual CrCasOwner? CrCasCarInformation2 { get; set; }
        public virtual CrMasSupCarBrand? CrCasCarInformationBrandNavigation { get; set; }
        public virtual CrMasSupCarCategory? CrCasCarInformationCategoryNavigation { get; set; }
        public virtual CrMasSupPostCity? CrCasCarInformationCityNavigation { get; set; }
        public virtual CrMasSupCarCvt? CrCasCarInformationCvtNavigation { get; set; }
        public virtual CrMasSupCarDistribution? CrCasCarInformationDistributionNavigation { get; set; }
        public virtual CrMasSupCarColor? CrCasCarInformationFloorColorNavigation { get; set; }
        public virtual CrMasSupCarFuel? CrCasCarInformationFuelNavigation { get; set; }
        public virtual CrMasSupCarColor? CrCasCarInformationMainColorNavigation { get; set; }
        public virtual CrMasSupCarModel? CrCasCarInformationModelNavigation { get; set; }
        public virtual CrCasBeneficiary? CrCasCarInformationNavigation { get; set; }
        public virtual CrMasSupPostRegion? CrCasCarInformationRegionNavigation { get; set; }
        public virtual CrMasSupCarRegistration? CrCasCarInformationRegistrationNavigation { get; set; }
        public virtual CrMasSupCarColor? CrCasCarInformationSeatColorNavigation { get; set; }
        public virtual CrMasSupCarColor? CrCasCarInformationSecondaryColorNavigation { get; set; }
        public virtual ICollection<CrCasCarAdvantage>? CrCasCarAdvantages { get; set; }
    }
}
