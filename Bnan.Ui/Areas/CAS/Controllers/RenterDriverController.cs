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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;
using System.Numerics;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class RenterDriverController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IRenterDriver _renterDriver;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<RenterDriverController> _localizer;
        private readonly IAuthService _authService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserMainValidtion _userMainValidtion;
        private readonly IUserSubValidition _userSubValidition;
        private readonly IUserProcedureValidition _userProcedureValidition;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;

        public RenterDriverController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
             IMapper mapper, IUserService userService, IRenterDriver renterDriver,
             IUserLoginsService userLoginsService, IToastNotification toastNotification,
             IAuthService authService, IWebHostEnvironment webHostEnvironment,
             IUserMainValidtion userMainValidtion, IUserSubValidition userSubValidition,
             IUserProcedureValidition userProcedureValidition,
            IStringLocalizer<RenterDriverController> localizer, IAdminstritiveProcedures adminstritiveProcedures) :
             base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _renterDriver = renterDriver;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _authService = authService;
            _webHostEnvironment = webHostEnvironment;
            _userMainValidtion = userMainValidtion;
            _userSubValidition = userSubValidition;
            _userProcedureValidition = userProcedureValidition;
            _adminstritiveProcedures = adminstritiveProcedures;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207005", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            //sidebar Active
            ViewBag.id = "#sidebarServices";
            ViewBag.no = "4";
            // Get Lessor Code
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("207", "2207005", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            //var RenterDriver = await _unitOfWork.CrCasRenterPrivateDriverInformation.GetAllAsync();
            var RenterDrivers = _unitOfWork.CrCasRenterPrivateDriverInformation.FindAll(x => x.CrCasRenterPrivateDriverInformationStatus == "A" && x.CrCasRenterPrivateDriverInformationLessor==lessorCode,
                new[] { "CrCasRenterPrivateDriverInformationGenderNavigation", "CrCasRenterPrivateDriverInformationIdtrypeNavigation", "CrCasRenterPrivateDriverInformationLicenseTypeNavigation", "CrCasRenterPrivateDriverInformationNationalityNavigation" } ).ToList();
            return View(RenterDrivers);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetRenterDriversByStatus(string status)
        {
            // Get Lessor Code
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            if (!string.IsNullOrEmpty(status))
            {
                var RenterDriverbyStatusAll = _unitOfWork.CrCasRenterPrivateDriverInformation.FindAll(x => x.CrCasRenterPrivateDriverInformationLessor == lessorCode,
                new[] { "CrCasRenterPrivateDriverInformationGenderNavigation", "CrCasRenterPrivateDriverInformationIdtrypeNavigation", "CrCasRenterPrivateDriverInformationLicenseTypeNavigation", "CrCasRenterPrivateDriverInformationNationalityNavigation" }).ToList();
                if (status == Status.All) return PartialView("_DataTableRenterDriver", RenterDriverbyStatusAll.Where(x=>x.CrCasRenterPrivateDriverInformationStatus!=Status.Deleted));
                 RenterDriverbyStatusAll = _unitOfWork.CrCasRenterPrivateDriverInformation.FindAll(l => l.CrCasRenterPrivateDriverInformationStatus == status && l.CrCasRenterPrivateDriverInformationLessor == lessorCode).ToList();
                return PartialView("_DataTableRenterDriver", RenterDriverbyStatusAll.Where(x=>x.CrCasRenterPrivateDriverInformationStatus==status));
            }
            return PartialView();
        }


        [HttpGet]
        public JsonResult GetRenterDriverNationalityEn(string? prefix)
        {

            var res = _unitOfWork.CrMasSupRenterNationality.GetAll();
            var list = res.ToList();
            var NationEnglish = (from c in list
                                 where c.CrMasSupRenterNationalitiesEnName.Contains(prefix) && c.CrMasSupRenterNationalitiesStatus == "A" &&
                                 c.CrMasSupRenterNationalitiesCode != "1000000001" && c.CrMasSupRenterNationalitiesCode != "1000000002"
                                 select new
                                 {
                                     label = c.CrMasSupRenterNationalitiesEnName,
                                     val = c.CrMasSupRenterNationalitiesEnName
                                 }).ToList();
            return Json(NationEnglish);
        }

        [HttpGet]
        public JsonResult GetRenterDriverNationalityAr(string? prefix)
        {

            var res = _unitOfWork.CrMasSupRenterNationality.GetAll();
            var list = res.ToList();
            var NationAr = (from c in list
                            where c.CrMasSupRenterNationalitiesArName.Contains(prefix) && c.CrMasSupRenterNationalitiesStatus == "A" &&
                            c.CrMasSupRenterNationalitiesCode != "1000000001" && c.CrMasSupRenterNationalitiesCode != "1000000002"
                            select new
                            {
                                label = c.CrMasSupRenterNationalitiesArName,
                                val = c.CrMasSupRenterNationalitiesArName
                            }).ToList();
            return Json(NationAr);
        }


        [HttpGet]
        public ActionResult GetCode(string selectedOption)
        {

            var res = _unitOfWork.CrMasSupRenterNationality.GetAll();
            var list = res.ToList();
            var Code = (from c in list
                        where c.CrMasSupRenterNationalitiesArName == selectedOption || c.CrMasSupRenterNationalitiesEnName == selectedOption
                        select c.CrMasSupRenterNationalitiesCode).ToList()[0].ToString();

            return Json(new { data1 = Code });
        }




        [HttpGet]
        public async Task<IActionResult> AddRenterDriver()
        {
            //sidebar Active
            ViewBag.id = "#sidebarServices";
            ViewBag.no = "4";
            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("207", "2207005", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var Drivers = await _unitOfWork.CrCasRenterPrivateDriverInformation.GetAllAsync();

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;


            // View the License
            var LicenseType = _unitOfWork.CrMasSupRenterDrivingLicense.FindAll(x => x.CrMasSupRenterDrivingLicenseStatus == Status.Active);
            var LicenseTypeAr = LicenseType.Select(c => new SelectListItem { Value = c.CrMasSupRenterDrivingLicenseCode.ToString(), Text = c.CrMasSupRenterDrivingLicenseArName }).ToList();
            var LicenseTypeEn = LicenseType.Select(c => new SelectListItem { Value = c.CrMasSupRenterDrivingLicenseCode.ToString(), Text = c.CrMasSupRenterDrivingLicenseEnName }).ToList();

            ViewData["RenterDriverLicenseTypeAr"] = LicenseTypeAr;
            ViewData["RenterDriverLicenseTypeEn"] = LicenseTypeEn;


            // View the License
            var IDType = _unitOfWork.CrMasSupRenterIdtype.FindAll(x => x.CrMasSupRenterIdtypeStatus == Status.Active);
            var IDTypeAr = IDType.Select(c => new SelectListItem { Value = c.CrMasSupRenterIdtypeCode.ToString(), Text = c.CrMasSupRenterIdtypeArName }).ToList();
            var IDTypeEn = IDType.Select(c => new SelectListItem { Value = c.CrMasSupRenterIdtypeCode.ToString(), Text = c.CrMasSupRenterIdtypeEnName }).ToList();

            ViewData["RenterDriverTypeAr"] = IDTypeAr;
            ViewData["RenterDriverTypeEn"] = IDTypeEn;
            // View the Gender
            var Gender = _unitOfWork.CrMasSupRenterGender.FindAll(x => x.CrMasSupRenterGenderStatus == Status.Active && x.CrMasSupRenterGenderCode != "1100000001" && x.CrMasSupRenterGenderCode != "1100000002");
            var GenderAr = Gender.Select(c => new SelectListItem { Value = c.CrMasSupRenterGenderCode.ToString(), Text = c.CrMasSupRenterGenderArName }).ToList();
            var GenderEn = Gender.Select(c => new SelectListItem { Value = c.CrMasSupRenterGenderCode.ToString(), Text = c.CrMasSupRenterGenderEnName }).ToList();
            ViewData["RenterDriverGenderAr"] = GenderAr;
            ViewData["RenterDriverGenderEn"] = GenderEn;
            // Get Lessor Code
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            ViewBag.LessorID = lessorCode;
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddRenterDriver(RenterDriverVM RenterDrivermodel, IFormFile? SignatureImg, IFormFile? IDImg, IFormFile? LicenseImg)
        {

          

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;
            // View the License
            var LicenseType = _unitOfWork.CrMasSupRenterDrivingLicense.FindAll(x => x.CrMasSupRenterDrivingLicenseStatus == Status.Active);
            var LicenseTypeAr = LicenseType.Select(c => new SelectListItem { Value = c.CrMasSupRenterDrivingLicenseArName.ToString(), Text = c.CrMasSupRenterDrivingLicenseArName }).ToList();
            var LicenseTypeEn = LicenseType.Select(c => new SelectListItem { Value = c.CrMasSupRenterDrivingLicenseEnName.ToString(), Text = c.CrMasSupRenterDrivingLicenseEnName }).ToList();
            ViewData["RenterDriverLicenseTypeAr"] = LicenseTypeAr;
            ViewData["RenterDriverLicenseTypeEn"] = LicenseTypeEn;
            // View the License
            var IDType = _unitOfWork.CrMasSupRenterIdtype.FindAll(x => x.CrMasSupRenterIdtypeStatus == Status.Active);
            var IDTypeAr = IDType.Select(c => new SelectListItem { Value = c.CrMasSupRenterIdtypeArName.ToString(), Text = c.CrMasSupRenterIdtypeArName }).ToList();
            var IDTypeEn = IDType.Select(c => new SelectListItem { Value = c.CrMasSupRenterIdtypeEnName.ToString(), Text = c.CrMasSupRenterIdtypeEnName }).ToList();
            ViewData["RenterDriverTypeAr"] = IDTypeAr;
            ViewData["RenterDriverTypeEn"] = IDTypeEn;
            // View the Gender
            var Gender = _unitOfWork.CrMasSupRenterGender.FindAll(x => x.CrMasSupRenterGenderStatus == Status.Active && x.CrMasSupRenterGenderCode != "1100000001" && x.CrMasSupRenterGenderCode != "1100000001");
            var GenderAr = Gender.Select(c => new SelectListItem { Value = c.CrMasSupRenterGenderArName.ToString(), Text = c.CrMasSupRenterGenderArName }).ToList();
            var GenderEn = Gender.Select(c => new SelectListItem { Value = c.CrMasSupRenterGenderEnName.ToString(), Text = c.CrMasSupRenterGenderEnName }).ToList();

            ViewData["RenterDriverGenderAr"] = GenderAr;
            ViewData["RenterDriverGenderEn"] = GenderEn;

            // Get Lessor Code
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            RenterDrivermodel.CrCasRenterPrivateDriverInformationLessor = lessorCode;
            ViewBag.LessorID = lessorCode;
            string currentCulture = CultureInfo.CurrentCulture.Name;
            var RenterDrivers = _unitOfWork.CrCasRenterPrivateDriverInformation.FindAll(x=>x.CrCasRenterPrivateDriverInformationLessor == lessorCode);
            var existingIDriverLessor = RenterDrivers.FirstOrDefault(x => x.CrCasRenterPrivateDriverInformationId == RenterDrivermodel.CrCasRenterPrivateDriverInformationId );
            var existingIDriverArNameLessor = RenterDrivers.FirstOrDefault(x => x.CrCasRenterPrivateDriverInformationArName == RenterDrivermodel.CrCasRenterPrivateDriverInformationArName );
            var existingIDriverEnNameLessor = RenterDrivers.FirstOrDefault(x => x.CrCasRenterPrivateDriverInformationEnName == RenterDrivermodel.CrCasRenterPrivateDriverInformationEnName );

            if (existingIDriverLessor != null) ModelState.AddModelError("CrCasRenterPrivateDriverInformationId", _localizer["DriverIdIsExist"]);
            if (existingIDriverArNameLessor != null) ModelState.AddModelError("CrCasRenterPrivateDriverInformationArName", _localizer["IsExist"]);
            if (existingIDriverEnNameLessor != null) ModelState.AddModelError("CrCasRenterPrivateDriverInformationEnName", _localizer["IsExist"]);


            if (ModelState.IsValid)
            {
                string foldername = $"{"images\\Company"}\\{lessorCode}\\{"Drivers"}\\{RenterDrivermodel.CrCasRenterPrivateDriverInformationId}";
                string filePathImageSignature = null;
                string filePathImageID = null;
                string filePathImageLicense = null;
                if (IDImg != null)
                {
                    string fileNameImg = RenterDrivermodel.CrCasRenterPrivateDriverInformationEnName + "_ID_" + RenterDrivermodel.CrCasRenterPrivateDriverInformationId.ToString().Substring(RenterDrivermodel.CrCasRenterPrivateDriverInformationId.ToString().Length - 3);
                    filePathImageID = await IDImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                }
                if (LicenseImg != null)
                {
                    string fileNameImg = RenterDrivermodel.CrCasRenterPrivateDriverInformationEnName + "_License_" + RenterDrivermodel.CrCasRenterPrivateDriverInformationId.ToString().Substring(RenterDrivermodel.CrCasRenterPrivateDriverInformationId.ToString().Length - 3);
                    filePathImageLicense = await LicenseImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                }
                if (SignatureImg != null)
                {
                    string fileNameImg = RenterDrivermodel.CrCasRenterPrivateDriverInformationEnName + "_SignatureImg_" + RenterDrivermodel.CrCasRenterPrivateDriverInformationId.ToString().Substring(RenterDrivermodel.CrCasRenterPrivateDriverInformationId.ToString().Length - 3);
                    filePathImageSignature = await SignatureImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                }

                RenterDrivermodel.CrCasRenterPrivateDriverInformationIdImage = filePathImageID;
                RenterDrivermodel.CrCasRenterPrivateDriverInformationSignature = filePathImageSignature;
                RenterDrivermodel.CrCasRenterPrivateDriverInformationLicenseImage = filePathImageLicense;

                var RenterDriverModel = _mapper.Map<CrCasRenterPrivateDriverInformation>(RenterDrivermodel);
                await _renterDriver.AddRenterDriver(RenterDriverModel);

               
               
                var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207005", "2");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة سائق", "Add Driver", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "246", "20", currentUser.CrMasUserInformationLessor, "100",
                    RenterDriverModel.CrCasRenterPrivateDriverInformationId, null, null, null, null, null, null, null, null, "اضافة", "Insert", "I", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                return RedirectToAction("Index", "RenterDriver");

            }
            return View(RenterDrivermodel);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string lessor)
        {
            //sidebar Active
            ViewBag.id = "#sidebarServices";
            ViewBag.no = "4";
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("207", "2207005", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);


            var drivers = _unitOfWork.CrCasRenterPrivateDriverInformation.GetAll();
            var driver = drivers.Where(x => x.CrCasRenterPrivateDriverInformationId == id && x.CrCasRenterPrivateDriverInformationLessor == lessor).FirstOrDefault();
            if (driver == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;
            var LicenseType = _unitOfWork.CrMasSupRenterDrivingLicense.FindAll(x => x.CrMasSupRenterDrivingLicenseStatus == Status.Active);
            // View the License
            var LicenseTypeAr = LicenseType.FirstOrDefault(c => c.CrMasSupRenterDrivingLicenseCode == driver.CrCasRenterPrivateDriverInformationLicenseType).CrMasSupRenterDrivingLicenseArName;
            var LicenseTypeEn = LicenseType.FirstOrDefault(c => c.CrMasSupRenterDrivingLicenseCode == driver.CrCasRenterPrivateDriverInformationLicenseType).CrMasSupRenterDrivingLicenseEnName;
            ViewData["RenterDriverLicenseTypeAr"] = LicenseTypeAr;
            ViewData["RenterDriverLicenseTypeEn"] = LicenseTypeEn;
            // View the ID
            var IDType = _unitOfWork.CrMasSupRenterIdtype.FindAll(x => x.CrMasSupRenterIdtypeStatus == Status.Active);
            var IDTypeAr = IDType.Where(x => x.CrMasSupRenterIdtypeCode == driver.CrCasRenterPrivateDriverInformationIdtrype).FirstOrDefault().CrMasSupRenterIdtypeArName;
            var IDTypeEn = IDType.Where(x => x.CrMasSupRenterIdtypeCode == driver.CrCasRenterPrivateDriverInformationIdtrype).FirstOrDefault().CrMasSupRenterIdtypeEnName;
            ViewData["RenterDriverIDTypeAr"] = IDTypeAr;
            ViewData["RenterDriverIDTypeEn"] = IDTypeEn;
            // View the Nationality
            var Nationality = _unitOfWork.CrMasSupRenterNationality.FindAll(x => x.CrMasSupRenterNationalitiesStatus == Status.Active && x.CrMasSupRenterNationalitiesCode != "1000000001" && x.CrMasSupRenterNationalitiesCode != "1000000002");
            var NationalityAr = Nationality.Where(x => x.CrMasSupRenterNationalitiesCode == driver.CrCasRenterPrivateDriverInformationNationality).FirstOrDefault().CrMasSupRenterNationalitiesArName;
            var NationalityEn = Nationality.Where(x => x.CrMasSupRenterNationalitiesCode == driver.CrCasRenterPrivateDriverInformationNationality).FirstOrDefault().CrMasSupRenterNationalitiesEnName;
            ViewData["RenterDriverNationalityAr"] = NationalityAr;
            ViewData["RenterDriverNationalityEn"] = NationalityEn;
            // View the Gender
            var Gender = _unitOfWork.CrMasSupRenterGender.FindAll(x => x.CrMasSupRenterGenderStatus == Status.Active && x.CrMasSupRenterGenderCode != "1100000001" && x.CrMasSupRenterGenderCode != "1100000001");
            var GenderAr = Gender.Where(x => x.CrMasSupRenterGenderCode == driver.CrCasRenterPrivateDriverInformationGender).FirstOrDefault().CrMasSupRenterGenderArName;
            var GenderEn = Gender.Where(x => x.CrMasSupRenterGenderCode == driver.CrCasRenterPrivateDriverInformationGender).FirstOrDefault().CrMasSupRenterGenderEnName;
            ViewData["RenterDriverGenderAr"] = GenderAr;
            ViewData["RenterDriverGenderEn"] = GenderEn;
            // View the date
            ViewData["ExpiryIdDate"] = driver.CrCasRenterPrivateDriverInformationExpiryIdDate;
            ViewData["IssueIdDate"] = driver.CrCasRenterPrivateDriverInformationIssueIdDate;
            ViewData["LicenseDate"] = driver.CrCasRenterPrivateDriverInformationLicenseDate;
            ViewData["LicenseExpiry"] = driver.CrCasRenterPrivateDriverInformationLicenseExpiry;
            ViewData["BirthDate"] = driver.CrCasRenterPrivateDriverInformationBirthDate?.ToString("dd/MM/yyyy");
            // Get Lessor Code
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            driver.CrCasRenterPrivateDriverInformationLessor = lessorCode;
            var model = _mapper.Map<RenterDriverVM>(driver);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RenterDriverVM model, IFormFile? SignatureImg, IFormFile? IDImg, IFormFile? LicenseImg)
        {
           
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);


            var driver = _unitOfWork.CrCasRenterPrivateDriverInformation.Find(x => x.CrCasRenterPrivateDriverInformationId == model.CrCasRenterPrivateDriverInformationId&&x.CrCasRenterPrivateDriverInformationLessor==user.CrMasUserInformationLessor);
            if (driver != null)
            {
                if (ModelState.IsValid)
                {
                    string foldername = $"{"images\\Company"}\\{user.CrMasUserInformationLessor}\\{"Drivers"}\\{model.CrCasRenterPrivateDriverInformationId}";
                    string filePathImageSignature = null;
                    string filePathImageID = null;
                    string filePathImageLicense = null;
                    if (IDImg != null)
                    {
                        string fileNameImg = model.CrCasRenterPrivateDriverInformationEnName + "_ID_" + model.CrCasRenterPrivateDriverInformationId.ToString().Substring(model.CrCasRenterPrivateDriverInformationId.ToString().Length - 3);
                        filePathImageID = await IDImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrCasRenterPrivateDriverInformationIdImage = filePathImageID;
                    }
                    if (LicenseImg != null)
                    {
                        string fileNameImg = model.CrCasRenterPrivateDriverInformationEnName + "_License_" + model.CrCasRenterPrivateDriverInformationId.ToString().Substring(model.CrCasRenterPrivateDriverInformationId.ToString().Length - 3);
                        filePathImageLicense = await LicenseImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrCasRenterPrivateDriverInformationLicenseImage = filePathImageLicense;
                    }
                    if (SignatureImg != null)
                    {
                        string fileNameImg = model.CrCasRenterPrivateDriverInformationEnName + "_SignatureImg_" + model.CrCasRenterPrivateDriverInformationId.ToString().Substring(model.CrCasRenterPrivateDriverInformationId.ToString().Length - 3);
                        filePathImageSignature = await SignatureImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrCasRenterPrivateDriverInformationSignature = filePathImageSignature;
                    }
                    var RenterDriver = _mapper.Map<CrCasRenterPrivateDriverInformation>(model);
                    await _renterDriver.UpdateRenterDriver(RenterDriver);
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207005", "2");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "246", "20", currentUser.CrMasUserInformationLessor, "100",
                        RenterDriver.CrCasRenterPrivateDriverInformationId, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("Index", "RenterDriver");
                }
            }
            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;
            var LicenseType = _unitOfWork.CrMasSupRenterDrivingLicense.FindAll(x => x.CrMasSupRenterDrivingLicenseStatus == Status.Active);
            // View the License
            var LicenseTypeAr = LicenseType.FirstOrDefault(c => c.CrMasSupRenterDrivingLicenseCode == driver.CrCasRenterPrivateDriverInformationLicenseType).CrMasSupRenterDrivingLicenseArName;
            var LicenseTypeEn = LicenseType.FirstOrDefault(c => c.CrMasSupRenterDrivingLicenseCode == driver.CrCasRenterPrivateDriverInformationLicenseType).CrMasSupRenterDrivingLicenseEnName;
            ViewData["RenterDriverLicenseTypeAr"] = LicenseTypeAr;
            ViewData["RenterDriverLicenseTypeEn"] = LicenseTypeEn;
            // View the ID
            var IDType = _unitOfWork.CrMasSupRenterIdtype.FindAll(x => x.CrMasSupRenterIdtypeStatus == Status.Active);
            var IDTypeAr = IDType.Where(x => x.CrMasSupRenterIdtypeCode == driver.CrCasRenterPrivateDriverInformationIdtrype).FirstOrDefault().CrMasSupRenterIdtypeArName;
            var IDTypeEn = IDType.Where(x => x.CrMasSupRenterIdtypeCode == driver.CrCasRenterPrivateDriverInformationIdtrype).FirstOrDefault().CrMasSupRenterIdtypeEnName;
            ViewData["RenterDriverIDTypeAr"] = IDTypeAr;
            ViewData["RenterDriverIDTypeEn"] = IDTypeEn;
            // View the Nationality
            var Nationality = _unitOfWork.CrMasSupRenterNationality.FindAll(x => x.CrMasSupRenterNationalitiesStatus == Status.Active && x.CrMasSupRenterNationalitiesCode != "1000000001" && x.CrMasSupRenterNationalitiesCode != "1000000002");
            var NationalityAr = Nationality.Where(x => x.CrMasSupRenterNationalitiesCode == driver.CrCasRenterPrivateDriverInformationNationality).FirstOrDefault().CrMasSupRenterNationalitiesArName;
            var NationalityEn = Nationality.Where(x => x.CrMasSupRenterNationalitiesCode == driver.CrCasRenterPrivateDriverInformationNationality).FirstOrDefault().CrMasSupRenterNationalitiesEnName;
            ViewData["RenterDriverNationalityAr"] = NationalityAr;
            ViewData["RenterDriverNationalityEn"] = NationalityEn;
            // View the Gender
            var Gender = _unitOfWork.CrMasSupRenterGender.FindAll(x => x.CrMasSupRenterGenderStatus == Status.Active && x.CrMasSupRenterGenderCode != "1100000001" && x.CrMasSupRenterGenderCode != "1100000001");
            var GenderAr = Gender.Where(x => x.CrMasSupRenterGenderCode == driver.CrCasRenterPrivateDriverInformationGender).FirstOrDefault().CrMasSupRenterGenderArName;
            var GenderEn = Gender.Where(x => x.CrMasSupRenterGenderCode == driver.CrCasRenterPrivateDriverInformationGender).FirstOrDefault().CrMasSupRenterGenderEnName;
            ViewData["RenterDriverGenderAr"] = GenderAr;
            ViewData["RenterDriverGenderEn"] = GenderEn;


            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string lessor, string status)
        {
            string sAr = "";
            string sEn = "";
            var renterdriver = _unitOfWork.CrCasRenterPrivateDriverInformation.GetAll();
            var RenterDriver = renterdriver.Where(x => x.CrCasRenterPrivateDriverInformationLessor == lessor && x.CrCasRenterPrivateDriverInformationId == code).FirstOrDefault();

            if (RenterDriver != null)
            {
                if (await CheckUserSubValidationProcdures("2207005", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Stop";
                        RenterDriver.CrCasRenterPrivateDriverInformationStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Delete";
                        RenterDriver.CrCasRenterPrivateDriverInformationStatus = Status.Deleted;
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع";
                        sEn = "Retrieve";
                        RenterDriver.CrCasRenterPrivateDriverInformationStatus = Status.Active;
                    }

                    _unitOfWork.CrCasRenterPrivateDriverInformation.Update(RenterDriver);
                    _unitOfWork.Complete();
                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207005", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "246", "20", currentUser.CrMasUserInformationLessor, "100",
                        RenterDriver.CrCasRenterPrivateDriverInformationId, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("Index", "AccountRenterDriver");
                }
            }


            return View(RenterDriver);

        }

    }
}
