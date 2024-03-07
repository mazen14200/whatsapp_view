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
    public class CarAdvantageController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IMasCarAdvantage _carAdvantage;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<CarAdvantageController> _localizer;


        public CarAdvantageController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IMasCarAdvantage carAdvantage,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<CarAdvantageController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _carAdvantage = carAdvantage;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107008", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            var titles = await setTitle("107", "1107008", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupCarAdvantage.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupCarAdvantagesStatus == "A").ToList();
            var CarsInfo_count_all = _carAdvantage.GetAllCarAdvantagesCount();
            Tuple<IEnumerable<CrMasSupCarAdvantage>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarAdvantage>, List<List<string>>>(contract, CarsInfo_count_all);
            return View(tb);
        }

        [HttpGet]
        public PartialViewResult GetCarAdvantageByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    //var CarAdvantagebyStatusAll = _unitOfWork.CrMasSupCarAdvantage.GetAll();
                    //return PartialView("_DataTableCarAdvantage", CarAdvantagebyStatusAll);

                    var CarAdvantagebyStatusAll = _unitOfWork.CrMasSupCarAdvantage.FindAll(l => l.CrMasSupCarAdvantagesStatus == Status.Hold || l.CrMasSupCarAdvantagesStatus == Status.Active);
                    var CarsInfo_count_all1 = _carAdvantage.GetAllCarAdvantagesCount();
                    Tuple<IEnumerable<CrMasSupCarAdvantage>, List<List<string>>> tb1 = new Tuple<IEnumerable<CrMasSupCarAdvantage>, List<List<string>>>(CarAdvantagebyStatusAll, CarsInfo_count_all1);
                    return PartialView("_DataTableCarAdvantage", tb1);
                }
                var CarAdvantagebyStatus = _unitOfWork.CrMasSupCarAdvantage.FindAll(l => l.CrMasSupCarAdvantagesStatus == status).ToList();
                var CarsInfo_count_all = _carAdvantage.GetAllCarAdvantagesCount();
                Tuple<IEnumerable<CrMasSupCarAdvantage>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarAdvantage>, List<List<string>>>(CarAdvantagebyStatus, CarsInfo_count_all);
                return PartialView("_DataTableCarAdvantage", tb);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddCarAdvantage()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("107", "1107008", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var CarAdvantageCode = "";
            var CarAdvantages = await _unitOfWork.CrMasSupCarAdvantage.GetAllAsync();
            if (CarAdvantages.Count() != 0)
            {
                CarAdvantageCode = (BigInteger.Parse(CarAdvantages.LastOrDefault().CrMasSupCarAdvantagesCode) + 1).ToString();
            }
            else
            {
                CarAdvantageCode = "10";
            }
            ViewBag.CarAdvantageCode = CarAdvantageCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCarAdvantage(CarAdvantageVM CarAdvantages, IFormFile? AcceptImg)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";


            if (ModelState.IsValid)
            {
                if (CarAdvantages != null)
                {
                    var CarAdvantageVMT = _mapper.Map<CrMasSupCarAdvantage>(CarAdvantages);
                    var All_CarAdvantages = await _unitOfWork.CrMasSupCarAdvantage.GetAllAsync();
                    var existingCarAdvantage_En = All_CarAdvantages.FirstOrDefault(x =>
                        x.CrMasSupCarAdvantagesEnName == CarAdvantageVMT.CrMasSupCarAdvantagesEnName);
                    var existingCarAdvantage_Ar = All_CarAdvantages.FirstOrDefault(x =>
                        x.CrMasSupCarAdvantagesArName == CarAdvantageVMT.CrMasSupCarAdvantagesArName);

                    // Generate code for the second time
                    var CarAdvantageCode = (BigInteger.Parse(All_CarAdvantages.LastOrDefault().CrMasSupCarAdvantagesCode) + 1).ToString();
                    CarAdvantages.CrMasSupCarAdvantagesCode = CarAdvantageCode;
                    ViewBag.CarAdvantageCode = CarAdvantageCode;
                    if (CarAdvantageVMT.CrMasSupCarAdvantagesArName != null && CarAdvantageVMT.CrMasSupCarAdvantagesEnName != null)
                    {
                        if (existingCarAdvantage_Ar != null && existingCarAdvantage_En != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarAdvantages);
                        }
                        else if (existingCarAdvantage_En != null)
                        {
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarAdvantages);
                        }
                        else if (existingCarAdvantage_Ar != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            return View(CarAdvantages);
                        }
                    }

                    if (AcceptImg != null)
                    {
                        string fileNameImg = "CarAdvantage_" + CarAdvantages.CrMasSupCarAdvantagesCode.ToString();
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }


                    CarAdvantageVMT.CrMasSupCarAdvantagesImage = filePathImageAccept;
                    CarAdvantageVMT.CrMasSupCarAdvantagesStatus = "A";
                    await _unitOfWork.CrMasSupCarAdvantage.AddAsync(CarAdvantageVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107008", "1");
                    var RecordAr = CarAdvantageVMT.CrMasSupCarAdvantagesArName;
                    var RecordEn = CarAdvantageVMT.CrMasSupCarAdvantagesEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode,RecordAr, RecordEn, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddCarAdvantage", CarAdvantages);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("107", "1107008", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupCarAdvantage.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            int countCarAdvantages = 0;
            countCarAdvantages = _carAdvantage.GetOneCarAdvantageCount(id);
            ViewBag.CarAdvantages_Count = countCarAdvantages;
            var model = _mapper.Map<CarAdvantageVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CarAdvantageVM model, IFormFile? AcceptImg)
        {
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);

            if (user != null)
            {
                if (model != null)
                {

                    if (AcceptImg != null)
                    {
                        string fileNameImg = "CarAdvantage_" + model.CrMasSupCarAdvantagesCode.ToString();
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupCarAdvantagesImage = filePathImageAccept;
                    }


                    var contract = _mapper.Map<CrMasSupCarAdvantage>(model);

                    _unitOfWork.CrMasSupCarAdvantage.Update(contract);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107008", "1");
                    var RecordAr = contract.CrMasSupCarAdvantagesArName;
                    var RecordEn = contract.CrMasSupCarAdvantagesEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "CarAdvantage");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupCarAdvantage.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupCarAdvantagesStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    int CountCarAdvantages = 0;
                    CountCarAdvantages = _carAdvantage.GetOneCarAdvantageCount(code);
                    if (CountCarAdvantages == 0)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        Contract.CrMasSupCarAdvantagesStatus = Status.Deleted;
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
                    Contract.CrMasSupCarAdvantagesStatus = Status.Active;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing
                var RecordAr = Contract.CrMasSupCarAdvantagesArName;
                var RecordEn = Contract.CrMasSupCarAdvantagesEnName;
                var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107008", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "CarAdvantage");
            }


            return View(Contract);

        }

        [HttpPost]
        public async Task<IActionResult> CheckChangedField(string Exist_lang, string dataField)
        {
            var All_CarAdvantages = await _unitOfWork.CrMasSupCarAdvantage.GetAllAsync();

            if (dataField != null && All_CarAdvantages != null)
            {
                if (Exist_lang == "ExistAr")
                {
                    var existingCarAdvantage_Ar = All_CarAdvantages.FirstOrDefault(x =>
                        x.CrMasSupCarAdvantagesArName == dataField);
                    if (existingCarAdvantage_Ar != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }
                else if (Exist_lang == "ExistEn")
                {
                    var existingCarAdvantage_En = All_CarAdvantages.FirstOrDefault(x =>
                        x.CrMasSupCarAdvantagesEnName == dataField);
                    if (existingCarAdvantage_En != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }

            }
            return View();
        }



        //public  IActionResult CannotDelete() 
        //{ 

        //_toastNotification.AddErrorToastMessage(_localizer["SureTo_Cannot_delete"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

        //    return View();
        //}
    }
  }
