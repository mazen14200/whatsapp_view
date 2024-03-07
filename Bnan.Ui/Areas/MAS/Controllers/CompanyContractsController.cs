using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.Areas.CAS.Controllers;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class CompanyContractsController : BaseController
    {
        private readonly ICompnayContract _compnayContract;
        private readonly IUserLoginsService _userLoginsService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<CompanyContractsController> _localizer;



        public CompanyContractsController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, ICompnayContract compnayContract, IUserLoginsService userLoginsService, IToastNotification toastNotification, IStringLocalizer<CompanyContractsController> localizer) : base(userManager, unitOfWork, mapper)
        {
            _compnayContract = compnayContract;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
        }

        public async Task<IActionResult> CompanyContracts()
        {
            var (mainTask, subTask, system, currentUser) = await SetTrace("101", "1101003", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "2";

            // Set Title
            var titles = await setTitle("101", "1101003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            //Check User Sub Validation
            var UserValidation = await CheckUserSubValidation("1101003");
            if (UserValidation == false) return RedirectToAction("Index", "Home", new { area = "MAS" });
            // get all contracts bnan of companies and show it
            var companyContractBnan = _unitOfWork.CrMasContractCompany.FindAll(x => x.CrMasContractCompanyProcedures == "112" &&
                                                                                    x.CrMasContractCompanyLessorNavigation.CrMasLessorInformationStatus == Status.Active, new[] { "CrMasContractCompanyLessorNavigation" }).ToList();
            return View(companyContractBnan);
        }

        [HttpGet]
        public PartialViewResult GetCompanyContractsByStatus(string status)
        {
            var lessor = _unitOfWork.CrMasLessorInformation.GetAll();
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var companyContractsAll = _unitOfWork.CrMasContractCompany.FindAll(l => l.CrMasContractCompanyProcedures == "112" &&
                                                                                            l.CrMasContractCompanyLessorNavigation.CrMasLessorInformationStatus == Status.Active, new[] { "CrMasContractCompanyLessorNavigation" });
                    return PartialView("_DataTableCompanyContracts", companyContractsAll);
                }
                var companyContractsbyStatus = _unitOfWork.CrMasContractCompany.FindAll(l => l.CrMasContractCompanyStatus == status && l.CrMasContractCompanyProcedures == "112" &&
                                                                                             l.CrMasContractCompanyLessorNavigation.CrMasLessorInformationStatus == Status.Active
                                                                                             , new[] { "CrMasContractCompanyLessorNavigation" }).ToList();
                return PartialView("_DataTableCompanyContracts", companyContractsbyStatus);
            }
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;

            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "2";
            //To Set Title
            var titles = await setTitle("101", "1101003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var BnanContract = _unitOfWork.CrMasContractCompany.Find(x=>x.CrMasContractCompanyNo==id, new[] { "CrMasContractCompanyProceduresNavigation", "CrMasContractCompanyLessorNavigation" });
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
            if (companyContractDetailed.Count()>0)
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
        [HttpPost]
        public async Task<IActionResult> Edit(string CompanyContractCode, string DateContract, string StartDateContract, string EndDateContract, string ContractCompanyAnnualFees,
            string ContractCompanyTaxRate, string ContractCompanyDiscountRate, string SelectedOption, List<ContractDetailedVM>? data )
        {
            var activiation="";
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
            var companyCode = _unitOfWork.CrMasContractCompany.Find(x=>x.CrMasContractCompanyNo== CompanyContractCode).CrMasContractCompanyLessor;
            var companyName = _unitOfWork.CrMasLessorInformation.Find(x=>x.CrMasLessorInformationCode == companyCode);
            var RecordAr = $"{companyName.CrMasLessorInformationArShortName} - ( {CompanyContractCode} )" ;
            var RecordEn = $"{companyName.CrMasLessorInformationEnShortName} - ( {CompanyContractCode} )";
            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            return Json(new { success = true});
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string code)
        {
            var companyContract= await _unitOfWork.CrMasContractCompany.FindAsync(x=>x.CrMasContractCompanyNo==code);

            if (companyContract == null)
            {
                return Json(new { success = false });
            }
            else
            {
                await _compnayContract.AddCompanyContractAfterDelete(companyContract.CrMasContractCompanyLessor);
                companyContract.CrMasContractCompanyStatus = "D";
                _unitOfWork.CrMasContractCompany.Update(companyContract); // Update the entity in the context
                await _unitOfWork.CompleteAsync(); // Save the changes
                // SaveTracing
                var (mainTask, subTask, system, currentUser) = await SetTrace("101", "1101003", "1");
                var companyCode = _unitOfWork.CrMasContractCompany.Find(x=>x.CrMasContractCompanyNo== companyContract.CrMasContractCompanyNo).CrMasContractCompanyLessor;
                var companyName = _unitOfWork.CrMasLessorInformation.Find(x=>x.CrMasLessorInformationCode == companyCode);
                var RecordAr = $"{companyName.CrMasLessorInformationArShortName} - ( {companyContract.CrMasContractCompanyNo} )" ;
                var RecordEn = $"{companyName.CrMasLessorInformationEnShortName} - ( {companyContract.CrMasContractCompanyNo} )";
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "حذف", "Delete", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                return Json(new { success = true });
            }
        }
        public IActionResult SuccesssMessageCompanyContract()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("CompanyContracts");
        }
        public IActionResult FailedMessageCompanyContract()
        {
            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("CompanyContracts");
        }
    }
}
