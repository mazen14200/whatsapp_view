using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IFeedBoxBS
    {
        Task<bool> UpdateUserInfo(string UserCode, string lessorCode, decimal Creditor);
        Task<bool> UpdateBranch(string BranchCode, string lessorCode, decimal Creditor);
        Task<bool> UpdateSalesPoint(string lessorCode, string BranchCode, decimal Creditor);
        Task<bool> UpdateBranchValidity(string UserCode, string lessorCode, string BranchCode, decimal Creditor);
        Task<bool> AddAccountReceipt(string AdmintritiveNo, string lessorCode, string UserLogin, string BranchCode, decimal Creditor, string reasons);
        Task<bool> UpdateAdminstritive(string AdminstritiveNo, string UserCode, string status, string Reasons);

    }
}
