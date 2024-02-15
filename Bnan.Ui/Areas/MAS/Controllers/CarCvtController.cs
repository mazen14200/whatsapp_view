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


namespace Bnan.Ui.Areas.MAS
{

    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class CarCvtController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IMasCarCvt _carCvt;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<CarCvtController> _localizer;


        public CarCvtController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IMasCarCvt carCvt,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<CarCvtController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _carCvt = carCvt;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {

            var titles = await setTitle("107", "1107003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupCarCvt.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupCarCvtStatus == "A").ToList();
            var CarsInfo_count_all = _carCvt.GetAllCarCvtsCount();
            Tuple<IEnumerable<CrMasSupCarCvt>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarCvt>, List<List<string>>>(contract, CarsInfo_count_all);
            return View(tb);
        }

        [HttpGet]
        public PartialViewResult GetCarCvtByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    //var CarCvtbyStatusAll = _unitOfWork.CrMasSupCarCvt.GetAll();
                    //return PartialView("_DataTableCarCvt", CarCvtbyStatusAll);

                    var CarCvtbyStatusAll = _unitOfWork.CrMasSupCarCvt.FindAll(l => l.CrMasSupCarCvtStatus == Status.Hold || l.CrMasSupCarCvtStatus == Status.Active);
                    var CarsInfo_count_all1 = _carCvt.GetAllCarCvtsCount();
                    Tuple<IEnumerable<CrMasSupCarCvt>, List<List<string>>> tb1 = new Tuple<IEnumerable<CrMasSupCarCvt>, List<List<string>>>(CarCvtbyStatusAll, CarsInfo_count_all1);
                    return PartialView("_DataTableCarCvt", tb1);
                }
                var CarCvtbyStatus = _unitOfWork.CrMasSupCarCvt.FindAll(l => l.CrMasSupCarCvtStatus == status).ToList();
                var CarsInfo_count_all = _carCvt.GetAllCarCvtsCount();
                Tuple<IEnumerable<CrMasSupCarCvt>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarCvt>, List<List<string>>>(CarCvtbyStatus, CarsInfo_count_all);
                return PartialView("_DataTableCarCvt", tb);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddCarCvt()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("107", "1107003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var CarCvtCode = "";
            var CarCvts = await _unitOfWork.CrMasSupCarCvt.GetAllAsync();
            if (CarCvts.Count() != 0)
            {
                CarCvtCode = (BigInteger.Parse(CarCvts.LastOrDefault().CrMasSupCarCvtCode) + 1).ToString();
            }
            else
            {
                CarCvtCode = "10";
            }
            ViewBag.CarCvtCode = CarCvtCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCarCvt(CarCvtVM CarCvts, IFormFile? AcceptImg)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";


            if (ModelState.IsValid)
            {
                if (CarCvts != null)
                {
                    var CarCvtVMT = _mapper.Map<CrMasSupCarCvt>(CarCvts);
                    var All_CarCvts = await _unitOfWork.CrMasSupCarCvt.GetAllAsync();
                    var existingCarCvt_En = All_CarCvts.FirstOrDefault(x =>
                        x.CrMasSupCarCvtEnName == CarCvtVMT.CrMasSupCarCvtEnName);
                    var existingCarCvt_Ar = All_CarCvts.FirstOrDefault(x =>
                        x.CrMasSupCarCvtArName == CarCvtVMT.CrMasSupCarCvtArName);

                    // Generate code for the second time
                    var CarCvtCode = (BigInteger.Parse(All_CarCvts.LastOrDefault().CrMasSupCarCvtCode) + 1).ToString();
                    CarCvts.CrMasSupCarCvtCode = CarCvtCode;
                    ViewBag.CarCvtCode = CarCvtCode;
                    if (CarCvtVMT.CrMasSupCarCvtArName != null && CarCvtVMT.CrMasSupCarCvtEnName != null)
                    {
                        if (existingCarCvt_Ar != null && existingCarCvt_En != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarCvts);
                        }
                        else if (existingCarCvt_En != null)
                        {
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarCvts);
                        }
                        else if (existingCarCvt_Ar != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            return View(CarCvts);
                        }
                    }

                    if (AcceptImg != null)
                    {
                        //string fileNameImg = CarCvts.CrMasSupCarCvtEnName + "_CarCvt_" + CarCvts.CrMasSupCarCvtCode.ToString();
                        string fileNameImg ="CarCvt_" + CarCvts.CrMasSupCarCvtCode.ToString();
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }


                    CarCvtVMT.CrMasSupCarCvtImage = filePathImageAccept;
                    CarCvtVMT.CrMasSupCarCvtStatus = "A";
                    await _unitOfWork.CrMasSupCarCvt.AddAsync(CarCvtVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107003", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddCarCvt", CarCvts);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("107", "1107003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupCarCvt.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            int countCarCvts = 0;
            countCarCvts = _carCvt.GetOneCarCvtCount(id);
            ViewBag.CarCvts_Count = countCarCvts;
            var model = _mapper.Map<CarCvtVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CarCvtVM model, IFormFile? AcceptImg)
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
                        //string fileNameImg = model.CrMasSupCarCvtEnName + "_CarCvt_" + model.CrMasSupCarCvtCode.ToString();
                        string fileNameImg = "CarCvt_" + model.CrMasSupCarCvtCode.ToString();
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupCarCvtImage = filePathImageAccept;
                    }


                    var contract = _mapper.Map<CrMasSupCarCvt>(model);

                    _unitOfWork.CrMasSupCarCvt.Update(contract);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107003", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "CarCvt");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupCarCvt.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupCarCvtStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    int CountCarCvts = 0;
                    CountCarCvts = _carCvt.GetOneCarCvtCount(code);
                    if (CountCarCvts == 0)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        Contract.CrMasSupCarCvtStatus = Status.Deleted;
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
                    Contract.CrMasSupCarCvtStatus = Status.Active;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107003", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "CarCvt");
            }


            return View(Contract);

        }

        [HttpPost]
        public async Task<IActionResult> CheckChangedField(string Exist_lang, string dataField)
        {
            var All_CarCvts = await _unitOfWork.CrMasSupCarCvt.GetAllAsync();

            if (dataField != null && All_CarCvts != null)
            {
                if (Exist_lang == "ExistAr")
                {
                    var existingCarCvt_Ar = All_CarCvts.FirstOrDefault(x =>
                        x.CrMasSupCarCvtArName == dataField);
                    if (existingCarCvt_Ar != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }
                else if (Exist_lang == "ExistEn")
                {
                    var existingCarCvt_En = All_CarCvts.FirstOrDefault(x =>
                        x.CrMasSupCarCvtEnName == dataField);
                    if (existingCarCvt_En != null)
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
