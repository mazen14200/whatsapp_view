using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
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


namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class LessorsKSAController : BaseController
    {

        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<LessorsKSAController> _localizer;
        private readonly ILessorImage _LessorImage;
        private readonly IOwner _Owner;
        private readonly IBeneficiary _Beneficiary;
        private readonly ILessorMembership _LessorMembership;
        private readonly ILessorMechanism _LessorMechanism;
        private readonly ICompnayContract _CompnayContract;
        private readonly IBranchInformation _BranchInformation;
        private readonly IBranchDocument _BranchDocument;
        private readonly IPostBranch _PostBranch;
        private readonly IAccountBank _AccountBank;
        private readonly ISalesPoint _SalesPoint;
        private readonly IAuthService _authService;
        private readonly IUserMainValidtion _UserMainValidtion;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public LessorsKSAController(IUnitOfWork unitOfWork,
                                    IMapper mapper,
                                    UserManager<CrMasUserInformation> userManager,
                                    IUserLoginsService userLoginsService,
                                    IUserService userService,
                                    IToastNotification toastNotification,
                                    ILessorImage lessorImage,
                                    IOwner owner, IBeneficiary beneficiary,
                                    ILessorMembership lessorMembership,
                                    ILessorMechanism lessorMechanism,
                                    ICompnayContract compnayContract,
                                    IBranchInformation branchInformation,
                                    IBranchDocument branchDocument,
                                    IPostBranch postBranch,
                                    IAccountBank accountBank,
                                    ISalesPoint salesPoint,
                                    IStringLocalizer<LessorsKSAController> localizer,
                                    IAuthService authService,
                                    IUserMainValidtion userMainValidtion, IWebHostEnvironment webHostEnvironment) : base(userManager, unitOfWork, mapper)
        {
            _userLoginsService = userLoginsService;
            _userService = userService;
            _toastNotification = toastNotification;
            _LessorImage = lessorImage;
            _Owner = owner;
            _Beneficiary = beneficiary;
            _LessorMembership = lessorMembership;
            _LessorMechanism = lessorMechanism;
            _CompnayContract = compnayContract;
            _BranchInformation = branchInformation;
            _BranchDocument = branchDocument;
            _PostBranch = postBranch;
            _AccountBank = accountBank;
            _SalesPoint = salesPoint;
            _localizer = localizer;
            _localizer = localizer;
            _authService = authService;
            _UserMainValidtion = userMainValidtion;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "0";

            // Set Title
            var titles = await setTitle("101", "1101001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            //Check User Sub Validation
            var UserValidation = await CheckUserSubValidation("1101001");
            if (UserValidation == false) return RedirectToAction("Index", "Home", new { area = "MAS" });

            var Lessors = _unitOfWork.CrMasLessorInformation.FindAll(l => l.CrMasLessorInformationCode != "0000");
            return View(Lessors);
        }

        [HttpGet]
        public PartialViewResult GetLessorsByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var LessorbyStatusAll = _unitOfWork.CrMasLessorInformation.FindAll(l => l.CrMasLessorInformationCode != "0000");
                    return PartialView("_DataTablelessors", LessorbyStatusAll);
                }
                var LessorbyStatus = _unitOfWork.CrMasLessorInformation.FindAll(l => l.CrMasLessorInformationStatus == status && l.CrMasLessorInformationCode != "0000").ToList();
                return PartialView("_DataTablelessors", LessorbyStatus);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddLessorKSA()
        {
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "0";

            //pass Classification Arabic
            var ClassificationAr = _unitOfWork.CrCasLessorClassification.GetAll();
            var ClassificationDropDownAr = ClassificationAr.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationArName }).ToList();
            ClassificationDropDownAr.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            ViewData["ClassificationDropDownAr"] = ClassificationDropDownAr;


            //pass Classification English
            var ClassificationEn = _unitOfWork.CrCasLessorClassification.GetAll();
            var ClassificationDropDownEn = ClassificationAr.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationEnName }).ToList();
            ClassificationDropDownEn.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            ViewData["ClassificationDropDownEn"] = ClassificationDropDownEn;

        
            //To Set Title;
            var titles = await setTitle("101", "1101001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "اضافة", "Create", titles[3]);

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive && x.CrMasSysCallingKeysNo == "966");
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;

            // Pass All callingKeys to the view 
            var callingKeysWhats = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive);
            var callingKeyListWhats = callingKeysWhats.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeysWhats"] = callingKeyListWhats;

            //Check User Sub Validation Procdures
            var UserValidation = await CheckUserSubValidationProcdures("1101001", Status.Insert);
            if (UserValidation == false) return RedirectToAction("Index", "Home", new { area = "MAS" });

            var Lessors = await _unitOfWork.CrMasLessorInformation.GetAllAsync();
            var LessorCode = (int.Parse(Lessors.LastOrDefault().CrMasLessorInformationCode) + 1).ToString();
            ViewBag.LessorCode = LessorCode;
            return View();
        }

        [HttpGet]
        public JsonResult GetCities()
        {
            if(CultureInfo.CurrentCulture.Name == "ar-EG")
            {
                var citiesAr = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
                var citiesArarrayAr = citiesAr.Select(c => new { text = c.CrMasSupPostCityConcatenateArName, value = c.CrMasSupPostCityCode });
                return Json(citiesArarrayAr);
            }

            var citiesEn = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
            var citiesArarrayEn = citiesEn.Select(c => new { text =  c.CrMasSupPostCityConcatenateEnName, value = c.CrMasSupPostCityCode});
            return Json(citiesArarrayEn);
        }


        [HttpPost]
        public async Task<IActionResult> AddLessorKSA(CrMasLessorInformationVM lessorVM)

        {
            var IsNameArLong = _unitOfWork.CrMasLessorInformation.FindAll(l => l.CrMasLessorInformationArLongName == lessorVM.CrMasLessorInformationArLongName).Count() > 0;
            var IsNameArShort = _unitOfWork.CrMasLessorInformation.FindAll(l => l.CrMasLessorInformationArShortName == lessorVM.CrMasLessorInformationArShortName).Count() > 0;
            var IsNameEnLong = _unitOfWork.CrMasLessorInformation.FindAll(l => l.CrMasLessorInformationEnLongName == lessorVM.CrMasLessorInformationEnLongName).Count() > 0;
            var IsNameEnShort = _unitOfWork.CrMasLessorInformation.FindAll(l => l.CrMasLessorInformationEnShortName == lessorVM.CrMasLessorInformationEnShortName).Count() > 0;
            var IsGovNo = _unitOfWork.CrCasBranchInformation.FindAll(l => l.CrCasBranchInformationGovernmentNo == lessorVM.CrMasLessorInformationGovernmentNo).Count() > 0;
            var IsTaxNo = _unitOfWork.CrCasBranchInformation.FindAll(l => l.CrCasBranchInformationTaxNo == lessorVM.CrMasLessorInformationTaxNo).Count() > 0;
            var IsValidCity = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityConcatenateArName == lessorVM.BranchPostVM.CrCasBranchPostCity || l.CrMasSupPostCityConcatenateEnName == lessorVM.BranchPostVM.CrCasBranchPostCity).FirstOrDefault();

            if (IsValidCity == null) ModelState.AddModelError("BranchPostVM.CrCasBranchPostCity", _localizer["IsNotValidCity"]);
            if (IsNameArLong) ModelState.AddModelError("CrMasLessorInformationArLongName", _localizer["IsTaken"]);
            if (IsNameArShort) ModelState.AddModelError("CrMasLessorInformationArShortName", _localizer["IsTaken"]);
            if (IsNameEnLong) ModelState.AddModelError("CrMasLessorInformationEnLongName", _localizer["IsTaken"]);
            if (IsNameEnShort) ModelState.AddModelError("CrMasLessorInformationEnShortName", _localizer["IsTaken"]);
            if (IsGovNo) ModelState.AddModelError("CrMasLessorInformationGovernmentNo", _localizer["IsTakenGov"]);
            if (IsTaxNo) ModelState.AddModelError("CrMasLessorInformationTaxNo", _localizer["IsTakenTax"]);
            if (lessorVM.BranchPostVM.CrCasBranchPostCity == "") ModelState.AddModelError("BranchPostVM.CrCasBranchPostCity", _localizer["requiredFiled"]);
            if (lessorVM.CrMasLessorInformationClassification == "") ModelState.AddModelError("CrMasLessorInformationClassification", _localizer["requiredFiled"]);

            if (ModelState.IsValid)
            {
                if (lessorVM != null)
                {
                    lessorVM.CrMasLessorInformationStatus = "A";
                    var LessorVMTlessor = _mapper.Map<CrMasLessorInformation>(lessorVM);
                    var BranchPostVMToBranchPost = _mapper.Map<CrCasBranchPost>(lessorVM.BranchPostVM);
                    // To create generate Code start from 4000 ;
                    if (LessorVMTlessor.CrMasLessorInformationCode=="1")
                    {
                        int code = 4000 + int.Parse(lessorVM.CrMasLessorInformationCode);
                        LessorVMTlessor.CrMasLessorInformationCode = code.ToString();
                    }
                    await _unitOfWork.CrMasLessorInformation.AddAsync(LessorVMTlessor);

                    await _LessorImage.AddLessorImage(LessorVMTlessor.CrMasLessorInformationCode);

                    await _Owner.AddOwner(LessorVMTlessor.CrMasLessorInformationCode);

                    await _Beneficiary.AddBeneficiary(LessorVMTlessor.CrMasLessorInformationCode);

                    await _LessorMembership.AddLessorMemberShip(LessorVMTlessor.CrMasLessorInformationCode);

                    await _LessorMechanism.AddLessorMechanism(LessorVMTlessor.CrMasLessorInformationCode);

                    await _CompnayContract.AddCompanyContract(LessorVMTlessor.CrMasLessorInformationCode);

                    await _BranchInformation.AddBranchInformationDefault(LessorVMTlessor.CrMasLessorInformationCode);

                    await _AccountBank.AddAcountBankDefalut(LessorVMTlessor.CrMasLessorInformationCode);

                    await _PostBranch.AddPostBranchDefault(LessorVMTlessor.CrMasLessorInformationCode, BranchPostVMToBranchPost, IsValidCity);

                    await _SalesPoint.AddSalesPointDefault(LessorVMTlessor.CrMasLessorInformationCode);

                    await _BranchDocument.AddBranchDocumentDefault(LessorVMTlessor.CrMasLessorInformationCode);

                    await _authService.AddUserDefault(LessorVMTlessor.CrMasLessorInformationCode);

                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("101", "1101001", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة شركه", "Add lessor", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


                    FileExtensions.CreateFolderLessor(_webHostEnvironment, LessorVMTlessor.CrMasLessorInformationCode);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "0";

            var Lessors = await _unitOfWork.CrMasLessorInformation.GetAllAsync();
            var LessorCode = (int.Parse(Lessors.LastOrDefault().CrMasLessorInformationCode) + 1).ToString();
            ViewBag.LessorCode = LessorCode;


            if (lessorVM.CrMasLessorInformationClassification != "")
            {
                //pass Classification Arabic
                var ClassificationAr = _unitOfWork.CrCasLessorClassification.GetAll();
                var ClassificationDropDownAr = ClassificationAr.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationArName, Selected = (c.CrCasLessorClassificationCode == lessorVM.CrMasLessorInformationClassification) }).ToList();
                ViewData["ClassificationDropDownAr"] = ClassificationDropDownAr;

                //pass Classification English
                var ClassificationEn = _unitOfWork.CrCasLessorClassification.GetAll();
                var ClassificationDropDownEn = ClassificationEn.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationEnName, Selected = (c.CrCasLessorClassificationCode == lessorVM.CrMasLessorInformationClassification) }).ToList();
                ViewData["ClassificationDropDownEn"] = ClassificationDropDownEn;
            }
            else
            {
                //pass Classification Arabic
                var ClassificationAr = _unitOfWork.CrCasLessorClassification.GetAll();
                var ClassificationDropDownAr = ClassificationAr.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationArName }).ToList();
                ClassificationDropDownAr.Add(new SelectListItem { Text = "", Value = "", Selected = true });
                ViewData["ClassificationDropDownAr"] = ClassificationDropDownAr;

                //pass Classification English
                var ClassificationEn = _unitOfWork.CrCasLessorClassification.GetAll();
                var ClassificationDropDownEn = ClassificationAr.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationEnName }).ToList();
                ClassificationDropDownEn.Add(new SelectListItem { Text = "", Value = "", Selected = true });
                ViewData["ClassificationDropDownEn"] = ClassificationDropDownEn;

            }

            if (lessorVM.BranchPostVM.CrCasBranchPostCity != "")
            {
                // Pass the City Post Arabic key to the view 
                var citiesAr = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
                var CityDropDownAr = citiesAr.Select(c => new SelectListItem { Value = c.CrMasSupPostCityConcatenateArName?.ToString(), Text = c.CrMasSupPostCityConcatenateArName, Selected = (c.CrMasSupPostCityCode == lessorVM.BranchPostVM.CrCasBranchPostCity) }).ToList();
                ViewData["CityDropDownAr"] = CityDropDownAr;

                // Pass the City Post English key to the view 
                var citiesEn = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
                var CityDropDownEn = citiesEn.Select(c => new SelectListItem { Value = c.CrMasSupPostCityConcatenateEnName?.ToString(), Text = c.CrMasSupPostCityConcatenateEnName, Selected = (c.CrMasSupPostCityCode == lessorVM.BranchPostVM.CrCasBranchPostCity) }).ToList();
                ViewData["CityDropDownEn"] = CityDropDownEn;
            }
            else
            {
                // Pass the City Post Arabic key to the view 
                var citiesAr = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
                var CityDropDownAr = citiesAr.Select(c => new SelectListItem { Value = c.CrMasSupPostCityConcatenateArName?.ToString(), Text = c.CrMasSupPostCityConcatenateArName }).ToList();
                CityDropDownAr.Add(new SelectListItem { Text = "", Value = "", Selected = true });
                ViewData["CityDropDownAr"] = CityDropDownAr;

                // Pass the City Post English key to the view 
                var citiesEn = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
                var CityDropDownEn = citiesEn.Select(c => new SelectListItem { Value = c.CrMasSupPostCityConcatenateEnName?.ToString(), Text = c.CrMasSupPostCityConcatenateEnName }).ToList();
                CityDropDownEn.Add(new SelectListItem { Text = "", Value = "", Selected = true });
                ViewData["CityDropDownEn"] = CityDropDownEn;
            }

            //To Set Title;
            var titles = await setTitle("101", "1101001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "اضافة", "Create", titles[3]);

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive && x.CrMasSysCallingKeysNo == "966");
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;

            // Pass All callingKeys to the view 
            var callingKeysWhats = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive);
            var callingKeyListWhats = callingKeysWhats.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeysWhats"] = callingKeyListWhats;
            return View("AddLessorKSA", lessorVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "0";

            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(id);
            var model = _mapper.Map<CrMasLessorInformationVM>(lessor);
            var BranchPost = await _unitOfWork.CrCasBranchPost.FindAsync(l => l.CrCasBranchPostLessor == lessor.CrMasLessorInformationCode && l.CrCasBranchPostBranch == "100", new[] { "CrCasBranchPostCityNavigation" });
            model.BranchPostVM = _mapper.Map<BranchPostVM>(BranchPost);

            //To Set Title
            var titles = await setTitle("101", "1101001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            //pass Classification Arabic
            var ClassificationAr = _unitOfWork.CrCasLessorClassification.GetAll();
            var ClassificationDropDownAr = ClassificationAr.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationArName }).ToList();
            ViewData["ClassificationDropDownAr"] = ClassificationDropDownAr;

            //pass Classification English
            var ClassificationEn = _unitOfWork.CrCasLessorClassification.GetAll();
            var ClassificationDropDownEn = ClassificationAr.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationEnName }).ToList();
            ViewData["ClassificationDropDownEn"] = ClassificationDropDownEn;

            // Pass the City Post Arabic key to the view 
           /* var citiesAr = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
            var CityDropDownAr = citiesAr.Select(c => new SelectListItem { Value = c.CrMasSupPostCityCode?.ToString(), Text = c.CrMasSupPostCityConcatenateArName }).ToList();
            ViewData["CityDropDownAr"] = CityDropDownAr;

            // Pass the City Post English key to the view 
            var citiesEn = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
            var CityDropDownEn = citiesEn.Select(c => new SelectListItem { Value = c.CrMasSupPostCityCode?.ToString(), Text = c.CrMasSupPostCityConcatenateEnName }).ToList();
            ViewData["CityDropDownEn"] = CityDropDownEn;*/

            // Pass the City Post Arabic key to the view 
            var citiesAr = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11").FirstOrDefault(l => l.CrMasSupPostCityCode == BranchPost.CrCasBranchPostCity);
            ViewBag.CityDropDownAr = citiesAr.CrMasSupPostCityConcatenateArName;

            // Pass the City Post English key to the view 
            var citiesEn = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11").FirstOrDefault(l => l.CrMasSupPostCityCode == BranchPost.CrCasBranchPostCity);
            ViewBag.CityDropDownEn = citiesEn.CrMasSupPostCityConcatenateEnName;
            //Get City code
            ViewBag.cityCode = citiesAr.CrMasSupPostCityCode;

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive && x.CrMasSysCallingKeysNo == "966");
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;

            // Pass All callingKeys to the view 
            var callingKeysWhats = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive);
            var callingKeyListWhats = callingKeysWhats.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeysWhats"] = callingKeyListWhats;

            if (lessor == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Users");
            }


            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CrMasLessorInformationVM CrMasLessorInformationVM)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            var lessor = _mapper.Map<CrMasLessorInformation>(CrMasLessorInformationVM);
            var BranchPost = _mapper.Map<CrCasBranchPost>(CrMasLessorInformationVM.BranchPostVM);
            BranchPost.CrCasBranchPostLessor = lessor.CrMasLessorInformationCode;
            BranchPost.CrCasBranchPostBranch = "100";

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    if (lessor != null)
                    {
                        _unitOfWork.CrMasLessorInformation.Update(lessor);
                        _unitOfWork.CrCasBranchPost.Update(BranchPost);
                        _unitOfWork.Complete();

                        // SaveTracing
                        var (mainTask, subTask, system, currentUser) = await SetTrace("101", "1101001", "1");
                        await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                        subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                        subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                        _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                        return RedirectToAction("Index");
                    }

                }
            }

            //To Set Title;
            var titles = await setTitle("101", "1101001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "اضافة", "Create", titles[3]);

            // Pass the KSA callingKeys to the view 
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive && x.CrMasSysCallingKeysNo == "966");
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList;

            // Pass All callingKeys to the view 
            var callingKeysWhats = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Acive);
            var callingKeyListWhats = callingKeysWhats.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeysWhats"] = callingKeyListWhats;

            if (CrMasLessorInformationVM.CrMasLessorInformationClassification != "")
            {
                //pass Classification Arabic
                var ClassificationAr = _unitOfWork.CrCasLessorClassification.GetAll();
                var ClassificationDropDownAr = ClassificationAr.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationArName, Selected = (c.CrCasLessorClassificationCode == CrMasLessorInformationVM.CrMasLessorInformationClassification) }).ToList();
                ViewData["ClassificationDropDownAr"] = ClassificationDropDownAr;

                //pass Classification English
                var ClassificationEn = _unitOfWork.CrCasLessorClassification.GetAll();
                var ClassificationDropDownEn = ClassificationEn.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationEnName, Selected = (c.CrCasLessorClassificationCode == CrMasLessorInformationVM.CrMasLessorInformationClassification) }).ToList();
                ViewData["ClassificationDropDownEn"] = ClassificationDropDownEn;
            }
            else
            {
                //pass Classification Arabic
                var ClassificationAr = _unitOfWork.CrCasLessorClassification.GetAll();
                var ClassificationDropDownAr = ClassificationAr.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationArName }).ToList();
                ClassificationDropDownAr.Add(new SelectListItem { Text = "", Value = "", Selected = true });
                ViewData["ClassificationDropDownAr"] = ClassificationDropDownAr;

                //pass Classification English
                var ClassificationEn = _unitOfWork.CrCasLessorClassification.GetAll();
                var ClassificationDropDownEn = ClassificationAr.Select(c => new SelectListItem { Value = c.CrCasLessorClassificationCode?.ToString(), Text = c.CrCasLessorClassificationEnName }).ToList();
                ClassificationDropDownEn.Add(new SelectListItem { Text = "", Value = "", Selected = true });
                ViewData["ClassificationDropDownEn"] = ClassificationDropDownEn;

            }

            if (CrMasLessorInformationVM.BranchPostVM.CrCasBranchPostCity != "")
            {
                // Pass the City Post Arabic key to the view 
                var citiesAr = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
                var CityDropDownAr = citiesAr.Select(c => new SelectListItem { Value = c.CrMasSupPostCityConcatenateArName?.ToString(), Text = c.CrMasSupPostCityConcatenateArName, Selected = (c.CrMasSupPostCityCode == CrMasLessorInformationVM.BranchPostVM.CrCasBranchPostCity) }).ToList();
                ViewData["CityDropDownAr"] = CityDropDownAr;

                // Pass the City Post English key to the view 
                var citiesEn = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
                var CityDropDownEn = citiesEn.Select(c => new SelectListItem { Value = c.CrMasSupPostCityConcatenateEnName?.ToString(), Text = c.CrMasSupPostCityConcatenateEnName, Selected = (c.CrMasSupPostCityCode == CrMasLessorInformationVM.BranchPostVM.CrCasBranchPostCity) }).ToList();
                ViewData["CityDropDownEn"] = CityDropDownEn;
            }
            else
            {
                // Pass the City Post Arabic key to the view 
                var citiesAr = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
                var CityDropDownAr = citiesAr.Select(c => new SelectListItem { Value = c.CrMasSupPostCityConcatenateArName?.ToString(), Text = c.CrMasSupPostCityConcatenateArName }).ToList();
                CityDropDownAr.Add(new SelectListItem { Text = "", Value = "", Selected = true });
                ViewData["CityDropDownAr"] = CityDropDownAr;

                // Pass the City Post English key to the view 
                var citiesEn = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityRegionsCode != "10" && l.CrMasSupPostCityRegionsCode != "11");
                var CityDropDownEn = citiesEn.Select(c => new SelectListItem { Value = c.CrMasSupPostCityConcatenateEnName?.ToString(), Text = c.CrMasSupPostCityConcatenateEnName }).ToList();
                CityDropDownEn.Add(new SelectListItem { Text = "", Value = "", Selected = true });
                ViewData["CityDropDownEn"] = CityDropDownEn;
            }
            return View("Edit", CrMasLessorInformationVM);
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(code);
            if (lessor != null)
            {
                if (await CheckUserSubValidationProcdures("2201001", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف شركه";
                        sEn = "Hold Lessor";
                        lessor.CrMasLessorInformationStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف شركه";
                        sEn = "Remove Lessor";
                        lessor.CrMasLessorInformationStatus = Status.Deleted;
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع شركه";
                        sEn = "Retrive Lessor";
                        lessor.CrMasLessorInformationStatus = Status.Acive;

                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("101", "1101001", "1");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    return RedirectToAction("Index", "LessorsKSA");
                }
            }

            return View(lessor);

        }


    }
}