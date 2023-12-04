using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.Areas.CAS.Controllers;
using Bnan.Ui.ViewModels.Identitiy;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;
using static StackExchange.Redis.Role;

namespace Bnan.Ui.Areas.Identity.Controllers
{

    [Area("Identity")]
    public class AccountController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<AccountController> _localizer;
        private readonly SignInManager<CrMasUserInformation> _signInManager;


        public AccountController(IAuthService authService, IUserService userService, UserManager<CrMasUserInformation> userManager, SignInManager<CrMasUserInformation> signInManager, IStringLocalizer<AccountController> localizer, IUnitOfWork unitOfWork, IMapper mapper) : base(userManager, unitOfWork, mapper)
        {
            _authService = authService;
            _userService = userService;
            _signInManager = signInManager;
            _localizer = localizer;
        }

        public async Task<IActionResult> Login()
        {
            if (CultureInfo.CurrentUICulture.Name == "en-US") await ViewData.SetPageTitleAsync("Log in", "Bnan", "", "", "", "");
            else await ViewData.SetPageTitleAsync("تسجيل الدخول", "بنان", "", "", "","");
            var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
            var isAuth = _userService.CheckUserifAuth(User);
            if (user != null)
            {
                if (isAuth)
                {
                    bool? userAuthBnan = user.CrMasUserInformationAuthorizationBnan;
                    bool? userAuthOwners = user.CrMasUserInformationAuthorizationOwner;
                    bool? userAuthAdmin = user.CrMasUserInformationAuthorizationAdmin;
                    bool? userAuthBranch = user.CrMasUserInformationAuthorizationBranch;
                    if (userAuthBnan == true)
                    {
                        return RedirectToAction("Index", "Home", new { area = "MAS" });
                    }

                    if (userAuthAdmin == true && userAuthBranch == true && userAuthOwners==true)
                    {
                        return RedirectToAction("Systems", "Account");
                    }
                    else if ((userAuthAdmin==true && userAuthBranch==true) || (userAuthAdmin == true && userAuthOwners == true) || (userAuthBranch == true && userAuthOwners==true))
                    {
                        return RedirectToAction("Systems", "Account");
                    }
                    else if (userAuthAdmin == true && userAuthBranch == false&& userAuthOwners == false)
                    {
                        return RedirectToAction("Index", "Home", new { area = "CAS" });
                    }
                    else if (userAuthAdmin == false && userAuthBranch == true && userAuthOwners == false)
                    {
                        return RedirectToAction("Index", "Home", new { area = "BS" });
                    }
                    else
                    {
                        return RedirectToAction("Logout", "Account", new { area = "BS" });
                    }
                }
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {


                var user = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.UserName, new[] { "CrMasUserInformationLessorNavigation", "CrMasUserBranchValidities.CrMasUserBranchValidity1" });




                if (user == null)
                {
                    ModelState.AddModelError(nameof(model.UserName), _localizer["UserNameInvalid"]);
                    return View(model);
                }
                // If Password Is Invalid Or
                if (await _authService.CheckPassword(model.UserName, model.Password) == false)
                {
                    if (user.CrMasUserInformationRemindMe != null)
                    {
                        ModelState.AddModelError("Hint", _localizer["PasswordInvalid"] + ":" + user.CrMasUserInformationRemindMe);
                    }
                    else
                    {
                        ModelState.AddModelError("Hint", _localizer["PasswordInvalid"]);
                    }
                    return View(model);
                }
                //else if (await _authService.CheckPassword(model.UserName, model.Password) == true)
                //{
                //    if (user.CrMasUserInformationOperationStatus == true)
                //    {
                //        ModelState.AddModelError("Hint", _localizer["UserIsOpen"]);
                //        return View(model);
                //    }
                //}
                //Check The Status Of User 
                if (user.CrMasUserInformationStatus == Status.Deleted)
                {
                    ModelState.AddModelError("Hint", _localizer["AccountDelete"]);
                    return View(model);
                }
                if (user.CrMasUserInformationLessorNavigation.CrMasLessorInformationStatus == Status.Deleted)
                {
                    ModelState.AddModelError("Hint", _localizer["CompanyDeleted"]);
                    return View(model);
                }
                if (user.CrMasUserInformationAuthorizationBranch==true&&user.CrMasUserInformationAuthorizationAdmin==false&&user.CrMasUserInformationAuthorizationOwner==false)
                {
                    var branchValidities = user.CrMasUserBranchValidities.Where(x => x.CrMasUserBranchValidityBranchStatus == Status.Active);
                    if (branchValidities.Count()==0)
                    {
                        ModelState.AddModelError("Hint", _localizer["NoHaveBranches"]);
                        return View(model);
                    }
                    else if (branchValidities.Count()==1)
                    {
                        if (branchValidities.FirstOrDefault().CrMasUserBranchValidity1.CrCasBranchInformationStatus==Status.Deleted)
                        {
                            ModelState.AddModelError("Hint", _localizer["HaveOneBranchDeleted"]);
                            return View(model);
                        }
                    }
                }


                var result = await _authService.LoginAsync(model.UserName, model.Password);
                if (result.Succeeded)
                {
                    if (user.CrMasUserInformationDefaultLanguage.ToLower() == "en") SetLanguage("~/","en-US");
                    else SetLanguage("~/", "ar-EG");
                    var UserInforation = await _userService.GetUserByUserNameAsync(model.UserName);
                    UserInforation.CrMasUserInformationOperationStatus = true;
                    UserInforation.CrMasUserInformationEntryLastDate = DateTime.Now.Date;
                    UserInforation.CrMasUserInformationEntryLastTime = DateTime.Now.TimeOfDay;
                    await _userService.SaveChanges(UserInforation);

                    bool? userAuthBnan = user.CrMasUserInformationAuthorizationBnan;
                    bool? userAuthAdmin = user.CrMasUserInformationAuthorizationAdmin;
                    bool? userAuthBranch = user.CrMasUserInformationAuthorizationBranch;
                    bool? userAuthOwners = user.CrMasUserInformationAuthorizationOwner;


                    if (userAuthBnan == true)
                    {
                        return RedirectToAction("Index", "Home", new { area = "MAS" });
                    }

                    if (userAuthAdmin == true && userAuthBranch == true && userAuthOwners == true)
                    {
                        return RedirectToAction("Systems", "Account");
                    }
                    else if ((userAuthAdmin == true && userAuthBranch == true) || (userAuthAdmin == true && userAuthOwners == true) || (userAuthBranch == true && userAuthOwners == true))
                    {
                        return RedirectToAction("Systems", "Account");
                    }
                    else if (userAuthAdmin == true && userAuthBranch == false && userAuthOwners == false)
                    {
                        return RedirectToAction("Index", "Home", new { area = "CAS" });
                    }
                    else if (userAuthAdmin == false && userAuthBranch == true && userAuthOwners == false)
                    {
                        return RedirectToAction("Index", "Home", new { area = "BS" });
                    }
                    else
                    {
                        ModelState.AddModelError("Stat", _localizer["AuthEmplpoyee"]);
                        return View(model);
                    }
                }
                if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }

            // If UserName Or Password is Null
            if (model.UserName == null && model.Password == null)
            {
                ModelState.AddModelError(nameof(model.UserName), _localizer["PleaseEnterUserName"]);
                ModelState.AddModelError(nameof(model.Password), _localizer["PleaseEntePassword"]);
                return View(model);
            }
            else if (model.UserName == null)
            {
                ModelState.AddModelError(nameof(model.UserName), _localizer["PleaseEnterUserName"]);
                return View(model);
            }
            else if (model.Password == null)
            {
                ModelState.AddModelError(nameof(model.Password), _localizer["PleaseEntePassword"]);
                return View(model);
            }

            return View(model);

        }

