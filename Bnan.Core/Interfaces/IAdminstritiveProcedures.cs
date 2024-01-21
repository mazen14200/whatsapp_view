using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IAdminstritiveProcedures
    {
        Task<bool> SaveAdminstritive(string userCode, string SectorId, string ProcedureCode,
            string ClassificationCode, string LessorCode, string BranchCode, string? Targeted, decimal? Debit, decimal? Creditor, string? DocumentNo, DateTime? DocumentDate,
            DateTime? DocumentStartDate, DateTime? DocumentEndDate, string? CarFrom, string? CarTo, string ArDescription, string EnDescription, string Status, string? Reasons);
        Task<bool> SaveAdminstritiveForRepairCar(string userCode , string LessorCode, string BranchCode, string? Targeted, DateTime? DocumentDate,
                                                 string ArDescription, string EnDescription, string Status, string? Reasons);

        Task<CrCasSysAdministrativeProcedure> SaveAdminstritiveCustody(string userCode, string LessorCode, string BranchCode, string? Targeted, string Creditor, string Debit, string? Reasons,List<string> ReceiptsNo);
    }
}
