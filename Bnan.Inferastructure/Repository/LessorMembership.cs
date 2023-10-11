using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public class LessorMembership : ILessorMembership
    {
        private IUnitOfWork _unitOfWork;

        public LessorMembership(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddLessorMemberShip(string LessorCode)
        {
            var RenterMemberShip = await _unitOfWork.CrMasSupRenterMembership.GetAllAsync();
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(LessorCode);
            if (lessor != null)
            {
                foreach (var item in RenterMemberShip)
                {
                    CrCasLessorMembership lessorMembership = new CrCasLessorMembership
                    {
                        CrCasLessorMembershipConditions = item.CrMasSupRenterMembershipCode,
                        CrCasLessorMembershipConditionsLessor = lessor.CrMasLessorInformationCode,
                        CrCasLessorMembershipConditionsActivate = false,
                        CrCasLessorMembershipConditionsLink1 = "3",
                        CrCasLessorMembershipConditionsLink2 = "3",
                        CrCasLessorMembershipConditionsIsCorrecte = true,
                        CrCasLessorMembershipConditionsAmount = 0,
                        CrCasLessorMembershipConditionsContractNo = 0,
                        CrCasLessorMembershipConditionsKm = 0,

                    };

                    await _unitOfWork.CrCasLessorMembership.AddAsync(lessorMembership);
                }
            }

            return true;
        }
    }
}
