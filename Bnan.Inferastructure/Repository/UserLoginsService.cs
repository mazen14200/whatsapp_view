using Azure;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class UserLoginsService : BaseRepository<CrMasUserLogin>, IUserLoginsService
    {
        private readonly UserManager<CrMasUserInformation> _userManager;
        private readonly SignInManager<CrMasUserInformation> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;


        public UserLoginsService(UserManager<CrMasUserInformation> userManager, SignInManager<CrMasUserInformation> signInManager, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, BnanKSAContext db) : base(db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        public async Task SaveTracing(string userCode, string operationAr, string operationEn,string mainTaskCode,string subTaskCode,
            string mainTaskAr, string subTaskAr, string mainTaskEn, string subTaskEn,string systemCode, string systemAr, string systemEn)
        {
            int newLoginNo;
            CrMasUserLogin userLogin = new CrMasUserLogin();

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            // to get last record to auto increament
            var userLogins = _unitOfWork.CrMasUserLogins.Count();
            if (userLogins==0)
            {
                newLoginNo = 1;
            }
            else
            {
                var lastUserLogin = _unitOfWork.CrMasUserLogins.GetAll().OrderByDescending(x => x.CrMasUserLoginNo).FirstOrDefault(x => x.CrMasUserLoginNo != null);
                newLoginNo = lastUserLogin != null ? lastUserLogin.CrMasUserLoginNo + 1 : 1;
            }

            var currentDate=DateTime.Now;
            TimeSpan loginEntryTime = new TimeSpan(currentDate.Hour, currentDate.Minute, currentDate.Second);
            if (user != null)
            {
                userLogin.CrMasUserLoginNo = newLoginNo;
                userLogin.CrMasUserLoginEntryDate = currentDate.Date;
                userLogin.CrMasUserLoginEntryTime = loginEntryTime;
                userLogin.CrMasUserLoginUser = user.CrMasUserInformationCode;
                userLogin.CrMasUserLoginLessor = user.CrMasUserInformationLessor;
                userLogin.CrMasUserLoginBranch = user.CrMasUserInformationDefaultBranch;
                userLogin.CrMasUserLoginSystem = systemCode;
                userLogin.CrMasUserLoginMainTask = mainTaskCode;
                userLogin.CrMasUserLoginSubTask = subTaskCode;
                userLogin.CrMasUserLoginArOperation = operationAr;
                userLogin.CrMasUserLoginEnOperation = operationEn;
                //userLogin.CrMasUserLoginConcatenateOperationArDescription = $"{user.CrMasUserInformationArName.Trim()} - {systemAr} - {mainTaskAr} - {subTaskAr} - {operationAr}";
                //userLogin.CrMasUserLoginConcatenateOperationEnDescription = $"{user.CrMasUserInformationEnName.Trim()} - {systemEn} - {mainTaskEn} - {mainTaskEn} - {operationEn}";
                userLogin.CrMasUserLoginConcatenateOperationArDescription = $"{systemAr} - {mainTaskAr} - {subTaskAr} - {operationAr}";
                userLogin.CrMasUserLoginConcatenateOperationEnDescription = $"{systemEn} - {mainTaskEn} - {subTaskEn} - {operationEn}";
                await  _unitOfWork.CrMasUserLogins.AddAsync(userLogin);
                await _unitOfWork.CompleteAsync();
                
            }
        }

        public async Task SaveTracing(string userCode, string RecordAr, string RecordEn, string operationAr, string operationEn, string mainTaskCode, string subTaskCode,
            string mainTaskAr, string subTaskAr, string mainTaskEn, string subTaskEn, string systemCode, string systemAr, string systemEn)
        {
            int newLoginNo;
            CrMasUserLogin userLogin = new CrMasUserLogin();

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            // to get last record to auto increament
            var userLogins = _unitOfWork.CrMasUserLogins.Count();
            if (userLogins == 0)
            {
                newLoginNo = 1;
            }
            else
            {
                var lastUserLogin = _unitOfWork.CrMasUserLogins.GetAll().OrderByDescending(x => x.CrMasUserLoginNo).FirstOrDefault(x => x.CrMasUserLoginNo != null);
                newLoginNo = lastUserLogin != null ? lastUserLogin.CrMasUserLoginNo + 1 : 1;
            }

            var currentDate = DateTime.Now;
            TimeSpan loginEntryTime = new TimeSpan(currentDate.Hour, currentDate.Minute, currentDate.Second);
            if (user != null)
            {
                userLogin.CrMasUserLoginNo = newLoginNo;
                userLogin.CrMasUserLoginEntryDate = currentDate.Date;
                userLogin.CrMasUserLoginEntryTime = loginEntryTime;
                userLogin.CrMasUserLoginUser = user.CrMasUserInformationCode;
                userLogin.CrMasUserLoginLessor = user.CrMasUserInformationLessor;
                userLogin.CrMasUserLoginBranch = user.CrMasUserInformationDefaultBranch;
                userLogin.CrMasUserLoginSystem = systemCode;
                userLogin.CrMasUserLoginMainTask = mainTaskCode;
                userLogin.CrMasUserLoginSubTask = subTaskCode;
                userLogin.CrMasUserLoginArOperation = operationAr;
                userLogin.CrMasUserLoginEnOperation = operationEn;
                //userLogin.CrMasUserLoginConcatenateOperationArDescription = $"{user.CrMasUserInformationArName.Trim()} - {systemAr} - {mainTaskAr} - {subTaskAr} - {operationAr}";
                //userLogin.CrMasUserLoginConcatenateOperationEnDescription = $"{user.CrMasUserInformationEnName.Trim()} - {systemEn} - {mainTaskEn} - {mainTaskEn} - {operationEn}";
                userLogin.CrMasUserLoginConcatenateOperationArDescription = $"{systemAr} - {mainTaskAr} - {subTaskAr} - {operationAr} - {RecordAr}";
                userLogin.CrMasUserLoginConcatenateOperationEnDescription = $"{systemEn} - {mainTaskEn} - {subTaskEn} - {operationEn} - {RecordEn}";
                await _unitOfWork.CrMasUserLogins.AddAsync(userLogin);
                await _unitOfWork.CompleteAsync();

            }
        }
    }
}
