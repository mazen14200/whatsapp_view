using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.Identitiy
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "PleaseEnterUserName")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "PleaseEnterPassword")]
        public string? Password { get; set; }
    }
}
