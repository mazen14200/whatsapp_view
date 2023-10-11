using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class BankService : IBankService
    {
        public IUnitOfWork _unitOfWork;

        public BankService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CrMasSupAccountBank?>> GetAllBanksByStatusAsync()
        {
            var banks = await _unitOfWork.CrMasSupAccountBanks.GetAllAsync();
            return (List<CrMasSupAccountBank?>)banks;

        }

      
    }
}
