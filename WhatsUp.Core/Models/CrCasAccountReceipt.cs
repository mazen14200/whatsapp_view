using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsUp.Core.Models
{
     
    public partial class CrCasAccountReceipt
    {
        public CrCasAccountReceipt()
        {
            
        }

        public string CrCasAccountReceipt_No { get; set; } = null!;

        public string? CrCasAccountReceipt_LessorCode { get; set; }
        public DateTime? CrCasAccountReceipt_DateTime { get; set; }
        public string? CrCasAccountReceipt_Reference_No { get; set; }
        public decimal? CrCasAccountReceipt_Payment { get; set; }
        public decimal? CrCasAccountReceipt_Receipt { get; set; }

    }
}
