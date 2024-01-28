using Bnan.Core.Extensions;
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

        public async Task<bool> UpdateUserInfo(string UserCode, string lessorCode, string Creditor)
        {
            var user = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationLessor == lessorCode && x.CrMasUserInformationCode == UserCode);
            if (user != null)
            {
                user.CrMasUserInformationAvailableBalance -= decimal.Parse(Creditor);
                user.CrMasUserInformationReservedBalance += decimal.Parse(Creditor);
                if (_unitOfWork.CrMasUserInformation.Update(user) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateSalesPoint(string lessorCode, string BranchCode, string SalesPointCode, string Creditor)
        {
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointLessor == lessorCode &&
                                                                               x.CrCasAccountSalesPointCode == SalesPointCode &&
                                                                               x.CrCasAccountSalesPointBrn == BranchCode);
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
            if (UserBranchValididy != null && BankNo != "" && BankNo != null)
            {
                if (BankNo == "00")
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
        public async Task<bool> AddAccountReceiptReceivedCustody(string AdmintritiveNo, string lessorCode, string BranchCode,
                                                                 string TotalAmount, string reasons)
        {

            CrCasAccountReceipt receipt = new CrCasAccountReceipt();
            var adminstritive = await _unitOfWork.CrCasSysAdministrativeProcedure.FindAsync(x => x.CrCasSysAdministrativeProceduresNo == AdmintritiveNo);
            var accountReceipt = _unitOfWork.CrCasAccountReceipt.Find(x => x.CrCasAccountReceiptPassingReference == AdmintritiveNo);
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointCode == accountReceipt.CrCasAccountReceiptSalesPoint);
            var UserTarget = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == adminstritive.CrCasSysAdministrativeProceduresTargeted.Trim());
            receipt.CrCasAccountReceiptNo = GetAccountReceiptNo(adminstritive.CrCasSysAdministrativeProceduresBranch,UserTarget.CrMasUserInformationCode);
            receipt.CrCasAccountReceiptYear = DateTime.Now.ToString("yy");
            receipt.CrCasAccountReceiptType = "302";
            receipt.CrCasAccountReceiptLessorCode = lessorCode;
            receipt.CrCasAccountReceiptBranchCode = BranchCode;
            receipt.CrCasAccountReceiptDate = DateTime.Now;
            if (SalesPoint.CrCasAccountSalesPointBank == "00") receipt.CrCasAccountReceiptPaymentMethod = "10";
            else receipt.CrCasAccountReceiptPaymentMethod = "40";
            receipt.CrCasAccountReceiptReferenceType = "15";
            receipt.CrCasAccountReceiptReferenceNo = adminstritive.CrCasSysAdministrativeProceduresNo;
            receipt.CrCasAccountReceiptReceipt = decimal.Parse(TotalAmount);
            receipt.CrCasAccountReceiptPayment = 0;
            receipt.CrCasAccountReceiptBank = SalesPoint.CrCasAccountSalesPointBank;
            receipt.CrCasAccountReceiptAccount = SalesPoint.CrCasAccountSalesPointAccountBank;
            receipt.CrCasAccountReceiptSalesPoint = SalesPoint.CrCasAccountSalesPointCode;
            receipt.CrCasAccountReceiptSalesPointPreviousBalance = SalesPoint.CrCasAccountSalesPointTotalBalance;
            receipt.CrCasAccountReceiptUser = UserTarget.CrMasUserInformationCode;
            receipt.CrCasAccountReceiptUserPreviousBalance = UserTarget.CrMasUserInformationTotalBalance;
            receipt.CrCasAccountReceiptIsPassing = "3";
            receipt.CrCasAccountReceiptPassingUser = adminstritive.CrCasSysAdministrativeProceduresUserInsert;
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
                Adminstritive.CrCasSysAdministrativeProceduresUserInsert = UserCode;
                if (status==Status.Reject)
                {
                    if (!string.IsNullOrEmpty(Reasons)) Adminstritive.CrCasSysAdministrativeProceduresReasons = Reasons;
                }
                if (_unitOfWork.CrCasSysAdministrativeProcedure.Update(Adminstritive) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateBranchReceivedCustody(string BranchCode, string lessorCode, string TotalAmount, string status)
        {
            var branch = await _unitOfWork.CrCasBranchInformation.FindAsync(x => x.CrCasBranchInformationLessor == lessorCode && x.CrCasBranchInformationCode == BranchCode);
            if (branch != null)
            {
                if (branch.CrCasBranchInformationAvailableBalance == null) branch.CrCasBranchInformationAvailableBalance = 0;
                if (branch.CrCasBranchInformationReservedBalance == null) branch.CrCasBranchInformationReservedBalance = 0;
                if (status == Status.Accept)
                {
                    branch.CrCasBranchInformationReservedBalance -= decimal.Parse(TotalAmount);
                    branch.CrCasBranchInformationTotalBalance -= decimal.Parse(TotalAmount);
                }
                else
                {
                    branch.CrCasBranchInformationReservedBalance -= decimal.Parse(TotalAmount);
                    branch.CrCasBranchInformationAvailableBalance += decimal.Parse(TotalAmount);
                }

                if (_unitOfWork.CrCasBranchInformation.Update(branch) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateSalesPointReceivedCustody(string lessorCode, string BranchCode, string AdminstritiveNo, string TotalAmount, string status)
        {
            var accountReceipt = _unitOfWork.CrCasAccountReceipt.Find(x => x.CrCasAccountReceiptPassingReference == AdminstritiveNo);
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointCode == accountReceipt.CrCasAccountReceiptSalesPoint);

            if (SalesPoint != null)
            {
                if (status == Status.Accept)
                {
                    SalesPoint.CrCasAccountSalesPointTotalBalance -= decimal.Parse(TotalAmount);
                    SalesPoint.CrCasAccountSalesPointTotalReserved -= decimal.Parse(TotalAmount);
                }
                else
                {
                    SalesPoint.CrCasAccountSalesPointTotalAvailable += decimal.Parse(TotalAmount);
                    SalesPoint.CrCasAccountSalesPointTotalReserved -= decimal.Parse(TotalAmount);
                }

                if (_unitOfWork.CrCasAccountSalesPoint.Update(SalesPoint) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserInfoReceivedCustody(string UserCode, string lessorCode, string TotalAmount, string status)
        {
            var user = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationLessor == lessorCode && x.CrMasUserInformationCode == UserCode);
            if (user != null)
            {
                if (status == Status.Accept)
                {
                    user.CrMasUserInformationTotalBalance -= decimal.Parse(TotalAmount);
                    user.CrMasUserInformationReservedBalance -= decimal.Parse(TotalAmount);
                }
                else
                {
                    user.CrMasUserInformationAvailableBalance += decimal.Parse(TotalAmount);
                    user.CrMasUserInformationReservedBalance -= decimal.Parse(TotalAmount);
                }

                if (_unitOfWork.CrMasUserInformation.Update(user) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateBranchValidityReceivedCustody(string UserCode, string lessorCode, string BranchCode, string AdminstritiveNo, string TotalAmount, string status)
        {
            var UserBranchValididy = await _unitOfWork.CrMasUserBranchValidity.FindAsync(x => x.CrMasUserBranchValidityId == UserCode && x.CrMasUserBranchValidityLessor == lessorCode &&
                                                                               x.CrMasUserBranchValidityBranch == BranchCode);

            var accountReceipt = _unitOfWork.CrCasAccountReceipt.Find(x => x.CrCasAccountReceiptPassingReference == AdminstritiveNo);
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointCode == accountReceipt.CrCasAccountReceiptSalesPoint);


            if (UserBranchValididy != null && SalesPoint.CrCasAccountSalesPointBank != "" && SalesPoint.CrCasAccountSalesPointBank != null)
            {
                if (SalesPoint.CrCasAccountSalesPointBank == "00")
                {
                    if (status == Status.Accept)
                    {
                        UserBranchValididy.CrMasUserBranchValidityBranchCashBalance -= decimal.Parse(TotalAmount);
                        UserBranchValididy.CrMasUserBranchValidityBranchCashReserved -= decimal.Parse(TotalAmount);
                    }
                    else
                    {
                        UserBranchValididy.CrMasUserBranchValidityBranchCashAvailable += decimal.Parse(TotalAmount);
                        UserBranchValididy.CrMasUserBranchValidityBranchCashReserved -= decimal.Parse(TotalAmount);
                    }

                }
                else
                {
                    if (status == Status.Accept)
                    {
                        UserBranchValididy.CrMasUserBranchValidityBranchSalesPointBalance -= decimal.Parse(TotalAmount);
                        UserBranchValididy.CrMasUserBranchValidityBranchSalesPointReserved -= decimal.Parse(TotalAmount);
                    }
                    else
                    {
                        UserBranchValididy.CrMasUserBranchValidityBranchSalesPointAvailable += decimal.Parse(TotalAmount);
                        UserBranchValididy.CrMasUserBranchValidityBranchSalesPointReserved -= decimal.Parse(TotalAmount);
                    }

                }
                if (_unitOfWork.CrMasUserBranchValidity.Update(UserBranchValididy) != null) return true;

            }
            return false;
        }

        public bool UpdateAccountReceiptReceivedCustody(string AdminstritiveNo, string status, string Reasons)
        {
            var AccountReceipts = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptPassingReference == AdminstritiveNo);
            var PasssingNo = "";
            if (status == Status.Accept) PasssingNo = "3";
            else PasssingNo = "1";
            if (AccountReceipts != null && AccountReceipts.Count() != 0)
            {
                foreach (var AccountReceipt in AccountReceipts)
                {
                    if (status == Status.Reject) AccountReceipt.CrCasAccountReceiptPassingReference = null;
                    if(status==Status.Accept) AccountReceipt.CrCasAccountReceiptPassingDate = DateTime.Now.Date;
                    AccountReceipt.CrCasAccountReceiptIsPassing = PasssingNo;
                    AccountReceipt.CrCasAccountReceiptReasons = Reasons;
                    _unitOfWork.CrCasAccountReceipt.Update(AccountReceipt);
                }
                return true;
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
                x.CrCasAccountReceiptType == "302"
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
            var receipt = y + "-" + "1" + "302" + "-" + lessorCode + BranchCode + "-" + Serial;
            return receipt;
        }
    }
}
