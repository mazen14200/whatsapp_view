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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class DocumentsController : BaseController
    {
        private readonly IBranchDocument _BranchDocument;
        private readonly IUserLoginsService _userLoginsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _UserService;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<DocumentsController> _localizer;

        public DocumentsController(UserManager<CrMasUserInformation> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserService userService,
            IBranchDocument branchDocument,
            IWebHostEnvironment webHostEnvironment,
            IUserLoginsService userLoginsService,
            IAdminstritiveProcedures adminstritiveProcedures,
            IStringLocalizer<DocumentsController> localizer,
            IToastNotification toastNotification) : base(userManager, unitOfWork, mapper)
        {
            _UserService = userService;
            _BranchDocument = branchDocument;
            _webHostEnvironment = webHostEnvironment;
            _userLoginsService = userLoginsService;
            _adminstritiveProcedures = adminstritiveProcedures;
            _localizer = localizer;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<ActionResult> Documents()
        {
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "1";

            // Set Title
            var titles = await setTitle("201", "2201002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var user = await _UserService.GetUserLessor(User);

            var Docs = _unitOfWork.CrCasBranchDocument.FindAll(l => l.CrCasBranchDocumentsLessor == user.CrMasUserInformationLessor && l.CrCasBranchDocumentsStatus != Status.Active &&l.CrCasBranchDocumentsBranchStatus!=Status.Deleted, new[] { "CrCasBranchDocumentsProceduresNavigation", "CrCasBranchDocuments" }).ToList();
            if (Docs.Count() < 1)
            {
                var DocsActive = _unitOfWork.CrCasBranchDocument.FindAll(l => l.CrCasBranchDocumentsLessor == user.CrMasUserInformationLessor && l.CrCasBranchDocumentsStatus == Status.Active && l.CrCasBranchDocumentsBranchStatus != Status.Deleted, new[] { "CrCasBranchDocumentsProceduresNavigation", "CrCasBranchDocuments" }).ToList();
                return View(DocsActive);

            }
            return View(Docs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromQuery] string Procedureid, [FromQuery] string BranchId)
        {
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "1";

            // Set Title
            var titles = await setTitle("201", "2201002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var user = await _UserService.GetUserLessor(User);
            var BranchDocument = _unitOfWork.CrCasBranchDocument.Find(l => l.CrCasBranchDocumentsLessor == user.CrMasUserInformationLessor && l.CrCasBranchDocumentsBranch == BranchId && l.CrCasBranchDocumentsProcedures == Procedureid, new[] { "CrCasBranchDocumentsProceduresNavigation", "CrCasBranchDocuments" });

            if (CultureInfo.CurrentCulture.Name == "ar-EG")
            {
                ViewBag.branch = BranchDocument.CrCasBranchDocuments.CrCasBranchInformationArName;
                ViewBag.type = BranchDocument.CrCasBranchDocumentsProceduresNavigation.CrMasSysProceduresArName;
            }
            else
            {
                ViewBag.branch = BranchDocument.CrCasBranchDocuments.CrCasBranchInformationEnShortName;
                ViewBag.type = BranchDocument.CrCasBranchDocumentsProceduresNavigation.CrMasSysProceduresEnName;
            }
            ViewBag.date = BranchDocument.CrCasBranchDocumentsDate?.ToString("dd/MM/yyyy");
            ViewBag.startDate = BranchDocument.CrCasBranchDocumentsStartDate?.ToString("dd/MM/yyyy");
            ViewBag.endDate = BranchDocument.CrCasBranchDocumentsEndDate?.ToString("dd/MM/yyyy"); 

            var BranchDocumentVM = _mapper.Map<BranchDocumentVM>(BranchDocument);
            return View(BranchDocumentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BranchDocumentVM branchDocumentVM, IFormFile? DoucmentImg)
        {
            if (ModelState.IsValid)
            {

                var branchDocument = _mapper.Map<CrCasBranchDocument>(branchDocumentVM);
                string foldername = $"{"images\\Company"}\\{branchDocumentVM.CrCasBranchDocumentsLessor}\\{"Branches"}\\{branchDocumentVM.CrCasBranchDocumentsBranch}\\{"Documentions"}";
                string filePathImage;

                if (DoucmentImg != null)
                {
                    var sysProcedure = _unitOfWork.CrMasSysProcedure.Find(x => x.CrMasSysProceduresClassification == "10" && x.CrMasSysProceduresCode == branchDocumentVM.CrCasBranchDocumentsProcedures);
                    string fileNameImg = sysProcedure.CrMasSysProceduresEnName;
                    filePathImage = await DoucmentImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    branchDocument.CrCasBranchDocumentsImage = filePathImage;
                }
                else if (branchDocumentVM.CrCasBranchDocumentsImage!=null)
                {
                    branchDocument.CrCasBranchDocumentsImage = branchDocument.CrCasBranchDocumentsImage;
                }
                else
                {
                    branchDocument.CrCasBranchDocumentsImage = null;
                }
                //branchDocument.CrCasBranchDocumentsStatus = Status.Active;
                await _BranchDocument.UpdateBranchDocument(branchDocument);

                //SaveTracing
                var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201002", "2");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "202", "20", currentUser.CrMasUserInformationLessor, "100",
                branchDocument.CrCasBranchDocumentsNo, null, null, branchDocument.CrCasBranchDocumentsNo, branchDocument.CrCasBranchDocumentsDate, branchDocument.CrCasBranchDocumentsStartDate, branchDocument.CrCasBranchDocumentsEndDate,
                null, null, "اضافة","Insert", "I", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            }
            return RedirectToAction("Documents");
        }


        [HttpGet]
        public async Task<PartialViewResult> GetDocumentByStatus(string status)
        {
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "1";

            var lessor = await _UserService.GetUserLessor(User);
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var DocumentbyStatusAll = _unitOfWork.CrCasBranchDocument.FindAll(l => l.CrCasBranchDocumentsLessor == lessor.CrMasUserInformationLessor && l.CrCasBranchDocumentsBranchStatus != Status.Deleted, new[] { "CrCasBranchDocumentsProceduresNavigation", "CrCasBranchDocuments" }).ToList();
                    return PartialView("_DataTableDoc", DocumentbyStatusAll);
                }
                var DocumentbyStatus = _unitOfWork.CrCasBranchDocument.FindAll(l => l.CrCasBranchDocumentsStatus == status && l.CrCasBranchDocumentsLessor == lessor.CrMasUserInformationLessor && l.CrCasBranchDocumentsBranchStatus != Status.Deleted, new[] { "CrCasBranchDocumentsProceduresNavigation", "CrCasBranchDocuments" }).ToList();
                return PartialView("_DataTableDoc", DocumentbyStatus);
            }
            return PartialView();
        }

      
        [HttpDelete]
        public async Task<bool> EditDocumentStatus(string DocumentsLessor, string DocumentsBranch, string DocumentsProcedures, string status)
        {
            string sAr = "";
            string sEn = "";
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(DocumentsLessor);
            var branchDocument = await _BranchDocument.GetBranchDocument(DocumentsLessor, DocumentsBranch, DocumentsProcedures);
            
            if (lessor != null)
            {
                if (await CheckUserSubValidationProcdures("2201002", status))
                {
                    if (status == Status.Deleted)
                    {
                        string foldername = $"{"images\\Company"}\\{branchDocument.CrCasBranchDocumentsLessor}\\{"Branches"}\\{branchDocument.CrCasBranchDocumentsBranch}\\{"Documentions"}";
                        var sysProcedure = _unitOfWork.CrMasSysProcedure.Find(x => x.CrMasSysProceduresClassification == "10" && x.CrMasSysProceduresCode == branchDocument.CrCasBranchDocumentsProcedures);
                        string fileNameImg = sysProcedure.CrMasSysProceduresEnName;
                        sAr = "حذف المستند";
                        sEn = "Remove Document";
                        branchDocument.CrCasBranchDocumentsStatus = Status.Deleted;
                        await _BranchDocument.UpdateBranchDocument(branchDocument);
                        await FileExtensions.RemoveImage(_webHostEnvironment, foldername, fileNameImg, ".png");

                    }

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201002", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "202", "20", currentUser.CrMasUserInformationLessor, "100",
                        branchDocument.CrCasBranchDocumentsNo, null, null, branchDocument.CrCasBranchDocumentsNo, branchDocument.CrCasBranchDocumentsDate, branchDocument.CrCasBranchDocumentsStartDate, branchDocument.CrCasBranchDocumentsEndDate,
                        null, null, "تعديل", "Edit", "U", null);

                    return true;
                }
            }

            return false;

        }
        public IActionResult SuccessToast()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Documents", "Documents");
        }

    }
}
