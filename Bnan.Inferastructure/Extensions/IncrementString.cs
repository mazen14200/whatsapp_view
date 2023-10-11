using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Extensions
{
    public static class IncrementString
    {
        public static string IncrementStringExtension(string year, string sector, string procdureCode, string lessorCode, string BranchCode, string id)
        {
            if(id == null)  return $"{year}-{sector}{procdureCode}-{lessorCode}{BranchCode}-000001";

            // Combine the parts and return the result
            string lastPart = (int.Parse(id) + 1).ToString().PadLeft(6, '0');

            return $"{year}-{sector}{procdureCode}-{lessorCode}{BranchCode}-{lastPart}";
        }
    }
}
