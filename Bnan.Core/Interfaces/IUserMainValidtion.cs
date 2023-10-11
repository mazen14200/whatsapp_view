using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IUserMainValidtion
    {
        Task<bool> AddMainValiditionsForEachUser(string userCode,string systemCode);

        Task<bool> AddMainValidaitionToUserWhenAddLessor(string userCode);
    }
}
