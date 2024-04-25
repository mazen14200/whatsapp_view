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
    public class FinancialTransactionOfSalesPointController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IFinancialTransactionOfSalesPoint _FinancialTransactionOfSalesPoint;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<FinancialTransactionOfSalesPointController> _localizer;


        public FinancialTransactionOfSalesPointController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IFinancialTransactionOfSalesPoint FinancialTransactionOfSalesPoint,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<FinancialTransactionOfSalesPointController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _FinancialTransactionOfSalesPoint = FinancialTransactionOfSalesPoint;
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
            ViewBag.no = "10";

            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205011", "2");
            ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

            var titles = await setTitle("205", "2205011", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var FinancialTransactionOfSalesPointAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] { "CrCasAccountReceiptSalesPointNavigation", "CrCasAccountReceiptNavigation" }).Where(x=> x.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointNo != null).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();

            List<CrCasAccountReceipt>? FinancialTransactionOfSalesPoints_Filtered = new List<CrCasAccountReceipt>();

            List<List<string>>? All_Counts = new List<List<string>>();

            foreach (var FT_SalesPoint1 in FinancialTransactionOfSalesPointAll)
            {
                decimal? Total_Creditor = 0;
                decimal? Total_Debtor = 0;
                var x = FinancialTransactionOfSalesPoints_Filtered.Find(x => x.CrCasAccountReceiptSalesPoint == FT_SalesPoint1.CrCasAccountReceiptSalesPoint);
                if (x == null)
                {
                    var counter = 0;
                    foreach (var FT_SalesPoint_2 in FinancialTransactionOfSalesPointAll)
                    {
                        if (FT_SalesPoint1.CrCasAccountReceiptSalesPoint == FT_SalesPoint_2.CrCasAccountReceiptSalesPoint && FT_SalesPoint1.CrCasAccountReceiptLessorCode == FT_SalesPoint_2.CrCasAccountReceiptLessorCode)
                        {
                            //Total_Creditor = FT_SalesPoint_2.CrCasSalesPointContractBasicExpectedTotal + Total_Creditor;
                            //Total_Debtor = FT_SalesPoint_2.CrCasSalesPointContractBasicExpectedTotal + Total_Debtor;
                            Total_Creditor = 0;
                            Total_Debtor = 0;
                            counter = counter + 1;
                        }

                    }
                    All_Counts.Add(new List<string> { FT_SalesPoint1.CrCasAccountReceiptSalesPoint, counter.ToString(), Total_Creditor?.ToString("N2", CultureInfo.InvariantCulture), Total_Debtor?.ToString("N2", CultureInfo.InvariantCulture) });
                    FinancialTransactionOfSalesPoints_Filtered.Add(FT_SalesPoint1);
                }
            }

            FinancialTransactionOfSalesPointVM FT_SalesPointVM = new FinancialTransactionOfSalesPointVM();
            FT_SalesPointVM.crCasAccountReceipt = FinancialTransactionOfSalesPointAll;
            FT_SalesPointVM.FinancialTransactionOfSalesPoints_Filtered = FinancialTransactionOfSalesPoints_Filtered;
            //FT_SalesPointVM.All_Counts = All_Counts;
            //FT_SalesPointVM.FinancialTransactionOfSalesPoints_Filtered = FinancialTransactionOfSalesPoints_Filtered;

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            return View(FT_SalesPointVM);
        }




        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "10";
            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205011", "2");
            ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("205", "2205011", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var FinancialTransactionOfSalesPointAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302") , new[] { "CrCasAccountReceiptReferenceTypeNavigation", "CrCasAccountReceiptUserNavigation" , "CrCasAccountReceiptSalesPointNavigation" }).Where(x=> id == x.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointCode && x.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointNo != null).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();
            

            FinancialTransactionOfSalesPointVM FT_SalesPointVM = new FinancialTransactionOfSalesPointVM();
            FT_SalesPointVM.crCasAccountReceipt = FinancialTransactionOfSalesPointAll;


            if (FinancialTransactionOfSalesPointAll == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }

            ViewBag.CountRecord = FinancialTransactionOfSalesPointAll.Count;

            var Single_data_Account_Reciept = _unitOfWork.CrCasAccountSalesPoint.Find(x => id == x.CrCasAccountSalesPointCode && x.CrCasAccountSalesPointLessor == currentUser.CrMasUserInformationLessor);

            var Single_data_SPoint = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] { "CrCasAccountReceiptReferenceTypeNavigation", "CrCasAccountReceiptUserNavigation", "CrCasAccountReceiptNavigation", "CrCasAccountReceiptBankNavigation", "CrCasAccountReceiptAccountNavigation", "CrCasAccountReceiptSalesPointNavigation" }).Where(x => id == x.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointCode && x.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointNo != null).FirstOrDefault();

            ViewBag.Single_FT_SalesPointId = Single_data_Account_Reciept?.CrCasAccountSalesPointCode;
            ViewBag.Single_FT_SalesPointNo = Single_data_Account_Reciept?.CrCasAccountSalesPointNo;
            ViewBag.Single_FT_SalesPointNameAr = Single_data_Account_Reciept?.CrCasAccountSalesPointArName;
            ViewBag.Single_FT_SalesPointNameEn = Single_data_Account_Reciept?.CrCasAccountSalesPointEnName;

            ViewBag.FTS_BranchAr = Single_data_SPoint?.CrCasAccountReceiptNavigation?.CrCasBranchInformationArShortName;
            ViewBag.FTS_BankAr = Single_data_SPoint?.CrCasAccountReceiptBankNavigation?.CrMasSupAccountBankArName;
            ViewBag.FTS_BranchEn = Single_data_SPoint?.CrCasAccountReceiptNavigation?.CrCasBranchInformationEnShortName;
            ViewBag.FTS_BankEn = Single_data_SPoint?.CrCasAccountReceiptBankNavigation?.CrMasSupAccountBankEnName;
            ViewBag.FTS_IBAn = Single_data_SPoint?.CrCasAccountReceiptAccountNavigation?.CrCasAccountBankIban;
            ViewBag.FTS_Account_NameAr = Single_data_SPoint?.CrCasAccountReceiptAccountNavigation?.CrCasAccountBankArName;
            ViewBag.FTS_Account_NameEn = Single_data_SPoint?.CrCasAccountReceiptAccountNavigation?.CrCasAccountBankEnName;

            ViewBag.AvailableBalance = Single_data_Account_Reciept?.CrCasAccountSalesPointTotalAvailable?.ToString("N2", CultureInfo.InvariantCulture);
            ViewBag.ReservedBalance = Single_data_Account_Reciept?.CrCasAccountSalesPointTotalReserved?.ToString("N2", CultureInfo.InvariantCulture);
            ViewBag.FTR_Balance = Single_data_Account_Reciept?.CrCasAccountSalesPointTotalBalance?.ToString("N2", CultureInfo.InvariantCulture);

            return View(FT_SalesPointVM);
        }


        [HttpGet]
        public async Task<IActionResult> Edit2Date(string _max, string _mini, string id)
        {

            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205011", "2");
            ViewBag.CurrentLessor = currentUser.CrMasUserInformationLessor;

            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "10";
            if (id != null)
            {
                ViewBag.startDate = DateTime.Parse(_mini).Date.ToString("yyyy-MM-dd");
                ViewBag.EndDate = DateTime.Parse(_max).Date.ToString("yyyy-MM-dd");
            }
            else
            {
                return RedirectToAction("Index");
            }
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("205", "2205011", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            if (!string.IsNullOrEmpty(_max) && !string.IsNullOrEmpty(_mini) && _max.Length > 0)
            {
                _max = DateTime.Parse(_max).Date.AddDays(1).ToString("yyyy-MM-dd");
                var FinancialTransactionOfSalesPointAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptDate < DateTime.Parse(_max).Date && x.CrCasAccountReceiptDate >= DateTime.Parse(_mini).Date && currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode  && (x.CrCasAccountReceiptType=="301" || x.CrCasAccountReceiptType == "302") , new[] { "CrCasAccountReceiptReferenceTypeNavigation", "CrCasAccountReceiptUserNavigation" , "CrCasAccountReceiptSalesPointNavigation" }).Where(x => id == x.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointCode && x.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointNo != null).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();

                

                FinancialTransactionOfSalesPointVM FT_SalesPointVM = new FinancialTransactionOfSalesPointVM();
                FT_SalesPointVM.crCasAccountReceipt = FinancialTransactionOfSalesPointAll;

                if (FinancialTransactionOfSalesPointAll == null)
                {
                    ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                    return View("Index");
                }

                ViewBag.CountRecord = FinancialTransactionOfSalesPointAll.Count;


                var Single_data_Account_Reciept = _unitOfWork.CrCasAccountSalesPoint.Find(x => id == x.CrCasAccountSalesPointCode && x.CrCasAccountSalesPointLessor == currentUser.CrMasUserInformationLessor);

                var Single_data_SPoint = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] { "CrCasAccountReceiptReferenceTypeNavigation", "CrCasAccountReceiptUserNavigation", "CrCasAccountReceiptNavigation", "CrCasAccountReceiptBankNavigation", "CrCasAccountReceiptAccountNavigation" , "CrCasAccountReceiptSalesPointNavigation" }).Where(x => id == x.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointCode && x.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointNo != null).FirstOrDefault();

                ViewBag.Single_FT_SalesPointId = Single_data_Account_Reciept?.CrCasAccountSalesPointCode;
                ViewBag.Single_FT_SalesPointNo = Single_data_Account_Reciept?.CrCasAccountSalesPointNo;
                ViewBag.Single_FT_SalesPointNameAr = Single_data_Account_Reciept?.CrCasAccountSalesPointArName;
                ViewBag.Single_FT_SalesPointNameEn = Single_data_Account_Reciept?.CrCasAccountSalesPointEnName;

                ViewBag.FTS_BranchAr = Single_data_SPoint?.CrCasAccountReceiptNavigation?.CrCasBranchInformationArShortName;
                ViewBag.FTS_BankAr = Single_data_SPoint?.CrCasAccountReceiptBankNavigation?.CrMasSupAccountBankArName;
                ViewBag.FTS_BranchEn = Single_data_SPoint?.CrCasAccountReceiptNavigation?.CrCasBranchInformationEnShortName;
                ViewBag.FTS_BankEn = Single_data_SPoint?.CrCasAccountReceiptBankNavigation?.CrMasSupAccountBankEnName;
                ViewBag.FTS_IBAn = Single_data_SPoint?.CrCasAccountReceiptAccountNavigation?.CrCasAccountBankIban;
                ViewBag.FTS_Account_NameAr = Single_data_SPoint?.CrCasAccountReceiptAccountNavigation?.CrCasAccountBankArName;
                ViewBag.FTS_Account_NameEn = Single_data_SPoint?.CrCasAccountReceiptAccountNavigation?.CrCasAccountBankEnName;

                ViewBag.AvailableBalance = Single_data_Account_Reciept?.CrCasAccountSalesPointTotalAvailable?.ToString("N2", CultureInfo.InvariantCulture);
                ViewBag.ReservedBalance = Single_data_Account_Reciept?.CrCasAccountSalesPointTotalReserved?.ToString("N2", CultureInfo.InvariantCulture);
                ViewBag.FTR_Balance = Single_data_Account_Reciept?.CrCasAccountSalesPointTotalBalance?.ToString("N2", CultureInfo.InvariantCulture);

                return View(FT_SalesPointVM);

            }
            return View();
        }
        public async Task<IActionResult> FailedMessageReport_NoData()
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "10";
            var titles = await setTitle("205", "2205011", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205011", "2");

            var FinancialTransactionOfSalesPointAll = _unitOfWork.CrCasAccountReceipt.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasAccountReceiptLessorCode && (x.CrCasAccountReceiptType == "301" || x.CrCasAccountReceiptType == "302"), new[] { "CrCasAccountReceiptSalesPointNavigation", "CrCasAccountReceiptNavigation" }).Where(x=> x.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointNo != null).OrderByDescending(x => x.CrCasAccountReceiptDate).ToList();
            if (FinancialTransactionOfSalesPointAll?.Count() < 1)
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

