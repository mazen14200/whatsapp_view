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
    public class TaxDuesController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IFinancialTransactionOfRenter _FinancialTransactionOfRenter;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<TaxDuesController> _localizer;


        public TaxDuesController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IFinancialTransactionOfRenter FinancialTransactionOfRenter,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<TaxDuesController> localizer) : base(userManager, unitOfWork, mapper)
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
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "4";
            var (mainTask, subTask, system, currentUser) = await SetTrace("204", "2204005", "2");
            ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

            var titles = await setTitle("204", "2204005", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllTaxOwed = _unitOfWork.CrCasAccountContractTaxOwed.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountContractTaxOwedLessor).OrderByDescending(x => x.CrCasAccountContractTaxOwedDate).ToList();

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            return View(AllTaxOwed);
        }




        //[HttpGet]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    //sidebar Active
        //    ViewBag.id = "#sidebarAcount";
        //    ViewBag.no = "4";
        //    var (mainTask, subTask, system, currentUser) = await SetTrace("204", "2204005", "2");
        //    ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

        //    //To Set Title !!!!!!!!!!!!!
        //    var titles = await setTitle("204", "2204005", "2");
        //    await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

        //    var AllTaxOwed = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && id == x.CrCasAccountReceiptRenterId && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] { "CrCasAccountReceiptRenter" }).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();
        //    var AllRenterLessor = _unitOfWork.CrCasRenterLessor.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterLessorCode && x.CrCasRenterLessorAvailableBalance != 0 && x.CrCasRenterLessorStatus != "R").ToList();

        //    AllTaxOwed = AllTaxOwed.Where(x => AllRenterLessor.Any(y => y.CrCasRenterLessorCode == x.CrCasAccountReceiptLessorCode && y.CrCasRenterLessorId == x.CrCasAccountReceiptRenterId)).ToList();

        //    List<CrCasAccountReceipt>? FinancialTransactionOfRente_Filtered = new List<CrCasAccountReceipt>();

        //    List<List<string>>? All_Counts = new List<List<string>>();

        //    foreach (var FT_Renter1 in AllTaxOwed)
        //    {
        //        decimal? Total_Creditor = 0;
        //        decimal? Total_Debtor = 0;
        //        var x = FinancialTransactionOfRente_Filtered.Find(x => x.CrCasAccountReceiptRenterId == FT_Renter1.CrCasAccountReceiptRenterId);
        //        if (x == null)
        //        {
        //            var counter = 0;
        //            foreach (var FT_Renter_2 in AllTaxOwed)
        //            {
        //                if (FT_Renter1.CrCasAccountReceiptRenterId == FT_Renter_2.CrCasAccountReceiptRenterId && FT_Renter1.CrCasAccountReceiptLessorCode == FT_Renter_2.CrCasAccountReceiptLessorCode)
        //                {
        //                    //Total_Creditor = FT_Renter_2.CrCasRenterContractBasicExpectedTotal + Total_Creditor;
        //                    //Total_Debtor = FT_Renter_2.CrCasRenterContractBasicExpectedTotal + Total_Debtor;
        //                    Total_Creditor = 0;
        //                    Total_Debtor = 0;
        //                    counter = counter + 1;
        //                }

        [HttpGet]
        public async Task<PartialViewResult> GetAllContractsByDate_statusAsync(string _max, string _mini, string status)
        {
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "4";
            var (mainTask, subTask, system, currentUser) = await SetTrace("204", "2204005", "2");
            ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

            var titles = await setTitle("204", "2204005", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            if (!string.IsNullOrEmpty(_max) && !string.IsNullOrEmpty(_mini) && _max.Length > 0)
            {
                _max = DateTime.Parse(_max).Date.AddDays(1).ToString("yyyy-MM-dd");
                var AllTaxOwed = _unitOfWork.CrCasAccountContractTaxOwed.FindAll(x => x.CrCasAccountContractTaxOwedDate < DateTime.Parse(_max).Date && x.CrCasAccountContractTaxOwedDate >= DateTime.Parse(_mini).Date && currentUser.CrMasUserInformationLessor == x.CrCasAccountContractTaxOwedLessor).OrderByDescending(x => x.CrCasAccountContractTaxOwedDate).ToList();

                if (status == "All")
                {
                    return PartialView("_DataTableBasic", AllTaxOwed);
                }
                else
                {

                    if (status == "1")
                    {
                        AllTaxOwed = AllTaxOwed.Where(x => x.CrCasAccountContractTaxOwedIsPaid == true).ToList();
                    }
                    else if (status == "0")
                    {
                        AllTaxOwed = AllTaxOwed.Where(x => x.CrCasAccountContractTaxOwedIsPaid == false).ToList();

                    }
                    else
                    {
                    }

                    return PartialView("_DataTableBasic", AllTaxOwed);
                }

            }
            return PartialView();
        }




        public async Task<IActionResult> FailedMessageReport_NoData()
        {
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "4";
            var titles = await setTitle("205", "2204005", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var (mainTask, subTask, system, currentUser) = await SetTrace("204", "2204005", "2");

            var AllTaxOwed = _unitOfWork.CrCasAccountContractTaxOwed.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountContractTaxOwedLessor).OrderByDescending(x => x.CrCasAccountContractTaxOwedDate).ToList();
            if (AllTaxOwed?.Count() < 1)
            {
                ViewBag.Data = "0";
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