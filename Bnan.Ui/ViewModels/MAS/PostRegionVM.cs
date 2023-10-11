using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Bnan.Ui.ViewModels.MAS
{
    public class IsValidLocation : ValidationAttribute
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
    public class PostRegionVM 
    {
        public string CrMasSupPostRegionsCode { get; set; } = null!;
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupPostRegionsArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrMasSupPostRegionsEnName { get; set; }
        public string? CrMasSupPostRegionsLocation { get; set; }
       // [IsValidLocation(ErrorMessage = "PostLongRequired")]     
        public string? CrMasSupPostRegionsLongitude { get; set; }

        //[IsValidLocation(ErrorMessage = "PostLatRequired")]
        public string? CrMasSupPostRegionsLatitude { get; set; }

        public string? CrMasSupPostRegionsStatus { get; set; }
        public string? CrMasSupPostRegionsReasons { get; set; }
    }
}
