using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupContractCarCheckup
    {
        public string CrMasSupContractCarCheckupCode { get; set; } = null!;
        public string? CrMasSupContractCarCheckupArName { get; set; }
        public string? CrMasSupContractCarCheckupEnName { get; set; }
        public string? CrMasSupContractCarCheckupAcceptImage { get; set; }
        public string? CrMasSupContractCarCheckupRejectImage { get; set; }
        public string? CrMasSupContractCarCheckupBlockImage { get; set; }
        public string? CrMasSupContractCarCheckupStatus { get; set; }
        public string? CrMasSupContractCarCheckupReasons { get; set; }
    }
}
