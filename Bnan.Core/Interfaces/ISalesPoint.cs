using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface ISalesPoint
    {
        Task<bool> AddSalesPointDefault(string LessorCode);
        Task<bool> AddSalesPoint(CrCasBranchInformation crCasBranchInformation);
        Task<CrCasAccountSalesPoint> CreateSalesPoint(CrCasAccountSalesPoint crCasAccountSalesPoint, string userCode);


    }
}
