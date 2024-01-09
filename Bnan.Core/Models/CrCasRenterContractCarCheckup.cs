using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasRenterContractCarCheckup
    {
        public string CrCasRenterContractCarCheckupNo { get; set; } = null!;
        public string CrCasRenterContractCarCheckupCode { get; set; } = null!;
        public string CrCasRenterContractCarCheckupType { get; set; } = null!;
        public string? CrCasRenterContractCarCheckupReasons { get; set; }

        public virtual CrMasSupContractCarCheckup CrCasRenterContractCarCheckupCodeNavigation { get; set; } = null!;
        public virtual CrCasRenterContractBasic CrCasRenterContractCarCheckupNoNavigation { get; set; } = null!;
    }
}
