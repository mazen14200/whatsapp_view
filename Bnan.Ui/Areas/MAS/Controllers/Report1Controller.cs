using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Numerics;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class Report1Controller : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<Report1Controller> _localizer;


        public Report1Controller(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, 
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<Report1Controller> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {

            var titles = await setTitle("104", "1104001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasUserLogins.GetAllAsync();
            //var contract = contracts.Where(x => x.CrMasUserLoginEntryDate >= "A").ToList();
            //var CarsInfo_count_all = _userLoginsService.GetAllUserLoginsCount();
            List<List<string>> CarsInfo_count_all = new List<List<string>>() { new List<string>() { "rgf1", "e2" }, new List<string>() { "rgf1", "e2" }, };

            Tuple<IEnumerable<CrMasUserLogin>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasUserLogin>, List<List<string>>>(contracts, CarsInfo_count_all);
            return View(tb);
        }

        [HttpGet]
        public PartialViewResult GetUserLoginByDate(string _max, string _mini)
        {
            if (!string.IsNullOrEmpty(_max) && !string.IsNullOrEmpty(_mini))
            {
               
                //var UserLoginbyStatusAll = _unitOfWork.CrMasUserLogins.GetAll();
                //return PartialView("_DataTableUserLogin", UserLoginbyStatusAll);
                var UserLoginAll = _unitOfWork.CrMasUserLogins.GetAll();
                var UserLoginbyDateAll = UserLoginAll.Where(l => l.CrMasUserLoginEntryDate >= DateTime.Parse(_mini) || l.CrMasUserLoginEntryDate < DateTime.Parse(_max)).ToList();
                //var CarsInfo_count_all1 = _userLoginsService.GetAllUserLoginsCount();
                List<List<string>> CarsInfo_count_all1 = new List<List<string>>() { new List<string>() { "rgf1", "e2" }, new List<string>() { "rgf1", "e2" }, };
                //CarsInfo_count_all1.Add(["rgf1", "e2"]);
                //CarsInfo_count_all1.Add(["3r", "2e"]);
                Tuple<IEnumerable<CrMasUserLogin>, List<List<string>>> tb1 = new Tuple<IEnumerable<CrMasUserLogin>, List<List<string>>>(UserLoginbyDateAll, CarsInfo_count_all1);
                return PartialView("_DataTableUserLogin", tb1);
                
            }
            return PartialView();
        }


        //[HttpGet]
        //public async Task<IActionResult> AddUserLogin()
        //{

        //    // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
        //    var titles = await setTitle("104", "1104001", "1");
        //    await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

        //    var UserLoginCode = "";
        //    var UserLogins = await _unitOfWork.CrMasUserLogins.GetAllAsync();
        //    if (UserLogins.Count() != 0)
        //    {
        //        UserLoginCode = (BigInteger.Parse(UserLogins.LastOrDefault().CrMasUserLoginsCode) + 1).ToString();
        //    }
        //    else
        //    {
        //        UserLoginCode = "1";
        //    }
        //    ViewBag.UserLoginCode = UserLoginCode;
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddUserLogin(UserLoginVM UserLogins)
        //{
        //    string currentCulture = CultureInfo.CurrentCulture.Name;


        //    if (ModelState.IsValid)
        //    {
        //        if (UserLogins != null)
        //        {
        //            var UserLoginVMT = _mapper.Map<CrMasUserLogin>(UserLogins);
        //            var All_UserLogins = await _unitOfWork.CrMasUserLogins.GetAllAsync();
        //            var existingUserLogin_En = All_UserLogins.FirstOrDefault(x =>
        //                x.CR_Mas_User_Login_Concatenate_Operation_En_Description == UserLoginVMT.CR_Mas_User_Login_Concatenate_Operation_En_Description);
        //            var existingUserLogin_Ar = All_UserLogins.FirstOrDefault(x =>
        //                x.CR_Mas_User_Login_Concatenate_Operation_Ar_Description == UserLoginVMT.CR_Mas_User_Login_Concatenate_Operation_Ar_Description);

        //            // Generate code for the second time
        //            var UserLoginCode = (BigInteger.Parse(All_UserLogins.LastOrDefault().CrMasUserLoginsCode) + 1).ToString();
        //            UserLogins.CrMasUserLoginsCode = UserLoginCode;
        //            ViewBag.UserLoginCode = UserLoginCode;
        //            if (UserLoginVMT.CR_Mas_User_Login_Concatenate_Operation_Ar_Description != null && UserLoginVMT.CR_Mas_User_Login_Concatenate_Operation_En_Description != null)
        //            {
        //                if (existingUserLogin_Ar != null && existingUserLogin_En != null)
        //                {
        //                    ModelState.AddModelError("ExistAr", _localizer["Existing"]);
        //                    ModelState.AddModelError("ExistEn", _localizer["Existing"]);
        //                    return View(UserLogins);
        //                }
        //                else if (existingUserLogin_En != null)
        //                {
        //                    ModelState.AddModelError("ExistEn", _localizer["Existing"]);
        //                    return View(UserLogins);
        //                }
        //                else if (existingUserLogin_Ar != null)
        //                {
        //                    ModelState.AddModelError("ExistAr", _localizer["Existing"]);
        //                    return View(UserLogins);
        //                }
        //            }

        //            UserLoginVMT.CrMasUserLoginsStatus = "A";
        //            await _unitOfWork.CrMasUserLogins.AddAsync(UserLoginVMT);

        //            _unitOfWork.Complete();

        //            var (mainTask, subTask, system, currentUser) = await SetTrace("104", "1104001", "1");

        //            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
        //            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
        //            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

        //            _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View("AddUserLogin", UserLogins);
        //}



        //[HttpGet]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    //To Set Title !!!!!!!!!!!!!
        //    var titles = await setTitle("104", "1104001", "1");
        //    await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

        //    var contract = await _unitOfWork.CrMasUserLogins.GetByIdAsync(id);
        //    if (contract == null)
        //    {
        //        ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
        //        return View("Index");
        //    }
        //    int countUserLogins = 0;
        //    countUserLogins = _userLoginsService.GetOneUserLoginCount(id);
        //    ViewBag.UserLogins_Count = countUserLogins;
        //    var model = _mapper.Map<UserLoginVM>(contract);

        //    return View(model);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Edit(UserLoginVM model)
        //{
        //    var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);

        //    if (user != null)
        //    {
        //        if (model != null)
        //        {

        //            var contract = _mapper.Map<CrMasUserLogin>(model);

        //            _unitOfWork.CrMasUserLogins.Update(contract);
        //            _unitOfWork.Complete();

        //            // SaveTracing
        //            var (mainTask, subTask, system, currentUser) = await SetTrace("104", "1104001", "1");

        //            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
        //            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
        //            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

        //            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

        //        }

        //    }

        //    return RedirectToAction("Index", "UserLogin");
        //}


        //[HttpPost]
        //public async Task<IActionResult> EditStatus(string code, string status)
        //{
        //    string sAr = "";
        //    string sEn = "";
        //    var Contract = await _unitOfWork.CrMasUserLogins.GetByIdAsync(code);
        //    if (Contract != null)
        //    {
        //        if (status == Status.Hold)
        //        {
        //            sAr = "ايقاف";
        //            sEn = "Hold";
        //            Contract.CrMasUserLoginsStatus = Status.Hold;
        //        }
        //        else if (status == Status.Deleted)
        //        {
        //            int CountUserLogins = 0;
        //            CountUserLogins = _userLoginsService.GetOneUserLoginCount(code);
        //            if (CountUserLogins == 0)
        //            {
        //                sAr = "حذف";
        //                sEn = "Remove";
        //                Contract.CrMasUserLoginsStatus = Status.Deleted;
        //            }
        //            else
        //            {
        //                return View(Contract);
        //            }

        //        }
        //        else if (status == "Reactivate")
        //        {
        //            sAr = "استرجاع";
        //            sEn = "Retrive";
        //            Contract.CrMasUserLoginsStatus = Status.Active;
        //        }

        //        await _unitOfWork.CompleteAsync();

        //        // SaveTracing

        //        var (mainTask, subTask, system, currentUser) = await SetTrace("104", "1104001", "1");
        //        await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
        //        subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
        //        subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

        //        return RedirectToAction("Index", "UserLogin");
        //    }


        //    return View(Contract);

        //}

        //[HttpPost]
        //public async Task<IActionResult> CheckChangedField(string Exist_lang, string dataField)
        //{
        //    var All_UserLogins = await _unitOfWork.CrMasUserLogins.GetAllAsync();

        //    if (dataField != null && All_UserLogins != null)
        //    {
        //        if (Exist_lang == "ExistAr")
        //        {
        //            var existingUserLogin_Ar = All_UserLogins.FirstOrDefault(x =>
        //                x.CR_Mas_User_Login_Concatenate_Operation_Ar_Description == dataField);
        //            if (existingUserLogin_Ar != null)
        //            {
        //                ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
        //                return View();
        //            }
        //        }
        //        else if (Exist_lang == "ExistEn")
        //        {
        //            var existingUserLogin_En = All_UserLogins.FirstOrDefault(x =>
        //                x.CR_Mas_User_Login_Concatenate_Operation_En_Description == dataField);
        //            if (existingUserLogin_En != null)
        //            {
        //                ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
        //                return View();
        //            }
        //        }

        //    }
        //    return View();
        //}



    }
  }
