using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IRenterDriver
    {
        Task<bool> AddRenterDriver(CrCasRenterPrivateDriverInformation model);
        Task<bool> UpdateRenterDriver(CrCasRenterPrivateDriverInformation model);

    }

}
