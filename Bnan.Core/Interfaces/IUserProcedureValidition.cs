using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IUserProcedureValidition
    {
        Task<bool> AddProceduresValiditionsForEachUser(string userCode, string systemCode);
    }
}
