using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Bnan.Ui.ViewModels.MAS
{
    public class StartsWith7Attribute : ValidationAttribute
    {

        public override bool IsValid(object? value)
        {
            if (value == null || !(value is string))
            {
                return false;
            }

            string input = (string)value;
            return input.StartsWith("7");
        }
    }
    public class StartsWith5Attribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
            {
                return false;
            }

            string input = (string)value;
            return input.StartsWith("5");
        }
    }

    public class CrMasLessorInformationVM
    {
        public string CrMasLessorInformationCode { get; set; } = null!;

        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasLessorInformationArLongName { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasLessorInformationArShortName { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasLessorInformationEnLongName { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasLessorInformationEnShortName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasLessorInformationClassification { get; set; }
        public string? CrMasLessorInformationLocation { get; set; }

        [RegularExpression(@"^\d{10}$")]
        [Required(ErrorMessage = "requiredNoStartWithFiled7")]
        [StartsWith7(ErrorMessage = "requiredNoLengthFiled")]
        public string? CrMasLessorInformationGovernmentNo { get; set; }

        [RegularExpression(@"^\d{15}$")]
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasLessorInformationTaxNo { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasLessorInformationDirectorArName { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasLessorInformationDirectorEnName { get; set; }

        //[RegularExpression(@"^\d{10}$")]
        //[Required(ErrorMessage = "requiredNoStartWithFiled5")]
        //[StartsWith5Attribute(ErrorMessage = "Must Start With Number 5")]
        public string? CrMasLessorInformationCommunicationMobile { get; set; }
        public string? CrMasLessorInformationCommunicationMobileKey { get; set; }

        //[RegularExpression(@"^\d{9}$")]
        //[Required(ErrorMessage = "requiredNoStartWithFiled1")]
        public string? CrMasLessorInformationCallFree { get; set; }
        public string? CrMasLessorInformationCallFreeKey { get; set; }

        public string? CrMasLessorInformationEmail { get; set; }
        public string? CrMasLessorInformationTwiter { get; set; }
        public string? CrMasLessorInformationFaceBook { get; set; }
        public string? CrMasLessorInformationInstagram { get; set; }
        public string? CrMasLessorInformationContWhatsapp { get; set; }
        public string? CrMasLessorInformationContWhatsappKey { get; set; }
        public string? CrMasLessorInformationContEmail { get; set; }
        public string? CrMasLessorInformationStatus { get; set; }
        public string? CrMasLessorInformationReasons { get; set; }

        public BranchPostVM BranchPostVM { get; set; }
    }
}