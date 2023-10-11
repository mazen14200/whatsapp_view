using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class ContractOptions:IContractOptions
    {
        public IUnitOfWork _unitOfWork;

        public ContractOptions(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CrMasSupContractOption?>> GetAllOptionsByStatusAsync()
        {
            var options = await _unitOfWork.CrMasSupContractOption.GetAllAsync();
            return (List<CrMasSupContractOption?>)options;

        }
    }
}
