using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class UserProcedureValiditionService:IUserProcedureValidition
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProcedureValiditionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddProceduresValiditionsForEachUser(string userCode, string systemCode)
        {
            var subTasks = _unitOfWork.CrMasSysSubTasks.FindAll(x => x.CrMasSysSubTasksSystemCode == systemCode);
            foreach (var item in subTasks)
            {
                if (item.CrMasSysSubTasksCode != "2206001" || item.CrMasSysSubTasksCode != "2206002" || item.CrMasSysSubTasksCode != "2206003")
                {
                    CrMasUserProceduresValidation crMasUserProceduresValidation = new CrMasUserProceduresValidation();
                    crMasUserProceduresValidation.CrMasUserProceduresValidationCode = userCode;
                    crMasUserProceduresValidation.CrMasUserProceduresValidationSystem = systemCode;
                    crMasUserProceduresValidation.CrMasUserProceduresValidationMainTask = item.CrMasSysSubTasksMainCode;
                    crMasUserProceduresValidation.CrMasUserProceduresValidationSubTasks = item.CrMasSysSubTasksCode;
                    crMasUserProceduresValidation.CrMasUserProceduresValidationDeleteAuthorization = false;
                    crMasUserProceduresValidation.CrMasUserProceduresValidationUnDeleteAuthorization = false;
                    crMasUserProceduresValidation.CrMasUserProceduresValidationUpDateAuthorization = false;
                    crMasUserProceduresValidation.CrMasUserProceduresValidationInsertAuthorization = false;
                    crMasUserProceduresValidation.CrMasUserProceduresValidationHoldAuthorization = false;
                    crMasUserProceduresValidation.CrMasUserProceduresValidationUnHoldAuthorization = false;
                    _unitOfWork.CrMasUserProceduresValidations.Add(crMasUserProceduresValidation);
                }
                
            }
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
