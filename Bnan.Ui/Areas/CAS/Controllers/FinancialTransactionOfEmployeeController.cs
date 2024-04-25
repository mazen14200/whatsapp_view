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

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class FinancialTransactionOfEmployeeController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IFinancialTransactionOfEmployee _FinancialTransactionOfEmployee;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<FinancialTransactionOfEmployeeController> _localizer;


        public FinancialTransactionOfEmployeeController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IFinancialTransactionOfEmployee FinancialTransactionOfEmployee,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<FinancialTransactionOfEmployeeController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _FinancialTransactionOfEmployee = FinancialTransactionOfEmployee;
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
            ViewBag.no = "11";

            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205012", "2");
            ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

            var titles = await setTitle("205", "2205012", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var FinancialTransactionOfEmployeeAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] {  "CrCasAccountReceiptUserNavigation" }).Where(x=>x.CrCasAccountReceiptUserNavigation?.CrMasUserInformationCode != null).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();

            List<CrCasAccountReceipt>? FinancialTransactionOfEmployee_Filtered = new List<CrCasAccountReceipt>();

            List<List<string>>? All_Counts = new List<List<string>>();

            foreach (var FT_Employee1 in FinancialTransactionOfEmployeeAll)
            {
                decimal? Total_Creditor = 0;
                decimal? Total_Debtor = 0;
                var x = FinancialTransactionOfEmployee_Filtered.Find(x => x.CrCasAccountReceiptUserNavigation?.CrMasUserInformationCode == FT_Employee1.CrCasAccountReceiptUserNavigation?.CrMasUserInformationCode);
                if (x == null)
                {
                    var counter = 0;
                    foreach (var FT_Employee_2 in FinancialTransactionOfEmployeeAll)
                    {
                        if (FT_Employee1.CrCasAccountReceiptUserNavigation?.CrMasUserInformationCode == FT_Employee_2.CrCasAccountReceiptUserNavigation?.CrMasUserInformationCode && FT_Employee1.CrCasAccountReceiptLessorCode == FT_Employee_2.CrCasAccountReceiptLessorCode)
                        {
                            //Total_Creditor = FT_Employee_2.CrCasEmployeeContractBasicExpectedTotal + Total_Creditor;
                            //Total_Debtor = FT_Employee_2.CrCasEmployeeContractBasicExpectedTotal + Total_Debtor;
                            Total_Creditor = 0;
                            Total_Debtor = 0;
                            counter = counter + 1;
                        }

                    }
                    All_Counts.Add(new List<string> { FT_Employee1.CrCasAccountReceiptUserNavigation.CrMasUserInformationCode, counter.ToString(), Total_Creditor?.ToString("N2", CultureInfo.InvariantCulture), Total_Debtor?.ToString("N2", CultureInfo.InvariantCulture) });
                    FinancialTransactionOfEmployee_Filtered.Add(FT_Employee1);
                }
            }

            FinancialTransactionOfEmployeeVM FT_EmployeeVM = new FinancialTransactionOfEmployeeVM();
            FT_EmployeeVM.crCasAccountReceipt = FinancialTransactionOfEmployeeAll;
            FT_EmployeeVM.All_Counts = All_Counts;
            FT_EmployeeVM.FinancialTransactionOfEmployee_Filtered = FinancialTransactionOfEmployee_Filtered;

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            return View(FT_EmployeeVM);
        }




        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "11";
            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205012", "2");
            ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("205", "2205012", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var FinancialTransactionOfEmployeeAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] { "CrCasAccountReceiptReferenceTypeNavigation", "CrCasAccountReceiptUserNavigation", "CrCasAccountReceiptNavigation" }).Where(x=> id == x.CrCasAccountReceiptUserNavigation?.CrMasUserInformationCode).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();


            FinancialTransactionOfEmployeeVM FT_EmployeeVM = new FinancialTransactionOfEmployeeVM();
            FT_EmployeeVM.crCasAccountReceipt = FinancialTransactionOfEmployeeAll;


            if (FinancialTransactionOfEmployeeAll == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }

            ViewBag.CountRecord = FinancialTransactionOfEmployeeAll.Count;

            var Single_data = _unitOfWork.CrMasUserInformation.Find(x => id == x.CrMasUserInformationCode && x.CrMasUserInformationLessor == currentUser.CrMasUserInformationLessor);


            ViewBag.Single_FT_EmployeeId = Single_data.CrMasUserInformationCode;
            ViewBag.Single_FT_EmployeeNameAr = Single_data.CrMasUserInformationArName;
            ViewBag.Single_FT_EmployeeNameEn = Single_data.CrMasUserInformationEnName;


            ViewBag.AvailableBalance = Single_data?.CrMasUserInformationAvailableBalance?.ToString("N2", CultureInfo.InvariantCulture);
            ViewBag.ReservedBalance = Single_data?.CrMasUserInformationReservedBalance?.ToString("N2", CultureInfo.InvariantCulture);
            ViewBag.FTR_Balance = Single_data?.CrMasUserInformationTotalBalance?.ToString("N2", CultureInfo.InvariantCulture);
            ViewBag.FTR_Credit_Limit = Single_data?.CrMasUserInformationCreditLimit?.ToString("N2", CultureInfo.InvariantCulture);

            return View(FT_EmployeeVM);
        }



        [HttpGet]
        public async Task<PartialViewResult> GetAllContractsByDate_statusAsync(string _max, string _mini, string status,string id)
        {
            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205012", "2");
            ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "11";

            ViewBag.startDate = DateTime.Parse(_mini).Date.ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Parse(_max).Date.ToString("yyyy-MM-dd");
            


            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("205", "2205012", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            if (!string.IsNullOrEmpty(_max) && !string.IsNullOrEmpty(_mini) && _max.Length > 0)
            {
                _max = DateTime.Parse(_max).Date.AddDays(1).ToString("yyyy-MM-dd");
                var FinancialTransactionOfEmployeeAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptDate < DateTime.Parse(_max).Date && x.CrCasAccountReceiptDate >= DateTime.Parse(_mini).Date && currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] { "CrCasAccountReceiptReferenceTypeNavigation", "CrCasAccountReceiptUserNavigation", "CrCasAccountReceiptNavigation" }).Where(x => id == x.CrCasAccountReceiptUserNavigation?.CrMasUserInformationCode).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();



                if (status == "All")
                {
                    FinancialTransactionOfEmployeeVM FT_EmployeeVM = new FinancialTransactionOfEmployeeVM();
                    FT_EmployeeVM.crCasAccountReceipt = FinancialTransactionOfEmployeeAll;
                    return PartialView("_DataTable_Internal_Contract", FT_EmployeeVM);
                }
                else
                {

                    if (status == "deported")
                    {
                        FinancialTransactionOfEmployeeAll = FinancialTransactionOfEmployeeAll.Where(x => x.CrCasAccountReceiptIsPassing =="3").ToList();
                    }
                    else if (status == "Reserved")
                    {
                        FinancialTransactionOfEmployeeAll = FinancialTransactionOfEmployeeAll.Where(x => x.CrCasAccountReceiptIsPassing == "2").ToList();
                    }
                    else if (status == "Custody")
                    {
                        FinancialTransactionOfEmployeeAll = FinancialTransactionOfEmployeeAll.Where(x => x.CrCasAccountReceiptIsPassing == "1").ToList();
                    }
                    else
                    {

                    }

                    FinancialTransactionOfEmployeeVM FT_EmployeeVM = new FinancialTransactionOfEmployeeVM();
                    FT_EmployeeVM.crCasAccountReceipt = FinancialTransactionOfEmployeeAll;

                    return PartialView("_DataTable_Internal_Contract", FT_EmployeeVM);

                }

            }
            return PartialView();
        }



        [HttpGet]
        public async Task<IActionResult> Edit2Date(string _max, string _mini,string status , string id)
        {


            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205012", "2");
            ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "11";

            ViewBag.startDate = DateTime.Parse(_mini).Date.ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Parse(_max).Date.ToString("yyyy-MM-dd");


            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("205", "2205012", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            if (!string.IsNullOrEmpty(_max) && !string.IsNullOrEmpty(_mini) && _max.Length > 0)
            {
                _max = DateTime.Parse(_max).Date.AddDays(1).ToString("yyyy-MM-dd");
                var FinancialTransactionOfEmployeeAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptDate < DateTime.Parse(_max).Date && x.CrCasAccountReceiptDate >= DateTime.Parse(_mini).Date && currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] { "CrCasAccountReceiptReferenceTypeNavigation", "CrCasAccountReceiptUserNavigation", "CrCasAccountReceiptNavigation" }).Where(x=> id == x.CrCasAccountReceiptUserNavigation?.CrMasUserInformationCode).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();

                FinancialTransactionOfEmployeeVM FT_EmployeeVM = new FinancialTransactionOfEmployeeVM();
                FT_EmployeeVM.crCasAccountReceipt = FinancialTransactionOfEmployeeAll;

                if (FinancialTransactionOfEmployeeAll == null)
                {
                    ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                    return View("Index");
                }

                ViewBag.CountRecord = FinancialTransactionOfEmployeeAll.Count;


                var Single_data = _unitOfWork.CrMasUserInformation.Find(x => id == x.CrMasUserInformationCode && x.CrMasUserInformationLessor == currentUser.CrMasUserInformationLessor);


                ViewBag.Single_FT_EmployeeId = Single_data.CrMasUserInformationCode;
                ViewBag.Single_FT_EmployeeNameAr = Single_data.CrMasUserInformationArName;
                ViewBag.Single_FT_EmployeeNameEn = Single_data.CrMasUserInformationEnName;


                ViewBag.AvailableBalance = Single_data?.CrMasUserInformationAvailableBalance?.ToString("N2", CultureInfo.InvariantCulture);
                ViewBag.ReservedBalance = Single_data?.CrMasUserInformationReservedBalance?.ToString("N2", CultureInfo.InvariantCulture);
                ViewBag.FTR_Balance = Single_data?.CrMasUserInformationTotalBalance?.ToString("N2", CultureInfo.InvariantCulture);
                ViewBag.FTR_Credit_Limit = Single_data?.CrMasUserInformationCreditLimit?.ToString("N2", CultureInfo.InvariantCulture);


                return View(FT_EmployeeVM);

            }
            return View();
        }
        public async Task<IActionResult> FailedMessageReport_NoData()
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "11";
            var titles = await setTitle("205", "2205012", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205012", "2");

            var FinancialTransactionOfEmployeeAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] { "CrCasAccountReceiptUserNavigation" }).Where(x => x.CrCasAccountReceiptUserNavigation?.CrMasUserInformationCode != null).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();
            if (FinancialTransactionOfEmployeeAll?.Count() < 1)
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


        [HttpGet]
        public async Task<IActionResult> GetReceiptDetails(string ReceiptNo)
        {
            var receipt = await _unitOfWork.CrCasAccountReceipt.FindAsync(x => x.CrCasAccountReceiptNo == ReceiptNo, new[] {
                                                                                                                            "CrCasAccountReceiptReferenceTypeNavigation",
                                                                                                                            "CrCasAccountReceiptBankNavigation",
                                                                                                                            "CrCasAccountReceiptSalesPointNavigation",
                                                                                                                            "CrCasAccountReceiptPaymentMethodNavigation",
                                                                                                                             "CrCasAccountReceiptAccountNavigation"});
            var userRecevied = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == receipt.CrCasAccountReceiptPassingUser);
            if (receipt == null) return Json(false);
            ReceiptDetailsVM receiptDetails = new ReceiptDetailsVM();

            receiptDetails.ReceiptNo = ReceiptNo;
            receiptDetails.Date = receipt.CrCasAccountReceiptDate?.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            receiptDetails.Creditor = receipt.CrCasAccountReceiptPayment?.ToString("N2", CultureInfo.InvariantCulture);
            receiptDetails.Debit = receipt.CrCasAccountReceiptReceipt?.ToString("N2", CultureInfo.InvariantCulture);
            receiptDetails.ReferenceNo = receipt.CrCasAccountReceiptReferenceNo;
            receiptDetails.ReferenceTypeAr = receipt.CrCasAccountReceiptReferenceTypeNavigation?.CrMasSupAccountReceiptReferenceArName;
            receiptDetails.ReferenceTypeEn = receipt.CrCasAccountReceiptReferenceTypeNavigation?.CrMasSupAccountReceiptReferenceEnName;
            if (receipt.CrCasAccountReceiptBank == "00")
            {
                receiptDetails.AccountBankCode = "";
                receiptDetails.BankAr = "";
                receiptDetails.BankEn = "";
            }
            else
            {
                receiptDetails.AccountBankCode = receipt.CrCasAccountReceiptAccountNavigation?.CrCasAccountBankIban;
                receiptDetails.BankAr = receipt.CrCasAccountReceiptBankNavigation?.CrMasSupAccountBankArName;
                receiptDetails.BankEn = receipt.CrCasAccountReceiptBankNavigation?.CrMasSupAccountBankEnName;
            }
            receiptDetails.SalesPointAr = receipt.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointArName;
            receiptDetails.SalesPointEn = receipt.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointEnName;
            receiptDetails.PaymentMethodAr = receipt.CrCasAccountReceiptPaymentMethodNavigation?.CrMasSupAccountPaymentMethodArName;
            receiptDetails.PaymentMethodEn = receipt.CrCasAccountReceiptPaymentMethodNavigation?.CrMasSupAccountPaymentMethodEnName;
            receiptDetails.CustodyNo = receipt.CrCasAccountReceiptPassingReference;
            receiptDetails.StatusReceipt = receipt.CrCasAccountReceiptIsPassing;
            receiptDetails.UserReceivedAr = userRecevied?.CrMasUserInformationArName;
            receiptDetails.UserReceivedEn = userRecevied?.CrMasUserInformationEnName;
            receiptDetails.ReceivedDate = receipt.CrCasAccountReceiptPassingDate?.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            receiptDetails.Reasons = receipt.CrCasAccountReceiptReasons;



            return Json(receiptDetails);
        }
    }
}

