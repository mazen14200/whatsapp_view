using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.Identitiy

{
    public class ChangePasswordVM
    {
      
            [Required(ErrorMessage = "Please Enter Current Password")]
            public string? CurrentPassword { get; set; }
            [Required(ErrorMessage = "Please Enter New Passowrd")]
            public string? NewPassword { get; set; }
            [Required(ErrorMessage = "Please Enter Confirm New Passowrd")]
            public string? ConfirmPassword { get; set; }

    }
}
