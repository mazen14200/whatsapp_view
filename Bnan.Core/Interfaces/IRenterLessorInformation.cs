using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IRenterLessorInformation 
    {
        public List<List<string>> AdressSplit(List<CrMasRenterPost> RenterLessorInformationAllA);

    }
}
