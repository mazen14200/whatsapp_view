using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsUp.Core.Models
{
    public partial class Renter
    {
        public Renter() 
        {
        }

        public string RenterId { get; set; } = null!;

        public string? RenterPersonEnName { get; set; }

        public string? RenterPersonArName { get; set; }

        public string? RentercountryKey { get; set; }

        public string? RenterPhoneNumber { get; set; }

        public string? RenterStatus { get; set; }

    }
}
