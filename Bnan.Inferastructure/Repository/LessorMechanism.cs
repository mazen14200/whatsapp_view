using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class LessorMechanism : ILessorMechanism
    {
        private IUnitOfWork _unitOfWork;

        public LessorMechanism(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddLessorMechanism(string LessorCode)
        {
            var sysProcedures = _unitOfWork.CrMasSysProcedure.FindAll(l => l.CrMasSysProceduresStatus == "A" && l.CrMasSysProceduresSubjectAlert == true);
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(LessorCode);
            if (lessor != null)
            {
                foreach (var item in sysProcedures)
                {
                    CrCasLessorMechanism lessorMechanism = new CrCasLessorMechanism
                    {
                        CrCasLessorMechanismCode = lessor.CrMasLessorInformationCode,
                        CrCasLessorMechanismProcedures = item.CrMasSysProceduresCode,
                        CrCasLessorMechanismProceduresClassification = item.CrMasSysProceduresClassification,
                        CrCasLessorMechanismActivate = true,
                        CrCasLessorMechanismDaysAlertAboutExpire = (int?)item.CrMasSysProceduresDaysAlertAboutExpire,
                        CrCasLessorMechanismKmAlertAboutExpire = (int?)item.CrMasSysProceduresKmAlertAboutExpire,
                    };

                    await _unitOfWork.CrCasLessorMechanism.AddAsync(lessorMechanism);
                }
            }

            return true;
        }
    }
}
