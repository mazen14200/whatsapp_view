using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface ITransferToFromRenter
    {
        Task<bool> AddAccountReceiptTransferToRenter(string AdmintritiveNo, string RenterId, string userCode,string ProcedureCode,string ReferenceType, string lessorCode, string BankNo, string AccountNo, string TotalAmountPayment,string TotalAmountReceipt, string reasons);
        Task<bool> UpdateRenterInformation(string RenterId, string IBanNo, string BankNo);
        Task<bool> UpdateCasRenterLessorTransferFrom(string RenterId, string lessorCode, string Amount);
        Task<bool> UpdateCasRenterLessorTransferTo(string RenterId, string lessorCode, string Amount);
        Task<CrCasSysAdministrativeProcedure> SaveAdminstritiveTransferRenter(string AdmintritiveCode, string userCode, string ProcedureCode, string ClassificationCode, string LessorCode,
                                                                   string? Targeted, decimal? Debit, decimal? Creditor, string? Reasons);

    }
}
