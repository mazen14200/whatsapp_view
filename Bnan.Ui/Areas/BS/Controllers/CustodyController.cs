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

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class CustodyController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<CustodyController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly ICustody _custodyService;

        public CustodyController(IStringLocalizer<CustodyController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IAdminstritiveProcedures adminstritiveProcedures, ICustody custodyService) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _adminstritiveProcedures = adminstritiveProcedures;
            _custodyService = custodyService;
        }
        public async Task<IActionResult> Index()
        {
            //To Set Title 
            var titles = await setTitle("501", "5501011", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();


            //Get ContractCode
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var sector = "1";
            var autoinc = GetContractLastRecord("1", lessorCode, bsLayoutVM.SelectedBranch).CrCasSysAdministrativeProceduresNo;
            var ReferenceNo = y + "-" + sector + "304" + "-" + lessorCode + bsLayoutVM.SelectedBranch + "-" + autoinc;
            ViewBag.ReceiptPassingReferenceNo = ReferenceNo;
            ViewBag.Date = year.ToString("yyyy/MM/dd");
            var salesPoint = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointBrn == bsLayoutVM.SelectedBranch &&
                                                                             x.CrCasAccountSalesPointLessor == lessorCode &&
                                                                             x.CrCasAccountSalesPointTotalAvailable > 0).ToList();
            bsLayoutVM.SalesPointHaveBalance = salesPoint;
            return View(bsLayoutVM);
        }

        public async Task<PartialViewResult> GetDetailsByBranch(string branchCode, string salesPointCode)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var UserBranchValidity = await _unitOfWork.CrMasUserBranchValidity.FindAsync(x => x.CrMasUserBranchValidityId == userLogin.CrMasUserInformationCode &&
                                                                                           x.CrMasUserBranchValidityBranch == branchCode &&
                                                                                           x.CrMasUserBranchValidityLessor == lessorCode);
            var salesPoint = _unitOfWork.CrCasAccountSalesPoint.Find(x => x.CrCasAccountSalesPointCode == salesPointCode &&
                                                                        x.CrCasAccountSalesPointLessor == lessorCode &&
                                                                        x.CrCasAccountSalesPointBrn == branchCode);
            BSLayoutVM bSLayoutVM = new BSLayoutVM();
            bSLayoutVM.SalesPointBalanceTotal = salesPoint.CrCasAccountSalesPointTotalBalance;
            bSLayoutVM.SalesPointBalanceResereved = salesPoint.CrCasAccountSalesPointTotalReserved;
            bSLayoutVM.SalesPointBalanceAvaliable = salesPoint.CrCasAccountSalesPointTotalAvailable;
            if (salesPoint.CrCasAccountSalesPointBank == "00")
            {
                bSLayoutVM.UserBalanceTotal = UserBranchValidity.CrMasUserBranchValidityBranchCashBalance;
                bSLayoutVM.UserBalanceAvaliable = UserBranchValidity.CrMasUserBranchValidityBranchCashAvailable;
                bSLayoutVM.UserBalanceResereved = UserBranchValidity.CrMasUserBranchValidityBranchCashReserved;
                bSLayoutVM.CustodyReceipts = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptBank == "00" &&
                                                                                       x.CrCasAccountReceiptLessorCode == lessorCode &&
                                                                                       x.CrCasAccountReceiptBranchCode == branchCode &&
                                                                                       x.CrCasAccountReceiptIsPassing == "1" &&
                                                                                       x.CrCasAccountReceiptUser == userLogin.CrMasUserInformationCode,
                                                                                       new[] {
                                                                                           "CrCasAccountReceiptPaymentMethodNavigation",
                                                                                           "CrCasAccountReceiptReferenceTypeNavigation" }).ToList();
            }
            else
            {
                bSLayoutVM.UserBalanceTotal = UserBranchValidity.CrMasUserBranchValidityBranchSalesPointBalance;
                bSLayoutVM.UserBalanceAvaliable = UserBranchValidity.CrMasUserBranchValidityBranchSalesPointAvailable;
                bSLayoutVM.UserBalanceResereved = UserBranchValidity.CrMasUserBranchValidityBranchSalesPointReserved;
                bSLayoutVM.CustodyReceipts = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptBank != "00" &&
                                                                                       x.CrCasAccountReceiptLessorCode == lessorCode &&
                                                                                       x.CrCasAccountReceiptBranchCode == branchCode &&
                                                                                       x.CrCasAccountReceiptIsPassing == "1" &&
                                                                                       x.CrCasAccountReceiptUser == userLogin.CrMasUserInformationCode,
                                                                                       new[] {
                                                                                           "CrCasAccountReceiptPaymentMethodNavigation",
                                                                                           "CrCasAccountReceiptReferenceTypeNavigation" }).ToList();
            }
            return PartialView("_CustodyData", bSLayoutVM);
        }
        [HttpPost]
        public async Task<IActionResult> SendCustody(BSLayoutVM bSLayout, List<string> ReceiptsNo, string ReferenceNo, string Reasons,
                                                     string TotalReceived, string TotalPayment, string TotalReceipt)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;



            // Save Adminstrive Procedures
            var adminstritive = await _adminstritiveProcedures.SaveAdminstritiveCustody(userLogin.CrMasUserInformationCode, userLogin.CrMasUserInformationLessor, bSLayout.SelectedBranch,
                                                                                      userLogin.CrMasUserInformationCode, TotalReceived, TotalPayment, Reasons);
            var checkUpdateReceipt = true;
            if (adminstritive != null)
            {

                string[] receiptValues = ReceiptsNo[0].Split(',');

                foreach (var Receipt in receiptValues)
                {
                    if (checkUpdateReceipt)
                    {
                        var R = await _unitOfWork.CrCasAccountReceipt.FindAsync(x => x.CrCasAccountReceiptNo == Receipt);
                        if (R != null)
                        {
                            checkUpdateReceipt = await _custodyService.UpdateAccountReceipt(R.CrCasAccountReceiptNo, adminstritive.CrCasSysAdministrativeProceduresNo, Reasons);
                        }
                        else
                        {
                            checkUpdateReceipt = false;
                            break;
                        }
                    }
                }


            }

            // Table Changes When Submit Form 
            // User Informaion
            var CheckUpdateUserInfo = true;
            CheckUpdateUserInfo = await _custodyService.UpdateUserInfo(userLogin.CrMasUserInformationCode, lessorCode, TotalPayment);

            // User Informaion
            var CheckUpdateBranch = true;
            CheckUpdateBranch = await _custodyService.UpdateBranch(bSLayout.SelectedBranch, lessorCode, TotalPayment);




            // SalesPoint
            var CheckUpdateSalesPoint = true;
            var SalesPoint = _unitOfWork.CrCasAccountSalesPoint.Find(x => x.CrCasAccountSalesPointCode == bSLayout.SalesPointSelected &&
                                                                        x.CrCasAccountSalesPointLessor == lessorCode &&
                                                                        x.CrCasAccountSalesPointBrn == bSLayout.SelectedBranch);

            if (SalesPoint != null) CheckUpdateSalesPoint = await _custodyService.UpdateSalesPoint(lessorCode, bSLayout.SelectedBranch, SalesPoint.CrCasAccountSalesPointCode, TotalPayment);
            else CheckUpdateSalesPoint = false;

            // BranchValidity
            var CheckUpdateBranchValidity = true;

            CheckUpdateBranchValidity = await _custodyService.UpdateBranchValidity(userLogin.CrMasUserInformationCode, lessorCode,
                                                                                  bSLayout.SelectedBranch, SalesPoint.CrCasAccountSalesPointBank, TotalPayment);







            if (adminstritive != null && checkUpdateReceipt &&
                CheckUpdateUserInfo && CheckUpdateBranch &&
                CheckUpdateSalesPoint && CheckUpdateBranchValidity) if (await _unitOfWork.CompleteAsync() > 1) _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                else _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index", "Home");
        }

        public CrCasSysAdministrativeProcedure GetContractLastRecord(string Sector, string LessorCode, string BranchCode)
        {
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var SectorCode = Sector;
            var Lrecord = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresLessor == LessorCode &&
                 x.CrCasSysAdministrativeProceduresYear == y && x.CrCasSysAdministrativeProceduresBranch == BranchCode && x.CrCasSysAdministrativeProceduresCode == "304")
                .Max(x => x.CrCasSysAdministrativeProceduresNo?.Substring(x.CrCasSysAdministrativeProceduresNo.Length - 6, 6));

            CrCasSysAdministrativeProcedure c = new CrCasSysAdministrativeProcedure();
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                c.CrCasSysAdministrativeProceduresNo = val.ToString("000000");
            }
            else
            {
                c.CrCasSysAdministrativeProceduresNo = "000001";
            }

            return c;
        }
    }
}
