using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Ui.ViewModels.BS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

namespace Bnan.Ui.Areas.Base.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserManager<CrMasUserInformation> _userManager;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string[]> setTitle(string mainTaskCode, string subTaskCode, string systemCode)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            string MainTaskName;
            string SubTaskName;
            string SystemName;
            string userName;

            string currentCulture = CultureInfo.CurrentCulture.Name;
            if (currentCulture == "en-US")
            {
                MainTaskName = _unitOfWork.CrMasSysMainTasks.GetById(mainTaskCode).CrMasSysMainTasksEnName;
                SubTaskName = _unitOfWork.CrMasSysSubTasks.GetById(subTaskCode).CrMasSysSubTasksEnName;
                SystemName = _unitOfWork.CrMasSysSystems.GetById(systemCode).CrMasSysSystemEnName;
                userName = currentUser.CrMasUserInformationEnName;
            }
            else
            {
                MainTaskName = _unitOfWork.CrMasSysMainTasks.GetById(mainTaskCode).CrMasSysMainTasksArName;
                SubTaskName = _unitOfWork.CrMasSysSubTasks.GetById(subTaskCode).CrMasSysSubTasksArName;
                SystemName = _unitOfWork.CrMasSysSystems.GetById(systemCode).CrMasSysSystemArName;
                userName = currentUser.CrMasUserInformationArName;

            }

            string[] titles = { SystemName, MainTaskName, SubTaskName, userName };
            return titles;
        }

        public async Task<(CrMasSysMainTask, CrMasSysSubTask, CrMasSysSystem, CrMasUserInformation)> SetTrace(string mainTaskCode, string subTaskCode, string systemCode)
        {
            var mainTask = _unitOfWork.CrMasSysMainTasks.GetById(mainTaskCode);
            var subTask = _unitOfWork.CrMasSysSubTasks.GetById(subTaskCode);
            var system = _unitOfWork.CrMasSysSystems.GetById(systemCode);
            var currentUser = await _userManager.GetUserAsync(User);
            return (mainTask, subTask, system, currentUser);
        }

        //Check The Sub Validation of User With Default ActionResult(Index, Home, MAS)
        public async Task<bool> CheckUserSubValidation(string subTaskCode)
        {
            var usersubvalidation = await _userManager.Users.Include(l => l.CrMasUserSubValidations).Include(l => l.CrMasUserMainValidations).FirstOrDefaultAsync(l => l.UserName == User.Identity.Name);
            if (usersubvalidation?.CrMasUserSubValidations == null
                || usersubvalidation.CrMasUserSubValidations.Count == 0
                || usersubvalidation.CrMasUserSubValidations.FirstOrDefault(l => l.CrMasUserSubValidationSubTasks == subTaskCode)?.CrMasUserSubValidationAuthorization == false)
            {
                return false;

            }

            return true;

        }

        //Check The Sub Validation of User With Action Result
        [HttpGet]
        public async Task<bool> CheckUserSubValidationProcdures(string subTaskCodeProcdure, string status)
        {
            var usersubvalidation = await _userManager.Users.Include(l => l.CrMasUserSubValidations)
                                                            .Include(l => l.CrMasUserMainValidations)
                                                            .Include(l => l.CrMasUserProceduresValidations)
                                                            .FirstOrDefaultAsync(l => l.UserName == User.Identity.Name);

            if (usersubvalidation?.CrMasUserProceduresValidations != null || usersubvalidation?.CrMasUserProceduresValidations.Count != 0)
            {
                if (status == Status.Insert && usersubvalidation?.CrMasUserProceduresValidations
                                    .FirstOrDefault(l => l.CrMasUserProceduresValidationSubTasks == subTaskCodeProcdure)?
                                    .CrMasUserProceduresValidationInsertAuthorization == false)
                {
                    return false;
                }
                else if (status == Status.Deleted && usersubvalidation?.CrMasUserProceduresValidations
                                  .FirstOrDefault(l => l.CrMasUserProceduresValidationSubTasks == subTaskCodeProcdure)?
                                  .CrMasUserProceduresValidationDeleteAuthorization == false)
                {
                    return false;
                }
                else if (status == Status.Hold && usersubvalidation?.CrMasUserProceduresValidations
                               .FirstOrDefault(l => l.CrMasUserProceduresValidationSubTasks == subTaskCodeProcdure)?
                               .CrMasUserProceduresValidationHoldAuthorization == false)
                {
                    return false;
                }
                else if (status == Status.Save && usersubvalidation?.CrMasUserProceduresValidations
                             .FirstOrDefault(l => l.CrMasUserProceduresValidationSubTasks == subTaskCodeProcdure)?
                             .CrMasUserProceduresValidationUpDateAuthorization == false)
                {
                    return false;
                }
                else if (status == Status.UnDeleted && usersubvalidation?.CrMasUserProceduresValidations
                          .FirstOrDefault(l => l.CrMasUserProceduresValidationSubTasks == subTaskCodeProcdure)?
                          .CrMasUserProceduresValidationUnDeleteAuthorization == false)
                {
                    return false;
                }
                else if (status == Status.UnHold && usersubvalidation?.CrMasUserProceduresValidations
                        .FirstOrDefault(l => l.CrMasUserProceduresValidationSubTasks == subTaskCodeProcdure)?
                        .CrMasUserProceduresValidationUnHoldAuthorization == false)
                {
                    return false;
                }


            }

            return true;
        }

        [HttpGet]
        public async Task<bool> CheckAuth(string branchCode, string salespoint,string Balance,string CarsCount,string status)
        {
            return false;
        }


        public async Task<BSLayoutVM> GetBranchesAndLayout()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var userInformation = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationLessor == lessorCode && x.CrMasUserInformationCode == userLogin.CrMasUserInformationCode, new[] { "CrMasUserBranchValidities.CrMasUserBranchValidity1" });
            var branchesValidite = userInformation.CrMasUserBranchValidities.Where(x => x.CrMasUserBranchValidityBranchStatus == Status.Active);

            List<CrCasBranchInformation> branches = branchesValidite != null
                ? branchesValidite.Select(item => item.CrMasUserBranchValidity1).ToList()
                : new List<CrCasBranchInformation>();

            var selectBranch = userLogin.CrMasUserInformationDefaultBranch;
            if (selectBranch == null || selectBranch == "000") selectBranch = "100";
            var checkBranch = branches.Find(x => x.CrCasBranchInformationCode == selectBranch);
            if (checkBranch == null) selectBranch = branches.FirstOrDefault()?.CrCasBranchInformationCode;

            var branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == selectBranch&&x.CrCasBranchInformationLessor==lessorCode);

            return new BSLayoutVM
            {
                CrCasBranchInformations = branches,
                SelectedBranch = selectBranch,
                CrCasBranchInformation = branch,
                UserInformation = userInformation,
            };
        }
    }
}
