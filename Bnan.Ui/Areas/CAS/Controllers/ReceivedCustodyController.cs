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
    public class ReceivedCustodyController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<ReceivedCustodyController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly ICustody _Custody;


        public ReceivedCustodyController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
             IMapper mapper, IUserService userService, IAccountBank accountBank,
             IUserLoginsService userLoginsService, IToastNotification toastNotification,
             IStringLocalizer<ReceivedCustodyController> localizer, IAdminstritiveProcedures adminstritiveProcedures, ICustody custody) :
             base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _adminstritiveProcedures = adminstritiveProcedures;
            _Custody = custody;
        }

        public async Task<IActionResult> Index()
        {
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "1";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("204", "2204002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var AllAdminstritives = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresLessor == lessorCode &&
                                                                                       x.CrCasSysAdministrativeProceduresCode == "304" &&
                                                                                       x.CrCasSysAdministrativeProceduresTargeted != userLogin.CrMasUserInformationCode, new[] { "CrCasSysAdministrativeProceduresUserInsertNavigation" }).ToList();
            List<AdmintritiveCustodyVM> admintritives = new List<AdmintritiveCustodyVM>();
            foreach (var item in AllAdminstritives)
            {
                AdmintritiveCustodyVM newAdmins = new AdmintritiveCustodyVM();
                newAdmins.CrCasSysAdministrativeProceduresNo = item.CrCasSysAdministrativeProceduresNo;
                newAdmins.CrCasSysAdministrativeProceduresDate = item.CrCasSysAdministrativeProceduresDate?.ToString("yyyy/MM/dd");
                newAdmins.CrCasSysAdministrativeProceduresDocStartDate = item.CrCasSysAdministrativeProceduresDocStartDate?.ToString("yyyy/MM/dd");
                newAdmins.CrCasSysAdministrativeProceduresDocEndDate = item.CrCasSysAdministrativeProceduresDocEndDate?.ToString("yyyy/MM/dd");
                var SalesPointAccountReceipt = await _unitOfWork.CrCasAccountReceipt.FindAsync(x => x.CrCasAccountReceiptPassingReference == item.CrCasSysAdministrativeProceduresNo, new[] { "CrCasAccountReceiptSalesPointNavigation" });
                newAdmins.SalesPointNavigation = SalesPointAccountReceipt.CrCasAccountReceiptSalesPointNavigation;
                newAdmins.CrCasSysAdministrativeProceduresCreditor = item.CrCasSysAdministrativeProceduresCreditor?.ToString("N2");
                newAdmins.CrCasSysAdministrativeProceduresDebit = item.CrCasSysAdministrativeProceduresDebit?.ToString("N2");
                newAdmins.UserInsertNavigation = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                newAdmins.UserReceviedNavigation = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresUserInsert);
                newAdmins.CrCasSysAdministrativeProceduresStatus = item.CrCasSysAdministrativeProceduresStatus;
                newAdmins.DatePassing = SalesPointAccountReceipt.CrCasAccountReceiptPassingDate?.ToString("yyyy/MM/dd");
                newAdmins.CrCasSysAdministrativeProceduresReasons = item.CrCasSysAdministrativeProceduresReasons;
                admintritives.Add(newAdmins);
            }

            return View(admintritives);
        }


        [HttpGet]
        public async Task<PartialViewResult> GetCustodyStatus(string status)
        {
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "1";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;

            if (!string.IsNullOrEmpty(status))
            {
                var AdminstritiveAll = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresLessor == lessorCode &&
                                                                                                x.CrCasSysAdministrativeProceduresCode == "304" &&
                                                                                                x.CrCasSysAdministrativeProceduresTargeted != userLogin.CrMasUserInformationCode, new[] { "CrCasSysAdministrativeProceduresUserInsertNavigation" }).ToList();
                List<AdmintritiveCustodyVM> admintritives = new List<AdmintritiveCustodyVM>();
                foreach (var item in AdminstritiveAll)
                {
                    AdmintritiveCustodyVM newAdmins = new AdmintritiveCustodyVM();
                    newAdmins.CrCasSysAdministrativeProceduresNo = item.CrCasSysAdministrativeProceduresNo;
                    newAdmins.CrCasSysAdministrativeProceduresDate = item.CrCasSysAdministrativeProceduresDate?.ToString("yyyy/MM/dd");
                    newAdmins.CrCasSysAdministrativeProceduresDocStartDate = item.CrCasSysAdministrativeProceduresDocStartDate?.ToString("yyyy/MM/dd");
                    newAdmins.CrCasSysAdministrativeProceduresDocEndDate = item.CrCasSysAdministrativeProceduresDocEndDate?.ToString("yyyy/MM/dd");
                    var SalesPointAccountReceipt = await _unitOfWork.CrCasAccountReceipt.FindAsync(x => x.CrCasAccountReceiptPassingReference == item.CrCasSysAdministrativeProceduresNo, new[] { "CrCasAccountReceiptSalesPointNavigation" });

                    newAdmins.SalesPointNavigation = SalesPointAccountReceipt.CrCasAccountReceiptSalesPointNavigation;
                    newAdmins.CrCasSysAdministrativeProceduresCreditor = item.CrCasSysAdministrativeProceduresCreditor?.ToString("N2");
                    newAdmins.CrCasSysAdministrativeProceduresDebit = item.CrCasSysAdministrativeProceduresDebit?.ToString("N2");
                    newAdmins.UserInsertNavigation = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    newAdmins.UserReceviedNavigation = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresUserInsert);
                    newAdmins.CrCasSysAdministrativeProceduresStatus = item.CrCasSysAdministrativeProceduresStatus;
                    newAdmins.CrCasSysAdministrativeProceduresReasons = item.CrCasSysAdministrativeProceduresReasons;
                    admintritives.Add(newAdmins);
                }



                if (status == Status.All) return PartialView("_CustodyData", admintritives);
                return PartialView("_CustodyData", admintritives.Where(l => l.CrCasSysAdministrativeProceduresStatus == status));
            }
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("204", "2204002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);




            AdmintritiveCustodyVM newAdmins = new AdmintritiveCustodyVM();
            var adminstritive = await _unitOfWork.CrCasSysAdministrativeProcedure.FindAsync(x => x.CrCasSysAdministrativeProceduresNo == id);
            if (adminstritive != null)
            {
                var Receipts = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptPassingReference == adminstritive.CrCasSysAdministrativeProceduresNo,
                                                                          new[] { "CrCasAccountReceiptPaymentMethodNavigation", "CrCasAccountReceiptReferenceTypeNavigation" }).ToList();

                newAdmins.CrCasSysAdministrativeProceduresNo = adminstritive.CrCasSysAdministrativeProceduresNo;
                newAdmins.CrCasSysAdministrativeProceduresDate = adminstritive.CrCasSysAdministrativeProceduresDate?.ToString("yyyy/MM/dd");
                newAdmins.CrCasSysAdministrativeProceduresDocStartDate = adminstritive.CrCasSysAdministrativeProceduresDocStartDate?.ToString("yyyy/MM/dd");
                newAdmins.CrCasSysAdministrativeProceduresDocEndDate = adminstritive.CrCasSysAdministrativeProceduresDocEndDate?.ToString("yyyy/MM/dd");
                var SalesPointAccountReceipt = await _unitOfWork.CrCasAccountReceipt.FindAsync(x => x.CrCasAccountReceiptPassingReference == adminstritive.CrCasSysAdministrativeProceduresNo, new[] { "CrCasAccountReceiptSalesPointNavigation" });

                newAdmins.SalesPointNavigation = SalesPointAccountReceipt.CrCasAccountReceiptSalesPointNavigation;
                newAdmins.CrCasSysAdministrativeProceduresCreditor = adminstritive.CrCasSysAdministrativeProceduresCreditor?.ToString("N2");
                newAdmins.CrCasSysAdministrativeProceduresDebit = adminstritive.CrCasSysAdministrativeProceduresDebit?.ToString("N2");
                newAdmins.TotalAmount = (adminstritive.CrCasSysAdministrativeProceduresCreditor - adminstritive.CrCasSysAdministrativeProceduresDebit)?.ToString("N2");
                newAdmins.UserInsertNavigation = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == adminstritive.CrCasSysAdministrativeProceduresTargeted);
                newAdmins.UserReceviedNavigation = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == adminstritive.CrCasSysAdministrativeProceduresUserInsert);
                newAdmins.CrCasSysAdministrativeProceduresStatus = adminstritive.CrCasSysAdministrativeProceduresStatus;
                newAdmins.CrCasSysAdministrativeProceduresReasons = adminstritive.CrCasSysAdministrativeProceduresReasons;
                newAdmins.AccountReceipt = Receipts;
                return View(newAdmins);
            }

            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ActionCustody(AdmintritiveCustodyVM custodyVM, string status)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;

            var Adminstritive = await _unitOfWork.CrCasSysAdministrativeProcedure.FindAsync(x => x.CrCasSysAdministrativeProceduresNo == custodyVM.CrCasSysAdministrativeProceduresNo);
            if (Adminstritive != null)
            {
                var CheckAdminstritive = true;
                CheckAdminstritive = await _Custody.UpdateAdminstritive(Adminstritive.CrCasSysAdministrativeProceduresNo, userLogin.CrMasUserInformationCode, status, custodyVM.CrCasSysAdministrativeProceduresReasons);


                var CheckAddReceipt = true;
                var CheckUpdateBranch = true;
                var CheckUpdateSalesPoint = true;
                var CheckUpdateUserInfo = true;
                var CheckUpdateUserValidtyBrn = true;
                var CheckUpdateAccountReceipts = true;



                CheckAddReceipt = await _Custody.AddAccountReceiptReceivedCustody(Adminstritive.CrCasSysAdministrativeProceduresNo,
                                                                                  lessorCode, Adminstritive.CrCasSysAdministrativeProceduresBranch, custodyVM.TotalAmount,custodyVM.CrCasSysAdministrativeProceduresReasons);

                CheckUpdateBranch = await _Custody.UpdateBranchReceivedCustody(Adminstritive.CrCasSysAdministrativeProceduresBranch, lessorCode, custodyVM.TotalAmount, status);
                CheckUpdateSalesPoint = await _Custody.UpdateSalesPointReceivedCustody(lessorCode,Adminstritive.CrCasSysAdministrativeProceduresBranch, Adminstritive.CrCasSysAdministrativeProceduresNo, custodyVM.TotalAmount, status);
                CheckUpdateUserInfo = await _Custody.UpdateUserInfoReceivedCustody(Adminstritive.CrCasSysAdministrativeProceduresTargeted, lessorCode, custodyVM.TotalAmount, status);
                CheckUpdateUserValidtyBrn = await _Custody.UpdateBranchValidityReceivedCustody(Adminstritive.CrCasSysAdministrativeProceduresTargeted.Trim(), lessorCode, Adminstritive.CrCasSysAdministrativeProceduresBranch,
                                                                                               Adminstritive.CrCasSysAdministrativeProceduresNo, custodyVM.TotalAmount, status);
                CheckUpdateAccountReceipts =  _Custody.UpdateAccountReceiptReceivedCustody(Adminstritive.CrCasSysAdministrativeProceduresNo, status, custodyVM.CrCasSysAdministrativeProceduresReasons);



                if (CheckAdminstritive && CheckAddReceipt && CheckUpdateBranch &&
                    CheckUpdateSalesPoint && CheckUpdateUserInfo && 
                    CheckUpdateUserValidtyBrn) if (await _unitOfWork.CompleteAsync() > 1) _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                                               else _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            }

            return RedirectToAction("Index");
        }
        public async Task<string> GetAccountReceiptNo(string BranchCode)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var Lrecord = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresLessor == userLogin.CrMasUserInformationLessor &&
                x.CrCasSysAdministrativeProceduresCode == "302"
                && x.CrCasSysAdministrativeProceduresSector == "1"
                && x.CrCasSysAdministrativeProceduresYear == y).Max(x => x.CrCasSysAdministrativeProceduresNo.Substring(x.CrCasSysAdministrativeProceduresNo.Length - 6, 6));
            string Serial;
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                Serial = val.ToString("000000");
            }
            else
            {
                Serial = "000001";
            }
            var receipt = y + "-" + "1" + "302" + "-" + lessorCode + BranchCode + "-" + Serial;
            return receipt;
        }
    }
}