        [Authorize]
        [HttpGet]
        public async Task< IActionResult> Systems()
        {
            var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
            if (CultureInfo.CurrentUICulture.Name == "en-US") await ViewData.SetPageTitleAsync("Systems", "", "", "", "", user.CrMasUserInformationEnName);
            else await ViewData.SetPageTitleAsync("الأنظمة", "", "", "", "", user.CrMasUserInformationArName);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user==null) return RedirectToAction("Login", "Account");
            //var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
            user.CrMasUserInformationOperationStatus = false;
            user.CrMasUserInformationExitLastDate = DateTime.Now.Date;
            user.CrMasUserInformationExitLastTime = DateTime.Now.TimeOfDay;
            await _userService.SaveChanges(user);
            await _authService.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new CrMasUserInformation
                {
                    CrMasUserInformationCode = model.Code,
                    CrMasUserInformationPassWord = model.Code,
                    CrMasUserInformationRemindMe = "البريد",
                    CrMasUserInformationLessor = "4009",
                    CrMasUserInformationDefaultBranch = "",
                    CrMasUserInformationDefaultLanguage = "AR",
                    CrMasUserInformationAuthorizationBnan = false,
                    CrMasUserInformationAuthorizationAdmin = true,
                    CrMasUserInformationAuthorizationBranch = true,
                    CrMasUserInformationAuthorizationOwner = true,
                    CrMasUserInformationAuthorizationFoolwUp = true,
                    CrMasUserInformationArName = "تميم",
                    CrMasUserInformationEnName = "Tamem",
                    CrMasUserInformationTasksArName = "مدير الفني لنظام",
                    CrMasUserInformationTasksEnName = "System Technical Manager",
                    CrMasUserInformationReservedBalance = 0,
                    CrMasUserInformationTotalBalance = 0,
                    CrMasUserInformationAvailableBalance = 0,
                    CrMasUserInformationCreditLimit = 0,
                    CrMasUserInformationMobileNo = "",
                    CrMasUserInformationEmail = "",
                    CrMasUserInformationChangePassWordLastDate = null,
                    CrMasUserInformationEntryLastDate = null,
                    UserName = model.Code,
                    Id = model.Code
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction("Systems", "Account");
        }
        [HttpGet]
        public IActionResult SetLanguage(string returnUrl, string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}