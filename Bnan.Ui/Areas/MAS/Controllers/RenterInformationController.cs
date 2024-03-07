using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Numerics;
namespace Bnan.Ui.Areas.MAS.Controllers
{

    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class RenterInformationController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IRenterInformation _RenterInformation;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<RenterInformationController> _localizer;


        public RenterInformationController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IRenterInformation RenterInformation,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<RenterInformationController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _RenterInformation = RenterInformation;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var (mainTask, subTask, system, currentUser) = await SetTrace("103", "1103001", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            var titles = await setTitle("103", "1103001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterInformationAll = _unitOfWork.CrMasRenterInformation.GetAll(new[] { "CrMasRenterInformationNationalityNavigation", "CrMasRenterInformationEmployerNavigation", "CrMasRenterInformationProfessionNavigation", "CrMasRenterPost" });
            //var RenterInformationAll = await _unitOfWork.CrMasRenterInformation.GetAllAsync();
            //var RenterInformationAllA = RenterInformationAll.Where(x => x.CrMasRenterInformationStatus == "A").ToList();
            var RenterInformationAllA = RenterInformationAll.ToList();
            List<List<string>> ConcateAdress_short = new List<List<string>>();
            foreach (var item in RenterInformationAllA)
            {
                var ar = item.CrMasRenterPost.CrMasRenterPostArShortConcatenate.ToString(); 
                string[] values = ar.Split('-');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }
                var en = item.CrMasRenterPost.CrMasRenterPostEnShortConcatenate.ToString();
                string[] values2 = en.Split('-');
                for (int i = 0; i < values2.Length; i++)
                {
                    values2[i] = values2[i].Trim();
                }
                if (values.Length > 1 && values2.Length > 1)
                {
                    if (values2[0].Length < 4 && values2.Length > 2)
                    {
                        ConcateAdress_short.Add(new List<string> { item.CrMasRenterInformationId, values[0] + " - " + values[1], values2[0] + "-" + values2[1] + " - " + values2[2] });
                    }
                    else
                    {
                        ConcateAdress_short.Add(new List<string> { item.CrMasRenterInformationId, values[0] + " - " + values[1], values2[0] + " - " + values2[1] });

                    }
                }


            }
            ViewBag.ConcateAdress = ConcateAdress_short;
            //var CarsInfo_count_all = _RenterInformation.GetAllRenterInformationsCount();

            return View(RenterInformationAllA);
        }

        [HttpGet]
        public PartialViewResult GetRenterInformationByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    //var RenterInformationbyStatusAll = _unitOfWork.CrMasRenterInformation.GetAll();
                    //return PartialView("_DataTableRenterInformation", RenterInformationbyStatusAll);

