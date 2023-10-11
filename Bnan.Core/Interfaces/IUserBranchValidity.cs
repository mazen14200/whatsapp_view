using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IUserBranchValidity
    {
        Task<bool> AddUserBranchValidity(string userCode,string LessorCode,string branchCode,string status);
        Task<bool> UpdateUserBranchValidity(string userCode,string LessorCode,string branchCode,string status);
    }
}
