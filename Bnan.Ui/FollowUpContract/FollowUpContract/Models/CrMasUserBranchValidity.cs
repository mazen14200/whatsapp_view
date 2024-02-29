using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasUserBranchValidity
    {
        public string CrMasUserBranchValidityId { get; set; } = null!;
        public string CrMasUserBranchValidityLessor { get; set; } = null!;
        public string CrMasUserBranchValidityBranch { get; set; } = null!;
        public decimal? CrMasUserBranchValidityBranchCashBalance { get; set; }
        public decimal? CrMasUserBranchValidityBranchCashReserved { get; set; }
        public decimal? CrMasUserBranchValidityBranchCashAvailable { get; set; }
        public decimal? CrMasUserBranchValidityBranchSalesPointBalance { get; set; }
        public decimal? CrMasUserBranchValidityBranchSalesPointReserved { get; set; }
        public decimal? CrMasUserBranchValidityBranchSalesPointAvailable { get; set; }
        public decimal? CrMasUserBranchValidityBranchTransferBalance { get; set; }
        public decimal? CrMasUserBranchValidityBranchTransferReserved { get; set; }
        public decimal? CrMasUserBranchValidityBranchTransferAvailable { get; set; }
        public string? CrMasUserBranchValidityBranchStatus { get; set; }
        public string? CrMasUserBranchValidityBranchRecStatus { get; set; }

        public virtual CrCasBranchInformation CrMasUserBranchValidity1 { get; set; } = null!;
        public virtual CrMasUserInformation CrMasUserBranchValidityNavigation { get; set; } = null!;
    }
}
