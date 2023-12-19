using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IContract
    {
        Task<bool> AddRenterFromElmToMasRenter(string RenterID, CrElmEmployer crElmEmployer , CrElmPersonal crElmPersonal, CrElmLicense crElmLicense);

    }
}
