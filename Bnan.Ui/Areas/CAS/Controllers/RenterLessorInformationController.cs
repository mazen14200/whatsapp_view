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
using System.Globalization;
using Bnan.Ui.ViewModels.BS;
using System.Diagnostics.Contracts;
using System.Numerics;



namespace Bnan.Ui.Areas.CAS.Controllers
{

    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class RenterLessorInformationController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IRenterLessorInformation _RenterLessorInformation;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<RenterLessorInformationController> _localizer;


        public RenterLessorInformationController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IRenterLessorInformation RenterLessorInformation,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<RenterLessorInformationController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _RenterLessorInformation = RenterLessorInformation;
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
            ViewBag.no = "0";

            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203001", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            var titles = await setTitle("203", "2203001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterLessorInformationAll = _unitOfWork.CrCasRenterLessor.GetAll(new[] { "CrCasRenterLessorStatisticsNationalitiesNavigation", "CrCasRenterLessorNavigation", "CrCasRenterLessorStatisticsJobsNavigation", "CrCasRenterLessorStatisticsCityNavigation" , "CrCasRenterLessorStatisticsRegionsNavigation", "CrCasRenterLessorIdtrypeNavigation" , "CrCasRenterLessorSectorNavigation", "CrCasRenterLessorCodeNavigation", "CrCasRenterLessorStatisticsGenderNavigation", "CrCasRenterLessorMembershipNavigation" });
            //var RenterLessorInformationAll = await _unitOfWork.CrCasRenterLessor.GetAllAsync();
            //var RenterLessorInformationAllA = RenterLessorInformationAll.Where(x => x.CrCasRenterLessorStatus == "A").ToList();
            //var RenterLessorInformationAllA = RenterLessorInformationAll.ToList();
            var RenterLessorInformationAllA = _unitOfWork.CrMasRenterPost.FindAll(x => x.CrMasRenterPostArShortConcatenate != null).ToList();
            List<List<string>> ConcateAdress_short = new List<List<string>>();
            foreach (var item in RenterLessorInformationAllA)
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
            var rates = _unitOfWork.CrMasSysEvaluation.FindAll(x=>x.CrMasSysEvaluationsClassification=="1" && x.CrMasSysEvaluationsStatus=="A").ToList();

            ViewData["Rates"] = rates;
            ViewData["Adress"] = RenterLessorInformationAllA;

            ViewBag.ConcateAdress = ConcateAdress_short;
            //var x = ConcateAdress_short.Find(x => x[0] == "1011534174");
            //ViewBag.mazen = x;
            //var CarsInfo_count_all = _RenterLessorInformation.GetAllRenterLessorInformationsCount();

