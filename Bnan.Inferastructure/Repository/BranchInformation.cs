using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class BranchInformation : IBranchInformation
    {
        private IUnitOfWork _unitOfWork;

        public BranchInformation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddBranchInformation(CrCasBranchInformation CrCasBranchInformation)
        {
            var BranchInformation = new CrCasBranchInformation
            {
                CrCasBranchInformationLessor = CrCasBranchInformation.CrCasBranchInformationLessor,
                CrCasBranchInformationCode = CrCasBranchInformation.CrCasBranchInformationCode,
                CrCasBranchInformationGovernmentNo = CrCasBranchInformation.CrCasBranchInformationGovernmentNo,
                CrCasBranchInformationTaxNo = CrCasBranchInformation.CrCasBranchInformationTaxNo,
                CrCasBranchInformationArName = CrCasBranchInformation.CrCasBranchInformationArName,
                CrCasBranchInformationArShortName = CrCasBranchInformation.CrCasBranchInformationArShortName,
                CrCasBranchInformationEnName = CrCasBranchInformation.CrCasBranchInformationEnName,
                CrCasBranchInformationEnShortName = CrCasBranchInformation.CrCasBranchInformationEnShortName,
                CrCasBranchInformationDirectorArName = CrCasBranchInformation.CrCasBranchInformationDirectorArName,
                CrCasBranchInformationDirectorEnName = CrCasBranchInformation.CrCasBranchInformationDirectorEnName,
                CrCasBranchInformationMobile = CrCasBranchInformation.CrCasBranchInformationMobile,
                CrMasBranchInformationMobileKey = CrCasBranchInformation.CrMasBranchInformationMobileKey,
                CrCasBranchInformationTelephone = CrCasBranchInformation.CrCasBranchInformationTelephone,
                CrMasBranchInformationTeleKey = CrCasBranchInformation.CrMasBranchInformationMobileKey,
                CrCasBranchInformationDirectorSignature = CrCasBranchInformation.CrCasBranchInformationDirectorSignature,
                CrCasBranchInformationReasons = CrCasBranchInformation.CrCasBranchInformationReasons,
                CrCasBranchInformationAvailableBalance = 0,
                CrCasBranchInformationReservedBalance = 0,
                CrCasBranchInformationTotalBalance = 0,
                CrCasBranchInformationStatus = "A"

            };
            await _unitOfWork.CrCasBranchInformation.AddAsync(BranchInformation);
            return true;
        }

        public async Task<bool> AddBranchInformationDefault(string LesssorCode)
        {
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(LesssorCode);
            var BranchInformation = new CrCasBranchInformation
            {
                CrCasBranchInformationLessor = lessor.CrMasLessorInformationCode,
                CrCasBranchInformationCode = "100",
                CrCasBranchInformationGovernmentNo = lessor.CrMasLessorInformationGovernmentNo,
                CrCasBranchInformationTaxNo = lessor.CrMasLessorInformationTaxNo,
                CrCasBranchInformationArName = "الإدارة العامة - الفرع الرئيسي",
                CrCasBranchInformationArShortName = "الرئيسي",
                CrCasBranchInformationEnName = "General Administration - Head office",
                CrCasBranchInformationEnShortName = "Head office",
                CrCasBranchInformationDirectorArName = lessor.CrMasLessorInformationDirectorArName,
                CrCasBranchInformationDirectorEnName = lessor.CrMasLessorInformationDirectorEnName,
                CrMasBranchInformationMobileKey = lessor.CrMasLessorInformationCommunicationMobileKey,
                CrCasBranchInformationMobile = lessor.CrMasLessorInformationCommunicationMobile,
                CrMasBranchInformationTeleKey = lessor.CrMasLessorInformationCallFreeKey,
                CrCasBranchInformationTelephone = lessor.CrMasLessorInformationCallFree,
                CrCasBranchInformationDirectorSignature = "~/images/common/DefualtUserSignature",
                CrCasBranchInformationAvailableBalance = 0,
                CrCasBranchInformationReservedBalance = 0,
                CrCasBranchInformationTotalBalance = 0,
                CrCasBranchInformationStatus = "A"

            };
            await _unitOfWork.CrCasBranchInformation.AddAsync(BranchInformation);
            return true;
        }
    }
}
