using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Bnan.Ui.ViewModels.MAS
{
    public class IsValidLocationCity : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
            {
                return false;
            }

            string input = (string)value;

            // Ensure input is numeric and has maximum length of 25
            if (!Regex.IsMatch(input, @"^\d{1,4}(\.\d{1,20})?$") || input.Length > 25)
            {
                return false;
            }

            return true;
        }
    }
    public class PostCityVM
    {
        public string CrMasSupPostCityCode { get; set; } = null!;
        public string? CrMasSupPostCityGroupCode { get; set; }
        public string? CrMasSupPostCityRegionsCode { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupPostCityArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupPostCityEnName { get; set; }
        public string? CrMasSupPostCityConcatenateArName { get; set; }
        public string? CrMasSupPostCityConcatenateEnName { get; set; }
        public string? CrMasSupPostCityLocation { get; set; }
        //[IsValidLocationCity(ErrorMessage = "PostLongRequired")]
        public string? CrMasSupPostCityLongitude { get; set; }

        //[IsValidLocationCity(ErrorMessage = "PostLatRequired")]
        public string? CrMasSupPostCityLatitude { get; set; }
        public int? CrMasSupPostCityCounter { get; set; }
        public string? CrMasSupPostCityRegionsStatus { get; set; }
        public string? CrMasSupPostCityStatus { get; set; }
        public string? CrMasSupPostCityReasons { get; set; }
    }
}