                    var RenterInformationbyStatusAll = _unitOfWork.CrMasRenterInformation.FindAll(l => l.CrMasRenterInformationStatus == Status.Hold || l.CrMasRenterInformationStatus == Status.Active);
                    //var CarsInfo_count_all1 = _RenterInformation.GetAllRenterInformationsCount();
                    //Tuple<IEnumerable<CrMasRenterInformation>, List<List<string>>> tb1 = new Tuple<IEnumerable<CrMasRenterInformation>, List<List<string>>>(RenterInformationbyStatusAll, CarsInfo_count_all1);
                    return PartialView("_DataTableRenterInformation", RenterInformationbyStatusAll);
                }
                var RenterInformationbyStatus = _unitOfWork.CrMasRenterInformation.FindAll(l => l.CrMasRenterInformationStatus == status).ToList();
                //var CarsInfo_count_all = _RenterInformation.GetAllRenterInformationsCount();
                //Tuple<IEnumerable<CrMasRenterInformation>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasRenterInformation>, List<List<string>>>(RenterInformationbyStatus, CarsInfo_count_all);
                return PartialView("_DataTableRenterInformation", RenterInformationbyStatus);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddRenterInformation()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("103", "1103001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterInformationCode = "";
            var RenterInformations = await _unitOfWork.CrMasRenterInformation.GetAllAsync();
            if (RenterInformations.Count() != 0)
            {
                RenterInformationCode = (BigInteger.Parse(RenterInformations.LastOrDefault().CrMasRenterInformationId) + 1).ToString();
            }
            else
            {
                RenterInformationCode = "0";
            }
            ViewBag.RenterInformationCode = RenterInformationCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRenterInformation(RenterInformationVM RenterInformations)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;

            if (ModelState.IsValid)
            {
                if (RenterInformations != null)
                {
                    var RenterInformationVMT = _mapper.Map<CrMasRenterInformation>(RenterInformations);
                    var All_RenterInformations = await _unitOfWork.CrMasRenterInformation.GetAllAsync();
                    var existingRenterInformation_En = All_RenterInformations.FirstOrDefault(x =>
                        x.CrMasRenterInformationEnName == RenterInformationVMT.CrMasRenterInformationEnName);
                    var existingRenterInformation_Ar = All_RenterInformations.FirstOrDefault(x =>
                        x.CrMasRenterInformationArName == RenterInformationVMT.CrMasRenterInformationArName);

                    // Generate code for the second time
                    var RenterInformationCode = (BigInteger.Parse(All_RenterInformations.LastOrDefault().CrMasRenterInformationId) + 1).ToString();
                    RenterInformations.CrMasRenterInformationId = RenterInformationCode;
                    ViewBag.RenterInformationCode = RenterInformationCode;
                    if (RenterInformationVMT.CrMasRenterInformationArName != null && RenterInformationVMT.CrMasRenterInformationEnName != null)
                    {
                        if (existingRenterInformation_Ar != null && existingRenterInformation_En != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(RenterInformations);
                        }
                        else if (existingRenterInformation_En != null)
                        {
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(RenterInformations);
                        }
                        else if (existingRenterInformation_Ar != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            return View(RenterInformations);
                        }
                    }

                    RenterInformationVMT.CrMasRenterInformationStatus = "A";
                    await _unitOfWork.CrMasRenterInformation.AddAsync(RenterInformationVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("103", "1103001", "1");
                    var RecordAr = RenterInformationVMT.CrMasRenterInformationArName;
                    var RecordEn = RenterInformationVMT.CrMasRenterInformationEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddRenterInformation", RenterInformations);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("103", "1103001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasRenterInformation.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            int countRenterInformations = 0;
            //countRenterInformations = _RenterInformation.GetOneRenterInformationCount(id);
            ViewBag.RenterInformations_Count = countRenterInformations;
            var model = _mapper.Map<RenterInformationVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RenterInformationVM model)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);

            if (user != null)
            {
                if (model != null)
                {

                    var contract = _mapper.Map<CrMasRenterInformation>(model);

                    _unitOfWork.CrMasRenterInformation.Update(contract);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("103", "1103001", "1");
                    var RecordAr = contract.CrMasRenterInformationArName;
                    var RecordEn = contract.CrMasRenterInformationEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "RenterInformation");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasRenterInformation.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasRenterInformationStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    int CountRenterInformations = 0;
                    //CountRenterInformations = _RenterInformation.GetOneRenterInformationCount(code);
                    if (CountRenterInformations == 0)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        Contract.CrMasRenterInformationStatus = Status.Deleted;
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
                    Contract.CrMasRenterInformationStatus = Status.Active;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("103", "1103001", "1");
                var RecordAr = Contract.CrMasRenterInformationArName;
                var RecordEn = Contract.CrMasRenterInformationEnName;
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "RenterInformation");
            }


            return View(Contract);

        }

        [HttpPost]
        public async Task<IActionResult> CheckChangedField(string Exist_lang, string dataField)
        {
            var All_RenterInformations = await _unitOfWork.CrMasRenterInformation.GetAllAsync();

            if (dataField != null && All_RenterInformations != null)
            {
                if (Exist_lang == "ExistAr")
                {
                    var existingRenterInformation_Ar = All_RenterInformations.FirstOrDefault(x =>
                        x.CrMasRenterInformationArName == dataField);
                    if (existingRenterInformation_Ar != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }
                else if (Exist_lang == "ExistEn")
                {
                    var existingRenterInformation_En = All_RenterInformations.FirstOrDefault(x =>
                        x.CrMasRenterInformationEnName == dataField);
                    if (existingRenterInformation_En != null)
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

