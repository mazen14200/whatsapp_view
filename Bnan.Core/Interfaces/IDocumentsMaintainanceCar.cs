using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IDocumentsMaintainanceCar
    {
        Task<bool> AddDocumentCar(string serialNumber, string lessorCode, string branchCode, int currentMeter);
        Task<bool> AddMaintainaceCar(string serialNumber, string lessorCode, string branchCode, int currentMeter);
    }
}
