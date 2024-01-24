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
        Task<bool> AddAccountReceiptTransferToRenter(string AdmintritiveNo, string RenterId, string userCode, string lessorCode, string BankNo, string AccountNo, string TotalAmount, string reasons);
        Task<bool> UpdateRenterInformation(string RenterId, string IBanNo, string BankNo);
        Task<bool> UpdateCasRenterLessor(string RenterId, string lessorCode, string Amount);
        Task<CrCasSysAdministrativeProcedure> SaveAdminstritiveTransferRenter(string AdmintritiveCode, string userCode, string ProcedureCode, string ClassificationCode, string LessorCode,
                                                                   string? Targeted, decimal? Debit, decimal? Creditor, string? Reasons);

    }
}
