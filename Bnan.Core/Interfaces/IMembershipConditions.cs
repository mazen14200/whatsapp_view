using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IMembershipConditions
    {
        Task<bool> AddRenterMembership(string LessorCode,string code , string amount, string link1, string km, string link2,string contractNo,bool isActivate, string groupChar);

    }
}
