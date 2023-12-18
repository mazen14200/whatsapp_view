using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.BS
{
    public class Contract
    {
        [Required(ErrorMessage = "requiredFiled")]
        public string? RenterId { get; set; }

    }
}
