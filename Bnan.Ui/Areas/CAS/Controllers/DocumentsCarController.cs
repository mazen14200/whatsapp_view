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
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class DocumentsCarController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _UserService;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IDocumentsMaintainanceCar _documentsMaintainance;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<DocumentsCarController> _localizer;



        public DocumentsCarController(UserManager<CrMasUserInformation> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserService userService,
            IWebHostEnvironment webHostEnvironment,
            IUserLoginsService userLoginsService,
            IAdminstritiveProcedures adminstritiveProcedures,
            IDocumentsMaintainanceCar documentsMaintainance,
            IToastNotification toastNotification,
            IStringLocalizer<DocumentsCarController> localizer) : base(userManager, unitOfWork, mapper)
        {
            _UserService = userService;
            _webHostEnvironment = webHostEnvironment;
            _userLoginsService = userLoginsService;
            _adminstritiveProcedures = adminstritiveProcedures;
            _documentsMaintainance = documentsMaintainance;
            _toastNotification = toastNotification;
            _localizer = localizer;
        }
        [HttpGet]
        public async Task<ActionResult> DocumentsCar()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("202", "2202002", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "1";

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("202", "2202002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var Docs = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(l => l.CrCasCarDocumentsMaintenanceLessor == lessorCode && l.CrCasCarDocumentsMaintenanceStatus == Status.Renewed &&l.CrCasCarDocumentsMaintenanceProceduresClassification=="12" , new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation", "CrCasCarDocumentsMaintenanceSerailNoNavigation" }).ToList();
            return View(Docs);
        }
        [HttpGet]
        public async Task<PartialViewResult> GetDocumentsCarByStatus(string status)
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "1";

            var lessor = await _UserService.GetUserLessor(User);
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var DocumentsCarbyStatusAll = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(l => l.CrCasCarDocumentsMaintenanceLessor == lessor.CrMasUserInformationLessor && l.CrCasCarDocumentsMaintenanceStatus != Status.Deleted && l.CrCasCarDocumentsMaintenanceProceduresClassification == "12", new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation", "CrCasCarDocumentsMaintenanceSerailNoNavigation" }).ToList();
                    return PartialView("_DataTableDocsCar", DocumentsCarbyStatusAll);
                }
                var DocumentbyStatus = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(l => l.CrCasCarDocumentsMaintenanceStatus == status && l.CrCasCarDocumentsMaintenanceLessor == lessor.CrMasUserInformationLessor && l.CrCasCarDocumentsMaintenanceStatus != Status.Deleted && l.CrCasCarDocumentsMaintenanceProceduresClassification == "12", new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation", "CrCasCarDocumentsMaintenanceSerailNoNavigation" }).ToList();
                return PartialView("_DataTableDocsCar", DocumentbyStatus);
            }
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] string Procedureid, [FromQuery] string SerialNumber)
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "1";

            // Set Title
            var titles = await setTitle("202", "2202002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            var user = await _UserService.GetUserLessor(User);
            var DocumentCar = _unitOfWork.CrCasCarDocumentsMaintenance.Find(l => l.CrCasCarDocumentsMaintenanceLessor == user.CrMasUserInformationLessor && l.CrCasCarDocumentsMaintenanceSerailNo == SerialNumber && l.CrCasCarDocumentsMaintenanceProcedures == Procedureid, new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation", "CrCasCarDocumentsMaintenanceSerailNoNavigation" });
            ViewBag.date = DocumentCar.CrCasCarDocumentsMaintenanceDate?.ToString("dd/MM/yyyy");
            ViewBag.startDate = DocumentCar.CrCasCarDocumentsMaintenanceStartDate?.ToString("dd/MM/yyyy");
            ViewBag.endDate = DocumentCar.CrCasCarDocumentsMaintenanceEndDate?.ToString("dd/MM/yyyy");

            var DocumentsCarVM = _mapper.Map<DocumentsMaintainceCarVM>(DocumentCar);
            return View(DocumentsCarVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DocumentsMaintainceCarVM documentsMaintainceCarVM, IFormFile? DoucmentImg)
        {
            if (ModelState.IsValid)
            {
                var documentsMaintainceCar = _mapper.Map<CrCasCarDocumentsMaintenance>(documentsMaintainceCarVM);
                string foldername = $"{"images\\Company"}\\{documentsMaintainceCarVM.CrCasCarDocumentsMaintenanceLessor}\\{"Cars"}\\{documentsMaintainceCar.CrCasCarDocumentsMaintenanceSerailNo}";
                string filePathImage;
                if (DoucmentImg != null)
                {
                    var sysProcedure = _unitOfWork.CrMasSysProcedure.Find(x => x.CrMasSysProceduresClassification == "12" && x.CrMasSysProceduresCode == documentsMaintainceCarVM.CrCasCarDocumentsMaintenanceProcedures);
                    string fileNameImg = sysProcedure.CrMasSysProceduresCode;
                    filePathImage = await DoucmentImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    documentsMaintainceCar.CrCasCarDocumentsMaintenanceImage = filePathImage;
                }
                else if (documentsMaintainceCarVM.CrCasCarDocumentsMaintenanceImage != null)
                {
                    documentsMaintainceCar.CrCasCarDocumentsMaintenanceImage = documentsMaintainceCar.CrCasCarDocumentsMaintenanceImage;
                }
                else
                {
                    documentsMaintainceCar.CrCasCarDocumentsMaintenanceImage = null;
                }
                if (await _documentsMaintainance.UpdateDocumentCar(documentsMaintainceCar))
                {
                    await _unitOfWork.CompleteAsync();
                    //SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("202", "2202002", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "212", "20", currentUser.CrMasUserInformationLessor, "100",
                    documentsMaintainceCar.CrCasCarDocumentsMaintenanceNo, null, null, documentsMaintainceCar.CrCasCarDocumentsMaintenanceNo, documentsMaintainceCar.CrCasCarDocumentsMaintenanceDate, documentsMaintainceCar.CrCasCarDocumentsMaintenanceStartDate, documentsMaintainceCar.CrCasCarDocumentsMaintenanceEndDate,
                    null, null, "اضافة", "Insert", "I", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("DocumentsCar", "DocumentsCar");

                }
            }
            return View(documentsMaintainceCarVM);
        }


        [HttpDelete]
        public async Task<bool> EditDocumentStatus(string DocumentCarLessor, string DocumentCarBranch, string DocumentCarProcedures,string SerialNumber, string status)
        {
            string sAr = "";
            string sEn = "";
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(DocumentCarLessor);
            var CarDocument = await _unitOfWork.CrCasCarDocumentsMaintenance.FindAsync(x=>x.CrCasCarDocumentsMaintenanceLessor== DocumentCarLessor&&
                                                                                        x.CrCasCarDocumentsMaintenanceBranch== DocumentCarBranch &&
                                                                                        x.CrCasCarDocumentsMaintenanceProcedures== DocumentCarProcedures&&
                                                                                        x.CrCasCarDocumentsMaintenanceSerailNo== SerialNumber);

            if (lessor != null)
            {
                if (await CheckUserSubValidationProcdures("2202002", status))
                {
                    if (status == Status.Deleted)
                    {
                        string foldername = $"{"images\\Company"}\\{CarDocument.CrCasCarDocumentsMaintenanceLessor}\\{"Cars"}\\{CarDocument.CrCasCarDocumentsMaintenanceSerailNo}";
                        var sysProcedure = _unitOfWork.CrMasSysProcedure.Find(x => x.CrMasSysProceduresClassification == "12" && x.CrMasSysProceduresCode == CarDocument.CrCasCarDocumentsMaintenanceProcedures);
                        string fileNameImg = sysProcedure.CrMasSysProceduresEnName;
                        sAr = "حذف المستند";
                        sEn = "Remove Document";
                        CarDocument.CrCasCarDocumentsMaintenanceStatus = Status.Renewed;
                        CarDocument.CrCasCarDocumentsMaintenanceDate = null;
                        CarDocument.CrCasCarDocumentsMaintenanceStartDate = null;
                        CarDocument.CrCasCarDocumentsMaintenanceEndDate = null;
                        CarDocument.CrCasCarDocumentsMaintenanceDateAboutToFinish = null;
                        CarDocument.CrCasCarDocumentsMaintenanceImage = null;
                        CarDocument.CrCasCarDocumentsMaintenanceNo = null;
                        _unitOfWork.CrCasCarDocumentsMaintenance.Update(CarDocument);
                        await _unitOfWork.CompleteAsync();
                        await FileExtensions.RemoveImage(_webHostEnvironment, foldername, fileNameImg, ".png");

                    }

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("202", "2202002", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "212", "20", currentUser.CrMasUserInformationLessor, "100",
                        CarDocument.CrCasCarDocumentsMaintenanceNo, null, null, CarDocument.CrCasCarDocumentsMaintenanceNo, CarDocument.CrCasCarDocumentsMaintenanceDate, CarDocument.CrCasCarDocumentsMaintenanceStartDate, CarDocument.CrCasCarDocumentsMaintenanceEndDate,
                        null, null, "تعديل", "Edit", "U", null);

                    return true;
                }
            }

            return false;

        }
        public IActionResult SuccessMessage()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("DocumentsCar", "DocumentsCar");
        }
    }
}
