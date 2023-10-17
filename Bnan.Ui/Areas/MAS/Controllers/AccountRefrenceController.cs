using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.Identitiy;
using Bnan.Ui.ViewModels.MAS.UserValiditySystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.ViewModels.MAS;
using NToastNotify;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace Bnan.Ui.Areas.MAS.Controllers
{

    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class AccountRefrenceController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IAccountRefrence _accountRefrence;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<AccountRefrenceController> _localizer;


        public AccountRefrenceController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, 
            IMapper mapper, IUserService userService, IAccountRefrence accountRefrence, IUserLoginsService userLoginsService, 
            IToastNotification toastNotification , IStringLocalizer<AccountRefrenceController> localizer) :
            base(userManager, unitOfWork, mapper)
        {
            _accountRefrence = accountRefrence; 
            _toastNotification = toastNotification; 
             _userLoginsService = userLoginsService;
            _userService = userService;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var titles = await setTitle("109", "1109003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var Refrence = await _unitOfWork.CrMasSupAccountReference.GetAllAsync();

            return View(Refrence);
        }

        [HttpGet]
        public PartialViewResult GetAccountRefrenceByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var AccountRefrencebyStatusAll = _unitOfWork.CrMasSupAccountReference.GetAll();
                    return PartialView("_DataTableRefrence", AccountRefrencebyStatusAll);
                }
                var AccountRefrencebyStatus = _unitOfWork.CrMasSupAccountReference.FindAll(l => l.CrMasSupAccountPaymentMethodStatus == status).ToList();
                return PartialView("_DataTableRefrence", AccountRefrencebyStatus);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddRefrence()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("109", "1109003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var Refrences = await _unitOfWork.CrMasSupAccountReference.GetAllAsync();

            var RefrenceCode = (int.Parse(Refrences.LastOrDefault().CrMasSupAccountReceiptReferenceCode) + 1).ToString();
            ViewBag.RefrenceCode = RefrenceCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRefrence(AccountRefrenceVM AccountRefrence)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            if (ModelState.IsValid)
            {
                if (AccountRefrence != null)
                {
                    var AccountRefrenceVMT = _mapper.Map<CrMasSupAccountReference>(AccountRefrence);
                    var Refrences = await _unitOfWork.CrMasSupAccountReference.GetAllAsync();
                    var existingRefrence = Refrences.FirstOrDefault(x =>
                        x.CrMasSupAccountReceiptReferenceEnName == AccountRefrenceVMT.CrMasSupAccountReceiptReferenceEnName ||
                        x.CrMasSupAccountReceiptReferenceArName == AccountRefrenceVMT.CrMasSupAccountReceiptReferenceArName);

                    // Generate code for the second time
    
                    var RefrenceCode = (int.Parse(Refrences.LastOrDefault().CrMasSupAccountReceiptReferenceCode) + 1).ToString();
                    AccountRefrence.CrMasSupAccountReceiptReferenceCode = RefrenceCode;
                    ViewBag.RefrenceCode = RefrenceCode;

                    if (existingRefrence != null)
                    {
                        if (existingRefrence.CrMasSupAccountReceiptReferenceArName != null &&
                            existingRefrence.CrMasSupAccountReceiptReferenceArName == AccountRefrence.CrMasSupAccountReceiptReferenceArName &&
                             existingRefrence.CrMasSupAccountReceiptReferenceEnName != AccountRefrence.CrMasSupAccountReceiptReferenceEnName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistAr", "هذا الحقل مسجل من قبل.");
                            }
                            else
                            {
                                ModelState.AddModelError("ExistAr", "This field is Existed.");
                            }

                        }
                        if (existingRefrence.CrMasSupAccountReceiptReferenceEnName != null &&
                            existingRefrence.CrMasSupAccountReceiptReferenceEnName == AccountRefrence.CrMasSupAccountReceiptReferenceEnName &&
                             existingRefrence.CrMasSupAccountReceiptReferenceArName != AccountRefrence.CrMasSupAccountReceiptReferenceArName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistEn", "هذا الحقل مسجل من قبل.");
                            }
                            else
                            {
                                ModelState.AddModelError("ExistEn", "This field is Existed.");
                            }
                        }
                        if (existingRefrence.CrMasSupAccountReceiptReferenceArName != null && existingRefrence.CrMasSupAccountReceiptReferenceEnName != null
                            && existingRefrence.CrMasSupAccountReceiptReferenceEnName == existingRefrence.CrMasSupAccountReceiptReferenceEnName &&
                           existingRefrence.CrMasSupAccountReceiptReferenceArName == existingRefrence.CrMasSupAccountReceiptReferenceArName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistEn", "هذا الحقل مسجل من قبل.");
                                ModelState.AddModelError("ExistAr", "هذا الحقل مسجل من قبل.");

                            }
                            else
                            {
                                ModelState.AddModelError("ExistAr", "This field is Existed.");
                                ModelState.AddModelError("ExistEn", "This field is Existed.");
                            }
                        }
                        return View(AccountRefrence);
                    }

                    AccountRefrenceVMT.CrMasSupAccountPaymentMethodStatus = "A";
                    ViewBag.Status = AccountRefrenceVMT.CrMasSupAccountPaymentMethodStatus;
                    await _unitOfWork.CrMasSupAccountReference.AddAsync(AccountRefrenceVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("109", "1109003", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة مرجع", "Add Reference", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddRefrence", AccountRefrence);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("109", "1109003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var refrence = await _unitOfWork.CrMasSupAccountReference.GetByIdAsync(id);
            if (refrence == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var model = _mapper.Map<AccountRefrenceVM>(refrence);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AccountRefrenceVM model)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            var refrence = _mapper.Map<CrMasSupAccountReference>(model);

            if (user != null)
            {
                if (refrence != null)
                {
                    _unitOfWork.CrMasSupAccountReference.Update(refrence);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("109", "1109003", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "AccountRefrence");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var refrence = await _unitOfWork.CrMasSupAccountReference.GetByIdAsync(code);
            if (refrence != null)
            {
                if (await CheckUserSubValidationProcdures("1109003", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف مرجع";
                        sEn = "Hold Reference";
                        refrence.CrMasSupAccountPaymentMethodStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف مرجع";
                        sEn = "Remove Reference";
                        refrence.CrMasSupAccountPaymentMethodStatus = Status.Deleted;
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع مرجع";
                        sEn = "Retrive Reference";
                        refrence.CrMasSupAccountPaymentMethodStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("109", "1109003", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    return RedirectToAction("Index", "AccountRefrence");
                }
            }


            return View(refrence);

        }
    }
}
