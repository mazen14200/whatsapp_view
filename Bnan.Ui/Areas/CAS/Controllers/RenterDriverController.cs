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
            var titles = await setTitle("207", "2207005", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterDriver = await _unitOfWork.CrCasRenterPrivateDriverInformation.GetAllAsync();
            var RenterDrivers = RenterDriver.Where(x => x.CrCasRenterPrivateDriverInformationStatus == "A").ToList();
            return View(RenterDrivers);
        }

        [HttpGet]
        public PartialViewResult GetRenterDriversByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var RenterDriverbyStatusAll = _unitOfWork.CrCasRenterPrivateDriverInformation.GetAll();

                    return PartialView("_DataTableRenterDriver", RenterDriverbyStatusAll);
                }
                var RenterDriverbyStatus = _unitOfWork.CrCasRenterPrivateDriverInformation.FindAll(l => l.CrCasRenterPrivateDriverInformationStatus == status ).ToList();
                return PartialView("_DataTableRenterDriver", RenterDriverbyStatus);
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
                                    c.CrMasSupRenterNationalitiesCode!= "1000000001" && c.CrMasSupRenterNationalitiesCode!= "1000000002"
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

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("207", "2207005", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var Drivers = await _unitOfWork.CrCasRenterPrivateDriverInformation.GetAllAsync();
            
            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;


            // View the License
            var LicenseType = _unitOfWork.CrMasSupRenterDrivingLicense.FindAll(x => x.CrMasSupRenterDrivingLicenseStatus == Status.Acive);
            var LicenseTypeAr = LicenseType.Select(c => new SelectListItem { Value = c.CrMasSupRenterDrivingLicenseArName.ToString(), Text = c.CrMasSupRenterDrivingLicenseArName }).ToList();
            var LicenseTypeEn = LicenseType.Select(c => new SelectListItem { Value = c.CrMasSupRenterDrivingLicenseEnName.ToString(), Text = c.CrMasSupRenterDrivingLicenseEnName }).ToList();

            ViewData["RenterDriverLicenseTypeAr"] = LicenseTypeAr;
            ViewData["RenterDriverLicenseTypeEn"] = LicenseTypeEn;


            // View the License
            var IDType = _unitOfWork.CrMasSupRenterIdtype.FindAll(x => x.CrMasSupRenterIdtypeStatus == Status.Acive);
            var IDTypeAr = IDType.Select(c => new SelectListItem { Value = c.CrMasSupRenterIdtypeArName.ToString(), Text = c.CrMasSupRenterIdtypeArName }).ToList();
            var IDTypeEn = IDType.Select(c => new SelectListItem { Value = c.CrMasSupRenterIdtypeEnName.ToString(), Text = c.CrMasSupRenterIdtypeEnName }).ToList();

            ViewData["RenterDriverTypeAr"] = IDTypeAr;
            ViewData["RenterDriverTypeEn"] = IDTypeEn;


            // View the Nationality
            //var Nationality = _unitOfWork.CrMasSupRenterNationality.FindAll(x => x.CrMasSupRenterNationalitiesStatus == Status.Acive && x.CrMasSupRenterNationalitiesCode != "1000000001" && x.CrMasSupRenterNationalitiesCode != "1000000002");
            //var NationalityAr = Nationality.Select(c => new SelectListItem { Value = c.CrMasSupRenterNationalitiesArName.ToString(), Text = c.CrMasSupRenterNationalitiesArName }).ToList();
            //var NationalityEn = Nationality.Select(c => new SelectListItem { Value = c.CrMasSupRenterNationalitiesEnName.ToString(), Text = c.CrMasSupRenterNationalitiesEnName }).ToList();

            //ViewData["RenterDriverNationalityAr"] = NationalityAr;
            //ViewData["RenterDriverNationalityEn"] = NationalityEn;


            // View the Gender
            var Gender = _unitOfWork.CrMasSupRenterGender.FindAll(x => x.CrMasSupRenterGenderStatus == Status.Acive && x.CrMasSupRenterGenderCode != "1100000001" && x.CrMasSupRenterGenderCode != "1100000002");
            var GenderAr = Gender.Select(c => new SelectListItem { Value = c.CrMasSupRenterGenderArName.ToString(), Text = c.CrMasSupRenterGenderArName }).ToList();
            var GenderEn = Gender.Select(c => new SelectListItem { Value = c.CrMasSupRenterGenderEnName.ToString(), Text = c.CrMasSupRenterGenderEnName }).ToList();

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
            string foldername = $"{"images\\Common"}";
            string filePathImageSignature = "";
            string filePathImageID = "";
            string filePathImageLicense = "";

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive );
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;

          //  var CallingCode = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive && (x.CrMasSysCallingKeysCode == RenterDrivermodel.callingKey )).FirstOrDefault().CrMasSysCallingKeysCode;
            //RenterDrivermodel.callingKey = CallingCode;

            // View the License
            var LicenseType = _unitOfWork.CrMasSupRenterDrivingLicense.FindAll(x => x.CrMasSupRenterDrivingLicenseStatus == Status.Acive);
            var LicenseTypeAr = LicenseType.Select(c => new SelectListItem { Value = c.CrMasSupRenterDrivingLicenseArName.ToString(), Text = c.CrMasSupRenterDrivingLicenseArName }).ToList();
            var LicenseTypeEn = LicenseType.Select(c => new SelectListItem { Value = c.CrMasSupRenterDrivingLicenseEnName.ToString(), Text = c.CrMasSupRenterDrivingLicenseEnName }).ToList();

            ViewData["RenterDriverLicenseTypeAr"] = LicenseTypeAr;
            ViewData["RenterDriverLicenseTypeEn"] = LicenseTypeEn;

            var LicenseCode = _unitOfWork.CrMasSupRenterDrivingLicense.FindAll(x => x.CrMasSupRenterDrivingLicenseStatus == Status.Acive && (x.CrMasSupRenterDrivingLicenseEnName==RenterDrivermodel.RenterDriverLicenseTypeEn || x.CrMasSupRenterDrivingLicenseArName == RenterDrivermodel.RenterDriverLicenseTypeAr)).FirstOrDefault().CrMasSupRenterDrivingLicenseCode;
            RenterDrivermodel.CrCasRenterPrivateDriverInformationLicenseType = LicenseCode;


            // View the License
            var IDType = _unitOfWork.CrMasSupRenterIdtype.FindAll(x => x.CrMasSupRenterIdtypeStatus == Status.Acive);
            var IDTypeAr = IDType.Select(c => new SelectListItem { Value = c.CrMasSupRenterIdtypeArName.ToString(), Text = c.CrMasSupRenterIdtypeArName }).ToList();
            var IDTypeEn = IDType.Select(c => new SelectListItem { Value = c.CrMasSupRenterIdtypeEnName.ToString(), Text = c.CrMasSupRenterIdtypeEnName }).ToList();

            ViewData["RenterDriverTypeAr"] = IDTypeAr;
            ViewData["RenterDriverTypeEn"] = IDTypeEn;

            var IDTypeCode = _unitOfWork.CrMasSupRenterIdtype.FindAll(x => x.CrMasSupRenterIdtypeStatus == Status.Acive);
            var code= IDTypeCode.FirstOrDefault(x=>x.CrMasSupRenterIdtypeEnName == RenterDrivermodel.RenterDriverTypeEn || x.CrMasSupRenterIdtypeArName == RenterDrivermodel.RenterDriverTypeAr).CrMasSupRenterIdtypeCode.ToString();
            RenterDrivermodel.CrCasRenterPrivateDriverInformationIdtrype = code;

            //// View the Nationality
            //var Nationality = _unitOfWork.CrMasSupRenterNationality.FindAll(x => x.CrMasSupRenterNationalitiesStatus == Status.Acive && x.CrMasSupRenterNationalitiesCode != "1000000001" && x.CrMasSupRenterNationalitiesCode != "1000000002");
            //var NationalityAr = Nationality.Select(c => new SelectListItem { Value = c.CrMasSupRenterNationalitiesArName.ToString(), Text = c.CrMasSupRenterNationalitiesArName }).ToList();
            //var NationalityEn = Nationality.Select(c => new SelectListItem { Value = c.CrMasSupRenterNationalitiesEnName.ToString(), Text = c.CrMasSupRenterNationalitiesEnName }).ToList();

            //ViewData["RenterDriverNationalityAr"] = NationalityAr;
            //ViewData["RenterDriverNationalityEn"] = NationalityEn;

            //var NationalityCode = _unitOfWork.CrMasSupRenterNationality.FindAll(x => x.CrMasSupRenterNationalitiesStatus == Status.Acive && (x.CrMasSupRenterNationalitiesEnName == RenterDrivermodel.RenterDriverNationalityEn || x.CrMasSupRenterNationalitiesArName == RenterDrivermodel.RenterDriverNationalityAr)).FirstOrDefault().CrMasSupRenterNationalitiesCode;
            //RenterDrivermodel.CrCasRenterPrivateDriverInformationNationality = NationalityCode;

            // View the Gender
            var Gender = _unitOfWork.CrMasSupRenterGender.FindAll(x => x.CrMasSupRenterGenderStatus == Status.Acive && x.CrMasSupRenterGenderCode != "1100000001" && x.CrMasSupRenterGenderCode != "1100000001");
           
            var GenderAr = Gender.Select(c => new SelectListItem { Value = c.CrMasSupRenterGenderArName.ToString(), Text = c.CrMasSupRenterGenderArName }).ToList();
            var GenderEn = Gender.Select(c => new SelectListItem { Value = c.CrMasSupRenterGenderEnName.ToString(), Text = c.CrMasSupRenterGenderEnName }).ToList();

            ViewData["RenterDriverGenderAr"] = GenderAr;
            ViewData["RenterDriverGenderEn"] = GenderEn;

            var GenderCode = _unitOfWork.CrMasSupRenterGender.FindAll(x => x.CrMasSupRenterGenderStatus == Status.Acive && (x.CrMasSupRenterGenderEnName == RenterDrivermodel.RenterDriverGenderEn || x.CrMasSupRenterGenderArName == RenterDrivermodel.RenterDriverGenderAr)).FirstOrDefault().CrMasSupRenterGenderCode;
            RenterDrivermodel.CrCasRenterPrivateDriverInformationGender = GenderCode;

            // Get Lessor Code
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            RenterDrivermodel.CrCasRenterPrivateDriverInformationLessor = lessorCode;
            ViewBag.LessorID = lessorCode;
            //ViewBag.lessorCode = lessorCode;           

            string currentCulture = CultureInfo.CurrentCulture.Name;
            //  if (ModelState.IsValid)
            //{
            if (RenterDrivermodel != null)
            {
                if (RenterDrivermodel.CrCasRenterPrivateDriverInformationBirthDate!= null)
                {
                    var DateNow = DateTime.Now.Date;
                    if (RenterDrivermodel.CrCasRenterPrivateDriverInformationBirthDate>= DateNow)
                    {
                        if (currentCulture != "en-US")
                        {
                            ModelState.AddModelError("BirthdayValidate", ".برجاء ادخال تاريخ مناسب");

                        }
                        else
                        {
                            ModelState.AddModelError("BirthdayValidate", "Please Enter Valid Date.");
                        }
                        return View(RenterDrivermodel);
                    }
                    else
                    {
                        if (RenterDrivermodel.CrCasRenterPrivateDriverInformationIssueIdDate != null)
                        { 
                            if(RenterDrivermodel.CrCasRenterPrivateDriverInformationIssueIdDate<= RenterDrivermodel.CrCasRenterPrivateDriverInformationBirthDate)
                            {
                                if (currentCulture != "en-US")
                                {
                                    ModelState.AddModelError("IdIssueValidate", ".برجاء ادخال تاريخ مناسب");

                                }
                                else
                                {
                                    ModelState.AddModelError("IdIssueValidate", "Please Enter Valid Date.");
                                }
                                return View(RenterDrivermodel);
                            }
                            else
                            {
                                if(RenterDrivermodel.CrCasRenterPrivateDriverInformationExpiryIdDate <= RenterDrivermodel.CrCasRenterPrivateDriverInformationIssueIdDate)
                                {
                                    if (currentCulture != "en-US")
                                    {
                                        ModelState.AddModelError("IdExpireValidate", ".برجاء ادخال تاريخ مناسب");

                                    }
                                    else
                                    {
                                        ModelState.AddModelError("IdExpireValidate", "Please Enter Valid Date.");
                                    }
                                    return View(RenterDrivermodel);
                                }

                            }
                        }
                    }
                }


                if(RenterDrivermodel.CrCasRenterPrivateDriverInformationLicenseNo != null)
                {
                    if (RenterDrivermodel.CrCasRenterPrivateDriverInformationLicenseDate != null)
                    {
                        if(RenterDrivermodel.CrCasRenterPrivateDriverInformationLicenseExpiry != null)
                        {
                            if (RenterDrivermodel.CrCasRenterPrivateDriverInformationLicenseExpiry <= RenterDrivermodel.CrCasRenterPrivateDriverInformationLicenseDate)
                            {
                                if (currentCulture != "en-US")
                                {
                                    ModelState.AddModelError("LicenseValidate", ".برجاء ادخال تاريخ مناسب");

                                }
                                else
                                {
                                    ModelState.AddModelError("LicenseValidate", "Please Enter Valid Date.");
                                }
                                return View(RenterDrivermodel);
                            }
                        }
                    }                 
                }

                var RenterDrivers = await _unitOfWork.CrCasRenterPrivateDriverInformation.GetAllAsync();
                var existingIDriver = RenterDrivers.FirstOrDefault(x =>
                    x.CrCasRenterPrivateDriverInformationId == RenterDrivermodel.CrCasRenterPrivateDriverInformationId );
                var existingIDriverLessor = RenterDrivers.FirstOrDefault(x =>
                   x.CrCasRenterPrivateDriverInformationId == RenterDrivermodel.CrCasRenterPrivateDriverInformationId && x.CrCasRenterPrivateDriverInformationLessor==RenterDrivermodel.CrCasRenterPrivateDriverInformationLessor);
                if (existingIDriver != null || existingIDriverLessor!=null)
                {
                    if (currentCulture != "en-US")
                    {
                        ModelState.AddModelError("ExistID", "هذا الحقل مسجل من قبل.");

                    }
                    else
                    {
                        ModelState.AddModelError("ExistID", "This field is Existed.");
                    }
                    return View(RenterDrivermodel);
                }
                else
                {
                    var existingNamesForLessor = RenterDrivers.Where(x => x.CrCasRenterPrivateDriverInformationId == RenterDrivermodel.CrCasRenterPrivateDriverInformationId).ToList();
                    var existingRenterDriver = existingNamesForLessor.FirstOrDefault(x =>
                        x.CrCasRenterPrivateDriverInformationEnName == RenterDrivermodel.CrCasRenterPrivateDriverInformationEnName ||
                        x.CrCasRenterPrivateDriverInformationArName == RenterDrivermodel.CrCasRenterPrivateDriverInformationArName);

                    if (existingRenterDriver != null)
                    {
                        if (existingRenterDriver.CrCasRenterPrivateDriverInformationArName != null &&
                            existingRenterDriver.CrCasRenterPrivateDriverInformationArName == RenterDrivermodel.CrCasRenterPrivateDriverInformationArName &&
                             existingRenterDriver.CrCasRenterPrivateDriverInformationEnName != RenterDrivermodel.CrCasRenterPrivateDriverInformationEnName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistAr", "هذا الحقل مسجل من قبل.");
                            }
                            else
                            {
                                ModelState.AddModelError("ExistAr", "This field is Existed.");
                            }

                        }
                        if (existingRenterDriver.CrCasRenterPrivateDriverInformationEnName != null &&
                            existingRenterDriver.CrCasRenterPrivateDriverInformationEnName == RenterDrivermodel.CrCasRenterPrivateDriverInformationEnName &&
                             existingRenterDriver.CrCasRenterPrivateDriverInformationArName != RenterDrivermodel.CrCasRenterPrivateDriverInformationArName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistEn", "هذا الحقل مسجل من قبل.");
                            }
                            else
                            {
                                ModelState.AddModelError("ExistEn", "This field is Existed.");
                            }
                        }
                        if (existingRenterDriver.CrCasRenterPrivateDriverInformationArName != null && existingRenterDriver.CrCasRenterPrivateDriverInformationEnName != null
                            && existingRenterDriver.CrCasRenterPrivateDriverInformationEnName == RenterDrivermodel.CrCasRenterPrivateDriverInformationEnName &&
                           existingRenterDriver.CrCasRenterPrivateDriverInformationArName == RenterDrivermodel.CrCasRenterPrivateDriverInformationArName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistEn", "هذا الحقل مسجل من قبل.");
                                ModelState.AddModelError("ExistAr", "هذا الحقل مسجل من قبل.");

                            }
                            else
                            {
                                ModelState.AddModelError("ExistAr", "This field is Existed.");
                                ModelState.AddModelError("ExistEn", "This field is Existed.");
                            }
                        }

                        return View(RenterDrivermodel);
                    }

                }
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
                RenterDrivermodel.CrCasRenterPrivateDriverInformationStatus = "A";
                RenterDrivermodel.CrCasRenterPrivateDriverInformationContractCount = 0;
                RenterDrivermodel.CrCasRenterPrivateDriverInformationEvaluationTotal = 0;

                RenterDrivermodel.CrCasRenterPrivateDriverInformationEvaluationValue = 0; 
                RenterDrivermodel.CrCasRenterPrivateDriverInformationTraveledDistance = 0;
                RenterDrivermodel.CrCasRenterPrivateDriverInformationDaysCount = 0;
                if (RenterDrivermodel.CrCasRenterPrivateDriverInformationMobile != null)
                {
                    RenterDrivermodel.CrCasRenterPrivateDriverInformationMobile = RenterDrivermodel.callingKey + RenterDrivermodel.CrCasRenterPrivateDriverInformationMobile;
                }

 


                var RenterDriverVMTRenterDriver = _mapper.Map<CrCasRenterPrivateDriverInformation>(RenterDrivermodel);
                await _unitOfWork.CrCasRenterPrivateDriverInformation.AddAsync(RenterDriverVMTRenterDriver);

                _unitOfWork.Complete();

                var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207005", "2");

                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة سائق", "Add Driver", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "246", "20", currentUser.CrMasUserInformationLessor, "100",
                    RenterDriverVMTRenterDriver.CrCasRenterPrivateDriverInformationId, null, null, null, null, null, null, null, null, "اضافة", "Insert", "I", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });


            }
            return RedirectToAction("Index");
            //}
            //  return View("AddRenterDriver", RenterDrivermodel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id,string lessor)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("207", "2207005", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);      
            
            
            var drivers = _unitOfWork.CrCasRenterPrivateDriverInformation.GetAll();
            var driver = drivers.Where(x => x.CrCasRenterPrivateDriverInformationId == id && x.CrCasRenterPrivateDriverInformationLessor == lessor).FirstOrDefault();
            //var driver = await _unitOfWork.CrCasRenterPrivateDriverInformation.GetByIdAsync(id);
            if (driver == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;

            var LicenseType = _unitOfWork.CrMasSupRenterDrivingLicense.FindAll(x => x.CrMasSupRenterDrivingLicenseStatus == Status.Acive);
            // View the License
            var LicenseTypeAr = LicenseType.Select(c => new SelectListItem { Value = c.CrMasSupRenterDrivingLicenseArName.ToString(), Text = c.CrMasSupRenterDrivingLicenseArName }).ToList();
            var LicenseTypeEn = LicenseType.Select(c => new SelectListItem { Value = c.CrMasSupRenterDrivingLicenseEnName.ToString(), Text = c.CrMasSupRenterDrivingLicenseEnName }).ToList();

            ViewData["RenterDriverLicenseTypeAr"] = LicenseTypeAr;
            ViewData["RenterDriverLicenseTypeEn"] = LicenseTypeEn;


            // View the License
            var LicenseDataAr = LicenseType.Where(x => x.CrMasSupRenterDrivingLicenseCode == driver.CrCasRenterPrivateDriverInformationLicenseType).FirstOrDefault().CrMasSupRenterDrivingLicenseArName;
            var LicenseDataEn = LicenseType.Where(x => x.CrMasSupRenterDrivingLicenseCode == driver.CrCasRenterPrivateDriverInformationLicenseType).FirstOrDefault().CrMasSupRenterDrivingLicenseEnName;

            driver.RenterDriverLicenseTypeAr = LicenseDataAr;
            driver.RenterDriverLicenseTypeEn = LicenseDataEn;
       

            // View the ID
            var IDType = _unitOfWork.CrMasSupRenterIdtype.FindAll(x => x.CrMasSupRenterIdtypeStatus == Status.Acive);
            var IDTypeAr = IDType.Where(x => x.CrMasSupRenterIdtypeCode == driver.CrCasRenterPrivateDriverInformationIdtrype).FirstOrDefault().CrMasSupRenterIdtypeArName;
            var IDTypeEn = IDType.Where(x => x.CrMasSupRenterIdtypeCode == driver.CrCasRenterPrivateDriverInformationIdtrype).FirstOrDefault().CrMasSupRenterIdtypeEnName;

            driver.RenterDriverTypeAr = IDTypeAr;
            driver.RenterDriverTypeEn = IDTypeEn;



            // View the Nationality
            var Nationality = _unitOfWork.CrMasSupRenterNationality.FindAll(x => x.CrMasSupRenterNationalitiesStatus == Status.Acive &&x.CrMasSupRenterNationalitiesCode!= "1000000001" && x.CrMasSupRenterNationalitiesCode!= "1000000002");
            var NationalityAr = Nationality.Where(x => x.CrMasSupRenterNationalitiesCode == driver.CrCasRenterPrivateDriverInformationNationality).FirstOrDefault().CrMasSupRenterNationalitiesArName;
            var NationalityEn = Nationality.Where(x => x.CrMasSupRenterNationalitiesCode == driver.CrCasRenterPrivateDriverInformationNationality).FirstOrDefault().CrMasSupRenterNationalitiesEnName;

            driver.RenterDriverNationalityAr = NationalityAr;
            driver.RenterDriverNationalityEn = NationalityEn;

            // View the Gender
            var Gender = _unitOfWork.CrMasSupRenterGender.FindAll(x => x.CrMasSupRenterGenderStatus == Status.Acive&&x.CrMasSupRenterGenderCode!= "1100000001"&&x.CrMasSupRenterGenderCode!= "1100000001");
            var GenderAr = Gender.Where(x => x.CrMasSupRenterGenderCode == driver.CrCasRenterPrivateDriverInformationGender).FirstOrDefault().CrMasSupRenterGenderArName;
            var GenderEn = Gender.Where(x => x.CrMasSupRenterGenderCode == driver.CrCasRenterPrivateDriverInformationGender).FirstOrDefault().CrMasSupRenterGenderEnName;


            driver.RenterDriverGenderAr = GenderAr;
            driver.RenterDriverGenderEn = GenderEn;


            // Get Lessor Code
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            driver.CrCasRenterPrivateDriverInformationLessor = lessorCode;
            //ViewBag.LessorID = lessorCode;


            var model = _mapper.Map<RenterDriverVM>(driver);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RenterDriverVM model, IFormFile? SignatureImg, IFormFile? IDImg, IFormFile? LicenseImg)
        {
            string foldername = $"{"images\\Common"}";
            string filePathImageSignature = "";
            string filePathImageID = "";
            string filePathImageLicense = "";
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);

            

            if (user != null)
            {
                if (model != null)
                {
                    
                    var LicenseCode = _unitOfWork.CrMasSupRenterDrivingLicense.FindAll(x => x.CrMasSupRenterDrivingLicenseStatus == Status.Acive && (x.CrMasSupRenterDrivingLicenseEnName == model.RenterDriverLicenseTypeEn || x.CrMasSupRenterDrivingLicenseArName == model.RenterDriverLicenseTypeAr)).FirstOrDefault().CrMasSupRenterDrivingLicenseCode;
                    model.CrCasRenterPrivateDriverInformationLicenseType = LicenseCode;

                    //var CallingCode = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive && (x.CrMasSysCallingKeysCode == model.callingKey)).FirstOrDefault().CrMasSysCallingKeysCode;
                    //model.callingKey = CallingCode;


                    var IDTypeCode = _unitOfWork.CrMasSupRenterIdtype.FindAll(x => x.CrMasSupRenterIdtypeStatus == Status.Acive);
                    var code = IDTypeCode.FirstOrDefault(x => x.CrMasSupRenterIdtypeEnName == model.RenterDriverTypeEn || x.CrMasSupRenterIdtypeArName == model.RenterDriverTypeAr).CrMasSupRenterIdtypeCode.ToString();
                    model.CrCasRenterPrivateDriverInformationIdtrype = code;


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
                    model.CrCasRenterPrivateDriverInformationEvaluationValue = int.Parse(model.CrCasRenterPrivateDriverInformationEvaluationValue.ToString());

                    var RenterDriver = _mapper.Map<CrCasRenterPrivateDriverInformation>(model);

                    _unitOfWork.CrCasRenterPrivateDriverInformation.Update(RenterDriver);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("207", "2207005", "2");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "246", "20", currentUser.CrMasUserInformationLessor, "100",
                        RenterDriver.CrCasRenterPrivateDriverInformationId, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "RenterDriver");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code,string id, string status)
        {
            string sAr = "";
            string sEn = "";
            var renterdriver =  _unitOfWork.CrCasRenterPrivateDriverInformation.GetAll();
            var RenterDriver = renterdriver.Where(x => x.CrCasRenterPrivateDriverInformationLessor == id && x.CrCasRenterPrivateDriverInformationId == code).FirstOrDefault();

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
                        RenterDriver.CrCasRenterPrivateDriverInformationStatus = Status.Acive;
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
