using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.MAS;
using MessagePack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Numerics;
namespace Bnan.Ui.Areas.CAS.Controllers
{

    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class ReportContractController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly ICompnayContract _compnayContract;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<ReportContractController> _localizer;


        public ReportContractController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, ICompnayContract compnayContract,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<ReportContractController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _compnayContract = compnayContract;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {

            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "2";
            //ViewBag.StartDate = "2024-02-15";
            //ViewBag.EndDate = "2024-02-15";
            ViewBag.StartDate = "";
            ViewBag.EndDate = "";

            var titles = await setTitle("205", "2205003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            //var RenterContractAll = await _unitOfWork.CrCasRenterLessor.GetAllAsync();
            //var RenterContractAllA = RenterContractAll.Where(x => x.CrCasRenterLessorStatus == "A").ToList();
            //var RenterContractAllA = RenterContractAll.ToList();
            var RenterContractAllA = _unitOfWork.CrMasRenterPost.FindAll(x => x.CrMasRenterPostArShortConcatenate != null).ToList();
            ViewData["Adress"] = RenterContractAllA;

            var RenterContract_Basic_All = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic4", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation" }).Where(x=> x.CrCasRenterContractBasicStatus != Status.Extension).OrderByDescending(x => x.CrCasRenterContractBasicRenterId).ThenByDescending(y => y.CrCasRenterContractBasicIssuedDate).ToList();

            ViewData["Data_Table"] = RenterContract_Basic_All;

            var RenterNameAll = _unitOfWork.CrMasRenterInformation.GetAll().ToList();
            ViewData["RenterNameAll"] = RenterNameAll;

            //--------------------------------

