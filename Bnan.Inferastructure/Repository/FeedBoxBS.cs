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
    public class FeedBoxBS : IFeedBoxBS
    {
        private IUnitOfWork _unitOfWork;

        public FeedBoxBS(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAccountReceipt(string AdmintritiveNo, string lessorCode, string UserLogin, string BranchCode,
                                                                 decimal TotalAmount, string reasons)
        {

            CrCasAccountReceipt receipt = new CrCasAccountReceipt();
            var adminstritive = await _unitOfWork.CrCasSysAdministrativeProcedure.FindAsync(x => x.CrCasSysAdministrativeProceduresNo == AdmintritiveNo);
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x =>x.CrCasAccountSalesPointLessor==lessorCode&& x.CrCasAccountSalesPointBrn == BranchCode && x.CrCasAccountSalesPointBank == "00");
            var UserTarget = await _unitOfWork.CrMasUserInformation.FindAsync(x =>x.CrMasUserInformationLessor==lessorCode&& x.CrMasUserInformationCode == UserLogin);
            var userValidity = await _unitOfWork.CrMasUserBranchValidity.FindAsync(x => x.CrMasUserBranchValidityLessor == lessorCode && x.CrMasUserBranchValidityBranch == BranchCode && x.CrMasUserBranchValidityId == UserTarget.CrMasUserInformationCode);
            var userBranchValidityBalance = userValidity.CrMasUserBranchValidityBranchCashAvailable + userValidity.CrMasUserBranchValidityBranchSalesPointAvailable + userValidity.CrMasUserBranchValidityBranchTransferAvailable;
            receipt.CrCasAccountReceiptNo = GetAccountReceiptNo(BranchCode, UserTarget.CrMasUserInformationCode);
            receipt.CrCasAccountReceiptYear = DateTime.Now.ToString("yy");
            receipt.CrCasAccountReceiptType = "301";
            receipt.CrCasAccountReceiptLessorCode = lessorCode;
            receipt.CrCasAccountReceiptBranchCode = BranchCode;
            receipt.CrCasAccountReceiptDate = DateTime.Now;
            receipt.CrCasAccountReceiptPaymentMethod = "10";
            receipt.CrCasAccountReceiptReferenceType = "14";
            receipt.CrCasAccountReceiptReferenceNo = adminstritive.CrCasSysAdministrativeProceduresNo;
            receipt.CrCasAccountReceiptReceipt = 0;
            receipt.CrCasAccountReceiptPayment = TotalAmount;
            receipt.CrCasAccountReceiptBank = SalesPoint.CrCasAccountSalesPointBank;
            receipt.CrCasAccountReceiptAccount = SalesPoint.CrCasAccountSalesPointAccountBank;
            receipt.CrCasAccountReceiptSalesPoint = SalesPoint.CrCasAccountSalesPointCode;
            receipt.CrCasAccountReceiptSalesPointPreviousBalance = SalesPoint.CrCasAccountSalesPointTotalBalance;
            receipt.CrCasAccountReceiptUser = UserTarget.CrMasUserInformationCode;
            receipt.CrCasAccountReceiptUserPreviousBalance = UserTarget.CrMasUserInformationTotalBalance;
            receipt.CrCasAccountReceiptBranchUserPreviousBalance = userBranchValidityBalance;
            receipt.CrCasAccountReceiptIsPassing = "1";
            receipt.CrCasAccountReceiptPassingUser = UserTarget.CrMasUserInformationCode;
            receipt.CrCasAccountReceiptPassingDate = DateTime.Now;
            receipt.CrCasAccountReceiptReasons = reasons;
            if (await _unitOfWork.CrCasAccountReceipt.AddAsync(receipt) != null) return true;
            return false;
        }
        public async Task<bool> UpdateAdminstritive(string AdminstritiveNo, string UserCode, string status, string Reasons)
        {
            var Adminstritive = await _unitOfWork.CrCasSysAdministrativeProcedure.FindAsync(x => x.CrCasSysAdministrativeProceduresNo == AdminstritiveNo);
            if (Adminstritive != null)
            {
                Adminstritive.CrCasSysAdministrativeProceduresStatus = status;

                Adminstritive.CrCasSysAdministrativeProceduresReasons = Reasons;

                if (_unitOfWork.CrCasSysAdministrativeProcedure.Update(Adminstritive) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateBranch(string BranchCode, string lessorCode, decimal Creditor)
        {
            var branch = await _unitOfWork.CrCasBranchInformation.FindAsync(x => x.CrCasBranchInformationLessor == lessorCode && x.CrCasBranchInformationCode == BranchCode);
            if (branch != null)
            {
                if (branch.CrCasBranchInformationAvailableBalance == null) branch.CrCasBranchInformationAvailableBalance = 0;
                if (branch.CrCasBranchInformationTotalBalance == null) branch.CrCasBranchInformationReservedBalance = 0;

                branch.CrCasBranchInformationAvailableBalance += Creditor;
                branch.CrCasBranchInformationTotalBalance += Creditor;
                if (_unitOfWork.CrCasBranchInformation.Update(branch) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateBranchValidity(string UserCode, string lessorCode, string BranchCode, decimal Creditor)
        {
            var UserBranchValididy = await _unitOfWork.CrMasUserBranchValidity.FindAsync(x => x.CrMasUserBranchValidityId == UserCode && x.CrMasUserBranchValidityLessor == lessorCode &&
                                                                                           x.CrMasUserBranchValidityBranch == BranchCode);
            if (UserBranchValididy != null)
            {
                UserBranchValididy.CrMasUserBranchValidityBranchCashAvailable += Creditor;
                UserBranchValididy.CrMasUserBranchValidityBranchCashBalance += Creditor;
                if (_unitOfWork.CrMasUserBranchValidity.Update(UserBranchValididy) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateSalesPoint(string lessorCode, string BranchCode, decimal Creditor)
        {
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointLessor == lessorCode &&x.CrCasAccountSalesPointBank=="00"&&
                                                                               x.CrCasAccountSalesPointBrn == BranchCode);
            if (SalesPoint != null)
            {
                SalesPoint.CrCasAccountSalesPointTotalAvailable += Creditor;
                SalesPoint.CrCasAccountSalesPointTotalBalance += Creditor;
                if (_unitOfWork.CrCasAccountSalesPoint.Update(SalesPoint) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserInfo(string UserCode, string lessorCode, decimal Creditor)
        {
            var user = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationLessor == lessorCode && x.CrMasUserInformationCode == UserCode);
            if (user != null)
            {
                user.CrMasUserInformationAvailableBalance += Creditor;
                user.CrMasUserInformationTotalBalance += Creditor;
                if (_unitOfWork.CrMasUserInformation.Update(user) != null) return true;
            }
            return false;
        }

        public string GetAccountReceiptNo(string BranchCode, string UserCode)
        {
            var userLogin = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == UserCode);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var Lrecord = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptLessorCode == userLogin.CrMasUserInformationLessor &&
                x.CrCasAccountReceiptType == "301"
                && x.CrCasAccountReceiptYear == y).Max(x => x.CrCasAccountReceiptNo.Substring(x.CrCasAccountReceiptNo.Length - 6, 6));
            string Serial;
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                Serial = val.ToString("000000");
            }
            else
            {
                Serial = "000001";
            }
            var receipt = y + "-" + "1" + "301" + "-" + lessorCode + BranchCode + "-" + Serial;
            return receipt;
        }
    }
}
