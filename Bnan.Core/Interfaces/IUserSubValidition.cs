using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IUserSubValidition
    {
        Task<bool> AddSubValiditionsForEachUser(string userCode, string systemCode);
        Task<bool> AddSubValidaitionToUserWhenAddLessor(string userCode);
    }
}