            var RenterContract_Basic_All_Date = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic4", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation" }).Where(x => x.CrCasRenterContractBasicStatus != Status.Extension).OrderByDescending(y => y.CrCasRenterContractBasicIssuedDate).ToList();

            var lastDate = RenterContract_Basic_All_Date.FirstOrDefault(x => x.CrCasRenterContractBasicIssuedDate != null).CrCasRenterContractBasicIssuedDate;
            var startDate = RenterContract_Basic_All_Date.LastOrDefault(x => x.CrCasRenterContractBasicIssuedDate != null).CrCasRenterContractBasicIssuedDate;
            if (lastDate != null && startDate != null)
            {
                //var startDate = lastDate.Value.AddMonths(-1);
                ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
                ViewBag.EndDate = lastDate?.ToString("yyyy-MM-dd");


                //var UserLoginbyDate_filtered = UserLoginbyDateAll1.Where(x => x.CrMasUserLoginEntryDate <= lastDate && x.CrMasUserLoginEntryDate >= startDate);
            }
            return View();

        }
        
        [HttpGet]
        public PartialViewResult GetAllContractsByDate_status(string _max, string _mini, string status)
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "2";

            if (!string.IsNullOrEmpty(_max) && !string.IsNullOrEmpty(_mini))
            {
                // "today"  "tomorrow"   "after_longTime" 

                _max = DateTime.Parse(_max).Date.AddDays(1).ToString("yyyy-MM-dd");

                if (status == "All")
                {
                    var RenterContract_Basic_All1 = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic4", "CrCasRenterContractBasic5", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation" }).Where(x => x.CrCasRenterContractBasicIssuedDate < DateTime.Parse(_max).Date && x.CrCasRenterContractBasicIssuedDate >= DateTime.Parse(_mini).Date && x.CrCasRenterContractBasicStatus !=Status.Extension).OrderByDescending(x => x.CrCasRenterContractBasicRenterId).ThenByDescending(y => y.CrCasRenterContractBasicIssuedDate).ToList();

                    ViewData["Data_Table"] = RenterContract_Basic_All1;
                }
                else
                {
                    var today = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    var tomorrow = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");

                    var RenterContract_Basic_All = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic4", "CrCasRenterContractBasic5", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation" }).Where(x => x.CrCasRenterContractBasicIssuedDate < DateTime.Parse(_max).Date && x.CrCasRenterContractBasicIssuedDate >= DateTime.Parse(_mini).Date && x.CrCasRenterContractBasicStatus != Status.Extension).OrderByDescending(x => x.CrCasRenterContractBasicRenterId).ThenByDescending(y => y.CrCasRenterContractBasicIssuedDate).ToList();

                    if (status == "today")
                    {
                        ViewData["Data_Table"] = RenterContract_Basic_All.Where(x=>x.CrCasRenterContractBasicExpectedEndDate?.ToString("yyyy-MM-dd") ==today).ToList();
                    }
                    else if (status == "tomorrow")
                    {
                        ViewData["Data_Table"] = RenterContract_Basic_All.Where(x => x.CrCasRenterContractBasicExpectedEndDate?.ToString("yyyy-MM-dd") == tomorrow).ToList();
                    }
                    else if (status == "after_longTime")
                    {
                        ViewData["Data_Table"] = RenterContract_Basic_All.Where(x => x.CrCasRenterContractBasicExpectedEndDate > DateTime.Parse(tomorrow).Date && x.CrCasRenterContractBasicStatus !=Status.Expire).ToList();
                    }
                    else
                    {
                        ViewData["Data_Table"] = RenterContract_Basic_All.Where(x => x.CrCasRenterContractBasicStatus == status).ToList();
                    }
                }
                


                var RenterNameAll = _unitOfWork.CrMasRenterInformation.GetAll().ToList();
                ViewData["RenterNameAll"] = RenterNameAll;


                return PartialView("_DataTableReportContract");

            }
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult GetRenterContractsByStatus(string status)
        {
           
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;

            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "2";
            //To Set Title
            var titles = await setTitle("101", "1101003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var BnanContract = _unitOfWork.CrMasContractCompany.Find(x => x.CrMasContractCompanyNo == id, new[] { "CrMasContractCompanyProceduresNavigation", "CrMasContractCompanyLessorNavigation" });
            var model = _mapper.Map<ContractCompanyVM>(BnanContract);
            model.ProdcudureArTaskName = BnanContract.CrMasContractCompanyProceduresNavigation.CrMasSysProceduresArName;
            model.ProdcudureEnTaskName = BnanContract.CrMasContractCompanyProceduresNavigation.CrMasSysProceduresEnName;
            model.CompanyNameAr = BnanContract.CrMasContractCompanyLessorNavigation?.CrMasLessorInformationArShortName;
            model.CompanyNameEn = BnanContract.CrMasContractCompanyLessorNavigation?.CrMasLessorInformationEnShortName;
            var activiation = "";
            if (currentCulture == "en-US")
            {
                if (model.CrMasContractCompanyActivation == "1") activiation = "Subscribtion";
                if (model.CrMasContractCompanyActivation == "2") activiation = "Value";
                if (model.CrMasContractCompanyActivation == "3") activiation = "Rate";
            }
            else
            {
                if (model.CrMasContractCompanyActivation == "1") activiation = "اشتراك";
                if (model.CrMasContractCompanyActivation == "2") activiation = "قيمة";
                if (model.CrMasContractCompanyActivation == "3") activiation = "نسبة";
            }
            var companyContractDetailed = _unitOfWork.CrMasContractCompanyDetailed.FindAll(x => x.CrMasContractCompanyDetailedNo == model.CrMasContractCompanyNo);
            if (companyContractDetailed.Count() > 0)
            {
                ViewBag.ContractsDetailed = companyContractDetailed;
            }
            ViewBag.SelectOption = activiation;
            ViewBag.Date = model.CrMasContractCompanyDate?.Date.ToString("dd/MM/yyyy");
            ViewBag.StartDate = model.CrMasContractCompanyStartDate?.Date.ToString("dd/MM/yyyy");
            ViewBag.EndDate = model.CrMasContractCompanyEndDate?.Date.ToString("dd/MM/yyyy");
            ViewBag.CrMasContractCompanyTaxRate = model.CrMasContractCompanyTaxRate;
            ViewBag.CrMasContractCompanyDiscountRate = model.CrMasContractCompanyDiscountRate;
            ViewBag.CrMasContractCompanyAnnualFees = model.CrMasContractCompanyAnnualFees;

            return View(model);
        }

        public IActionResult SuccesssMessageReport1()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index");
        }
        public IActionResult FailedMessageReport1()
        {
            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> Edit(string CompanyContractCode, string DateContract, string StartDateContract, string EndDateContract, string ContractCompanyAnnualFees,
        string ContractCompanyTaxRate, string ContractCompanyDiscountRate, string SelectedOption, List<ContractDetailedVM>? data)
        {
            var activiation = "";
            if (SelectedOption == "Subscribtion") activiation = "1";
            if (SelectedOption == "Value") activiation = "2";
            if (SelectedOption == "Rate") activiation = "3";
            try
            {
                await _compnayContract.UpdateCompanyContract(CompanyContractCode, DateContract, StartDateContract, EndDateContract, ContractCompanyAnnualFees, ContractCompanyTaxRate, ContractCompanyDiscountRate, activiation);
                if (SelectedOption != "Subscribtion" && data != null)
                {
                    int serial = 0;
                    foreach (var item in data)
                    {
                        if (item.To != null && item.To != "" && item.Value != null && item.Value != "")
                        {
                            serial++;
                            await _compnayContract.AddCompanyContractDetailed(CompanyContractCode, item.From, item.To, item.Value, serial);
                        }
                    }
                }
                await _unitOfWork.CompleteAsync();

            }
            catch (Exception ex)
            {
                throw;
            }


            // SaveTracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("101", "1101003", "1");
            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            return Json(new { success = true });
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

