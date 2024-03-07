using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class TransferToFromRenter : ITransferToFromRenter
    {
        private IUnitOfWork _unitOfWork;
        private readonly UserManager<CrMasUserInformation> _userManager;

        public TransferToFromRenter(IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<bool> AddAccountReceiptTransferToRenter(string AdmintritiveNo,string RenterId, string userCode,string ProcedureCode,string ReferenceType, string lessorCode, string BankNo, string AccountNo, string TotalAmountPayment, string TotalAmountReceipt, string reasons)
        {
            CrCasAccountReceipt receipt = new CrCasAccountReceipt();
            var Renter = await _unitOfWork.CrCasRenterLessor.FindAsync(x => x.CrCasRenterLessorId == RenterId);
            var User = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == userCode);
            receipt.CrCasAccountReceiptNo = GetAccountReceiptNo("100", userCode, ProcedureCode);
            receipt.CrCasAccountReceiptYear = DateTime.Now.ToString("yy");
            receipt.CrCasAccountReceiptType = ProcedureCode;
            receipt.CrCasAccountReceiptLessorCode = lessorCode;
            receipt.CrCasAccountReceiptBranchCode = "100";
            receipt.CrCasAccountReceiptDate = DateTime.Now;
            receipt.CrCasAccountReceiptReferenceType = ReferenceType;
            receipt.CrCasAccountReceiptReferenceNo = AdmintritiveNo;
            receipt.CrCasAccountReceiptReceipt = decimal.Parse(TotalAmountReceipt, CultureInfo.InvariantCulture);
            receipt.CrCasAccountReceiptPayment = decimal.Parse(TotalAmountPayment, CultureInfo.InvariantCulture);
            receipt.CrCasAccountReceiptBank = BankNo;
            receipt.CrCasAccountReceiptAccount = AccountNo;
            receipt.CrCasAccountReceiptUser = userCode;
            receipt.CrCasAccountReceiptRenterId = Renter.CrCasRenterLessorId;
            receipt.CrCasAccountReceiptRenterPreviousBalance = Renter.CrCasRenterLessorBalance;
            receipt.CrCasAccountReceiptUserPreviousBalance = User.CrMasUserInformationTotalBalance;
            receipt.CrCasAccountReceiptIsPassing = "4";
            receipt.CrCasAccountReceiptPaymentMethod = "30";
            receipt.CrCasAccountReceiptPassingDate = DateTime.Now;
            receipt.CrCasAccountReceiptReasons = reasons;
            if (await _unitOfWork.CrCasAccountReceipt.AddAsync(receipt) != null) return true;
            return false;
        }

        public async Task<bool> UpdateRenterInformation(string RenterId, string IBanNo, string BankNo)
        {
            var Renter = await _unitOfWork.CrMasRenterInformation.FindAsync(x => x.CrMasRenterInformationId == RenterId);
            if (Renter != null)
            {
                Renter.CrMasRenterInformationBank = BankNo;
                Renter.CrMasRenterInformationIban = IBanNo;
                if (_unitOfWork.CrMasRenterInformation.Update(Renter) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateCasRenterLessorTransferFrom(string RenterId, string lessorCode, string Amount)
        {
            var Renter = await _unitOfWork.CrCasRenterLessor.FindAsync(x => x.CrCasRenterLessorId == RenterId);
            if (Renter != null)
            {
                Renter.CrCasRenterLessorBalance+=decimal.Parse(Amount, CultureInfo.InvariantCulture);
                Renter.CrCasRenterLessorAvailableBalance+=decimal.Parse(Amount, CultureInfo.InvariantCulture);
                if (_unitOfWork.CrCasRenterLessor.Update(Renter) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateCasRenterLessorTransferTo(string RenterId, string lessorCode, string Amount)
        {
            var Renter = await _unitOfWork.CrCasRenterLessor.FindAsync(x => x.CrCasRenterLessorId == RenterId);
            if (Renter != null)
            {
                Renter.CrCasRenterLessorBalance -= decimal.Parse(Amount, CultureInfo.InvariantCulture);
                Renter.CrCasRenterLessorAvailableBalance -= decimal.Parse(Amount, CultureInfo.InvariantCulture);
                if (_unitOfWork.CrCasRenterLessor.Update(Renter) != null) return true;
            }
            return false;
        }
        public string GetAccountReceiptNo(string BranchCode, string UserCode,string ProcedureCode)
        {
            var userLogin = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == UserCode);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var Lrecord = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptLessorCode == userLogin.CrMasUserInformationLessor &&
                x.CrCasAccountReceiptType == ProcedureCode
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
            var receipt = y + "-" + "1" + ProcedureCode + "-" + lessorCode + BranchCode + "-" + Serial;
            return receipt;
        }

        public async Task<CrCasSysAdministrativeProcedure> SaveAdminstritiveTransferRenter(string AdmintritiveCode,string userCode, string ProcedureCode, string ClassificationCode, string LessorCode,
                                                                             string? Targeted, decimal? Debit, decimal? Creditor, string? Reasons)
        {
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var currentUser = await _userManager.FindByIdAsync(userCode);
            var procedure = _unitOfWork.CrMasSysProcedure.Find(x => x.CrMasSysProceduresCode == ProcedureCode);
            CrCasSysAdministrativeProcedure crCasSysAdministrativeProcedure = new CrCasSysAdministrativeProcedure()
            {
                CrCasSysAdministrativeProceduresNo = AdmintritiveCode,
                CrCasSysAdministrativeProceduresYear = y,
                CrCasSysAdministrativeProceduresSector = "1",
                CrCasSysAdministrativeProceduresCode = ProcedureCode,
                CrCasSysAdministrativeProceduresClassification = ClassificationCode,
                CrCasSysAdministrativeProceduresLessor = LessorCode,
                CrCasSysAdministrativeProceduresBranch = "100",
                CrCasSysAdministrativeProceduresDate = DateTime.Now.Date,
                CrCasSysAdministrativeProceduresTime = DateTime.Now.TimeOfDay,
                CrCasSysAdministrativeProceduresTargeted = Targeted,
                CrCasSysAdministrativeProceduresDebit = Debit,
                CrCasSysAdministrativeProceduresCreditor = Creditor,
                CrCasSysAdministrativeProceduresUserInsert = userCode,
                CrCasSysAdministrativeProceduresArDescription = procedure.CrMasSysProceduresArName,
                CrCasSysAdministrativeProceduresEnDescription = procedure.CrMasSysProceduresEnName,
                CrCasSysAdministrativeProceduresStatus = Status.Insert,
                CrCasSysAdministrativeProceduresReasons = Reasons,
            };
            if (await _unitOfWork.CrCasSysAdministrativeProcedure.AddAsync(crCasSysAdministrativeProcedure) != null) return crCasSysAdministrativeProcedure;
            return null;
        }
    }
    
}
