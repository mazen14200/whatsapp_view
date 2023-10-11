using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class AccountBank : IAccountBank
    {
        private IUnitOfWork _unitOfWork;

        public AccountBank(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAcountBankDefalut(string LessorCode)
        {
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(LessorCode);
            var lessorImg = new CrCasAccountBank
            {
               CrCasAccountBankCode = $"{LessorCode}0001",
               CrCasAccountBankLessor = lessor.CrMasLessorInformationCode,
               CrCasAccountBankNo = "00",
               CrCasAccountBankSerail = "01",
               CrCasAccountBankArName = "الصندوق",
               CrCasAccountBankEnName = "FUNDS",
               CrCasAccountBankStatus = "A",
               CrCasAccountBankIban = $"{LessorCode}0001",

            };
            await _unitOfWork.CrCasAccountBank.AddAsync(lessorImg);
            return true;
        }
    }
}
