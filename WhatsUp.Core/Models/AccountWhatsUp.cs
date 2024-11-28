using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsUp.Core.Models
{
    public partial class AccountWhatsUp
    {
        public AccountWhatsUp() 
        {
        }

        public string AccountWhatsUpCompanyId { get; set; } = null!;

        public string? AccountWhatsUpName { get; set; }

        public string? AccountWhatsUpUserName { get; set; }
        public string? AccountWhatsUpPassword { get; set; }

        public string? AccountWhatsUpFullPhoneNumber { get; set; }
        public string? AccountWhatsUpApiToken { get; set; }
        public string? AccountWhatsUpImage { get; set; }
        public string? AccountWhatsUpStatus { get; set; }

    }
}
