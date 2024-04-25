using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class Car_Renter_Statistics : ICar_Renter_Statistics
    {
        public IUnitOfWork _unitOfWork;
        public Car_Renter_Statistics(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CrMasSupAccountReference?>> GetAllBranchStatistcs()
        {
            var refrences = await _unitOfWork.CrMasSupAccountReference.GetAllAsync();
            return (List<CrMasSupAccountReference?>)refrences;

        }
    }
}
