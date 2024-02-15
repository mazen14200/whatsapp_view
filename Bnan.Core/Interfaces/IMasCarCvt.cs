using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IMasCarCvt
    {
        List<List<string>> GetAllCarCvtsCount();

        int GetOneCarCvtCount(string id);
    }
}
