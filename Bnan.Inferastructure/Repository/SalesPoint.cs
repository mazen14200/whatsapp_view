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
    public class SalesPoint : ISalesPoint
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<CrMasUserInformation> _userManager;
        private readonly SignInManager<CrMasUserInformation> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SalesPoint(IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, SignInManager<CrMasUserInformation> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddSalesPoint(CrCasBranchInformation CrCasBranchInformation)
        {
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(CrCasBranchInformation.CrCasBranchInformationLessor);
            var lessorImg = new CrCasAccountSalesPoint
            {
                CrCasAccountSalesPointCode = $"{lessor.CrMasLessorInformationCode}{CrCasBranchInformation.CrCasBranchInformationCode}0001",
                CrCasAccountSalesPointLessor = CrCasBranchInformation.CrCasBranchInformationLessor,
                CrCasAccountSalesPointBranchStatus = "A",
                CrCasAccountSalesPointBrn = CrCasBranchInformation.CrCasBranchInformationCode,
                CrCasAccountSalesPointBank = "00",
                CrCasAccountSalesPointSerial = "01",
                CrCasAccountSalesPointAccountBank = $"{lessor.CrMasLessorInformationCode}0001",
                CrCasAccountSalesPointNo = $"{lessor.CrMasLessorInformationCode}10001",
                CrCasAccountSalesPointArName = $"صندوق فرع {CrCasBranchInformation.CrCasBranchInformationArShortName}",
                CrCasAccountSalesPointEnName = $"Branch Fund {CrCasBranchInformation.CrCasBranchInformationEnShortName}",
                CrCasAccountSalesPointTotalBalance = 0,
                CrCasAccountSalesPointTotalAvailable = 0,
                CrCasAccountSalesPointTotalReserved = 0,
                CrCasAccountSalesPointBankStatus = "A",
                CrCasAccountSalesPointStatus = "A",

            };
            await _unitOfWork.CrCasAccountSalesPoint.AddAsync(lessorImg);
            return true;
        }

        public async Task<bool> AddSalesPointDefault(string LessorCode)
        {
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(LessorCode);
            var lessorImg = new CrCasAccountSalesPoint
            {
                CrCasAccountSalesPointCode = $"{lessor.CrMasLessorInformationCode}1000001",
                CrCasAccountSalesPointLessor = lessor.CrMasLessorInformationCode,
                CrCasAccountSalesPointBranchStatus = "A",
                CrCasAccountSalesPointBrn = "100",
                CrCasAccountSalesPointBank = "00",
                CrCasAccountSalesPointSerial = "01",
                CrCasAccountSalesPointAccountBank = $"{lessor.CrMasLessorInformationCode}0001",
                CrCasAccountSalesPointNo = $"{lessor.CrMasLessorInformationCode}10001",
                CrCasAccountSalesPointArName = "صندوق الفرع الرئيسي ",
                CrCasAccountSalesPointEnName = "Main Branch Fund",
                CrCasAccountSalesPointTotalBalance = 0,
                CrCasAccountSalesPointTotalAvailable = 0,
                CrCasAccountSalesPointTotalReserved = 0,
                CrCasAccountSalesPointBankStatus = "A",
                CrCasAccountSalesPointStatus = "A",




            };
            await _unitOfWork.CrCasAccountSalesPoint.AddAsync(lessorImg);
            return true;
        }

        public async Task<CrCasAccountSalesPoint> CreateSalesPoint(CrCasAccountSalesPoint model,string userCode)
        {
            var currentUser = await _userManager.FindByNameAsync(userCode);

            var Lrecord = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == currentUser.CrMasUserInformationLessor &&
                x.CrCasAccountSalesPointBrn == model.CrCasAccountSalesPointBrn
                && x.CrCasAccountSalesPointBank == model.CrCasAccountSalesPointBank).Max(x => x.CrCasAccountSalesPointCode.Substring(x.CrCasAccountSalesPointCode.Length - 2, 2));
            string Serial;
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                Serial = val.ToString("00");
            }
            else
            {
                Serial = "01";
            }
            CrCasAccountSalesPoint crCasAccountSalesPoint = new CrCasAccountSalesPoint()
            {
                CrCasAccountSalesPointCode = model.CrCasAccountSalesPointLessor + model.CrCasAccountSalesPointBrn + model.CrCasAccountSalesPointBank + Serial,
                CrCasAccountSalesPointLessor = model.CrCasAccountSalesPointLessor,
                CrCasAccountSalesPointBrn = model.CrCasAccountSalesPointBrn,
                CrCasAccountSalesPointBank = model.CrCasAccountSalesPointBank,
                CrCasAccountSalesPointSerial = Serial,
                CrCasAccountSalesPointAccountBank = model.CrCasAccountSalesPointAccountBank,
                CrCasAccountSalesPointNo = model.CrCasAccountSalesPointNo,
                CrCasAccountSalesPointArName = model.CrCasAccountSalesPointArName,
                CrCasAccountSalesPointEnName = model.CrCasAccountSalesPointEnName,
                CrCasAccountSalesPointTotalAvailable = 0,
                CrCasAccountSalesPointTotalBalance = 0,
                CrCasAccountSalesPointTotalReserved = 0,
                CrCasAccountSalesPointBranchStatus = "A",
                CrCasAccountSalesPointStatus = "A",
                CrCasAccountSalesPointBankStatus = "A",
                CrCasAccountSalesPointReasons = model.CrCasAccountSalesPointReasons,
            };
            await _unitOfWork.CrCasAccountSalesPoint.AddAsync(crCasAccountSalesPoint);
            return crCasAccountSalesPoint;
        }

       
    }
}
