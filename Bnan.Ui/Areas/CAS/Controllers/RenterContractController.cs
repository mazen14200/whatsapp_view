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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Bnan.Ui.Areas.CAS.Controllers
{


    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class RenterContractController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IRenterContract _RenterContract;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<RenterContractController> _localizer;


        public RenterContractController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IRenterContract RenterContract,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<RenterContractController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _RenterContract = RenterContract;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "1";
            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203002", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            var titles = await setTitle("203", "2203002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterContractAll = _unitOfWork.CrCasRenterLessor.GetAll(new[] { "CrCasRenterLessorStatisticsNationalitiesNavigation", "CrCasRenterLessorNavigation", "CrCasRenterLessorStatisticsJobsNavigation", "CrCasRenterLessorStatisticsCityNavigation", "CrCasRenterLessorStatisticsRegionsNavigation", "CrCasRenterLessorIdtrypeNavigation", "CrCasRenterLessorSectorNavigation", "CrCasRenterLessorCodeNavigation", "CrCasRenterLessorStatisticsGenderNavigation", "CrCasRenterLessorMembershipNavigation" }).Where(x=>x.CrCasRenterLessorContractCount > 0);
            //var RenterContractAll = await _unitOfWork.CrCasRenterLessor.GetAllAsync();
            //var RenterContractAllA = RenterContractAll.Where(x => x.CrCasRenterLessorStatus == "A").ToList();
            //var RenterContractAllA = RenterContractAll.ToList();
            var RenterContractAllA = _unitOfWork.CrMasRenterPost.FindAll(x => x.CrMasRenterPostArShortConcatenate != null).ToList();

            var rates = _unitOfWork.CrMasSysEvaluation.FindAll(x => x.CrMasSysEvaluationsClassification == "1" && x.CrMasSysEvaluationsStatus == "A").ToList();

            ViewData["Rates"] = rates;
            ViewData["Adress"] = RenterContractAllA;


            return View(RenterContractAll);
        }

        [HttpGet]
        public async Task<IActionResult> RenterContract_Table()
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "1";
            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203002", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            var titles = await setTitle("203", "2203002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            //var RenterContractAll = await _unitOfWork.CrCasRenterLessor.GetAllAsync();
            //var RenterContractAllA = RenterContractAll.Where(x => x.CrCasRenterLessorStatus == "A").ToList();
            //var RenterContractAllA = RenterContractAll.ToList();
            var RenterContractAllA = _unitOfWork.CrMasRenterPost.FindAll(x => x.CrMasRenterPostArShortConcatenate != null).ToList();

            var rates = _unitOfWork.CrMasSysEvaluation.FindAll(x => x.CrMasSysEvaluationsClassification == "1" && x.CrMasSysEvaluationsStatus == "A").ToList();

            ViewData["Rates"] = rates;
            ViewData["Adress"] = RenterContractAllA;

            var RenterContract_Basic_All = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic4", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasicSectorNavigation" }).ToList();

            ViewData["Data_Table"] = RenterContract_Basic_All;
            //ViewBag.v1 = "mm";
            //var c1 = 1.5;
            //var c1_1 = 1.3;
            //var c2 = 1.5;
            //var c2_1 = 1.3;
            //double num = 45645.7;
            
            //if (num > 1000)
            //{
            //    c1 = num / 1000;
            //    c1_1 = num % 1000;
            //    if (c1 > 1000)
            //    {
            //        c2 = c1 / 1000;
            //        c2_1 = c1 % 1000;
            //    }
            //    else
            //    {
            //        string f = $"{c1},{c1_1}";
            //        ViewBag.v1 = f;
            //    }
            //}
            

            return View();
        }

        [HttpGet]
        public PartialViewResult GetRenterContractByStatus(string status)
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "1";
            if (!string.IsNullOrEmpty(status))
            {
                var RenterContractAllA = _unitOfWork.CrMasRenterPost.FindAll(x => x.CrMasRenterPostArShortConcatenate != null).ToList();
                List<List<string>> ConcateAdress_short = new List<List<string>>();
                foreach (var item in RenterContractAllA)
                {
                    var ar = item.CrMasRenterPostArShortConcatenate.ToString();
                    string[] values = ar.Split('-');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }
                    var en = item.CrMasRenterPostEnShortConcatenate.ToString();
                    string[] values2 = en.Split('-');
                    for (int i = 0; i < values2.Length; i++)
                    {
                        values2[i] = values2[i].Trim();
                    }
                    if (values.Length > 1 && values2.Length > 1)
                    {
                        if (values2[0].Length < 4 && values2.Length > 2)
                        {
                            ConcateAdress_short.Add(new List<string> { item.CrMasRenterPostCode, values[0] + " - " + values[1], values2[0] + "-" + values2[1] + " - " + values2[2] });
                        }
                        else
                        {
                            ConcateAdress_short.Add(new List<string> { item.CrMasRenterPostCode, values[0] + " - " + values[1], values2[0] + " - " + values2[1] });

                        }
                    }


                }
                ViewBag.ConcateAdress = ConcateAdress_short;

                ViewData["Adress"] = RenterContractAllA;

                var rates = _unitOfWork.CrMasSysEvaluation.FindAll(x => x.CrMasSysEvaluationsClassification == "1" && x.CrMasSysEvaluationsStatus == "A").ToList();

                ViewData["Rates"] = rates;
                //var x = ConcateAdress_short.Find( x => x[0] == "1011534174");
                //ViewBag.mazen = x;

                if (status == Status.All)
                {


                    var RenterContractbyStatusAll = _unitOfWork.CrCasRenterLessor.FindAll(l => l.CrCasRenterLessorStatus == Status.Hold || l.CrCasRenterLessorStatus == Status.Active || l.CrCasRenterLessorStatus == Status.Rented, new[] { "CrCasRenterLessorStatisticsNationalitiesNavigation", "CrCasRenterLessorNavigation", "CrCasRenterLessorStatisticsJobsNavigation", "CrCasRenterLessorStatisticsCityNavigation", "CrCasRenterLessorStatisticsRegionsNavigation", "CrCasRenterLessorIdtrypeNavigation", "CrCasRenterLessorSectorNavigation", "CrCasRenterLessorCodeNavigation", "CrCasRenterLessorStatisticsGenderNavigation", "CrCasRenterLessorMembershipNavigation" }).Where(x => x.CrCasRenterLessorContractCount > 0).ToList();
                    return PartialView("_DataTableRenterContract", RenterContractbyStatusAll);
                }
                var RenterContractbyStatus = _unitOfWork.CrCasRenterLessor.FindAll(l => l.CrCasRenterLessorStatus == status, new[] { "CrCasRenterLessorStatisticsNationalitiesNavigation", "CrCasRenterLessorNavigation", "CrCasRenterLessorStatisticsJobsNavigation", "CrCasRenterLessorStatisticsCityNavigation", "CrCasRenterLessorStatisticsRegionsNavigation", "CrCasRenterLessorIdtrypeNavigation", "CrCasRenterLessorSectorNavigation", "CrCasRenterLessorCodeNavigation", "CrCasRenterLessorStatisticsGenderNavigation", "CrCasRenterLessorMembershipNavigation" }).Where(x => x.CrCasRenterLessorContractCount > 0).ToList();
                return PartialView("_DataTableRenterContract", RenterContractbyStatus);
            }
            return PartialView();
        }


        //[HttpGet]
        //public async Task<IActionResult> AddRenterContract()
        //{

        //    // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
        //    var titles = await setTitle("203", "2203002", "2");
        //    await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

        //    var RenterContractCode = "";
        //    var RenterContracts = await _unitOfWork.CrCasRenterLessor.GetAllAsync();
        //    if (RenterContracts.Count() != 0)
        //    {
        //        RenterContractCode = (BigInteger.Parse(RenterContracts.LastOrDefault().CrCasRenterLessorId) + 1).ToString();
        //    }
        //    else
        //    {
        //        RenterContractCode = "0";
        //    }
        //    ViewBag.RenterContractCode = RenterContractCode;
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddRenterContract(CasRenterContractVM RenterContracts)
        //{
        //    string currentCulture = CultureInfo.CurrentCulture.Name;

        //    if (ModelState.IsValid)
        //    {
        //        if (RenterContracts != null)
        //        {
        //            var CasRenterContractVMT = _mapper.Map<CrCasRenterLessor>(RenterContracts);
        //            var All_RenterContracts = await _unitOfWork.CrCasRenterLessor.GetAllAsync();
        //            var existingRenterContract_En = All_RenterContracts.FirstOrDefault(x =>
        //                x.CrCasRenterLessorNavigation.CrMasRenterInformationEnName == CasRenterContractVMT.CrCasRenterLessorNavigation.CrMasRenterInformationEnName);
        //            var existingRenterContract_Ar = All_RenterContracts.FirstOrDefault(x =>
        //                x.CrCasRenterLessorNavigation.CrMasRenterInformationArName == CasRenterContractVMT.CrCasRenterLessorNavigation.CrMasRenterInformationArName);

        //            // Generate code for the second time
        //            var RenterContractCode = (BigInteger.Parse(All_RenterContracts.LastOrDefault().CrCasRenterLessorId) + 1).ToString();
        //            RenterContracts.CrCasRenterLessorId = RenterContractCode;
        //            ViewBag.RenterContractCode = RenterContractCode;
        //            if (CasRenterContractVMT.CrCasRenterLessorNavigation.CrMasRenterInformationArName != null && CasRenterContractVMT.CrCasRenterLessorNavigation.CrMasRenterInformationEnName != null)
        //            {
        //                if (existingRenterContract_Ar != null && existingRenterContract_En != null)
        //                {
        //                    ModelState.AddModelError("ExistAr", _localizer["Existing"]);
        //                    ModelState.AddModelError("ExistEn", _localizer["Existing"]);
        //                    return View(RenterContracts);
        //                }
        //                else if (existingRenterContract_En != null)
        //                {
        //                    ModelState.AddModelError("ExistEn", _localizer["Existing"]);
        //                    return View(RenterContracts);
        //                }
        //                else if (existingRenterContract_Ar != null)
        //                {
        //                    ModelState.AddModelError("ExistAr", _localizer["Existing"]);
        //                    return View(RenterContracts);
        //                }
        //            }

        //            CasRenterContractVMT.CrCasRenterLessorStatus = "A";
        //            await _unitOfWork.CrCasRenterLessor.AddAsync(CasRenterContractVMT);

        //            _unitOfWork.Complete();

        //            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203002", "2");
        //            var RecordAr = CasRenterContractVMT.CrCasRenterLessorNavigation.CrMasRenterInformationArName;
        //            var RecordEn = CasRenterContractVMT.CrCasRenterLessorNavigation.CrMasRenterInformationEnName;
        //            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
        //            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
        //            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

        //            _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View("AddRenterContract", RenterContracts);
        //}



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "1";

            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("203", "2203002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == id, new[] { "CrCasRenterLessorStatisticsNationalitiesNavigation", "CrCasRenterLessorNavigation", "CrCasRenterLessorStatisticsJobsNavigation", "CrCasRenterLessorStatisticsCityNavigation", "CrCasRenterLessorStatisticsRegionsNavigation", "CrCasRenterLessorIdtrypeNavigation", "CrCasRenterLessorSectorNavigation", "CrCasRenterLessorCodeNavigation", "CrCasRenterLessorStatisticsGenderNavigation", "CrCasRenterLessorMembershipNavigation" });
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var Mechanism = _unitOfWork.CrMasSysEvaluation.FindAll(x => x.CrMasSysEvaluationsClassification == "1" && x.CrMasSysEvaluationsStatus == "A").ToList();
            ViewData["Mechanism"] = Mechanism;

            var workPlace = _unitOfWork.CrMasSupRenterEmployer.Find(x => x.CrMasSupRenterEmployerCode == contract.CrCasRenterLessorNavigation.CrMasRenterInformationEmployer);
            ViewBag.workPlaceAr = workPlace?.CrMasSupRenterEmployerArName;
            ViewBag.workPlaceEn = workPlace?.CrMasSupRenterEmployerEnName;

            var Bank = _unitOfWork.CrMasSupAccountBanks.Find(x => x.CrMasSupAccountBankCode == contract.CrCasRenterLessorNavigation.CrMasRenterInformationBank);
            ViewBag.BankAr = Bank?.CrMasSupAccountBankArName;
            ViewBag.BankEn = Bank?.CrMasSupAccountBankEnName;

            var DrivingType = _unitOfWork.CrElmLicense.Find(x => x.CrElmLicenseNo == contract.CrCasRenterLessorNavigation.CrMasRenterInformationDrivingLicenseNo);
            ViewBag.DrivingTypeAr = DrivingType.CrElmLicenseArName;
            ViewBag.DrivingTypeEn = DrivingType.CrElmLicenseEnName;

            var RenterContractAllA = _unitOfWork.CrMasRenterPost.FindAll(x => x.CrMasRenterPostArShortConcatenate != null).ToList();
            ViewData["Adress"] = RenterContractAllA;

            var RenterContract_address = _unitOfWork.CrMasRenterPost.Find(x => x.CrMasRenterPostArShortConcatenate != null);
            ViewBag.AddressAr = RenterContract_address?.CrMasRenterPostArShortConcatenate;
            ViewBag.AddressEn = RenterContract_address?.CrMasRenterPostEnShortConcatenate;

            int countRenterContracts = 0;
            //countRenterContracts = _RenterContract.GetOneRenterContractCount(id);
            ViewBag.RenterContracts_Count = countRenterContracts;
            var model = _mapper.Map<CasRenterContractVM>(contract);

            var RenterContract_Basic_All = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic4", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasicSectorNavigation" }).Where(x=>x.CrCasRenterContractBasicRenterId==model?.CrCasRenterLessorId).ToList();

            ViewData["Data_Table"] = RenterContract_Basic_All;

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CasRenterContractVM model)
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "1";

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);

            if (user != null)
            {
                if (model != null)
                {

                    var contract = _mapper.Map<CrCasRenterLessor>(model);

                    var singlExist = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == contract.CrCasRenterLessorId);
                    if (singlExist != null)
                    {
                        singlExist.CrCasRenterLessorDealingMechanism = contract.CrCasRenterLessorDealingMechanism;
                        singlExist.CrCasRenterLessorReasons = contract.CrCasRenterLessorReasons;
                        _unitOfWork.CrCasRenterLessor.Update(singlExist);
                    }
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203002", "2");
                    var RecordAr = contract.CrCasRenterLessorNavigation.CrMasRenterInformationArName;
                    var RecordEn = contract.CrCasRenterLessorNavigation.CrMasRenterInformationEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "RenterContract");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {

            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrCasRenterLessor.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrCasRenterLessorStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    int CountRenterContracts = 0;
                    //CountRenterContracts = _RenterContract.GetOneRenterContractCount(code);
                    if (CountRenterContracts == 0)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        Contract.CrCasRenterLessorStatus = Status.Deleted;
                    }
                    else
                    {
                        return View(Contract);
                    }

                }
                else if (status == "Reactivate")
                {
                    sAr = "استرجاع";
                    sEn = "Retrive";
                    Contract.CrCasRenterLessorStatus = Status.Active;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203002", "2");
                var RecordAr = Contract.CrCasRenterLessorNavigation.CrMasRenterInformationArName;
                var RecordEn = Contract.CrCasRenterLessorNavigation.CrMasRenterInformationEnName;
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "RenterContract");
            }


            return View(Contract);

        }

        [HttpPost]
        public async Task<IActionResult> CheckChangedField(string Exist_lang, string dataField)
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "1";
            var All_RenterContracts = await _unitOfWork.CrCasRenterLessor.GetAllAsync();

            if (dataField != null && All_RenterContracts != null)
            {
                if (Exist_lang == "ExistAr")
                {
                    var existingRenterContract_Ar = All_RenterContracts.FirstOrDefault(x =>
                        x.CrCasRenterLessorNavigation.CrMasRenterInformationArName == dataField);
                    if (existingRenterContract_Ar != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }
                else if (Exist_lang == "ExistEn")
                {
                    var existingRenterContract_En = All_RenterContracts.FirstOrDefault(x =>
                        x.CrCasRenterLessorNavigation.CrMasRenterInformationEnName == dataField);
                    if (existingRenterContract_En != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }

            }
            return View();
        }
    }
}
