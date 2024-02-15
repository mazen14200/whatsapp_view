using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IMasCarCategory
    {
        List<List<string>> GetAllCarCategorysCount();

        int GetOneCarCategoryCount(string id);
    }
}
