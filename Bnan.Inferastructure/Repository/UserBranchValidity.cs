using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class UserBranchValidity : IUserBranchValidity
    {
        private IUnitOfWork _unitOfWork;

        public UserBranchValidity(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddUserBranchValidity(string userCode , string LessorCode, string branchCode,string status)
        {
            CrMasUserBranchValidity crMasUserBranchValidity = new CrMasUserBranchValidity()
            {
                CrMasUserBranchValidityId = userCode,
                CrMasUserBranchValidityLessor = LessorCode,
                CrMasUserBranchValidityBranch = branchCode,
                CrMasUserBranchValidityBranchCashAvailable = 0,
                CrMasUserBranchValidityBranchCashBalance = 0,
                CrMasUserBranchValidityBranchCashReserved = 0,
                CrMasUserBranchValidityBranchTransferAvailable = 0,
                CrMasUserBranchValidityBranchTransferBalance = 0,
                CrMasUserBranchValidityBranchTransferReserved = 0,
                CrMasUserBranchValidityBranchSalesPointBalance = 0,
                CrMasUserBranchValidityBranchSalesPointReserved = 0,
                CrMasUserBranchValidityBranchSalesPointAvailable = 0,
                CrMasUserBranchValidityBranchStatus = status
            };
            await _unitOfWork.CrMasUserBranchValidity.AddAsync(crMasUserBranchValidity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateUserBranchValidity(string userCode, string LessorCode, string branchCode, string status)
        {
            var branchValidate = _unitOfWork.CrMasUserBranchValidity.Find(x => x.CrMasUserBranchValidityId == userCode && x.CrMasUserBranchValidityLessor == LessorCode && x.CrMasUserBranchValidityBranch == branchCode);
            if (branchValidate==null)
            {
                await AddUserBranchValidity(userCode, LessorCode, branchCode, status);
                var NewbranchValidate = _unitOfWork.CrMasUserBranchValidity.Find(x => x.CrMasUserBranchValidityId == userCode && x.CrMasUserBranchValidityLessor == LessorCode && x.CrMasUserBranchValidityBranch == branchCode);
                NewbranchValidate.CrMasUserBranchValidityBranchStatus = status;
                _unitOfWork.CrMasUserBranchValidity.Update(NewbranchValidate);
            }
            else
            {
                branchValidate.CrMasUserBranchValidityBranchStatus = status;
                _unitOfWork.CrMasUserBranchValidity.Update(branchValidate);
            }
            await _unitOfWork.CompleteAsync();
            return false;
        }
    }
}
