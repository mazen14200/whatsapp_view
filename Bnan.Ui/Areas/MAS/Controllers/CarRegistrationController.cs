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
    public class CarRegistrationController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IMasCarRegistration _carRegistration;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<CarRegistrationController> _localizer;


        public CarRegistrationController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IMasCarRegistration carRegistration,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<CarRegistrationController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _carRegistration = carRegistration;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107001", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            var titles = await setTitle("107", "1107001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupCarRegistration.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupCarRegistrationStatus == "A").ToList();
            var CarsInfo_count_all = _carRegistration.GetAllCarRegistrationsCount();
            Tuple<IEnumerable<CrMasSupCarRegistration>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarRegistration>, List<List<string>>>(contract, CarsInfo_count_all);
            return View(tb);
        }

        [HttpGet]
        public PartialViewResult GetCarRegistrationByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    //var CarRegistrationbyStatusAll = _unitOfWork.CrMasSupCarRegistration.GetAll();
                    //return PartialView("_DataTableCarRegistration", CarRegistrationbyStatusAll);

                    var CarRegistrationbyStatusAll = _unitOfWork.CrMasSupCarRegistration.FindAll(l => l.CrMasSupCarRegistrationStatus == Status.Hold || l.CrMasSupCarRegistrationStatus == Status.Active);
                    var CarsInfo_count_all1 = _carRegistration.GetAllCarRegistrationsCount();
                    Tuple<IEnumerable<CrMasSupCarRegistration>, List<List<string>>> tb1 = new Tuple<IEnumerable<CrMasSupCarRegistration>, List<List<string>>>(CarRegistrationbyStatusAll, CarsInfo_count_all1);
                    return PartialView("_DataTableCarRegistration", tb1);
                }
                var CarRegistrationbyStatus = _unitOfWork.CrMasSupCarRegistration.FindAll(l => l.CrMasSupCarRegistrationStatus == status).ToList();
                var CarsInfo_count_all = _carRegistration.GetAllCarRegistrationsCount();
                Tuple<IEnumerable<CrMasSupCarRegistration>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarRegistration>, List<List<string>>>(CarRegistrationbyStatus, CarsInfo_count_all);
                return PartialView("_DataTableCarRegistration", tb);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddCarRegistration()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("107", "1107001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var CarRegistrationCode = "";
            var CarRegistrations = await _unitOfWork.CrMasSupCarRegistration.GetAllAsync();
            if (CarRegistrations.Count() != 0)
            {
                CarRegistrationCode = (BigInteger.Parse(CarRegistrations.LastOrDefault().CrMasSupCarRegistrationCode) + 1).ToString();
            }
            else
            {
                CarRegistrationCode = "10";
            }
            ViewBag.CarRegistrationCode = CarRegistrationCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCarRegistration(CarRegistrationVM CarRegistrations)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;


            if (ModelState.IsValid)
            {
                if (CarRegistrations != null)
                {
                    var CarRegistrationVMT = _mapper.Map<CrMasSupCarRegistration>(CarRegistrations);
                    var All_CarRegistrations = await _unitOfWork.CrMasSupCarRegistration.GetAllAsync();
                    var existingCarRegistration_En = All_CarRegistrations.FirstOrDefault(x =>
                        x.CrMasSupCarRegistrationEnName == CarRegistrationVMT.CrMasSupCarRegistrationEnName);
                    var existingCarRegistration_Ar = All_CarRegistrations.FirstOrDefault(x =>
                        x.CrMasSupCarRegistrationArName == CarRegistrationVMT.CrMasSupCarRegistrationArName);

                    // Generate code for the second time
                    var CarRegistrationCode = (BigInteger.Parse(All_CarRegistrations.LastOrDefault().CrMasSupCarRegistrationCode) + 1).ToString();
                    CarRegistrations.CrMasSupCarRegistrationCode = CarRegistrationCode;
                    ViewBag.CarRegistrationCode = CarRegistrationCode;
                    if (CarRegistrationVMT.CrMasSupCarRegistrationArName != null && CarRegistrationVMT.CrMasSupCarRegistrationEnName != null)
                    {
                        if (existingCarRegistration_Ar != null && existingCarRegistration_En != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarRegistrations);
                        }
                        else if (existingCarRegistration_En != null)
                        {
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarRegistrations);
                        }
                        else if (existingCarRegistration_Ar != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            return View(CarRegistrations);
                        }
                    }

                    CarRegistrationVMT.CrMasSupCarRegistrationStatus = "A";
                    await _unitOfWork.CrMasSupCarRegistration.AddAsync(CarRegistrationVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107001", "1");
                    var RecordAr = CarRegistrationVMT.CrMasSupCarRegistrationArName;
                    var RecordEn = CarRegistrationVMT.CrMasSupCarRegistrationEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddCarRegistration", CarRegistrations);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("107", "1107001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupCarRegistration.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            int countCarRegistrations = 0;
            countCarRegistrations = _carRegistration.GetOneCarRegistrationCount(id);
            ViewBag.CarRegistrations_Count = countCarRegistrations;
            var model = _mapper.Map<CarRegistrationVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CarRegistrationVM model)
        {
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);

            if (user != null)
            {
                if (model != null)
                {

                    var contract = _mapper.Map<CrMasSupCarRegistration>(model);

                    _unitOfWork.CrMasSupCarRegistration.Update(contract);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107001", "1");
                    var RecordAr = contract.CrMasSupCarRegistrationArName;
                    var RecordEn = contract.CrMasSupCarRegistrationEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "CarRegistration");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupCarRegistration.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupCarRegistrationStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    int CountCarRegistrations = 0;
                    CountCarRegistrations = _carRegistration.GetOneCarRegistrationCount(code);
                    if (CountCarRegistrations == 0)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        Contract.CrMasSupCarRegistrationStatus = Status.Deleted;
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
                    Contract.CrMasSupCarRegistrationStatus = Status.Active;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107001", "1");
                var RecordAr = Contract.CrMasSupCarRegistrationArName;
                var RecordEn = Contract.CrMasSupCarRegistrationEnName;
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "CarRegistration");
            }


            return View(Contract);

        }

        [HttpPost]
        public async Task<IActionResult> CheckChangedField(string Exist_lang, string dataField)
        {
            var All_CarRegistrations = await _unitOfWork.CrMasSupCarRegistration.GetAllAsync();

            if (dataField != null && All_CarRegistrations != null)
            {
                if (Exist_lang == "ExistAr")
                {
                    var existingCarRegistration_Ar = All_CarRegistrations.FirstOrDefault(x =>
                        x.CrMasSupCarRegistrationArName == dataField);
                    if (existingCarRegistration_Ar != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }
                else if (Exist_lang == "ExistEn")
                {
                    var existingCarRegistration_En = All_CarRegistrations.FirstOrDefault(x =>
                        x.CrMasSupCarRegistrationEnName == dataField);
                    if (existingCarRegistration_En != null)
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
