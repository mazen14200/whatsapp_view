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
    public class RentersController  : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<RentersController> _localizer;
        private readonly IContract _ContractServices;
        public RentersController(IStringLocalizer<RentersController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IContract contractServices) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _ContractServices = contractServices;
        }
        public async Task<IActionResult> Index()
        {
            //To Set Title 
            var userLogin = await _userManager.GetUserAsync(User);
            if (CultureInfo.CurrentUICulture.Name == "en-US") await ViewData.SetPageTitleAsync("Branches", "Renters", "", "", "", userLogin.CrMasUserInformationEnName);
            else await ViewData.SetPageTitleAsync("الفروع", "المستأجرين", "", "", "", userLogin.CrMasUserInformationArName);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bSLayoutVM = await GetBranchesAndLayout();
            var RenterAll = _unitOfWork.CrCasRenterLessor.FindAll(x => x.CrCasRenterLessorCode == userLogin.CrMasUserInformationLessor, new[] { "CrCasRenterLessorNavigation" }).ToList();
            var mecahnizmEvaluations = _unitOfWork.CrMasSysEvaluation.FindAll(x => x.CrMasSysEvaluationsStatus == Status.Active).ToList();
            bSLayoutVM.RentersLessor = RenterAll;
            bSLayoutVM.Evaluations= mecahnizmEvaluations;
            return View(bSLayoutVM);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetRentersByStatus(string status, string search)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            BSLayoutVM bSLayoutVM = new BSLayoutVM();
            if (!string.IsNullOrEmpty(status))
            {
                var RenterAll = _unitOfWork.CrCasRenterLessor.FindAll(x => x.CrCasRenterLessorCode == userLogin.CrMasUserInformationLessor, new[] { "CrCasRenterLessorNavigation" }).ToList();
                var mecahnizmEvaluations = _unitOfWork.CrMasSysEvaluation.FindAll(x => x.CrMasSysEvaluationsStatus == Status.Active).ToList();
                bSLayoutVM.Evaluations = mecahnizmEvaluations;

                if (status == Status.All) {
                    bSLayoutVM.RentersLessor = RenterAll.FindAll(x => x.CrCasRenterLessorId.Contains(search) || x.CrCasRenterLessorNavigation.CrMasRenterInformationArName.Contains(search) || x.CrCasRenterLessorNavigation.CrMasRenterInformationEnName.Contains(search)).ToList();
                    return PartialView("_RentersDataTable", bSLayoutVM);
                }
                bSLayoutVM.RentersLessor = RenterAll.Where(x => x.CrCasRenterLessorStatus == status&&
                                                              ( x.CrCasRenterLessorId.Contains(search) ||
                                                                x.CrCasRenterLessorNavigation.CrMasRenterInformationArName.Contains(search) ||
                                                                x.CrCasRenterLessorNavigation.CrMasRenterInformationEnName.Contains(search))).ToList();
                return PartialView("_RentersDataTable", bSLayoutVM);
            }
            return PartialView();
        }
        public async Task<IActionResult> Details(string id)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            if (CultureInfo.CurrentUICulture.Name == "en-US") await ViewData.SetPageTitleAsync("Branches", "Renters", "RenterInformation", "", "", userLogin.CrMasUserInformationEnName);
            else await ViewData.SetPageTitleAsync("الفروع", "المستأجرين", "بيانات المستأجر", "", "", userLogin.CrMasUserInformationArName);
            var Renter = await _unitOfWork.CrCasRenterLessor.FindAsync(x=>x.CrCasRenterLessorId==id&&x.CrCasRenterLessorCode==lessorCode,
                                                                       new[] { "CrCasRenterLessorNavigation.CrMasRenterInformationEmployerNavigation",
                                                                               "CrCasRenterLessorNavigation.CrMasRenterInformationDrivingLicenseTypeNavigation",
                                                                               "CrCasRenterLessorNavigation.CrMasRenterPost",
                                                                               "CrCasRenterContractBasicCrCasRenterContractBasicNavigations",
                                                                               "CrCasRenterLessorIdtrypeNavigation",
                                                                               "CrCasRenterLessorMembershipNavigation",
                                                                               "CrCasRenterLessorSectorNavigation",
                                                                               "CrCasRenterLessorStatisticsCityNavigation",
                                                                               "CrCasRenterLessorStatisticsGenderNavigation",
                                                                               "CrCasRenterLessorStatisticsJobsNavigation",
                                                                               "CrCasRenterLessorStatisticsNationalitiesNavigation",
                                                                               "CrCasRenterLessorStatisticsRegionsNavigation"});
            if (Renter==null)
            {
                _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("Index");
            }
            var Contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicRenterId == id && x.CrCasRenterContractBasicLessor == lessorCode, new[] { "CrCasRenterContractBasicCarSerailNoNavigation" }).Distinct().ToList();
            var ContractsVM = _mapper.Map<List<DetailsContractsForRenterVM>>(Contracts);
            foreach (var Contract in ContractsVM)
            {
                var invoices = _unitOfWork.CrCasAccountInvoice.FindAll(x => x.CrCasAccountInvoiceReferenceContract == Contract.CrCasRenterContractBasicNo);
                var receipts = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptReferenceNo == Contract.CrCasRenterContractBasicNo);
                int copyValue = Contract.CrCasRenterContractBasicCopy;

                if (invoices.Count() > 0)
                {
                    if (copyValue >= 1 && copyValue <= invoices.Count())
                    {
                        var invoice = invoices.Skip(copyValue).FirstOrDefault(); // Skip the appropriate number of invoices
                        Contract.InvoiceArReceipt = invoice?.CrCasAccountInvoiceArPdfFile;
                        Contract.InvoiceEnReceipt = invoice?.CrCasAccountInvoiceEnPdfFile;
                    }
                    else
                    {
                        var invoice = invoices.FirstOrDefault(); // Default to the first invoice
                        Contract.InvoiceArReceipt = invoice?.CrCasAccountInvoiceArPdfFile;
                        Contract.InvoiceEnReceipt = invoice?.CrCasAccountInvoiceEnPdfFile;
                    }
                }
                if (receipts.Count() > 0)
                {
                    if (copyValue >= 1 && copyValue <= receipts.Count())
                    {
                        var receipt = receipts.Skip(copyValue).FirstOrDefault(); // Skip the appropriate number of invoices
                        Contract.ArReceipt = receipt?.CrCasAccountReceiptArPdfFile;
                        Contract.EnReceipt = receipt?.CrCasAccountReceiptEnPdfFile;
                    }
                    else
                    {
                        var receipt = receipts.FirstOrDefault(); // Default to the first invoice
                        Contract.ArReceipt = receipt?.CrCasAccountReceiptArPdfFile;
                        Contract.EnReceipt = receipt?.CrCasAccountReceiptEnPdfFile;
                    }
                }
            }
            var bSLayoutVM = await GetBranchesAndLayout();
            bSLayoutVM.Renter = Renter;
            bSLayoutVM.RenterContracts = ContractsVM;
            return View(bSLayoutVM);
        }
    }
}
