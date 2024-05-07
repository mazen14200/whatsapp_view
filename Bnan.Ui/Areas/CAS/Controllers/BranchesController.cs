
using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class BranchesController : BaseController
    {
        public IUserService _UserService;
        public IBranchInformation _BranchInformation;
        public IPostBranch _PostBranch;
        public IBranchDocument _BranchDocument;
        public ISalesPoint _SalesPoint;
        private readonly IUserLoginsService _userLoginsService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<LessorsKSAController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BranchesController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IBranchInformation branchInformation, IUserLoginsService userLoginsService, IStringLocalizer<LessorsKSAController> localizer, IToastNotification toastNotification, IPostBranch postBranch, IBranchDocument branchDocument, ISalesPoint salesPoint, IAdminstritiveProcedures adminstritiveProcedures, IWebHostEnvironment webHostEnvironment) : base(userManager, unitOfWork, mapper)
        {
            _UserService = userService;
            _BranchInformation = branchInformation;
            _userLoginsService = userLoginsService;
            _localizer = localizer;
            _toastNotification = toastNotification;
            _PostBranch = postBranch;
            _BranchDocument = branchDocument;
            _SalesPoint = salesPoint;
            _adminstritiveProcedures = adminstritiveProcedures;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Branches()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201001", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "0";


            // Set Title
            var titles = await setTitle("201", "2201001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var lessor = await _UserService.GetUserLessor(User);
            var lessornumber = lessor?.CrMasUserInformationLessorNavigation?.CrMasLessorInformationCode;
            var Bracnhes = _unitOfWork?.CrCasBranchInformation.FindAll(l => l.CrCasBranchInformationLessor == lessornumber, new[] { "CrCasCarInformations" });
            var BranchPost = _unitOfWork?.CrCasBranchPost.FindAll(l => l.CrCasBranchPostLessor == lessor.CrMasUserInformationLessor, new[] { "CrCasBranchPostCityNavigation", "CrCasBranchPostNavigation" }).ToList();

            return View(BranchPost?.Where(x=>x.CrCasBranchPostNavigation.CrCasBranchInformationStatus==Status.Active));
        }
        [HttpGet]
        public async Task<PartialViewResult> GetBranchesByStatus(string status)
        {
            var lessor = await _UserService.GetUserLessor(User);
            var lessornumber = lessor.CrMasUserInformationLessorNavigation.CrMasLessorInformationCode;
            var Bracnhes = _unitOfWork?.CrCasBranchInformation.FindAll(l => l.CrCasBranchInformationLessor == lessornumber, new[] { "CrCasCarInformations" });
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var BranchbyStatusAll = _unitOfWork.CrCasBranchPost.FindAll(l => l.CrCasBranchPostLessor == lessor.CrMasUserInformationLessor, new[] { "CrCasBranchPostCityNavigation", "CrCasBranchPostNavigation" }).ToList();
                    return PartialView("_DataTableBranches", BranchbyStatusAll.Where(x=>x.CrCasBranchPostNavigation.CrCasBranchInformationStatus!=Status.Deleted));
                }
                var BranchbyStatus = _unitOfWork.CrCasBranchPost.FindAll(l => l.CrCasBranchPostNavigation.CrCasBranchInformationStatus == status && l.CrCasBranchPostLessor == lessor.CrMasUserInformationLessor, new[] { "CrCasBranchPostCityNavigation", "CrCasBranchPostNavigation" }).ToList();
                return PartialView("_DataTableBranches", BranchbyStatus);
            }
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> AddBranch()
        {
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "0";

            // Set Title
            var titles = await setTitle("201", "2201001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;

            var lessor = await _UserService.GetUserLessor(User);
            var lessornumber = lessor.CrMasUserInformationLessorNavigation.CrMasLessorInformationCode;

            var branch = _unitOfWork.CrCasBranchInformation.FindAll(l => l.CrCasBranchInformationLessor == lessornumber).LastOrDefault();
            var branchNumber = (Int32.Parse(branch?.CrCasBranchInformationCode) + 1).ToString();

            ViewBag.branchnumber = branchNumber;
            ViewBag.lessornumber = lessornumber;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBranch(BranchVM branchVM, IFormFile? SigntureFile)
        {
            var user = await _UserService.GetUserLessor(User);

            var IsValidCity = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityConcatenateArName == branchVM.BranchPostVM.CrCasBranchPostCity || l.CrMasSupPostCityConcatenateEnName == branchVM.BranchPostVM.CrCasBranchPostCity).FirstOrDefault();
            var IsGovNo = _unitOfWork.CrCasBranchInformation.FindAll(l => l.CrCasBranchInformationGovernmentNo == branchVM.CrCasBranchInformationGovernmentNo).Count() > 0;
            var IsTaxNo = _unitOfWork.CrCasBranchInformation.FindAll(l => l.CrCasBranchInformationTaxNo == branchVM.CrCasBranchInformationTaxNo).Count() > 0;
            bool NameArIsExist = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationArName == branchVM.CrCasBranchInformationArName && x.CrCasBranchInformationLessor == user.CrMasUserInformationLessor).Count() > 0;
            bool NameArShortIsExist = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationArShortName == branchVM.CrCasBranchInformationArShortName && x.CrCasBranchInformationLessor == user.CrMasUserInformationLessor).Count() > 0;
            bool NameEnIsExist = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationEnName == branchVM.CrCasBranchInformationEnName && x.CrCasBranchInformationLessor == user.CrMasUserInformationLessor).Count() > 0;
            bool NameEnShortIsExist = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationEnShortName == branchVM.CrCasBranchInformationEnShortName && x.CrCasBranchInformationLessor == user.CrMasUserInformationLessor).Count() > 0;
            if (IsValidCity == null) ModelState.AddModelError("BranchPostVM.CrCasBranchPostCity", _localizer["IsNotValidCity"]);
            if (IsGovNo) ModelState.AddModelError("CrCasBranchInformationGovernmentNo", _localizer["IsTakenGov"]);
            if (IsTaxNo) ModelState.AddModelError("CrCasBranchInformationTaxNo", _localizer["IsTakenTax"]);
            if (NameArIsExist) ModelState.AddModelError("CrCasBranchInformationArName", _localizer["IsExist"]);
            if (NameArShortIsExist) ModelState.AddModelError("CrCasBranchInformationArShortName", _localizer["IsExist"]);
            if (NameEnIsExist) ModelState.AddModelError("CrCasBranchInformationEnName", _localizer["IsExist"]);
            if (NameEnShortIsExist) ModelState.AddModelError("CrCasBranchInformationEnShortName", _localizer["IsExist"]);

            if (ModelState.IsValid)
            {
                var BranchInformaiton = _mapper.Map<CrCasBranchInformation>(branchVM);
                var BranchPost = _mapper.Map<CrCasBranchPost>(branchVM.BranchPostVM);
                BranchPost.CrCasBranchPostLessor = user.CrMasUserInformationLessor;
                BranchPost.CrCasBranchPostBranch = BranchInformaiton.CrCasBranchInformationCode;
                BranchInformaiton.CrCasBranchInformationLessor = user.CrMasUserInformationLessor;

                string foldername = $"{"images\\Company"}\\{BranchInformaiton.CrCasBranchInformationLessor}\\{"Branches"}\\{BranchInformaiton.CrCasBranchInformationCode}";
                string filePathSignture;

                if (SigntureFile != null)
                {
                    string fileNameImg = "DirectorSignature";
                    filePathSignture = await SigntureFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                }
                else
                {
                    filePathSignture = "~/images/common/DefualtUserSignature.png";
                }
                BranchInformaiton.CrCasBranchInformationDirectorSignature = filePathSignture;

                await _BranchInformation.AddBranchInformation(BranchInformaiton);
                await _PostBranch.AddPostBranch(BranchPost, IsValidCity);
                await _BranchDocument.AddBranchDocument(branchVM.CrCasBranchInformationLessor, branchVM.CrCasBranchInformationCode);
                await _SalesPoint.AddSalesPoint(BranchInformaiton);
                await _unitOfWork.CompleteAsync();
                FileExtensions.CreateFolderBranch(_webHostEnvironment, user.CrMasUserInformationLessor, BranchInformaiton.CrCasBranchInformationCode);
                var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201001", "2");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة فرع", "Add branch", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "201", "20", currentUser.CrMasUserInformationLessor, "100",
                BranchInformaiton.CrCasBranchInformationCode, null, null, null, null, null, null, null, null, "اضافة", "Insert", "I", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("Branches");
            }



            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active && x.CrMasSysCallingKeysNo == "966");
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;
            var lessornumber = user.CrMasUserInformationLessorNavigation.CrMasLessorInformationCode;
            var branch = _unitOfWork.CrCasBranchInformation.FindAll(l => l.CrCasBranchInformationLessor == lessornumber).LastOrDefault();
            var branchNumber = (Int32.Parse(branch?.CrCasBranchInformationCode) + 1).ToString();
            ViewBag.branchnumber = branchNumber;
            ViewBag.lessornumber = lessornumber;

            return View(branchVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "0";

            // Set Title
            var titles = await setTitle("201", "2201001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            /*            var IsValidCity = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityConcatenateArName == branchVM.BranchPostVM.CrCasBranchPostCity || l.CrMasSupPostCityConcatenateEnName == branchVM.BranchPostVM.CrCasBranchPostCity).FirstOrDefault();
            */
            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;
            var lessor = await _UserService.GetUserLessor(User);
            var branchinformation = _unitOfWork.CrCasBranchInformation.Find(l => l.CrCasBranchInformationLessor == lessor.CrMasUserInformationLessor && l.CrCasBranchInformationCode == id);
            var BranchPost = _unitOfWork.CrCasBranchPost.Find(l => l.CrCasBranchPostLessor == lessor.CrMasUserInformationLessor && l.CrCasBranchPostBranch == id, new[] { "CrCasBranchPostNavigation", "CrCasBranchPostCityNavigation" });
            ViewBag.SalesPointCount = _unitOfWork.CrCasAccountSalesPoint.FindAll(l => l.CrCasAccountSalesPointLessor == lessor.CrMasUserInformationLessor && l.CrCasAccountSalesPointBrn == id&&l.CrCasAccountSalesPointStatus!=Status.Deleted&&l.CrCasAccountSalesPointBank!="00").Count();
            ViewBag.CarsCount = _unitOfWork.CrCasCarInformation.FindAll(l => l.CrCasCarInformationLessor == lessor.CrMasUserInformationLessor && l.CrCasCarInformationBranch == id&&l.CrCasCarInformationStatus!=Status.Deleted&&l.CrCasCarInformationStatus!=Status.Sold).Count();

            BranchVM branchVM = _mapper.Map<BranchVM>(branchinformation);
            branchVM.BranchPostVM = _mapper.Map<BranchPost1VM>(BranchPost);

            if (CultureInfo.CurrentCulture.Name == "en-US")
            {
                ViewBag.CrCasBranchPostCity = BranchPost.CrCasBranchPostCityNavigation.CrMasSupPostCityConcatenateEnName;
            }
            else
            {
                ViewBag.CrCasBranchPostCity = BranchPost.CrCasBranchPostCityNavigation.CrMasSupPostCityConcatenateArName;
            }


            return View(branchVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BranchVM branchVM, IFormFile? SigntureFile)
        {
            var IsValidCity = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityConcatenateArName == branchVM.BranchPostVM.CrCasBranchPostCity || l.CrMasSupPostCityConcatenateEnName == branchVM.BranchPostVM.CrCasBranchPostCity).FirstOrDefault();
            if (IsValidCity == null) ModelState.AddModelError("BranchPostVM.CrCasBranchPostCity", _localizer["IsNotValidCity"]);

            if (ModelState.IsValid)
            {
                var BranchInformaiton = _mapper.Map<CrCasBranchInformation>(branchVM);
                var BranchPost = _mapper.Map<CrCasBranchPost>(branchVM.BranchPostVM);
                BranchPost.CrCasBranchPostLessor = BranchInformaiton.CrCasBranchInformationLessor;
                BranchPost.CrCasBranchPostBranch = BranchInformaiton.CrCasBranchInformationCode;
                BranchPost.CrCasBranchPostCity = IsValidCity?.CrMasSupPostCityCode;
                string foldername = $"{"images\\Company"}\\{BranchInformaiton.CrCasBranchInformationLessor}\\{"Branches"}\\{BranchInformaiton.CrCasBranchInformationCode}";
                string filePathSignture;
                if (SigntureFile != null)
                {
                    string fileNameImg = "DirectorSignature";
                    filePathSignture = await SigntureFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                }
                else if (branchVM.CrCasBranchInformationDirectorSignature != null)
                {
                    filePathSignture = branchVM.CrCasBranchInformationDirectorSignature;
                }
                else
                {
                    filePathSignture = "~/images/common/DefualtUserSignature.png";
                }
                BranchInformaiton.CrCasBranchInformationDirectorSignature = filePathSignture;
                _unitOfWork.CrCasBranchInformation.Update(BranchInformaiton);
                _unitOfWork.CrCasBranchPost.Update(BranchPost);

                await _unitOfWork.CompleteAsync();
                var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201001", "2");

                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل فرع", "Edit branch", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "201", "20", currentUser.CrMasUserInformationLessor, "100",
               BranchInformaiton.CrCasBranchInformationCode, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);

                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("Branches");

            }

            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "0";

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active && x.CrMasSysCallingKeysNo == "966");
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;

            return View(branchVM);
        }

        [HttpGet]
        public JsonResult GetCities()
        {
            if (CultureInfo.CurrentCulture.Name == "ar-EG")
            {
                var citiesAr = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
                var citiesArarrayAr = citiesAr.Select(c => new { text = c.CrMasSupPostCityConcatenateArName, value = c.CrMasSupPostCityCode });
                return Json(citiesArarrayAr);
            }

            var citiesEn = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
            var citiesArarrayEn = citiesEn.Select(c => new { text = c.CrMasSupPostCityConcatenateEnName, value = c.CrMasSupPostCityCode });
            return Json(citiesArarrayEn);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatus(string lessorCode, string Branchcode, string status)
        {
            string sAr = "";
            string sEn = "";
            var branch = _unitOfWork.CrCasBranchInformation.Find(l => l.CrCasBranchInformationLessor == lessorCode && l.CrCasBranchInformationCode == Branchcode);
            var docs = _unitOfWork.CrCasBranchDocument.FindAll(l => l.CrCasBranchDocumentsLessor == lessorCode && l.CrCasBranchDocumentsBranch == Branchcode);
            var salesPoints = _unitOfWork.CrCasAccountSalesPoint.FindAll(l => l.CrCasAccountSalesPointLessor == lessorCode && l.CrCasAccountSalesPointBrn == Branchcode);
            var cars = _unitOfWork.CrCasCarInformation.FindAll(l => l.CrCasCarInformationLessor == lessorCode && l.CrCasCarInformationBranch == Branchcode&&l.CrCasCarInformationStatus!=Status.Sold);
            var userBranchValidity = _unitOfWork.CrMasUserBranchValidity.FindAll(l => l.CrMasUserBranchValidityLessor == lessorCode && l.CrMasUserBranchValidityBranch == Branchcode);
            if (branch != null)
            {
                if (await CheckUserSubValidationProcdures("2201001", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Hold";
                        branch.CrCasBranchInformationStatus = Status.Hold;
                        foreach (var doc in docs) doc.CrCasBranchDocumentsBranchStatus = Status.Hold;
                        foreach (var salesPoint in salesPoints) salesPoint.CrCasAccountSalesPointBranchStatus = Status.Hold;
                        foreach (var car in cars) car.CrCasCarInformationBranchStatus = Status.Hold;
                        foreach (var user in userBranchValidity) user.CrMasUserBranchValidityBranchRecStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        branch.CrCasBranchInformationStatus = Status.Deleted;
                        foreach (var doc in docs) doc.CrCasBranchDocumentsBranchStatus = Status.Deleted;
                        foreach (var salesPoint in salesPoints) salesPoint.CrCasAccountSalesPointBranchStatus = Status.Deleted;
                        foreach (var car in cars) car.CrCasCarInformationBranchStatus = Status.Deleted;
                        foreach (var user in userBranchValidity) user.CrMasUserBranchValidityBranchRecStatus = Status.Deleted;
                    }
                    else if (status == Status.Active)
                    {
                        sAr = "استرجاع";
                        sEn = "Retrive";
                        branch.CrCasBranchInformationStatus = Status.Active;
                        foreach (var doc in docs) doc.CrCasBranchDocumentsBranchStatus = Status.Active;
                        foreach (var salesPoint in salesPoints) salesPoint.CrCasAccountSalesPointBranchStatus = Status.Active;
                        foreach (var car in cars) car.CrCasCarInformationBranchStatus = Status.Active;
                        foreach (var user in userBranchValidity) user.CrMasUserBranchValidityBranchRecStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("201", "2201001", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "201", "20", currentUser.CrMasUserInformationLessor, "100",
                   branch.CrCasBranchInformationCode, null, null, null, null, null, null, null, null, sAr,  sEn, "U", null);

                    return RedirectToAction("Index", "LessorsKSA");
                }
            }

            return View(branch);

        }


    }
}
