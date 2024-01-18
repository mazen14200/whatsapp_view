using Bnan.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class Custody : ICustody
    {
        private IUnitOfWork _unitOfWork;

        public Custody(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdateAccountReceipt(string ReceiptNo, string ReferenceNo, string Reasons)
        {
            var receipt = await _unitOfWork.CrCasAccountReceipt.FindAsync(x => x.CrCasAccountReceiptNo == ReceiptNo);
            if (receipt != null)
            {
                receipt.CrCasAccountReceiptPassingDate = DateTime.Now.Date;
                receipt.CrCasAccountReceiptIsPassing = "2";
                receipt.CrCasAccountReceiptPassingReference = ReferenceNo;
                receipt.CrCasAccountReceiptReasons = Reasons;
                if (_unitOfWork.CrCasAccountReceipt.Update(receipt) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateBranch(string BranchCode, string lessorCode, string Creditor)
        {
            var branch = await _unitOfWork.CrCasBranchInformation.FindAsync(x => x.CrCasBranchInformationLessor == lessorCode && x.CrCasBranchInformationCode == BranchCode);
            if (branch != null)
            {
                if (branch.CrCasBranchInformationAvailableBalance == null) branch.CrCasBranchInformationAvailableBalance = 0;
                if (branch.CrCasBranchInformationReservedBalance == null) branch.CrCasBranchInformationReservedBalance = 0;

                branch.CrCasBranchInformationAvailableBalance -= decimal.Parse(Creditor);
                branch.CrCasBranchInformationReservedBalance += decimal.Parse(Creditor);
                if (_unitOfWork.CrCasBranchInformation.Update(branch) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserInfo( string UserCode, string lessorCode,string Creditor)
        {
            var user = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationLessor == lessorCode && x.CrMasUserInformationCode == UserCode);
            if (user != null)
            {
                user.CrMasUserInformationAvailableBalance -= decimal.Parse(Creditor);
                user.CrMasUserInformationReservedBalance += decimal.Parse(Creditor);
                if (_unitOfWork.CrMasUserInformation.Update(user) !=null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateSalesPoint(string lessorCode, string BranchCode ,string SalesPointCode, string Creditor)
        {
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointLessor == lessorCode &&
                                                                               x.CrCasAccountSalesPointCode == SalesPointCode &&
                                                                               x.CrCasAccountSalesPointBrn==BranchCode);
            if (SalesPoint != null)
            {
                SalesPoint.CrCasAccountSalesPointTotalAvailable -= decimal.Parse(Creditor);
                SalesPoint.CrCasAccountSalesPointTotalReserved += decimal.Parse(Creditor);
                if (_unitOfWork.CrCasAccountSalesPoint.Update(SalesPoint) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateBranchValidity(string UserCode, string lessorCode, string BranchCode, string BankNo, string Creditor)
        {
            var UserBranchValididy = await _unitOfWork.CrMasUserBranchValidity.FindAsync(x => x.CrMasUserBranchValidityId == UserCode && x.CrMasUserBranchValidityLessor == lessorCode &&
                                                                                           x.CrMasUserBranchValidityBranch == BranchCode);
            if (UserBranchValididy!=null&& BankNo!=""&& BankNo!= null)
            {
                if (BankNo=="00")
                {
                    UserBranchValididy.CrMasUserBranchValidityBranchCashAvailable -= decimal.Parse(Creditor);
                    UserBranchValididy.CrMasUserBranchValidityBranchCashReserved += decimal.Parse(Creditor);
                }
                else
                {
                    UserBranchValididy.CrMasUserBranchValidityBranchSalesPointAvailable -= decimal.Parse(Creditor);
                    UserBranchValididy.CrMasUserBranchValidityBranchSalesPointReserved += decimal.Parse(Creditor);
                }
                if (_unitOfWork.CrMasUserBranchValidity.Update(UserBranchValididy) != null) return true;

            }
            return false;
        }
    }
}
