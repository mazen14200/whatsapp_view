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
using Bnan.Ui.Areas.CAS.Controllers;
using Microsoft.Extensions.Localization;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class BankController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IBankService _bankService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<BankController> _localizer;



        public BankController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IBankService bankService, IUserLoginsService userLoginsService, IToastNotification toastNotification, IStringLocalizer<BankController> localizer) : base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _bankService = bankService;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("109", "1109001", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            var titles = await setTitle("109", "1109001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var banks = await _unitOfWork.CrMasSupAccountBanks.GetAllAsync();
            var bank = banks.Where(x => x.CrMasSupAccountBankCode != "00" && x.CrMasSupAccountBankStatus == "A").ToList();

            return View(bank);
        }


        [HttpGet]
        public PartialViewResult GetBanksByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var BankbyStatusAll = _unitOfWork.CrMasSupAccountBanks.GetAll();
                    var BankbyStatusAllFilter = BankbyStatusAll.Where(x => x.CrMasSupAccountBankCode != "00").ToList();
                    return PartialView("_DataTableBank", BankbyStatusAllFilter);
                }
                var BankbyStatus = _unitOfWork.CrMasSupAccountBanks.FindAll(l => l.CrMasSupAccountBankStatus == status).ToList();
                var BankbyStatusFilter = BankbyStatus.Where(x => x.CrMasSupAccountBankCode != "00").ToList();
                return PartialView("_DataTableBank", BankbyStatusFilter);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddBank()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("109", "1109001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var Banks = await _unitOfWork.CrMasSupAccountBanks.GetAllAsync();

            var BankCode = (int.Parse(Banks.LastOrDefault().CrMasSupAccountBankCode) + 1).ToString();
            ViewBag.BankCode = BankCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBank(BankVM AccountBank)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            if (ModelState.IsValid)
            {
                if (AccountBank != null)
                {
                    var banks = await _unitOfWork.CrMasSupAccountBanks.GetAllAsync();
                    var existingBank = banks.FirstOrDefault(x =>
                        x.CrMasSupAccountBankEnName == AccountBank.CrMasSupAccountBankEnName ||
                        x.CrMasSupAccountBankArName == AccountBank.CrMasSupAccountBankArName);

                    // Generate code for the second time
                    var Banks = await _unitOfWork.CrMasSupAccountBanks.GetAllAsync();
                    var BankCode = (int.Parse(Banks.LastOrDefault().CrMasSupAccountBankCode) + 1).ToString();
                    AccountBank.CrMasSupAccountBankCode = BankCode;
                    ViewBag.BankCode = BankCode;

                    if (existingBank != null)
                    {
                        if (existingBank.CrMasSupAccountBankArName != null &&
                            existingBank.CrMasSupAccountBankArName == AccountBank.CrMasSupAccountBankArName &&
                             existingBank.CrMasSupAccountBankEnName != AccountBank.CrMasSupAccountBankEnName)
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
                        if (existingBank.CrMasSupAccountBankEnName != null &&
                            existingBank.CrMasSupAccountBankEnName == AccountBank.CrMasSupAccountBankEnName &&
                             existingBank.CrMasSupAccountBankArName != AccountBank.CrMasSupAccountBankArName)
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
                        if (existingBank.CrMasSupAccountBankArName != null && existingBank.CrMasSupAccountBankEnName != null
                            && existingBank.CrMasSupAccountBankEnName == AccountBank.CrMasSupAccountBankEnName &&
                           existingBank.CrMasSupAccountBankArName == AccountBank.CrMasSupAccountBankArName)
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
                        return View(AccountBank);
                    }

                    AccountBank.CrMasSupAccountBankStatus = "A";
                    var BankVMTBank = _mapper.Map<CrMasSupAccountBank>(AccountBank);

                    await _unitOfWork.CrMasSupAccountBanks.AddAsync(BankVMTBank);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("109", "1109001", "1");
                    var RecordAr = BankVMTBank.CrMasSupAccountBankArName;
                    var RecordEn = BankVMTBank.CrMasSupAccountBankEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "اضافة بنك", "Add Bank", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddBank", AccountBank);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("109", "1109001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var bank = await _unitOfWork.CrMasSupAccountBanks.GetByIdAsync(id);
            if (bank == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            if (id == "00")
            {
                ModelState.AddModelError("Exist", "You Can't Edit This.");
                var bank2 = await _unitOfWork.CrMasSupAccountBanks.GetAllAsync();
                return View("Index", bank2);

            }
            var model = _mapper.Map<BankVM>(bank);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(BankVM model)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            var bank = _mapper.Map<CrMasSupAccountBank>(model);

            if (user != null)
            {
                if (bank != null)
                {
                    _unitOfWork.CrMasSupAccountBanks.Update(bank);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("109", "1109001", "1");
                    var RecordAr = model.CrMasSupAccountBankArName;
                    var RecordEn = model.CrMasSupAccountBankEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "Bank");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var bank = await _unitOfWork.CrMasSupAccountBanks.GetByIdAsync(code);
            if (bank != null)
            {
                if (await CheckUserSubValidationProcdures("1109001", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Hold";
                        bank.CrMasSupAccountBankStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        bank.CrMasSupAccountBankStatus = Status.Deleted;
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع";
                        sEn = "Retrive";
                        bank.CrMasSupAccountBankStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("109", "1109001", "1");
                    var RecordAr = bank.CrMasSupAccountBankArName;
                    var RecordEn = bank.CrMasSupAccountBankEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                return RedirectToAction("Index", "Bank");
            }
        }


            return View(bank);

    }
}
}
