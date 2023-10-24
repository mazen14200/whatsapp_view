using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface ICarInformation
    {
        Task<bool> AddCarInformation(CrCasCarInformation crCasCarInformation);
        Task<bool> AddAdvantagesToCar(string serialNumber,string advantageCode,string lessor,string distributionCode, string status);
        Task<bool> UpdateCarInformation(CrCasCarInformation crCasCarInformation);
        Task<bool> UpdateAdvantagesToCar(string serialNumber, string advantageCode, string lessor, string status);
        Task<bool> UpdateCarToSale(CrCasCarInformation crCasCarInformation);
    }
}
