using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class AccountRefrence:IAccountRefrence
    {
        public IUnitOfWork _unitOfWork;

        public AccountRefrence(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CrMasSupAccountReference?>> GetAllAccountsByStatusAsync()
        {
            var refrences = await _unitOfWork.CrMasSupAccountReference.GetAllAsync();
            return (List<CrMasSupAccountReference?>)refrences;

        }
    }
}
