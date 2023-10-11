using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Bnan.Core.Repository
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<CrMasUserInformation> _userManager;
        private readonly SignInManager<CrMasUserInformation> _signInManager;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _access;
        private readonly IServiceProvider _ser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMainValidtion _UserMainValidtion;
        private readonly IUserSubValidition _UserSubValidition;

        public AuthService(UserManager<CrMasUserInformation> userManager,
            SignInManager<CrMasUserInformation> signInManager, IUserService userService, IHttpContextAccessor access, IServiceProvider ser, IUnitOfWork unitOfWork, IUserMainValidtion userMainValidtion, IUserSubValidition userSubValidition)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _access = access;
            _ser = ser;
            _unitOfWork = unitOfWork;
            _UserMainValidtion = userMainValidtion;
            _UserSubValidition = userSubValidition;
        }

        public async Task<bool> AddRoleAsync(CrMasUserInformation user, string role)
        {
            var Role = await _userManager.AddToRoleAsync(user, role);
            if (Role.Succeeded)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> AddUserDefault(string LessorCode)
        {
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(LessorCode);
            var user = new CrMasUserInformation
            {
                CrMasUserInformationCode = $"CAS{lessor.CrMasLessorInformationCode}",
                CrMasUserInformationPassWord = $"CAS{LessorCode}",
                CrMasUserInformationLessor = lessor.CrMasLessorInformationCode,
                CrMasUserInformationRemindMe = "رقم الشركة",
                CrMasUserInformationDefaultBranch = "100",
                CrMasUserInformationDefaultLanguage = "AR",
                CrMasUserInformationAuthorizationAdmin = true,
                CrMasUserInformationAuthorizationBnan = false,
                CrMasUserInformationAuthorizationBranch = false,
                CrMasUserInformationAuthorizationFoolwUp = false,
                CrMasUserInformationAuthorizationOwner = false,
                CrMasUserInformationArName = lessor.CrMasLessorInformationDirectorArName,
                CrMasUserInformationEnName = lessor.CrMasLessorInformationDirectorEnName,
                CrMasUserInformationTasksArName = "مدير الشركة و مسؤول النظام",
                CrMasUserInformationTasksEnName = " Company Manager And System Administrator",
                CrMasUserInformationAvailableBalance = 0,
                CrMasUserInformationReservedBalance = 0,
                CrMasUserInformationTotalBalance = 0,
                CrMasUserInformationCreditLimit = 0,
                CrMasUserInformationMobileNo = lessor.CrMasLessorInformationCommunicationMobile,
                CrMasUserInformationCallingKey = lessor.CrMasLessorInformationCommunicationMobileKey,
                Email = lessor.CrMasLessorInformationEmail,
                CrMasUserInformationExitTimer = 5,
                CrMasUserInformationChangePassWordLastDate = null,
                CrMasUserInformationEntryLastDate = null,
                CrMasUserInformationExitLastDate    = null,
                CrMasUserInformationPicture = "~/images/common/user.jpg",
                CrMasUserInformationSignature = "~/images/common/DefualtUserSignature.png",
                CrMasUserInformationOperationStatus = false,
                CrMasUserInformationStatus = "A",
                UserName = $"CAS{LessorCode}",
                Id  = $"CAS{LessorCode}",

            };
            var result = await _userManager.CreateAsync(user, user.CrMasUserInformationPassWord);
            if (result.Succeeded)
            {
                await AddRoleAsync(user, "CAS");
                await _UserMainValidtion.AddMainValidaitionToUserWhenAddLessor(user.CrMasUserInformationCode);
                await _UserSubValidition.AddSubValidaitionToUserWhenAddLessor(user.CrMasUserInformationCode);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckPassword(string username, string password)
        {
            var user = await _userService.GetUserByUserNameAsync(username);
            return (await _userManager.CheckPasswordAsync(user, password));
        }

        public async Task<SignInResult> LoginAsync(string username, string password)
        {
            var user = await _userService.GetUserByUserNameAsync(username);
            var result = await _signInManager.PasswordSignInAsync(user, password, false, true);
            return result;
        }

        public async Task<bool> RegisterAsync(CrMasUserInformation model)
        {
            var user = new CrMasUserInformation
            {
                UserName = model.CrMasUserInformationCode,
                CrMasUserInformationCode = model.CrMasUserInformationCode,
                Id = model.CrMasUserInformationCode,
                CrMasUserInformationEmail = model.CrMasUserInformationEmail,
                CrMasUserInformationPassWord = model.CrMasUserInformationCode,
                CrMasUserInformationArName = model.CrMasUserInformationArName,
                CrMasUserInformationEnName = model.CrMasUserInformationEnName,
                CrMasUserInformationCallingKey = model.CrMasUserInformationCallingKey,
                CrMasUserInformationMobileNo = model.CrMasUserInformationMobileNo,
                CrMasUserInformationDefaultLanguage = "AR",
                CrMasUserInformationLessor = "0000",
                CrMasUserInformationDefaultBranch = "000",
                CrMasUserInformationStatus = "A",
                CrMasUserInformationOperationStatus = false,
                CrMasUserInformationExitTimer = 5,
                CrMasUserInformationRemindMe = model.CrMasUserInformationCode,
                CrMasUserInformationAvailableBalance = 0,
                CrMasUserInformationReservedBalance = 0,
                CrMasUserInformationTotalBalance = 0,
                CrMasUserInformationCreditLimit = 0,
                CrMasUserInformationAuthorizationBnan = true,
                CrMasUserInformationAuthorizationAdmin = false,
                CrMasUserInformationAuthorizationBranch = false,
                CrMasUserInformationAuthorizationFoolwUp = false,
                CrMasUserInformationAuthorizationOwner = false,
                Email = model.CrMasUserInformationEmail,
                CrMasUserInformationTasksArName = model.CrMasUserInformationTasksArName,
                CrMasUserInformationTasksEnName = model.CrMasUserInformationTasksEnName,
                CrMasUserInformationSignature = model.CrMasUserInformationSignature,
                CrMasUserInformationPicture = model.CrMasUserInformationPicture

            };
            var result = await _userManager.CreateAsync(user, user.CrMasUserInformationPassWord);
            if (result.Succeeded) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RegisterForCasAsync(CrMasUserInformation model)
        {
            var user = new CrMasUserInformation
            {
                UserName = model.CrMasUserInformationCode,
                CrMasUserInformationCode = model.CrMasUserInformationCode,
                Id = model.CrMasUserInformationCode,
                CrMasUserInformationEmail = model.CrMasUserInformationEmail,
                CrMasUserInformationPassWord = model.CrMasUserInformationCode,
                CrMasUserInformationArName = model.CrMasUserInformationArName,
                CrMasUserInformationEnName = model.CrMasUserInformationEnName,
                CrMasUserInformationCallingKey = model.CrMasUserInformationCallingKey,
                CrMasUserInformationMobileNo = model.CrMasUserInformationMobileNo,
                CrMasUserInformationLessor = model.CrMasUserInformationLessor,
                CrMasUserInformationDefaultLanguage ="AR",
                CrMasUserInformationDefaultBranch = "000",
                CrMasUserInformationStatus = "A",
                CrMasUserInformationOperationStatus = false,
                CrMasUserInformationExitTimer = 5,
                CrMasUserInformationRemindMe = model.CrMasUserInformationCode,
                CrMasUserInformationAvailableBalance = 0,
                CrMasUserInformationReservedBalance = 0,
                CrMasUserInformationTotalBalance = 0,
                CrMasUserInformationCreditLimit = model.CrMasUserInformationCreditLimit,
                CrMasUserInformationAuthorizationBnan = false,
                CrMasUserInformationAuthorizationAdmin = model.CrMasUserInformationAuthorizationAdmin,
                CrMasUserInformationAuthorizationBranch = model.CrMasUserInformationAuthorizationBranch,
                CrMasUserInformationAuthorizationFoolwUp = false,
                CrMasUserInformationAuthorizationOwner = model.CrMasUserInformationAuthorizationOwner,
                Email = model.CrMasUserInformationEmail,
                CrMasUserInformationTasksArName = model.CrMasUserInformationTasksArName,
                CrMasUserInformationTasksEnName = model.CrMasUserInformationTasksEnName,
                CrMasUserInformationSignature = model.CrMasUserInformationSignature,
                CrMasUserInformationPicture = model.CrMasUserInformationPicture,
                CrMasUserInformationReasons = model.CrMasUserInformationReasons

            };
            var result = await _userManager.CreateAsync(user, user.CrMasUserInformationPassWord);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public Task UserLogins(string username)
        {
            throw new NotImplementedException();
        }
    }
}