using Bnan.Ui.ViewModels.MAS;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class BranchVM
    {
        public string CrCasBranchInformationLessor { get; set; } = null!;
        public string CrCasBranchInformationCode { get; set; } = null!;
        public string? CrCasBranchInformationArTga { get; set; }
        public string? CrCasBranchInformationEnTga { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchInformationArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchInformationArShortName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchInformationEnName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchInformationEnShortName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        [RegularExpression(@"^7\d{9}$", ErrorMessage = "requiredNoLengthFiled10")]
        public string? CrCasBranchInformationGovernmentNo { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "requiredNoLengthFiled15")]
        public string? CrCasBranchInformationTaxNo { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchInformationDirectorArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchInformationDirectorEnName { get; set; }
        public string? CrMasBranchInformationTeleKey { get; set; }
        public string? CrCasBranchInformationTelephone { get; set; }
        public string? CrMasBranchInformationMobileKey { get; set; }
        public string? CrCasBranchInformationMobile { get; set; }
        public string? CrCasBranchInformationReasons { get; set; }
        public string? CrCasBranchInformationStatus { get; set; }
        public decimal? CrCasBranchInformationTotalBalance { get; set; }
        public decimal? CrCasBranchInformationReservedBalance { get; set; }
        public decimal? CrCasBranchInformationAvailableBalance { get; set; }
        public string? CrCasBranchInformationDirectorSignature { get; set; }

        public BranchPost1VM BranchPostVM { get; set; }

    }
}
