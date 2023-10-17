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
    public class Beneficiary : IBeneficiary
    {
        private IUnitOfWork _unitOfWork;

        public Beneficiary(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddBeneficiary(string LessorCode)
        {
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(LessorCode);
            var lessorBeneficiary = new CrCasBeneficiary
            {
                CrCasBeneficiaryCode = lessor.CrMasLessorInformationGovernmentNo,
                CrCasBeneficiaryLessorCode = lessor.CrMasLessorInformationCode,
                CrCasBeneficiarySector = "2",
                CrCasBeneficiaryArName = lessor.CrMasLessorInformationArLongName,
                CrCasBeneficiaryEnName = lessor.CrMasLessorInformationEnLongName,
                CrCasBeneficiaryStatus = Status.Active
            };
            await _unitOfWork.CrCasBeneficiary.AddAsync(lessorBeneficiary);
            return true;
        }
    }
}
