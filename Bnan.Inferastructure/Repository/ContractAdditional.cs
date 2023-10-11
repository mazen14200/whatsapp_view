using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class ContractAdditional:IContractAdditional
    {
        public IUnitOfWork _unitOfWork;

        public ContractAdditional(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CrMasSupContractAdditional?>> GetAllContractsByStatusAsync()
        {
            var Additionals = await _unitOfWork.CrMasSupContractAdditional.GetAllAsync();
            return (List<CrMasSupContractAdditional?>)Additionals;
        }
    }
}
