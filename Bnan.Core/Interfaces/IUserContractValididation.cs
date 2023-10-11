using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IUserContractValididation
    {
        Task<bool> AddContractValiditionsForEachUserInCas(string userCode, string lastRecordProcedure);
        Task<bool> EditContractValiditionsForEmployee(CrMasUserContractValidity model);

    }
}
