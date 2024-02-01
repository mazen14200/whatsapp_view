using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Bnan.Ui.ViewModels.Identitiy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class EmployeesController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<CustodyController> _localizer;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public EmployeesController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IAuthService authService, IStringLocalizer<CustodyController> localizer, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment) : base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _authService = authService;
            _localizer = localizer;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> MyAccount()
        { //To Set Title 
            var titles = await setTitle("501", "5501013", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var bsLayoutVM = await GetBranchesAndLayout();
            var userLogin = await _userManager.GetUserAsync(User);
            var user = await _userService.GetUserByUserNameAsync(userLogin.CrMasUserInformationCode);
            var CallingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active).ToList();
            bsLayoutVM.UserInformation = user;
            bsLayoutVM.CallingKeys = CallingKeys;
            return View(bsLayoutVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BSLayoutVM model, IFormFile Profile_Photo, IFormFile signature_img)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var user = await _userService.GetUserByUserNameAsync(userLogin.CrMasUserInformationCode);
            var UserModel = model.UserInformation;
            string foldername = $"{"images\\Company"}\\{user.CrMasUserInformationLessor}\\{"Users"}\\{user.CrMasUserInformationCode}";
            string filePathImage;
            string filePathSignture;
            if (Profile_Photo != null)
            {
                string fileNameImg = "Image";
                filePathImage = await Profile_Photo.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
            }
            else if (Profile_Photo == null&&string.IsNullOrEmpty(user.CrMasUserInformationPicture))
            {
                filePathImage = "~/images/common/user.jpg";
            }
            else
            {
                filePathImage = user.CrMasUserInformationPicture;
            }

            if (signature_img != null)
            {
                string fileNameSignture = "Signture";
                filePathSignture = await signature_img.SaveImageAsync(_webHostEnvironment, foldername, fileNameSignture, ".png");
            }
            else if(signature_img == null && string.IsNullOrEmpty(user.CrMasUserInformationSignature))
            {
                filePathSignture = "~/images/common/DefualtUserSignature.png";
            }
            else
            {
                filePathSignture=user.CrMasUserInformationSignature;
            }


            if (user != null)
            {
                user.CrMasUserInformationDefaultLanguage = UserModel.CrMasUserInformationDefaultLanguage;
                user.CrMasUserInformationMobileNo = UserModel.CrMasUserInformationMobileNo;
                user.CrMasUserInformationEmail = UserModel.CrMasUserInformationEmail;
                user.CrMasUserInformationRemindMe = UserModel.CrMasUserInformationRemindMe;
                user.CrMasUserInformationExitTimer = UserModel.CrMasUserInformationExitTimer;
                user.CrMasUserInformationCallingKey = UserModel.CrMasUserInformationCallingKey;
                user.CrMasUserInformationPicture = filePathImage;
                user.CrMasUserInformationSignature = filePathSignture;
                try
                {
                    await _userService.UpdateAsync(user);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("Index", "Home", new { area = "BS" });
                }
                catch (Exception)
                {
                    _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("Index", "Home", new { area = "BS" });
                    throw;
                }

            }

            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index", "Home", new { area = "BS" });
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(string Current, string New, string Confirm)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var user = await _userService.GetUserByUserNameAsync(userLogin.CrMasUserInformationCode);
            //// Check current password 
            //if (user.CrMasUserInformationPassWord.Trim() != model.CurrentPassword.Trim())
            //{
            //    if (currentCulture == "en-US") ModelState.AddModelError("NotExist", "Current password is incorrect. Please try again.");
            //    else ModelState.AddModelError("NotExist", "كلمة السر الحالية غير صحيحة, يرجي اعادة المحاولة");
            //    return View(model);
            //}
            //if (model.NewPassword != model.ConfirmPassword)
            //{
            //    if (currentCulture == "en-US") ModelState.AddModelError("Exist", "New password does not match. Enter new password again here.");
            //    else ModelState.AddModelError("Exist", "كلمة السر وتأكيد كلمة السر غير مطابقان, يرجي اعادة المحاولة");

            //    return View(model);
            //}
            //var result = await _userManager.ChangePasswordAsync(user, user.CrMasUserInformationPassWord.Trim(), model.NewPassword);
            //if (result.Succeeded)
            //{
            //    user.CrMasUserInformationPassWord = model.NewPassword;
            //    user.CrMasUserInformationChangePassWordLastDate = DateTime.Now.Date;
            //    _unitOfWork.Complete();
            //    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            //}

            //else
            //{
            //    ModelState.AddModelError("Exist", "Something wrong happed , please try again");
            //    return View(model);
            //}

            return RedirectToAction("Index", "Home");
        }

    }
}