            return View(RenterLessorInformationAll);
        }

        [HttpGet]
        public PartialViewResult GetRenterLessorInformationByStatus(string status)
        {

            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "0";
            if (!string.IsNullOrEmpty(status))
            {
                var RenterLessorInformationAllA = _unitOfWork.CrMasRenterPost.FindAll(x => x.CrMasRenterPostArShortConcatenate != null).ToList();
                List<List<string>> ConcateAdress_short = new List<List<string>>();
                foreach (var item in RenterLessorInformationAllA)
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

                ViewData["Adress"] = RenterLessorInformationAllA;

                var rates = _unitOfWork.CrMasSysEvaluation.FindAll(x => x.CrMasSysEvaluationsClassification == "1" && x.CrMasSysEvaluationsStatus == "A").ToList();

                ViewData["Rates"] = rates;
                //var x = ConcateAdress_short.Find( x => x[0] == "1011534174");
                //ViewBag.mazen = x;

                if (status == Status.All)
                {


                    var RenterLessorInformationbyStatusAll = _unitOfWork.CrCasRenterLessor.FindAll(l => l.CrCasRenterLessorStatus == Status.Hold || l.CrCasRenterLessorStatus == Status.Active || l.CrCasRenterLessorStatus == Status.Rented , new[] { "CrCasRenterLessorStatisticsNationalitiesNavigation", "CrCasRenterLessorNavigation", "CrCasRenterLessorStatisticsJobsNavigation", "CrCasRenterLessorStatisticsCityNavigation", "CrCasRenterLessorStatisticsRegionsNavigation", "CrCasRenterLessorIdtrypeNavigation" , "CrCasRenterLessorSectorNavigation" , "CrCasRenterLessorCodeNavigation" , "CrCasRenterLessorStatisticsGenderNavigation" , "CrCasRenterLessorMembershipNavigation" }).ToList();
                    return PartialView("_DataTableRenterLessorInformation", RenterLessorInformationbyStatusAll);
                }
                var RenterLessorInformationbyStatus = _unitOfWork.CrCasRenterLessor.FindAll(l => l.CrCasRenterLessorStatus == status , new[] { "CrCasRenterLessorStatisticsNationalitiesNavigation", "CrCasRenterLessorNavigation", "CrCasRenterLessorStatisticsJobsNavigation", "CrCasRenterLessorStatisticsCityNavigation", "CrCasRenterLessorStatisticsRegionsNavigation", "CrCasRenterLessorIdtrypeNavigation" , "CrCasRenterLessorSectorNavigation", "CrCasRenterLessorCodeNavigation", "CrCasRenterLessorStatisticsGenderNavigation", "CrCasRenterLessorMembershipNavigation" }).ToList();
                return PartialView("_DataTableRenterLessorInformation", RenterLessorInformationbyStatus);
            }
            return PartialView();
        }


        //[HttpGet]
        //public async Task<IActionResult> AddRenterLessorInformation()
        //{

        //    // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
        //    var titles = await setTitle("203", "2203001", "2");
        //    await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

        //    var RenterLessorInformationCode = "";
        //    var RenterLessorInformations = await _unitOfWork.CrCasRenterLessor.GetAllAsync();
        //    if (RenterLessorInformations.Count() != 0)
        //    {
        //        RenterLessorInformationCode = (BigInteger.Parse(RenterLessorInformations.LastOrDefault().CrCasRenterLessorId) + 1).ToString();
        //    }
        //    else
        //    {
        //        RenterLessorInformationCode = "0";
        //    }
        //    ViewBag.RenterLessorInformationCode = RenterLessorInformationCode;
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddRenterLessorInformation(RenterLessorInformationVM RenterLessorInformations)
        //{
        //    string currentCulture = CultureInfo.CurrentCulture.Name;

        //    if (ModelState.IsValid)
        //    {
        //        if (RenterLessorInformations != null)
        //        {
        //            var RenterLessorInformationVMT = _mapper.Map<CrCasRenterLessor>(RenterLessorInformations);
        //            var All_RenterLessorInformations = await _unitOfWork.CrCasRenterLessor.GetAllAsync();
        //            var existingRenterLessorInformation_En = All_RenterLessorInformations.FirstOrDefault(x =>
        //                x.CrCasRenterLessorNavigation.CrMasRenterInformationEnName == RenterLessorInformationVMT.CrCasRenterLessorNavigation.CrMasRenterInformationEnName);
        //            var existingRenterLessorInformation_Ar = All_RenterLessorInformations.FirstOrDefault(x =>
        //                x.CrCasRenterLessorNavigation.CrMasRenterInformationArName == RenterLessorInformationVMT.CrCasRenterLessorNavigation.CrMasRenterInformationArName);

        //            // Generate code for the second time
        //            var RenterLessorInformationCode = (BigInteger.Parse(All_RenterLessorInformations.LastOrDefault().CrCasRenterLessorId) + 1).ToString();
        //            RenterLessorInformations.CrCasRenterLessorId = RenterLessorInformationCode;
        //            ViewBag.RenterLessorInformationCode = RenterLessorInformationCode;
        //            if (RenterLessorInformationVMT.CrCasRenterLessorNavigation.CrMasRenterInformationArName != null && RenterLessorInformationVMT.CrCasRenterLessorNavigation.CrMasRenterInformationEnName != null)
        //            {
        //                if (existingRenterLessorInformation_Ar != null && existingRenterLessorInformation_En != null)
        //                {
        //                    ModelState.AddModelError("ExistAr", _localizer["Existing"]);
        //                    ModelState.AddModelError("ExistEn", _localizer["Existing"]);
        //                    return View(RenterLessorInformations);
        //                }
        //                else if (existingRenterLessorInformation_En != null)
        //                {
        //                    ModelState.AddModelError("ExistEn", _localizer["Existing"]);
        //                    return View(RenterLessorInformations);
        //                }
        //                else if (existingRenterLessorInformation_Ar != null)
        //                {
        //                    ModelState.AddModelError("ExistAr", _localizer["Existing"]);
        //                    return View(RenterLessorInformations);
        //                }
        //            }

        //            RenterLessorInformationVMT.CrCasRenterLessorStatus = "A";
        //            await _unitOfWork.CrCasRenterLessor.AddAsync(RenterLessorInformationVMT);

        //            _unitOfWork.Complete();

        //            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203001", "2");
        //            var RecordAr = RenterLessorInformationVMT.CrCasRenterLessorNavigation.CrMasRenterInformationArName;
        //            var RecordEn = RenterLessorInformationVMT.CrCasRenterLessorNavigation.CrMasRenterInformationEnName;
        //            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
        //            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
        //            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

        //            _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View("AddRenterLessorInformation", RenterLessorInformations);
        //}



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "0";

            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("203", "2203001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract =  _unitOfWork.CrCasRenterLessor.Find(x=>x.CrCasRenterLessorId == id, new[] { "CrCasRenterLessorStatisticsNationalitiesNavigation", "CrCasRenterLessorNavigation", "CrCasRenterLessorStatisticsJobsNavigation", "CrCasRenterLessorStatisticsCityNavigation", "CrCasRenterLessorStatisticsRegionsNavigation", "CrCasRenterLessorIdtrypeNavigation" , "CrCasRenterLessorSectorNavigation", "CrCasRenterLessorCodeNavigation", "CrCasRenterLessorStatisticsGenderNavigation", "CrCasRenterLessorMembershipNavigation" });
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var Mechanism = _unitOfWork.CrMasSysEvaluation.FindAll(x => x.CrMasSysEvaluationsClassification == "1" && x.CrMasSysEvaluationsStatus == "A").ToList();
            ViewData["Mechanism"] = Mechanism;

            var workPlace = _unitOfWork.CrMasSupRenterEmployer.Find(x=>x.CrMasSupRenterEmployerCode==contract.CrCasRenterLessorNavigation.CrMasRenterInformationEmployer);
            ViewBag.workPlaceAr = workPlace?.CrMasSupRenterEmployerArName;
            ViewBag.workPlaceEn = workPlace?.CrMasSupRenterEmployerEnName;

            var Bank = _unitOfWork.CrMasSupAccountBanks.Find(x=>x.CrMasSupAccountBankCode == contract.CrCasRenterLessorNavigation.CrMasRenterInformationBank);
            ViewBag.BankAr = Bank?.CrMasSupAccountBankArName;
            ViewBag.BankEn = Bank?.CrMasSupAccountBankEnName;

            var DrivingType = _unitOfWork.CrElmLicense.Find(x => x.CrElmLicenseNo == contract.CrCasRenterLessorNavigation.CrMasRenterInformationDrivingLicenseNo);
            ViewBag.DrivingTypeAr = DrivingType.CrElmLicenseArName;
            ViewBag.DrivingTypeEn = DrivingType.CrElmLicenseEnName;

            int countRenterLessorInformations = 0;
            //countRenterLessorInformations = _RenterLessorInformation.GetOneRenterLessorInformationCount(id);
            ViewBag.RenterLessorInformations_Count = countRenterLessorInformations;
            var model = _mapper.Map<RenterLessorInformationVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RenterLessorInformationVM model)
        {

            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "0";

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);

            if (user != null)
            {
                if (model != null)
                {

                    var contract = _mapper.Map<CrCasRenterLessor>(model);

                    var singlExist = _unitOfWork.CrCasRenterLessor.Find(x=>x.CrCasRenterLessorId == contract.CrCasRenterLessorId);
                    if (singlExist != null)
                    {
                        singlExist.CrCasRenterLessorDealingMechanism = contract.CrCasRenterLessorDealingMechanism;
                        singlExist.CrCasRenterLessorReasons = contract.CrCasRenterLessorReasons;
                        _unitOfWork.CrCasRenterLessor.Update(singlExist);
                    }
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203001", "2");
                    var RecordAr = contract.CrCasRenterLessorNavigation.CrMasRenterInformationArName;
                    var RecordEn = contract.CrCasRenterLessorNavigation.CrMasRenterInformationEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "RenterLessorInformation");
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
                    int CountRenterLessorInformations = 0;
                    //CountRenterLessorInformations = _RenterLessorInformation.GetOneRenterLessorInformationCount(code);
                    if (CountRenterLessorInformations == 0)
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

                var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203001", "2");
                var RecordAr = Contract.CrCasRenterLessorNavigation.CrMasRenterInformationArName;
                var RecordEn = Contract.CrCasRenterLessorNavigation.CrMasRenterInformationEnName;
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "RenterLessorInformation");
            }


            return View(Contract);

        }

        [HttpPost]
        public async Task<IActionResult> CheckChangedField(string Exist_lang, string dataField)
        {
            var All_RenterLessorInformations = await _unitOfWork.CrCasRenterLessor.GetAllAsync();

            if (dataField != null && All_RenterLessorInformations != null)
            {
                if (Exist_lang == "ExistAr")
                {
                    var existingRenterLessorInformation_Ar = All_RenterLessorInformations.FirstOrDefault(x =>
                        x.CrCasRenterLessorNavigation.CrMasRenterInformationArName == dataField);
                    if (existingRenterLessorInformation_Ar != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }
                else if (Exist_lang == "ExistEn")
                {
                    var existingRenterLessorInformation_En = All_RenterLessorInformations.FirstOrDefault(x =>
                        x.CrCasRenterLessorNavigation.CrMasRenterInformationEnName == dataField);
                    if (existingRenterLessorInformation_En != null)
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
