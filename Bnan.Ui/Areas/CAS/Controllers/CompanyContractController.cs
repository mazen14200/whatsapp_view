using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;
namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class CompanyContractsController : BaseController
    {
        private readonly ICompnayContract _compnayContract;
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<EmployeesController> _localizer;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyContractsController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, ICompnayContract compnayContract, IUserLoginsService userLoginsService, IUserService userService, IAdminstritiveProcedures adminstritiveProcedures, IToastNotification toastNotification, IStringLocalizer<EmployeesController> localizer, IWebHostEnvironment webHostEnvironment) : base(userManager, unitOfWork, mapper)
        {
            _compnayContract = compnayContract;
            _userLoginsService = userLoginsService;
            _userService = userService;
            _adminstritiveProcedures = adminstritiveProcedures;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> CompanyContracts()
        {       //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "2";

            // Set Title
            var titles = await setTitle("201", "2201003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);

            //Check User Sub Validation
            var UserValidation = await CheckUserSubValidation("2201003");
            if (UserValidation == false) return RedirectToAction("Index", "Home", new { area = "CAS" });
            // get all contracts bnan of companies and show it
            var companyContractBnan = _unitOfWork.CrMasContractCompany.FindAll(x => 
                                                                                    x.CrMasContractCompanyLessorNavigation.CrMasLessorInformationStatus == Status.Acive &&
                                                                                    (x.CrMasContractCompanyStatus == Status.Renewed || x.CrMasContractCompanyStatus==Status.Acive)&&
                                                                                    x.CrMasContractCompanyLessor== userLessor.CrMasUserInformationLessor
                                                                                    , new[] { "CrMasContractCompanyLessorNavigation", "CrMasContractCompanyProceduresNavigation" }).ToList();
            return View(companyContractBnan);
        }

        [HttpGet]
        public PartialViewResult GetCompanyContractsByStatus(string status)
        {
            var lessor = _unitOfWork.CrMasLessorInformation.GetAll();

            var user = _userService.GetUserByUserName(User);
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var companyContractsAll = _unitOfWork.CrMasContractCompany.FindAll(l => 
                                                                                            l.CrMasContractCompanyLessorNavigation.CrMasLessorInformationStatus == Status.Acive &&
                                                                                            l.CrMasContractCompanyLessor== user.CrMasUserInformationLessor
                                                                                            , new[] { "CrMasContractCompanyLessorNavigation", "CrMasContractCompanyProceduresNavigation" });
                    return PartialView("_DataTableCompanyContracts", companyContractsAll);
                }
                var companyContractsbyStatus = _unitOfWork.CrMasContractCompany.FindAll(l => l.CrMasContractCompanyStatus == status &&
                                                                                             l.CrMasContractCompanyLessorNavigation.CrMasLessorInformationStatus == Status.Acive &&
                                                                                             l.CrMasContractCompanyLessor == user.CrMasUserInformationLessor
                                                                                             , new[] { "CrMasContractCompanyLessorNavigation", "CrMasContractCompanyProceduresNavigation" }).ToList();
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
            var titles = await setTitle("201", "2201003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var BnanContract = _unitOfWork.CrMasContractCompany.Find(x => x.CrMasContractCompanyNo == id, new[] { "CrMasContractCompanyProceduresNavigation", "CrMasContractCompanyLessorNavigation" });
            var model = _mapper.Map<ContractCompanyVM>(BnanContract);
            model.ProdcudureArTaskName = BnanContract.CrMasContractCompanyProceduresNavigation.CrMasSysProceduresArName;
            model.ProdcudureEnTaskName = BnanContract.CrMasContractCompanyProceduresNavigation.CrMasSysProceduresEnName;
            model.CompanyNameAr = BnanContract.CrMasContractCompanyLessorNavigation?.CrMasLessorInformationArShortName;
            model.CompanyNameEn = BnanContract.CrMasContractCompanyLessorNavigation?.CrMasLessorInformationEnShortName;
            ViewBag.Date = model.CrMasContractCompanyDate?.Date.ToString("dd/MM/yyyy");
            ViewBag.StartDate = model.CrMasContractCompanyStartDate?.Date.ToString("dd/MM/yyyy");
            ViewBag.EndDate = model.CrMasContractCompanyEndDate?.Date.ToString("dd/MM/yyyy");
            ViewBag.UserID = model.CrMasContractCompanyUserId;
            ViewBag.Password = model.CrMasContractCompanyUserPassword;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ContractCompanyVM modelVM,IFormFile? ContractImg)
        {
            if (ModelState.IsValid)
            {

                string foldername = $"{"images\\Company"}\\{modelVM.CrMasContractCompanyLessor}\\{"Contracts"}";
                string filePathImage;
                if (ContractImg != null)
                {
                    var sysProcedure = _unitOfWork.CrMasSysProcedure.Find(x => x.CrMasSysProceduresClassification == "11" && x.CrMasSysProceduresCode == modelVM.CrMasContractCompanyProcedures);
                    string fileNameImg = sysProcedure.CrMasSysProceduresEnName;
                    string fileExtention = Path.GetExtension(ContractImg.FileName);
                    if (fileExtention==".pdf")
                    {
                        filePathImage = await ContractImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".pdf");
                    }
                    else
                    {
                        filePathImage = await ContractImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }
                    modelVM.CrMasContractCompanyImage = filePathImage;
                }
                else if (modelVM.CrMasContractCompanyImage != null)
                {
                    modelVM.CrMasContractCompanyImage = modelVM.CrMasContractCompanyImage;
                }
                else
                {
                    modelVM.CrMasContractCompanyImage = null;
                }
                var model = _mapper.Map<CrMasContractCompany>(modelVM);
                var update = await _compnayContract.UpdateCompanyContractCas(model);
                var doc = await _unitOfWork.CrMasContractCompany.FindAsync(x => x.CrMasContractCompanyNo == model.CrMasContractCompanyNo);
                // SaveTracing
                var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201003", "2");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "202", "20", currentUser.CrMasUserInformationLessor, "100",
                       doc.CrMasContractCompanyNo, null, null, doc.CrMasContractCompanyNumber, doc.CrMasContractCompanyDate, doc.CrMasContractCompanyStartDate, doc.CrMasContractCompanyEndDate, null, null, "اضافة",
                       "Insert", "I", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("CompanyContracts", "CompanyContracts");
            }
            return View(modelVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string code)
        {
            var companyContract = await _unitOfWork.CrMasContractCompany.FindAsync(x => x.CrMasContractCompanyNo == code);

            if (companyContract == null)
            {
                return Json(new { success = false });
            }
            else
            {
                string foldername = $"{"images\\Company"}\\{companyContract.CrMasContractCompanyLessor}\\{"Contracts"}";
                var sysProcedure = _unitOfWork.CrMasSysProcedure.Find(x => x.CrMasSysProceduresClassification == "11" && x.CrMasSysProceduresCode == companyContract.CrMasContractCompanyProcedures);
                string fileNameImg = sysProcedure.CrMasSysProceduresEnName;

                //await _compnayContract.AddCompanyContractCas(companyContract.CrMasContractCompanyLessor, companyContract.CrMasContractCompanyProcedures);
                companyContract.CrMasContractCompanyAboutToExpire = null;
                companyContract.CrMasContractCompanyDate = null;
                companyContract.CrMasContractCompanyStartDate = null;
                companyContract.CrMasContractCompanyEndDate = null;
                companyContract.CrMasContractCompanyNumber = null;
                companyContract.CrMasContractCompanyAnnualFees = null;
                companyContract.CrMasContractCompanyDiscountRate = null;
                companyContract.CrMasContractCompanyTaxRate = null;
                companyContract.CrMasContractCompanyUserId = null;
                companyContract.CrMasContractCompanyUserPassword = null;
                companyContract.CrMasContractCompanyImage = null;
                companyContract.CrMasContractCompanyStatus = "N";
                _unitOfWork.CrMasContractCompany.Update(companyContract); // Update the entity in the context
                await _unitOfWork.CompleteAsync(); // Save the changes
                await FileExtensions.RemoveImage(_webHostEnvironment, foldername, fileNameImg, ".png");
                await FileExtensions.RemoveImage(_webHostEnvironment, foldername, fileNameImg, ".pdf");

                // SaveTracing
                var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201003", "2");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "حذف", "Delete", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "202", "20", currentUser.CrMasUserInformationLessor, "100",
                  companyContract.CrMasContractCompanyNo, null, null, companyContract.CrMasContractCompanyNumber, companyContract.CrMasContractCompanyDate, companyContract.CrMasContractCompanyStartDate, companyContract.CrMasContractCompanyEndDate, null, null, "تعديل",
                  "Edit", "U", null);
                return Json(new { success = true });
            }
        }
    }
}
