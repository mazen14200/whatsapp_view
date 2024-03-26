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
    public class CarFuelController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IMasCarFuel _carFuel;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<CarFuelController> _localizer;


        public CarFuelController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IMasCarFuel carFuel,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<CarFuelController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _carFuel = carFuel;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107002", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            var titles = await setTitle("107", "1107002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupCarFuel.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupCarFuelStatus == "A").ToList();
            var CarsInfo_count_all = _carFuel.GetAllCarFuelsCount();
            Tuple<IEnumerable<CrMasSupCarFuel>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarFuel>, List<List<string>>>(contract, CarsInfo_count_all);
            return View(tb);
        }

        [HttpGet]
        public PartialViewResult GetCarFuelByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    //var CarFuelbyStatusAll = _unitOfWork.CrMasSupCarFuel.GetAll();
                    //return PartialView("_DataTableCarFuel", CarFuelbyStatusAll);

                    var CarFuelbyStatusAll = _unitOfWork.CrMasSupCarFuel.FindAll(l => l.CrMasSupCarFuelStatus == Status.Hold || l.CrMasSupCarFuelStatus == Status.Active);
                    var CarsInfo_count_all1 = _carFuel.GetAllCarFuelsCount();
                    Tuple<IEnumerable<CrMasSupCarFuel>, List<List<string>>> tb1 = new Tuple<IEnumerable<CrMasSupCarFuel>, List<List<string>>>(CarFuelbyStatusAll, CarsInfo_count_all1);
                    return PartialView("_DataTableCarFuel", tb1);
                }
                var CarFuelbyStatus = _unitOfWork.CrMasSupCarFuel.FindAll(l => l.CrMasSupCarFuelStatus == status).ToList();
                var CarsInfo_count_all = _carFuel.GetAllCarFuelsCount();
                Tuple<IEnumerable<CrMasSupCarFuel>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarFuel>, List<List<string>>>(CarFuelbyStatus, CarsInfo_count_all);
                return PartialView("_DataTableCarFuel", tb);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddCarFuel()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("107", "1107002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var CarFuelCode = "";
            var CarFuels = await _unitOfWork.CrMasSupCarFuel.GetAllAsync();
            if (CarFuels.Count() != 0)
            {
                CarFuelCode = (BigInteger.Parse(CarFuels.LastOrDefault().CrMasSupCarFuelCode) + 1).ToString();
            }
            else
            {
                CarFuelCode = "10";
            }
            ViewBag.CarFuelCode = CarFuelCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCarFuel(CarFuelVM CarFuels, IFormFile? AcceptImg)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";


            if (ModelState.IsValid)
            {
                if (CarFuels != null)
                {
                    var CarFuelVMT = _mapper.Map<CrMasSupCarFuel>(CarFuels);
                    var All_CarFuels = await _unitOfWork.CrMasSupCarFuel.GetAllAsync();
                    var existingCarFuel_En = All_CarFuels.FirstOrDefault(x =>
                        x.CrMasSupCarFuelEnName == CarFuelVMT.CrMasSupCarFuelEnName);
                    var existingCarFuel_Ar = All_CarFuels.FirstOrDefault(x =>
                        x.CrMasSupCarFuelArName == CarFuelVMT.CrMasSupCarFuelArName);

                    // Generate code for the second time
                    var CarFuelCode = (BigInteger.Parse(All_CarFuels.LastOrDefault().CrMasSupCarFuelCode) + 1).ToString();
                    CarFuels.CrMasSupCarFuelCode = CarFuelCode;
                    ViewBag.CarFuelCode = CarFuelCode;
                    if (CarFuelVMT.CrMasSupCarFuelArName != null && CarFuelVMT.CrMasSupCarFuelEnName != null)
                    {
                        if (existingCarFuel_Ar != null && existingCarFuel_En != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarFuels);
                        }
                        else if (existingCarFuel_En != null)
                        {
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarFuels);
                        }
                        else if (existingCarFuel_Ar != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            return View(CarFuels);
                        }
                    }

                    if (AcceptImg != null)
                    {
                        string fileNameImg = "CarFuel_" + CarFuels.CrMasSupCarFuelCode.ToString();
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }


                    CarFuelVMT.CrMasSupCarFuelImage = filePathImageAccept;
                    CarFuelVMT.CrMasSupCarFuelStatus = "A";
                    await _unitOfWork.CrMasSupCarFuel.AddAsync(CarFuelVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107002", "1");
                    var RecordAr = CarFuelVMT.CrMasSupCarFuelArName;
                    var RecordEn = CarFuelVMT.CrMasSupCarFuelEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddCarFuel", CarFuels);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("107", "1107002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupCarFuel.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            int countCarFuels = 0;
            countCarFuels = _carFuel.GetOneCarFuelCount(id);
            ViewBag.CarFuels_Count = countCarFuels;
            var model = _mapper.Map<CarFuelVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CarFuelVM model, IFormFile? AcceptImg)
        {
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = null;
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            var fuel =await _unitOfWork.CrMasSupCarFuel.FindAsync(x=>x.CrMasSupCarFuelCode==model.CrMasSupCarFuelCode);
            if (user != null)
            {
                if (model != null && fuel!=null)
                {

                    
                    if (AcceptImg != null)
                    {
                        string fileNameImg = "CarFuel_" + model.CrMasSupCarFuelCode.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"); // اسم مبني على التاريخ والوق
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png", fuel.CrMasSupCarFuelImage);
                    }
                    else if (!string.IsNullOrEmpty(fuel.CrMasSupCarFuelImage))
                    {
                        filePathImageAccept = fuel.CrMasSupCarFuelImage;
                    }
                    else
                    {
                        filePathImageAccept = "~/images/common/DefaultCar.png";
                    }

                    //var contract = _mapper.Map<CrMasSupCarFuel>(model);
                    fuel.CrMasSupCarFuelImage = filePathImageAccept;
                    fuel.CrMasSupCarFuelReasons = model.CrMasSupCarFuelReasons;

                    _unitOfWork.CrMasSupCarFuel.Update(fuel);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107002", "1");
                    var RecordAr = fuel.CrMasSupCarFuelArName;
                    var RecordEn = fuel.CrMasSupCarFuelEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "CarFuel");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupCarFuel.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupCarFuelStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    int CountCarFuels = 0;
                    CountCarFuels = _carFuel.GetOneCarFuelCount(code);
                    if (CountCarFuels == 0)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        Contract.CrMasSupCarFuelStatus = Status.Deleted;
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
                    Contract.CrMasSupCarFuelStatus = Status.Active;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing
                var RecordAr = Contract.CrMasSupCarFuelArName;
                var RecordEn = Contract.CrMasSupCarFuelEnName;
                var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107002", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "CarFuel");
            }


            return View(Contract);

        }

        [HttpPost]
        public async Task<IActionResult> CheckChangedField(string Exist_lang, string dataField)
        {
            var All_CarFuels = await _unitOfWork.CrMasSupCarFuel.GetAllAsync();

            if (dataField != null && All_CarFuels != null)
            {
                if (Exist_lang == "ExistAr")
                {
                    var existingCarFuel_Ar = All_CarFuels.FirstOrDefault(x =>
                        x.CrMasSupCarFuelArName == dataField);
                    if (existingCarFuel_Ar != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }
                else if (Exist_lang == "ExistEn")
                {
                    var existingCarFuel_En = All_CarFuels.FirstOrDefault(x =>
                        x.CrMasSupCarFuelEnName == dataField);
                    if (existingCarFuel_En != null)
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
