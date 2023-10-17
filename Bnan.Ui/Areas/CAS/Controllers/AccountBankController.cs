using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class AccountBankController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IAccountBank _accountBank;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<AccountBankController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;


        public AccountBankController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
             IMapper mapper, IUserService userService, IAccountBank accountBank,
             IUserLoginsService userLoginsService, IToastNotification toastNotification,
             IStringLocalizer<AccountBankController> localizer, IAdminstritiveProcedures adminstritiveProcedures) :
             base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _accountBank = accountBank;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _adminstritiveProcedures = adminstritiveProcedures;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var titles = await setTitle("207", "2207003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var AccountBanks =  _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankStatus == "A" && x.CrCasAccountBankNo != "00" && x.CrCasAccountBankLessor==userLogin.CrMasUserInformationLessor, new[] { "CrCasAccountBankNoNavigation" });
            return View(AccountBanks);
        }


        [HttpGet]
        public async Task<PartialViewResult> GetAccountBanksByStatus(string status)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            if (!string.IsNullOrEmpty(status))
            {
                var AccountBankbyStatusAll = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankNo != "00" && x.CrCasAccountBankLessor == userLogin.CrMasUserInformationLessor, new[] { "CrCasAccountBankNoNavigation" });
                if (status == Status.All) return PartialView("_DataTableAccountBank", AccountBankbyStatusAll);
                return PartialView("_DataTableAccountBank", AccountBankbyStatusAll.Where(l => l.CrCasAccountBankStatus == status));
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddAccountBank()
        {

            var titles = await setTitle("207", "2207003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var AccountBanks = await _unitOfWork.CrCasAccountBank.GetAllAsync();
            // View the Banks
            var BanksData = _unitOfWork.CrMasSupAccountBanks.FindAll(x => x.CrMasSupAccountBankStatus == Status.Active && x.CrMasSupAccountBankCode!="00");
            var BanksDatasAr = BanksData.Select(c => new SelectListItem { Value = c.CrMasSupAccountBankArName.ToString(), Text = c.CrMasSupAccountBankArName  }).ToList();
            var BanksDatasEn = BanksData.Select(c => new SelectListItem { Value = c.CrMasSupAccountBankEnName.ToString(), Text = c.CrMasSupAccountBankEnName }).ToList();

            ViewData["BanksDataAr"] = BanksDatasAr;
            ViewData["BanksDataEn"] = BanksDatasEn;           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAccountBank(AccountBankVM AccountBankmodel)
        {
            var BanksData = _unitOfWork.CrMasSupAccountBanks.FindAll(x => x.CrMasSupAccountBankStatus == Status.Active);
            var BanksDatasAr = BanksData.Select(c => new SelectListItem { Value = c.CrMasSupAccountBankArName.ToString(), Text = c.CrMasSupAccountBankArName }).ToList();
            var BanksDatasEn = BanksData.Select(c => new SelectListItem { Value = c.CrMasSupAccountBankEnName.ToString(), Text = c.CrMasSupAccountBankEnName }).ToList();

            ViewData["BanksDataAr"] = BanksDatasAr;
            ViewData["BanksDataEn"] = BanksDatasEn;
            // Get Lessor Code
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode= userLogin.CrMasUserInformationLessor;
            AccountBankmodel.CrCasAccountBankLessor = lessorCode;
            //ViewBag.lessorCode = lessorCode;

            // Get Bank No
            var banksDatas = _unitOfWork.CrMasSupAccountBanks.GetAll();
            var BankNo = banksDatas.FirstOrDefault(x => x.CrMasSupAccountBankArName == AccountBankmodel.CrCasBankArName 
            || x.CrMasSupAccountBankEnName==AccountBankmodel.CrCasBankEnName).CrMasSupAccountBankCode.ToString();
            //ViewBag.AccountBankCode= BankNo;
            AccountBankmodel.CrCasAccountBankNo = BankNo;
            // Get the Serial No
            var AccountBankSerial = await _unitOfWork.CrCasAccountBank.GetAllAsync();
            var SerialCount = AccountBankSerial.Where(x => x.CrCasAccountBankNo == BankNo).ToList().Count();

            var SerialNo = "01";
            var serialno = 1;
            if (SerialCount > 0)
            {
                serialno = int.Parse(SerialNo)+1;
            }
            else if (SerialCount > 9)
            {
                serialno = (int.Parse(SerialNo)+1);
            }
            else
            {
                serialno = 1;
            }
            SerialNo= serialno.ToString("D2");
            AccountBankmodel.CrCasAccountBankSerail = SerialNo;
            //ViewBag.SerialCode = SerialNo;

            // Concate the Bank code

            var ConcateBankcode = lessorCode.ToString() + BankNo.ToString() + SerialNo.ToString();
            AccountBankmodel.CrCasAccountBankCode = ConcateBankcode;

            string currentCulture = CultureInfo.CurrentCulture.Name;
          //  if (ModelState.IsValid)
            //{
                if (AccountBankmodel != null)
                {
                    var AccountBanks = await _unitOfWork.CrCasAccountBank.GetAllAsync();
                    var existingIBank = AccountBanks.FirstOrDefault(x =>x.CrCasAccountBankIban == AccountBankmodel.CrCasAccountBankIban);
                    var existingIBankArName= AccountBanks.FirstOrDefault(x =>x.CrCasAccountBankArName == AccountBankmodel.CrCasAccountBankArName);
                    var existingIBankEnName= AccountBanks.FirstOrDefault(x =>x.CrCasAccountBankEnName == AccountBankmodel.CrCasAccountBankEnName);

                if (existingIBank != null)
                    {
                            ModelState.AddModelError("ExistCodeAr", _localizer["IBanIsExist"]);
                            return View(AccountBankmodel);
                    }
                    else
                    {
                        var existingNamesForLessor = AccountBanks.Where(x => x.CrCasAccountBankLessor == AccountBankmodel.CrCasAccountBankLessor && x.CrCasAccountBankNo==AccountBankmodel.CrCasAccountBankNo).ToList();
                        var existingAccountBank = existingNamesForLessor.FirstOrDefault(x =>
                            x.CrCasAccountBankEnName == AccountBankmodel.CrCasAccountBankEnName ||
                            x.CrCasAccountBankArName == AccountBankmodel.CrCasAccountBankArName);

                        if (existingAccountBank != null)
                        {
                            if (existingAccountBank.CrCasAccountBankArName != null &&
                                existingAccountBank.CrCasAccountBankArName == AccountBankmodel.CrCasAccountBankArName &&
                                 existingAccountBank.CrCasAccountBankEnName != AccountBankmodel.CrCasAccountBankEnName)
                            {
                                 ModelState.AddModelError("ExistAr", _localizer["IsExist"]);
                            }
                            if (existingAccountBank.CrCasAccountBankEnName != null &&
                                existingAccountBank.CrCasAccountBankEnName == AccountBankmodel.CrCasAccountBankEnName &&
                                 existingAccountBank.CrCasAccountBankArName != AccountBankmodel.CrCasAccountBankArName)
                            {
                                
                                    ModelState.AddModelError("ExistEn", _localizer["FieldIsExist"]);
                            }
                            if (existingAccountBank.CrCasAccountBankArName != null && existingAccountBank.CrCasAccountBankEnName != null
                                && existingAccountBank.CrCasAccountBankEnName == AccountBankmodel.CrCasAccountBankEnName &&
                               existingAccountBank.CrCasAccountBankArName == AccountBankmodel.CrCasAccountBankArName)
                            {
                                
                                    ModelState.AddModelError("ExistEn", _localizer["FieldIsExist"]);
                                    ModelState.AddModelError("ExistAr", _localizer["FieldIsExist"]);
                            }

                            return View(AccountBankmodel);
                        }

                     }
                    AccountBankmodel.CrCasAccountBankStatus = "A";
                    var AccountBankVMTAccountBank = _mapper.Map<CrCasAccountBank>(AccountBankmodel);
                    await _unitOfWork.CrCasAccountBank.AddAsync(AccountBankVMTAccountBank);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207003", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة حساب", "Add Account", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                // Save Adminstrive Procedures
                     await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "243", "20", currentUser.CrMasUserInformationLessor, "100",
                    AccountBankVMTAccountBank.CrCasAccountBankCode, null, null, null, null, null, null, null, null, "اضافة", "Insert", "I", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });


                }
                return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("207", "2207003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var bank = await _unitOfWork.CrCasAccountBank.GetByIdAsync(id);
            if (bank == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var BanksData = _unitOfWork.CrMasSupAccountBanks.FindAll(x => x.CrMasSupAccountBankStatus == Status.Active && x.CrMasSupAccountBankCode==bank.CrCasAccountBankNo);
            var BanksDatasAr = BanksData.FirstOrDefault().CrMasSupAccountBankArName.ToString();
            var BanksDatasEn = BanksData.FirstOrDefault().CrMasSupAccountBankEnName.ToString();
            var SalesPointCount = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointAccountBank==bank.CrCasAccountBankCode&&x.CrCasAccountSalesPointStatus!=Status.Deleted).Count();
            ViewBag.BankNameAr = BanksDatasAr;
            ViewBag.BankNameEn = BanksDatasEn;
            ViewBag.SalesPointCount = SalesPointCount;
            bank.CrCasAccountBankArName = BanksDatasAr;
            bank.CrCasAccountBankEnName = BanksDatasEn;

            var model = _mapper.Map<AccountBankVM>(bank);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountBankVM model)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            var bank = _mapper.Map<CrCasAccountBank>(model);
            var bank2 = await _unitOfWork.CrCasAccountBank.GetByIdAsync(model.CrCasAccountBankCode);
            if (user != null)
            {
                if (bank != null)
                {
                    bank2.CrCasAccountBankReasons = model.CrCasAccountBankReasons;
                    //_unitOfWork.CrCasAccountBank.Update(bank);
                    _unitOfWork.Complete();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207003", "2");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "243", "20", currentUser.CrMasUserInformationLessor, "100",
                    bank2.CrCasAccountBankCode, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "AccountBank");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var bank = await _unitOfWork.CrCasAccountBank.GetByIdAsync(code);
            if (bank != null)
            {
                if (await CheckUserSubValidationProcdures("2207003", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Stop";
                        bank.CrCasAccountBankStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Delete";
                        bank.CrCasAccountBankStatus = Status.Deleted;
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع";
                        sEn = "Retrieve";
                        bank.CrCasAccountBankStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207003", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "243", "20", currentUser.CrMasUserInformationLessor, "100",
                   bank.CrCasAccountBankCode, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);
                    return RedirectToAction("Index", "AccountBank");
                }
            }


            return View(bank);

        }

    }
}
