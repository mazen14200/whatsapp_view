using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsUp.Core.Models
{
    public partial class Company
    {
        public Company()
        {

        }
        public string companyId { get; set; } = null!;

        public string? companyName { get; set; }

        public string? companyStatus { get; set; }

    }
}
