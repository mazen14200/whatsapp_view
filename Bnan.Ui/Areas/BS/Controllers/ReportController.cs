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
                                                                            x.CrCasAccountReceiptBranchCode == bsLayoutVM.SelectedBranch && x.CrCasAccountReceiptIsPassing != "4" &&
                                                                            x.CrCasAccountReceiptPaymentMethod != "30" && x.CrCasAccountReceiptPaymentMethod != "40" &&
                                                                            x.CrCasAccountReceiptUser == userLogin.CrMasUserInformationCode,
                                                                            new[] { "CrCasAccountReceiptPaymentMethodNavigation", "CrCasAccountReceiptReferenceTypeNavigation" }).ToList();

            var lastAccountReceipt = accountReceipts.OrderByDescending(x => x.CrCasAccountReceiptDate).FirstOrDefault();
            var ToDate = lastAccountReceipt?.CrCasAccountReceiptDate;
            var FromDate = ToDate?.AddDays(-30);
            ViewBag.ToDate = ToDate?.ToString("yyyy-MM-dd");
            ViewBag.FromDate = FromDate?.ToString("yyyy-MM-dd");

            var filterByDateAccountReceipt = accountReceipts.Where(x => x.CrCasAccountReceiptDate >= FromDate && x.CrCasAccountReceiptDate <= ToDate).ToList();


            bsLayoutVM.CrMasUserBranchValidity = branchValidity;
            bsLayoutVM.AccountReceipts = filterByDateAccountReceipt;
            bsLayoutVM.TotalCreditor = bsLayoutVM.AccountReceipts.Sum(x => x.CrCasAccountReceiptPayment);
            bsLayoutVM.TotalDebit = bsLayoutVM.AccountReceipts.Sum(x => x.CrCasAccountReceiptReceipt);
            return View(bsLayoutVM);
        }
        [HttpGet]
        public async Task<PartialViewResult> GetReceiptsByStatus(string status, string StartDate, string EndDate, string branchCode)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            BSLayoutVM bSLayoutVM = new BSLayoutVM();

            var accountReceipts = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptLessorCode == lessorCode &&
                                                                            x.CrCasAccountReceiptBranchCode == branchCode && x.CrCasAccountReceiptIsPassing != "4" &&
                                                                            x.CrCasAccountReceiptPaymentMethod != "30" && x.CrCasAccountReceiptPaymentMethod != "40" &&
                                                                            x.CrCasAccountReceiptUser == userLogin.CrMasUserInformationCode,
                                                                            new[] { "CrCasAccountReceiptPaymentMethodNavigation", "CrCasAccountReceiptReferenceTypeNavigation" }).ToList();
            if (status == Status.All) bSLayoutVM.AccountReceipts = accountReceipts.Where(x => x.CrCasAccountReceiptDate?.Date >= DateTime.Parse(StartDate) &&
                                                                                         x.CrCasAccountReceiptDate?.Date <= DateTime.Parse(EndDate)).ToList();



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
            ReceiptDetailsVM receiptDetails = new ReceiptDetailsVM()
            {
                ReceiptNo = ReceiptNo,
                Date = receipt.CrCasAccountReceiptDate?.ToString("yyyy/MM/dd"),
                Creditor = receipt.CrCasAccountReceiptPayment?.ToString("N2"),
                ReferenceNo = receipt.CrCasAccountReceiptReferenceNo,
                ReferenceTypeAr = receipt.CrCasAccountReceiptReferenceTypeNavigation?.CrMasSupAccountReceiptReferenceArName,
                ReferenceTypeEn = receipt.CrCasAccountReceiptReferenceTypeNavigation?.CrMasSupAccountReceiptReferenceEnName,
                AccountBankCode = receipt.CrCasAccountReceiptAccountNavigation?.CrCasAccountBankCode,
                BankAr = receipt.CrCasAccountReceiptBankNavigation?.CrMasSupAccountBankArName,
                BankEn = receipt.CrCasAccountReceiptBankNavigation?.CrMasSupAccountBankEnName,
                SalesPointAr = receipt.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointArName,
                SalesPointEn = receipt.CrCasAccountReceiptSalesPointNavigation?.CrCasAccountSalesPointEnName,
                PaymentMethodAr = receipt.CrCasAccountReceiptPaymentMethodNavigation?.CrMasSupAccountPaymentMethodArName,
                PaymentMethodEn = receipt.CrCasAccountReceiptPaymentMethodNavigation?.CrMasSupAccountPaymentMethodEnName,
                CustodyNo = receipt.CrCasAccountReceiptPassingReference,
                StatusReceipt = receipt.CrCasAccountReceiptIsPassing,
                UserReceivedAr = userRecevied?.CrMasUserInformationArName,
                UserReceivedEn = userRecevied?.CrMasUserInformationEnName,
                ReceivedDate = receipt.CrCasAccountReceiptPassingDate?.ToString("yyyy/MM/dd")
            };

            return Json(receiptDetails);
        }

    }
}
