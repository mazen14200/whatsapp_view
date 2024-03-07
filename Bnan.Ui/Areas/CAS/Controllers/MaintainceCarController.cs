using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class MaintainceCarController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _UserService;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IDocumentsMaintainanceCar _documentsMaintainance;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<MaintainceCarController> _localizer;

        public MaintainceCarController(UserManager<CrMasUserInformation> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserService userService,
            IWebHostEnvironment webHostEnvironment,
            IUserLoginsService userLoginsService,
            IAdminstritiveProcedures adminstritiveProcedures,
            IDocumentsMaintainanceCar documentsMaintainance,
            IToastNotification toastNotification,
            IStringLocalizer<MaintainceCarController> localizer) : base(userManager, unitOfWork, mapper)
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
        public async Task<ActionResult> MaintainceCar()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("202", "2202003", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "2";

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("202", "2202003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var Docs = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(l => l.CrCasCarDocumentsMaintenanceLessor == lessorCode && l.CrCasCarDocumentsMaintenanceStatus == Status.Renewed && l.CrCasCarDocumentsMaintenanceProceduresClassification == "13",
                                                                              new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation", "CrCasCarDocumentsMaintenanceSerailNoNavigation" });

            return View(Docs);
        }
        [HttpGet]
        public async Task<PartialViewResult> GetMaintainceCarByStatus(string status)
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "2";

            var lessor = await _UserService.GetUserLessor(User);
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var DocumentsCarbyStatusAll = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(l => l.CrCasCarDocumentsMaintenanceLessor == lessor.CrMasUserInformationLessor && l.CrCasCarDocumentsMaintenanceStatus != Status.Deleted && l.CrCasCarDocumentsMaintenanceProceduresClassification == "13", new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation", "CrCasCarDocumentsMaintenanceSerailNoNavigation" }).ToList();
                    return PartialView("_DataTableMaintainceCar", DocumentsCarbyStatusAll);
                }
                var DocumentbyStatus = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(l => l.CrCasCarDocumentsMaintenanceStatus == status && l.CrCasCarDocumentsMaintenanceLessor == lessor.CrMasUserInformationLessor && l.CrCasCarDocumentsMaintenanceStatus != Status.Deleted && l.CrCasCarDocumentsMaintenanceProceduresClassification == "13", new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation", "CrCasCarDocumentsMaintenanceSerailNoNavigation" }).ToList();
                return PartialView("_DataTableMaintainceCar", DocumentbyStatus);
            }
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] string Procedureid, [FromQuery] string SerialNumber)
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "2";

            // Set Title
            var titles = await setTitle("202", "2202003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            var user = await _UserService.GetUserLessor(User);
            var DocumentCar = _unitOfWork.CrCasCarDocumentsMaintenance.Find(l => l.CrCasCarDocumentsMaintenanceLessor == user.CrMasUserInformationLessor && l.CrCasCarDocumentsMaintenanceSerailNo == SerialNumber && l.CrCasCarDocumentsMaintenanceProcedures == Procedureid, new[] { "CrCasCarDocumentsMaintenanceProceduresNavigation", "CrCasCarDocumentsMaintenanceSerailNoNavigation" });
            ViewBag.date = DocumentCar.CrCasCarDocumentsMaintenanceDate?.ToString("dd/MM/yyyy");
            ViewBag.startDate = DocumentCar.CrCasCarDocumentsMaintenanceStartDate?.ToString("dd/MM/yyyy");
            ViewBag.endDate = DocumentCar.CrCasCarDocumentsMaintenanceEndDate?.ToString("dd/MM/yyyy");
            ViewBag.CurrentMeter = DocumentCar.CrCasCarDocumentsMaintenanceCurrentMeter?.ToString("N0");
            ViewBag.ConsumptionKm = DocumentCar.CrCasCarDocumentsMaintenanceConsumptionKm?.ToString("N0");
            ViewBag.KmEndsAt = DocumentCar.CrCasCarDocumentsMaintenanceKmEndsAt?.ToString("N0");

            var DocumentsCarVM = _mapper.Map<DocumentsMaintainceCarVM>(DocumentCar);
            return View(DocumentsCarVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DocumentsMaintainceCarVM documentsMaintainceCarVM)
        {
            if (ModelState.IsValid)
            {
                var documentsMaintainceCar = _mapper.Map<CrCasCarDocumentsMaintenance>(documentsMaintainceCarVM);
                if (await _documentsMaintainance.UpdateMaintainceCar(documentsMaintainceCar))
                {
                    await _unitOfWork.CompleteAsync();
                    //SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("202", "2202003", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "213", "20", currentUser.CrMasUserInformationLessor, "100",
                    documentsMaintainceCar.CrCasCarDocumentsMaintenanceNo, null, null, documentsMaintainceCar.CrCasCarDocumentsMaintenanceNo, documentsMaintainceCar.CrCasCarDocumentsMaintenanceDate, documentsMaintainceCar.CrCasCarDocumentsMaintenanceStartDate, documentsMaintainceCar.CrCasCarDocumentsMaintenanceEndDate,
                    null, null, "اضافة", "Insert", "I", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("MaintainceCar", "MaintainceCar");

                }
            }
            return View(documentsMaintainceCarVM);
        }
        [HttpDelete]
        public async Task<bool> EditMaintainceCarStatus(string DocumentCarLessor, string DocumentCarBranch, string DocumentCarProcedures, string SerialNumber, string status)
        {
            string sAr = "";
            string sEn = "";
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(DocumentCarLessor);
            var CarDocument = await _unitOfWork.CrCasCarDocumentsMaintenance.FindAsync(x => x.CrCasCarDocumentsMaintenanceLessor == DocumentCarLessor &&
                                                                                        x.CrCasCarDocumentsMaintenanceBranch == DocumentCarBranch &&
                                                                                        x.CrCasCarDocumentsMaintenanceProcedures == DocumentCarProcedures &&
                                                                                        x.CrCasCarDocumentsMaintenanceSerailNo == SerialNumber);
            var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationLessor == lessor.CrMasLessorInformationCode &&
                                                              x.CrCasCarInformationBranch == CarDocument.CrCasCarDocumentsMaintenanceBranch &&
                                                              x.CrCasCarInformationSerailNo == CarDocument.CrCasCarDocumentsMaintenanceSerailNo);
            if (lessor != null)
            {
                if (await CheckUserSubValidationProcdures("2202003", status))
                {
                    if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        CarDocument.CrCasCarDocumentsMaintenanceStatus = Status.Renewed;
                        CarDocument.CrCasCarDocumentsMaintenanceDate = null;
                        CarDocument.CrCasCarDocumentsMaintenanceStartDate = null;
                        CarDocument.CrCasCarDocumentsMaintenanceEndDate = null;
                        CarDocument.CrCasCarDocumentsMaintenanceDateAboutToFinish = null;
                        CarDocument.CrCasCarDocumentsMaintenanceImage = null;
                        CarDocument.CrCasCarDocumentsMaintenanceKmEndsAt = null;
                        CarDocument.CrCasCarDocumentsMaintenanceKmAboutToFinish = null;
                        CarDocument.CrCasCarDocumentsMaintenanceConsumptionKm = null;
                        CarDocument.CrCasCarDocumentsMaintenanceCurrentMeter = car.CrCasCarInformationCurrentMeter;
                        CarDocument.CrCasCarDocumentsMaintenanceNo = null;
                        CarDocument.CrCasCarDocumentsMaintenanceReasons = null;
                        _unitOfWork.CrCasCarDocumentsMaintenance.Update(CarDocument);
                        await _unitOfWork.CompleteAsync();
                        // SaveTracing
                        var (mainTask, subTask, system, currentUser) = await SetTrace("202", "2202003", "2");
                        await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                        subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                        subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                        // Save Adminstrive Procedures
                        await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "213", "20", currentUser.CrMasUserInformationLessor, "100",
                            CarDocument.CrCasCarDocumentsMaintenanceNo, null, null, CarDocument.CrCasCarDocumentsMaintenanceNo, CarDocument.CrCasCarDocumentsMaintenanceDate, CarDocument.CrCasCarDocumentsMaintenanceStartDate, CarDocument.CrCasCarDocumentsMaintenanceEndDate,
                            null, null, "تعديل", "Edit", "U", null);

                        return true;
                    }
                }
            }
            return false;
        }
        public IActionResult SuccessMessage()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("MaintainceCar", "MaintainceCar");
        }
    }
}
