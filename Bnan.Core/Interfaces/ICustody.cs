using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface ICustody
    {
        Task<bool> UpdateAccountReceipt(string ReceiptNo,string ReferenceNo, string Reasons);
        Task<bool> UpdateUserInfo(string UserCode, string lessorCode, string Creditor);
        Task<bool> UpdateBranch(string UserCode, string lessorCode,string Creditor);
        Task<bool> UpdateSalesPoint(string lessorCode, string BranchCode, string SalesPointCode, string Creditor);
        Task<bool> UpdateBranchValidity(string UserCode, string lessorCode, string BranchCode, string SalesPointCode, string Creditor);

    }
}
