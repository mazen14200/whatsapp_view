using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface ICarPrice
    {
        Task<bool> AddFeatures(string serialNumber, string id, string value);
        Task<bool> AddChoises(string serialNumber, string id, string value);
        Task<bool> AddAdditionals(string serialNumber, string id , string value);
        Task<string> AddPriceCar(CrCasPriceCarBasic crCasPriceCarBasic);

    }
}
