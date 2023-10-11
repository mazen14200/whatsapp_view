using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure;
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

namespace Bnan.Ui.Areas.Identity.Controllers
{

    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<AccountController> _localizer;
        private readonly UserManager<CrMasUserInformation> _userManager;
        private readonly SignInManager<CrMasUserInformation> _signInManager;
        private readonly IUnitOfWork _unitOfWork;


        public AccountController(IAuthService authService, IUserService userService, UserManager<CrMasUserInformation> userManager, SignInManager<CrMasUserInformation> signInManager, IStringLocalizer<AccountController> localizer, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Login()
        {
            var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
            var isAuth = _userService.CheckUserifAuth(User);
            if (user != null)
            {
                if (isAuth)
                {
                    bool? userAuthBnan = user.CrMasUserInformationAuthorizationBnan;
                    bool? userAuthAdmin = user.CrMasUserInformationAuthorizationAdmin;
                    bool? userAuthBranch = user.CrMasUserInformationAuthorizationBranch;

                    if (userAuthBnan == true)
                    {
                        return RedirectToAction("Index", "Home", new { area = "MAS" });
                    }
                    if (userAuthAdmin == true && userAuthBranch == true)
                    {
                        return RedirectToAction("Systems", "Account");
                    }
                    else if (userAuthAdmin == true && userAuthBranch == false)
                    {
                        return RedirectToAction("Index", "Home", new { area = "CAS" });
                    }
                    else if (userAuthAdmin == false && userAuthBranch == true)
                    {
                        return RedirectToAction("Index", "Home", new { area = "BS" });
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


                var user = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.UserName, new[] { "CrMasUserInformationLessorNavigation" });




                if (user == null)
                {
                    ModelState.AddModelError(nameof(model.UserName), _localizer["UserNameInvalid"]);
                    return View(model);
                }


                // If Password Is Invalid Or
                if (await _authService.CheckPassword(model.UserName, model.Password) == false)
                {

                    if (user.CrMasUserInformationReasons != null)
                    {
                        ModelState.AddModelError("Hint", _localizer["PasswordInvalid"] + ":" + user.CrMasUserInformationReasons);
                    }
                    else
                    {
                        ModelState.AddModelError("Hint", _localizer["PasswordInvalid"]);
                    }


                    return View(model);
                }
                /*else if (await _authService.CheckPassword(model.UserName, model.Password) == true)
                {
                    if (user.CrMasUserInformationOperationStatus == true)
                    {
                        ModelState.AddModelError("Hint", "User Is Logged In");
                        return View(model);
                    }
                }*/

                //Check The Status Of User 
                if (user.CrMasUserInformationStatus == Status.Deleted)
                {
                    ModelState.AddModelError("Stat", _localizer["AccountDelete"]);
                    return View(model);
                }
                else if (user.CrMasUserInformationStatus == Status.Hold)
                {
                    ModelState.AddModelError("Stat", _localizer["AccountHold"]);
                    return View(model);
                }
                if (user.CrMasUserInformationLessorNavigation.CrMasLessorInformationStatus == Status.Deleted)
                {
                    ModelState.AddModelError("Stat", _localizer["CompanyDeleted"]);
                    return View(model);
                }


                var result = await _authService.LoginAsync(model.UserName, model.Password);
                if (result.Succeeded)
                {
                    if (user.CrMasUserInformationDefaultLanguage.ToLower() == "en") SetLanguage("en-US");
                    else SetLanguage("ar-EG");
                    var UserInforation = await _userService.GetUserByUserNameAsync(model.UserName);
                    UserInforation.CrMasUserInformationOperationStatus = true;
                    UserInforation.CrMasUserInformationEntryLastDate = DateTime.Now.Date;
                    UserInforation.CrMasUserInformationEntryLastTime = DateTime.Now.TimeOfDay;
                    await _userService.SaveChanges(UserInforation);

                    bool? userAuthBnan = user.CrMasUserInformationAuthorizationBnan;
                    bool? userAuthAdmin = user.CrMasUserInformationAuthorizationAdmin;
                    bool? userAuthBranch = user.CrMasUserInformationAuthorizationBranch;

                    if (userAuthBnan == true)
                    {
                        return RedirectToAction("Index", "Home", new { area = "MAS" });
                    }
                    if (userAuthAdmin == true && userAuthBranch == true)
                    {
                        return RedirectToAction("Systems", "Account");
                    }
                    else if (userAuthAdmin == true && userAuthBranch == false)
                    {
                        return RedirectToAction("Index", "Home", new { area = "CAS" });
                    }
                    else if (userAuthAdmin == false && userAuthBranch == true)
                    {
                        return RedirectToAction("Index", "Home", new { area = "BS" });
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
        public IActionResult Systems()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
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
        public void SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
        }
    }
}