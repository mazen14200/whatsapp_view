using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class ReportController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<ReportController> _localizer;
        private readonly IContract _ContractServices;
        public ReportController(IStringLocalizer<ReportController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IContract contractServices) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _ContractServices = contractServices;
        }
        public async Task<IActionResult> Index()
        {
            //To Set Title 
            var titles = await setTitle("501", "5501012", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();

            var userInfo = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == userLogin.CrMasUserInformationCode, new[] { "CrMasUserBranchValidities" });
            var branchValidity = userInfo.CrMasUserBranchValidities.FirstOrDefault(x => x.CrMasUserBranchValidityBranch == bsLayoutVM.SelectedBranch);
            var accountReceipts = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptLessorCode == lessorCode &&
                                                                            x.CrCasAccountReceiptBranchCode == bsLayoutVM.SelectedBranch &&
                                                                            x.CrCasAccountReceiptUser == userLogin.CrMasUserInformationCode,
                                                                            new[] { "CrCasAccountReceiptPaymentMethodNavigation", "CrCasAccountReceiptReferenceTypeNavigation" }).ToList();

            var lastAccountReceipt = accountReceipts.OrderByDescending(x => x.CrCasAccountReceiptDate).FirstOrDefault();
            var ToDate = lastAccountReceipt?.CrCasAccountReceiptDate;
            var FromDate = ToDate?.AddDays(-30);
            ViewBag.ToDate = ToDate?.ToString("yyyy-MM-dd");
            ViewBag.FromDate = FromDate?.ToString("yyyy-MM-dd");

            var filterByDateAccountReceipt = accountReceipts.Where(x => x.CrCasAccountReceiptDate >= FromDate && x.CrCasAccountReceiptDate <= ToDate).ToList();


            bsLayoutVM.CrMasUserBranchValidity = branchValidity;
            bsLayoutVM.AccountReceipts = filterByDateAccountReceipt.OrderBy(x=>x.CrCasAccountReceiptDate).ToList();
            bsLayoutVM.TotalCreditor = bsLayoutVM.AccountReceipts.Where(x=>x.CrCasAccountReceiptIsPassing!="4").Sum(x => x.CrCasAccountReceiptPayment);
            bsLayoutVM.TotalDebit = bsLayoutVM.AccountReceipts.Where(x => x.CrCasAccountReceiptIsPassing != "4").Sum(x => x.CrCasAccountReceiptReceipt);
            return View(bsLayoutVM);
        }
        [HttpGet]
        public async Task<PartialViewResult> GetReceiptsByStatus(string status, string StartDate, string EndDate, string branchCode)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            BSLayoutVM bSLayoutVM = new BSLayoutVM();

            var accountReceipts = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptLessorCode == lessorCode &&
                                                                            x.CrCasAccountReceiptBranchCode == branchCode &&
                                                                            x.CrCasAccountReceiptUser == userLogin.CrMasUserInformationCode,
                                                                            new[] { "CrCasAccountReceiptPaymentMethodNavigation", "CrCasAccountReceiptReferenceTypeNavigation" }).OrderBy(x => x.CrCasAccountReceiptDate).ToList();
            if (status == Status.All) bSLayoutVM.AccountReceipts = accountReceipts.Where(x => x.CrCasAccountReceiptDate?.Date >= DateTime.Parse(StartDate) &&
                                                                                         x.CrCasAccountReceiptDate?.Date <= DateTime.Parse(EndDate)).ToList();

            else if (status== Status.Transfer) bSLayoutVM.AccountReceipts = accountReceipts.Where(x => x.CrCasAccountReceiptDate?.Date >= DateTime.Parse(StartDate) &&
                                                                                           x.CrCasAccountReceiptDate?.Date <= DateTime.Parse(EndDate) &&
                                                                                          (x.CrCasAccountReceiptIsPassing == Status.Transfer || x.CrCasAccountReceiptIsPassing == Status.Change)).ToList();
            


            else bSLayoutVM.AccountReceipts = accountReceipts.Where(x => x.CrCasAccountReceiptDate?.Date >= DateTime.Parse(StartDate) &&
                                                                         x.CrCasAccountReceiptDate?.Date <= DateTime.Parse(EndDate) &&
                                                                         x.CrCasAccountReceiptIsPassing == status).ToList();

            bSLayoutVM.TotalCreditor = bSLayoutVM.AccountReceipts.Sum(x => x.CrCasAccountReceiptPayment);
            bSLayoutVM.TotalDebit = bSLayoutVM.AccountReceipts.Sum(x => x.CrCasAccountReceiptReceipt);

            return PartialView("_ReportsData", bSLayoutVM);
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
            receiptDetails.Date = receipt.CrCasAccountReceiptDate?.ToString("yyyy/MM/dd");
            receiptDetails.Creditor = receipt.CrCasAccountReceiptPayment?.ToString("N2");
            receiptDetails.ReferenceNo = receipt.CrCasAccountReceiptReferenceNo;
            receiptDetails.ReferenceTypeAr = receipt.CrCasAccountReceiptReferenceTypeNavigation?.CrMasSupAccountReceiptReferenceArName;
            receiptDetails.ReferenceTypeEn = receipt.CrCasAccountReceiptReferenceTypeNavigation?.CrMasSupAccountReceiptReferenceEnName;
            if (receipt.CrCasAccountReceiptBank=="00")
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
            receiptDetails.ReceivedDate = receipt.CrCasAccountReceiptPassingDate?.ToString("yyyy/MM/dd");
            receiptDetails.Reasons = receipt.CrCasAccountReceiptReasons;



            return Json(receiptDetails);
        }

    }
}
