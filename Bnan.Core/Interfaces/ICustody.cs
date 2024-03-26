using Bnan.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface ICustody
    {
        Task<bool> UpdateAccountReceipt(string ReceiptNo,string ReferenceNo);
        Task<bool> UpdateUserInfo(string UserCode, string lessorCode, string Creditor);
        Task<bool> UpdateBranch(string BranchCode, string lessorCode,string Creditor);
        Task<bool> UpdateSalesPoint(string lessorCode, string BranchCode, string SalesPointCode, string Creditor);
        Task<bool> UpdateBranchValidity(string UserCode, string lessorCode, string BranchCode, string BankNo, string Creditor);
        Task<bool> UpdateBranchValidityReceivedCustody(string UserCode, string lessorCode, string BranchCode, string AdminstritiveNo, string TotalAmount,string status);
        Task<bool> AddAccountReceiptReceivedCustody(string AdmintritiveNo, string lessorCode, string BranchCode, string Creditor,string userCode, string reasons);
        Task<bool> UpdateUserInfoReceivedCustody(string UserCode, string lessorCode, string TotalAmount, string status);
        Task<bool> UpdateBranchReceivedCustody(string BranchCode, string lessorCode, string TotalAmount, string status);
        Task<bool> UpdateSalesPointReceivedCustody(string lessorCode, string BranchCode, string AdminstritiveNo, string TotalAmount, string status);
        bool UpdateAccountReceiptReceivedCustody(string AdminstritiveNo,string status, string Reasons);
        Task<bool> UpdateAdminstritive(string AdminstritiveNo, string UserCode, string status, string Reasons);

    }
}
