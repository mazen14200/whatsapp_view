using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class AdminstritiveProcedures : IAdminstritiveProcedures
    {
        private readonly UserManager<CrMasUserInformation> _userManager;
        private readonly SignInManager<CrMasUserInformation> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public AdminstritiveProcedures(IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, SignInManager<CrMasUserInformation> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> SaveAdminstritive(string userCode, string SectorId, string ProcedureCode, string ClassificationCode, string LessorCode,
            string BranchCode, string? Targeted, decimal? Debit, decimal? Creditor, string? DocumentNo, DateTime? DocumentDate, DateTime? DocumentStartDate, DateTime? DocumentEndDate,
            string? CarFrom, string? CarTo, string ArDescription, string EnDescription, string Status, string? Reasons)
        {

            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var currentUser = await _userManager.FindByIdAsync(userCode);
            var Lrecord = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresLessor == currentUser.CrMasUserInformationLessor &&
                x.CrCasSysAdministrativeProceduresCode == ProcedureCode
                && x.CrCasSysAdministrativeProceduresSector == SectorId
                && x.CrCasSysAdministrativeProceduresYear == y).Max(x => x.CrCasSysAdministrativeProceduresNo.Substring(x.CrCasSysAdministrativeProceduresNo.Length - 6, 6));
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
            CrCasSysAdministrativeProcedure crCasSysAdministrativeProcedure = new CrCasSysAdministrativeProcedure()
            {
                CrCasSysAdministrativeProceduresNo = y + "-" + SectorId + ProcedureCode + "-" + LessorCode + BranchCode +"-"+ Serial,
                CrCasSysAdministrativeProceduresYear = y,
                CrCasSysAdministrativeProceduresSector = SectorId,
                CrCasSysAdministrativeProceduresCode = ProcedureCode,
                CrCasSysAdministrativeProceduresClassification = ClassificationCode,
                CrCasSysAdministrativeProceduresLessor = LessorCode,
                CrCasSysAdministrativeProceduresBranch = BranchCode,
                CrCasSysAdministrativeProceduresDate = DateTime.Now.Date,
                CrCasSysAdministrativeProceduresTime = DateTime.Now.TimeOfDay,
                CrCasSysAdministrativeProceduresTargeted = Targeted,
                CrCasSysAdministrativeProceduresDebit = Debit,
                CrCasSysAdministrativeProceduresCreditor = Creditor,
                CrCasSysAdministrativeProceduresDocNo = DocumentNo,
                CrCasSysAdministrativeProceduresDocDate = DocumentDate,
                CrCasSysAdministrativeProceduresDocStartDate = DocumentStartDate,
                CrCasSysAdministrativeProceduresDocEndDate = DocumentEndDate,
                CrCasSysAdministrativeProceduresCarFrom = CarFrom,
                CrCasSysAdministrativeProceduresCarTo = CarTo,
                CrCasSysAdministrativeProceduresUserInsert = userCode,
                CrCasSysAdministrativeProceduresArDescription = ArDescription,
                CrCasSysAdministrativeProceduresEnDescription = EnDescription,
                CrCasSysAdministrativeProceduresStatus = Status,
                CrCasSysAdministrativeProceduresReasons = Reasons,
            };

            await _unitOfWork.CrCasSysAdministrativeProcedure.AddAsync(crCasSysAdministrativeProcedure);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> SaveAdminstritiveForRepairCar(string userCode, string LessorCode, string BranchCode, string? Targeted, DateTime? DocumentDate,
                                                              string ArDescription, string EnDescription, string Status, string? Reasons)
        {
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var currentUser = await _userManager.FindByIdAsync(userCode);
            var Lrecord = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresLessor == currentUser.CrMasUserInformationLessor &&
                x.CrCasSysAdministrativeProceduresCode == "214"
                && x.CrCasSysAdministrativeProceduresSector == "1"
                && x.CrCasSysAdministrativeProceduresYear == y).Max(x => x.CrCasSysAdministrativeProceduresNo.Substring(x.CrCasSysAdministrativeProceduresNo.Length - 6, 6));
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

            CrCasSysAdministrativeProcedure crCasSysAdministrativeProcedure = new CrCasSysAdministrativeProcedure()
            {
                CrCasSysAdministrativeProceduresNo = y + "-" + "1" + "214" + "-" + LessorCode + BranchCode + "-" + Serial,
                CrCasSysAdministrativeProceduresYear = y,
                CrCasSysAdministrativeProceduresSector = "1",
                CrCasSysAdministrativeProceduresCode = "214",
                CrCasSysAdministrativeProceduresClassification = "20",
                CrCasSysAdministrativeProceduresLessor = LessorCode,
                CrCasSysAdministrativeProceduresBranch = BranchCode,
                CrCasSysAdministrativeProceduresDate = DateTime.Now.Date,
                CrCasSysAdministrativeProceduresTime = DateTime.Now.TimeOfDay,
                CrCasSysAdministrativeProceduresTargeted = Targeted,
                CrCasSysAdministrativeProceduresDocNo = y + "-" + "1" + "214" + "-" + LessorCode + BranchCode + "-" + Serial,
                CrCasSysAdministrativeProceduresDocDate = DocumentDate,
                CrCasSysAdministrativeProceduresUserInsert = userCode,
                CrCasSysAdministrativeProceduresArDescription = ArDescription,
                CrCasSysAdministrativeProceduresEnDescription = EnDescription,
                CrCasSysAdministrativeProceduresStatus = Status,
                CrCasSysAdministrativeProceduresReasons = Reasons,
            };

            await _unitOfWork.CrCasSysAdministrativeProcedure.AddAsync(crCasSysAdministrativeProcedure);
            return true;

        }
    }
}
