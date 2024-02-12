using Bnan.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class MembershipConditions : IMembershipConditions
    {
        private IUnitOfWork _unitOfWork;

        public MembershipConditions(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddRenterMembership(string LessorCode, string code, string amount, string link1, string km, string link2, string contractNo, bool isActivate,string groupChar)
        {

            var membership = await _unitOfWork.CrCasLessorMembership.FindAsync(x => x.CrCasLessorMembershipConditionsLessor == LessorCode && x.CrCasLessorMembershipConditions == code);
            if (membership != null) { 
                membership.CrCasLessorMembershipConditionsActivate = isActivate;
                membership.CrCasLessorMembershipConditionsAmount = decimal.Parse(amount, CultureInfo.InvariantCulture);
                membership.CrCasLessorMembershipConditionsLink1= link1;
                membership.CrCasLessorMembershipConditionsKm=int.Parse(km);
                membership.CrCasLessorMembershipConditionsLink2= link2;
                membership.CrCasLessorMembershipConditionsContractNo= int.Parse(contractNo);
                membership.CrCasLessorMembershipConditionsGroup= groupChar;
                _unitOfWork.CrCasLessorMembership.Update(membership);
                return true;
            }
            return false;
        }
    }
}
