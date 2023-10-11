using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class UserMainValidtionService : IUserMainValidtion
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _UserService;

        public UserMainValidtionService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _UserService = userService;
        }

        public async Task<bool> AddMainValidaitionToUserWhenAddLessor(string userCode)
        {
            var MainTasks = _unitOfWork.CrMasSysMainTask.FindAll(l => l.CrMasSysMainTasksStatus == "A" && l.CrMasSysMainTasksSystem == "2");
            var user = await _UserService.GetUserByUserNameAsync(userCode);


            foreach (var item in MainTasks)
            {
                CrMasUserMainValidation CrMasUserMainValidation = new CrMasUserMainValidation();
                if (item.CrMasSysMainTasksCode == "206")
                {
                    CrMasUserMainValidation = new CrMasUserMainValidation
                    {
                        CrMasUserMainValidationUser = user.CrMasUserInformationCode,
                        CrMasUserMainValidationMainTasks = item.CrMasSysMainTasksCode,
                        CrMasUserMainValidationMainSystem = "2",
                        CrMasUserMainValidationAuthorization = true
                    };
                    await _unitOfWork.CrMasUserMainValidations.AddAsync(CrMasUserMainValidation);
                }
                /*  else
                  {
                      CrMasUserMainValidation = new CrMasUserMainValidation
                      {
                          CrMasUserMainValidationUser = user.CrMasUserInformationCode,
                          CrMasUserMainValidationMainTasks = item.CrMasSysMainTasksCode,
                          CrMasUserMainValidationMainSystem = "2",
                          CrMasUserMainValidationAuthorization = false
                      };
                  }*/

            }



            return true;
        }

        public async Task<bool> AddMainValiditionsForEachUser(string userCode, string systemCode)
        {
            var mainTasks = _unitOfWork.CrMasSysMainTasks.FindAll(x => x.CrMasSysMainTasksSystem == systemCode);
            foreach (var item in mainTasks)
            {
                if (item.CrMasSysMainTasksCode != "206")
                {
                    CrMasUserMainValidation crMasUserMainValidation = new CrMasUserMainValidation();
                    crMasUserMainValidation.CrMasUserMainValidationUser = userCode;
                    crMasUserMainValidation.CrMasUserMainValidationMainSystem = systemCode;
                    crMasUserMainValidation.CrMasUserMainValidationMainTasks = item.CrMasSysMainTasksCode;
                    crMasUserMainValidation.CrMasUserMainValidationAuthorization = false;
                    _unitOfWork.CrMasUserMainValidations.Add(crMasUserMainValidation);
                }

            }
            await _unitOfWork.CompleteAsync();
            return true;

        }
    }
}
