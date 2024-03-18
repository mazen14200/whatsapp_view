using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasRenterContractAlert
    {
        public string CrCasRenterContractAlertNo { get; set; } = null!;
        public string? CrCasRenterContractAlertLessor { get; set; }
        public string? CrCasRenterContractAlertBranch { get; set; }
        public int? CrCasRenterContractAlertDays { get; set; }
        public int? CrCasRenterContractAlertHour { get; set; }
        public DateTime? CrCasRenterContractAlertDayDate { get; set; }
        public DateTime? CrCasRenterContractAlertHourDate { get; set; }
        public DateTime? CrCasRenterContractAlertEndDate { get; set; }
        public string? CrCasRenterContractAlertStatus { get; set; }
        public string? CrCasRenterContractAlertArStatusMsg { get; set; }
        public string? CrCasRenterContractAlertEnStatusMsg { get; set; }
        public string? CrCasRenterContractAlertContractActiviteStatus { get; set; }
        public string? CrCasRenterContractAlertContractStatus { get; set; }

        public virtual CrCasBranchInformation? CrCasRenterContractAlertNavigation { get; set; }
    }
}
