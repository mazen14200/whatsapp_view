using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;
using System.Linq;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class RenterBalancesController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IFinancialTransactionOfRenter _FinancialTransactionOfRenter;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<RenterBalancesController> _localizer;


        public RenterBalancesController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IFinancialTransactionOfRenter FinancialTransactionOfRenter,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<RenterBalancesController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _FinancialTransactionOfRenter = FinancialTransactionOfRenter;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "2";
            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203003", "2");
            ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

            var titles = await setTitle("203", "2203003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var FinancialTransactionOfRenterAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302") , new[] { "CrCasAccountReceiptRenter" }).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();
            var AllRenterLessor = _unitOfWork.CrCasRenterLessor.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterLessorCode && x.CrCasRenterLessorAvailableBalance != 0 && x.CrCasRenterLessorStatus != "R").ToList();

            FinancialTransactionOfRenterAll = FinancialTransactionOfRenterAll.Where(x=> AllRenterLessor.Any(y=>y.CrCasRenterLessorCode==x.CrCasAccountReceiptLessorCode && y.CrCasRenterLessorId == x.CrCasAccountReceiptRenterId )).ToList();
            List<CrCasAccountReceipt>? FinancialTransactionOfRente_Filtered = new List<CrCasAccountReceipt>();

            List<List<string>>? All_Counts = new List<List<string>>();

            foreach (var FT_Renter1 in FinancialTransactionOfRenterAll)
            {
                decimal? Total_Creditor = 0;
                decimal? Total_Debtor = 0;
                var x = FinancialTransactionOfRente_Filtered.Find(x => x.CrCasAccountReceiptRenterId == FT_Renter1.CrCasAccountReceiptRenterId);
                if (x == null)
                {
                    var counter = 0;
                    foreach (var FT_Renter_2 in FinancialTransactionOfRenterAll)
                    {
                        if (FT_Renter1.CrCasAccountReceiptRenterId == FT_Renter_2.CrCasAccountReceiptRenterId && FT_Renter1.CrCasAccountReceiptLessorCode == FT_Renter_2.CrCasAccountReceiptLessorCode)
                        {
                            //Total_Creditor = FT_Renter_2.CrCasRenterContractBasicExpectedTotal + Total_Creditor;
                            //Total_Debtor = FT_Renter_2.CrCasRenterContractBasicExpectedTotal + Total_Debtor;
                            Total_Creditor = 0;
                            Total_Debtor = 0;
                            counter = counter + 1;
                        }

                    }
                    All_Counts.Add(new List<string> { FT_Renter1.CrCasAccountReceiptRenterId, counter.ToString(), Total_Creditor?.ToString("N2", CultureInfo.InvariantCulture), Total_Debtor?.ToString("N2", CultureInfo.InvariantCulture) });
                    FinancialTransactionOfRente_Filtered.Add(FT_Renter1);
                }
            }


            FinancialTransactionOfRenterVM FT_RenterVM = new FinancialTransactionOfRenterVM();
            FT_RenterVM.crCasAccountReceipt = FinancialTransactionOfRenterAll;
            FT_RenterVM.crCasRenterLessor = AllRenterLessor;
            FT_RenterVM.All_Counts = All_Counts;
            FT_RenterVM.FinancialTransactionOfRente_Filtered = FinancialTransactionOfRente_Filtered;

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            return View(FT_RenterVM);
        }




        //[HttpGet]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    //sidebar Active
        //    ViewBag.id = "#sidebarRenter";
        //    ViewBag.no = "2";
        //    var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203003", "2");
        //    ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

        //    //To Set Title !!!!!!!!!!!!!
        //    var titles = await setTitle("203", "2203003", "2");
        //    await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

        //    var FinancialTransactionOfRenterAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && id == x.CrCasAccountReceiptRenterId && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] { "CrCasAccountReceiptRenter" }).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();
        //    var AllRenterLessor = _unitOfWork.CrCasRenterLessor.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterLessorCode && x.CrCasRenterLessorAvailableBalance != 0 && x.CrCasRenterLessorStatus != "R").ToList();

        //    FinancialTransactionOfRenterAll = FinancialTransactionOfRenterAll.Where(x => AllRenterLessor.Any(y => y.CrCasRenterLessorCode == x.CrCasAccountReceiptLessorCode && y.CrCasRenterLessorId == x.CrCasAccountReceiptRenterId)).ToList();

        //    List<CrCasAccountReceipt>? FinancialTransactionOfRente_Filtered = new List<CrCasAccountReceipt>();

        //    List<List<string>>? All_Counts = new List<List<string>>();

        //    foreach (var FT_Renter1 in FinancialTransactionOfRenterAll)
        //    {
        //        decimal? Total_Creditor = 0;
        //        decimal? Total_Debtor = 0;
        //        var x = FinancialTransactionOfRente_Filtered.Find(x => x.CrCasAccountReceiptRenterId == FT_Renter1.CrCasAccountReceiptRenterId);
        //        if (x == null)
        //        {
        //            var counter = 0;
        //            foreach (var FT_Renter_2 in FinancialTransactionOfRenterAll)
        //            {
        //                if (FT_Renter1.CrCasAccountReceiptRenterId == FT_Renter_2.CrCasAccountReceiptRenterId && FT_Renter1.CrCasAccountReceiptLessorCode == FT_Renter_2.CrCasAccountReceiptLessorCode)
        //                {
        //                    //Total_Creditor = FT_Renter_2.CrCasRenterContractBasicExpectedTotal + Total_Creditor;
        //                    //Total_Debtor = FT_Renter_2.CrCasRenterContractBasicExpectedTotal + Total_Debtor;
        //                    Total_Creditor = 0;
        //                    Total_Debtor = 0;
        //                    counter = counter + 1;
        //                }

        //            }
        //            All_Counts.Add(new List<string> { FT_Renter1.CrCasAccountReceiptRenterId, counter.ToString(), Total_Creditor?.ToString("N2", CultureInfo.InvariantCulture), Total_Debtor?.ToString("N2", CultureInfo.InvariantCulture) });
        //            FinancialTransactionOfRente_Filtered.Add(FT_Renter1);
        //        }
        //    }

        //    FinancialTransactionOfRenterVM FT_RenterVM = new FinancialTransactionOfRenterVM();
        //    FT_RenterVM.crCasAccountReceipt = FinancialTransactionOfRenterAll;
        //    FT_RenterVM.crCasRenterLessor = AllRenterLessor;
        //    FT_RenterVM.All_Counts = All_Counts;
        //    FT_RenterVM.FinancialTransactionOfRente_Filtered = FinancialTransactionOfRente_Filtered;


        //    if (FinancialTransactionOfRenterAll == null)
        //    {
        //        ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
        //        return View("Index");
        //    }

        //    ViewBag.CountRecord = FinancialTransactionOfRenterAll.Count;

        //    var Single_data = _unitOfWork.CrMasRenterInformation.Find(x => id == x.CrMasRenterInformationId);

        //    var Single_data_Account_Reciept = _unitOfWork.CrCasRenterLessor.Find(x => id == x.CrCasRenterLessorId && x.CrCasRenterLessorCode == currentUser.CrMasUserInformationLessor);

        //    ViewBag.Single_FT_RenterId = Single_data.CrMasRenterInformationId;
        //    ViewBag.Single_FT_RenterNameAr = Single_data.CrMasRenterInformationArName;
        //    ViewBag.Single_FT_RenterNameEn = Single_data.CrMasRenterInformationEnName;

            
        //    ViewBag.AvailableBalance = Single_data_Account_Reciept?.CrCasRenterLessorAvailableBalance?.ToString("N2", CultureInfo.InvariantCulture);
        //    ViewBag.ReservedBalance = Single_data_Account_Reciept?.CrCasRenterLessorReservedBalance?.ToString("N2", CultureInfo.InvariantCulture);
        //    ViewBag.FTR_Balance = Single_data_Account_Reciept?.CrCasRenterLessorBalance?.ToString("N2", CultureInfo.InvariantCulture);

        //    return View(FT_RenterVM);
        //}


        //[HttpGet]
        //public async Task<IActionResult> Edit2Date(string _max, string _mini, string id)
        //{

        //    var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203003", "2");
        //    ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

        //    //sidebar Active
        //    ViewBag.id = "#sidebarRenter";
        //    ViewBag.no = "2";

        //    if (id != null)
        //    {
        //        ViewBag.startDate = DateTime.Parse(_mini).Date.ToString("yyyy-MM-dd");
        //        ViewBag.EndDate = DateTime.Parse(_max).Date.ToString("yyyy-MM-dd");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    //To Set Title !!!!!!!!!!!!!
        //    var titles = await setTitle("203", "2203003", "2");
        //    await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
        //    if (!string.IsNullOrEmpty(_max) && !string.IsNullOrEmpty(_mini) && _max.Length > 0)
        //    {
        //        _max = DateTime.Parse(_max).Date.AddDays(1).ToString("yyyy-MM-dd");
        //        var FinancialTransactionOfRenterAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptDate < DateTime.Parse(_max).Date && x.CrCasAccountReceiptDate >= DateTime.Parse(_mini).Date && currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && id == x.CrCasAccountReceiptRenterId  && (x.CrCasAccountReceiptType=="301" || x.CrCasAccountReceiptType == "302") , new[] { "CrCasAccountReceiptRenter" }).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();

        //        var AllRenterLessor = _unitOfWork.CrCasRenterLessor.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterLessorCode && x.CrCasRenterLessorAvailableBalance != 0 && x.CrCasRenterLessorStatus != "R").ToList();

        //        FinancialTransactionOfRenterAll = FinancialTransactionOfRenterAll.Where(x => AllRenterLessor.Any(y => y.CrCasRenterLessorCode == x.CrCasAccountReceiptLessorCode && y.CrCasRenterLessorId == x.CrCasAccountReceiptRenterId)).ToList();

        //        List<CrCasAccountReceipt>? FinancialTransactionOfRente_Filtered = new List<CrCasAccountReceipt>();

        //        List<List<string>>? All_Counts = new List<List<string>>();

        //        foreach (var FT_Renter1 in FinancialTransactionOfRenterAll)
        //        {
        //            decimal? Total_Creditor = 0;
        //            decimal? Total_Debtor = 0;
        //            var x = FinancialTransactionOfRente_Filtered.Find(x => x.CrCasAccountReceiptRenterId == FT_Renter1.CrCasAccountReceiptRenterId);
        //            if (x == null)
        //            {
        //                var counter = 0;
        //                foreach (var FT_Renter_2 in FinancialTransactionOfRenterAll)
        //                {
        //                    if (FT_Renter1.CrCasAccountReceiptRenterId == FT_Renter_2.CrCasAccountReceiptRenterId && FT_Renter1.CrCasAccountReceiptLessorCode == FT_Renter_2.CrCasAccountReceiptLessorCode)
        //                    {
        //                        //Total_Creditor = FT_Renter_2.CrCasRenterContractBasicExpectedTotal + Total_Creditor;
        //                        //Total_Debtor = FT_Renter_2.CrCasRenterContractBasicExpectedTotal + Total_Debtor;
        //                        Total_Creditor = 0;
        //                        Total_Debtor = 0;
        //                        counter = counter + 1;
        //                    }

        //                }
        //                All_Counts.Add(new List<string> { FT_Renter1.CrCasAccountReceiptRenterId, counter.ToString() , Total_Creditor?.ToString("N2", CultureInfo.InvariantCulture), Total_Debtor?.ToString("N2", CultureInfo.InvariantCulture) });
        //                FinancialTransactionOfRente_Filtered.Add(FT_Renter1);
        //            }
        //        }

        //        FinancialTransactionOfRenterVM FT_RenterVM = new FinancialTransactionOfRenterVM();
        //        FT_RenterVM.crCasAccountReceipt = FinancialTransactionOfRenterAll;
        //        FT_RenterVM.crCasRenterLessor = AllRenterLessor;
        //        FT_RenterVM.All_Counts = All_Counts;
        //        FT_RenterVM.FinancialTransactionOfRente_Filtered = FinancialTransactionOfRente_Filtered;


        //        if (FinancialTransactionOfRenterAll == null)
        //        {
        //            ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
        //            return View("Index");
        //        }

        //        ViewBag.CountRecord = FinancialTransactionOfRenterAll.Count;

        //        var Single_data = _unitOfWork.CrMasRenterInformation.Find(x => id == x.CrMasRenterInformationId);

        //        var Single_data_Account_Reciept = _unitOfWork.CrCasRenterLessor.Find(x => id == x.CrCasRenterLessorId && x.CrCasRenterLessorCode == currentUser.CrMasUserInformationLessor);

        //        ViewBag.Single_FT_RenterId = Single_data.CrMasRenterInformationId;
        //        ViewBag.Single_FT_RenterNameAr = Single_data.CrMasRenterInformationArName;
        //        ViewBag.Single_FT_RenterNameEn = Single_data.CrMasRenterInformationEnName;
                
        //        ViewBag.AvailableBalance = Single_data_Account_Reciept?.CrCasRenterLessorAvailableBalance?.ToString("N2", CultureInfo.InvariantCulture);
        //        ViewBag.ReservedBalance = Single_data_Account_Reciept?.CrCasRenterLessorReservedBalance?.ToString("N2", CultureInfo.InvariantCulture);
        //        ViewBag.FTR_Balance = Single_data_Account_Reciept?.CrCasRenterLessorBalance?.ToString("N2", CultureInfo.InvariantCulture);

        //        return View(FT_RenterVM);

        //    }
        //    return View();
        //}
        public async Task<IActionResult> FailedMessageReport_NoData()
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "2";
            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203003", "2");

            var AllRenterLessor = _unitOfWork.CrCasRenterLessor.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterLessorCode && x.CrCasRenterLessorAvailableBalance != 0 && x.CrCasRenterLessorStatus != "R").ToList();
            if (AllRenterLessor?.Count() < 1)
            {
                ViewBag.Data = "0";
                //_toastNotification.AddErrorToastMessage(_localizer["NoDataToShow"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return View();
            }
            else
            {
                ViewBag.Data = "1";
                return RedirectToAction("Index");
            }

        }


    }
}

