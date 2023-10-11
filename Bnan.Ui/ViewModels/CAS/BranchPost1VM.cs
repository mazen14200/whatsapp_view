using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class BranchPost1VM
    {
        public string? CrCasBranchPostLessor { get; set; } 
        public string? CrCasBranchPostBranch { get; set; } 

        public string? CrCasBranchPostShortCode { get; set; }
        public string? CrCasBranchPostRegions { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchPostCity { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchPostArDistrict { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchPostEnDistrict { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchPostArStreet { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchPostEnStreet { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchPostBuilding { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchPostUnitNo { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchPostZipCode { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasBranchPostAdditionalNumbers { get; set; }
        public string? CrCasBranchPostArConcatenate { get; set; }
        public string? CrCasBranchPostEnConcatenate { get; set; }
        public string? CrCasBranchPostArShortConcatenate { get; set; }
        public string? CrCasBranchPostEnShortConcatenate { get; set; }
        public string? CrCasBranchPostArMailManual { get; set; }
        public string? CrCasBranchPostEnMailManual { get; set; }
        public DateTime? CrCasBranchPostUpDateMail { get; set; }
        public string? CrCasBranchInformationLocation { get; set; }
        public decimal? CrCasBranchInformationLongitude { get; set; }
        public decimal? CrCasBranchInformationLatitude { get; set; }
        public string? CrCasBranchPostStatus { get; set; }
        public string? CrCasBranchPostReasons { get; set; }
    }
}