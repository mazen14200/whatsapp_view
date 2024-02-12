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
    public class CarBrandController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IContractAdditional _contractAdditional;
        private readonly ICarBrand _carBrand;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<ContractAdditionalController> _localizer;
        private readonly IWebHostEnvironment _;


        public CarBrandController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IContractAdditional contractAdditional, ICarBrand carBrand,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<ContractAdditionalController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _contractAdditional = contractAdditional;
            _carBrand = carBrand;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {

            var titles = await setTitle("107", "1107004", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupCarBrand.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupCarBrandStatus == "A").ToList();
            var CarsInfo_count_all = _carBrand.GetAllCarBrandsCount();
            Tuple<IEnumerable<CrMasSupCarBrand>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarBrand>, List<List<string>>>(contract, CarsInfo_count_all);
            return View(tb);
        }

        [HttpGet]
        public PartialViewResult GetCarBrandByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    //var CarBrandbyStatusAll = _unitOfWork.CrMasSupCarBrand.GetAll();
                    //return PartialView("_DataTableCarBrand", CarBrandbyStatusAll);

                    var CarBrandbyStatusAll = _unitOfWork.CrMasSupCarBrand.FindAll(l => l.CrMasSupCarBrandStatus == Status.Hold || l.CrMasSupCarBrandStatus == Status.Active);
                    var CarsInfo_count_all1 = _carBrand.GetAllCarBrandsCount();
                    Tuple<IEnumerable<CrMasSupCarBrand>, List<List<string>>> tb1 = new Tuple<IEnumerable<CrMasSupCarBrand>, List<List<string>>>(CarBrandbyStatusAll, CarsInfo_count_all1);
                    return PartialView("_DataTableCarBrand", tb1);
                }
                var CarBrandbyStatus = _unitOfWork.CrMasSupCarBrand.FindAll(l => l.CrMasSupCarBrandStatus == status).ToList();
                var CarsInfo_count_all = _carBrand.GetAllCarBrandsCount();
                Tuple<IEnumerable<CrMasSupCarBrand>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarBrand>, List<List<string>>>(CarBrandbyStatus, CarsInfo_count_all);
                return PartialView("_DataTableCarBrand", tb);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddCarBrand()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("107", "1107004", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var CarBrandCode = "";
            var CarBrands = await _unitOfWork.CrMasSupCarBrand.GetAllAsync();
            if (CarBrands.Count() != 0)
            {
                CarBrandCode = (BigInteger.Parse(CarBrands.LastOrDefault().CrMasSupCarBrandCode) + 1).ToString();
            }
            else
            {
                CarBrandCode = "3001";
            }
            ViewBag.CarBrandCode = CarBrandCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCarBrand(CarBrandsVM CarBrands, IFormFile? AcceptImg)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";


            if (ModelState.IsValid)
            {
                if (CarBrands != null)
                {
                    var CarBrandsVMT = _mapper.Map<CrMasSupCarBrand>(CarBrands);
                    var All_CarBrands = await _unitOfWork.CrMasSupCarBrand.GetAllAsync();
                    var existingCarBrand_En = All_CarBrands.FirstOrDefault(x =>
                        x.CrMasSupCarBrandEnName == CarBrandsVMT.CrMasSupCarBrandEnName);
                    var existingCarBrand_Ar = All_CarBrands.FirstOrDefault(x => 
                        x.CrMasSupCarBrandArName == CarBrandsVMT.CrMasSupCarBrandArName);

                    // Generate code for the second time
                    var CarBrandCode = (BigInteger.Parse(All_CarBrands.LastOrDefault().CrMasSupCarBrandCode) + 1).ToString();
                    CarBrands.CrMasSupCarBrandCode = CarBrandCode;
                    ViewBag.CarBrandCode = CarBrandCode;
                    if (CarBrandsVMT.CrMasSupCarBrandArName != null && CarBrandsVMT.CrMasSupCarBrandEnName != null) 
                    {
                        if (existingCarBrand_Ar != null && existingCarBrand_En != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarBrands);
                        }
                        else if (existingCarBrand_En != null)
                        {                  
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarBrands);
                        }
                        else if (existingCarBrand_Ar != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            return View(CarBrands);
                        }
                    }

                    if (AcceptImg != null)
                    {
                        string fileNameImg = CarBrands.CrMasSupCarBrandEnName + "_Accept_" + CarBrands.CrMasSupCarBrandCode.ToString().Substring(CarBrands.CrMasSupCarBrandCode.ToString().Length - 3);
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }


                    CarBrandsVMT.CrMasSupCarBrandImage = filePathImageAccept;
                    CarBrandsVMT.CrMasSupCarBrandStatus = "A";
                    await _unitOfWork.CrMasSupCarBrand.AddAsync(CarBrandsVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107004", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

            }
                return RedirectToAction("Index");
            }
            return View("AddCarBrand", CarBrands);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("107", "1107004", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupCarBrand.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            int countCarBrands = 0;
            countCarBrands = _carBrand.GetOneBrandCount(id);
            ViewBag.CarBrands_Count = countCarBrands;
            var model = _mapper.Map<CarBrandsVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CarBrandsVM model, IFormFile? AcceptImg)
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
                        string fileNameImg = model.CrMasSupCarBrandEnName + "_Accept_" + model.CrMasSupCarBrandCode.ToString().Substring(model.CrMasSupCarBrandCode.ToString().Length - 3);
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupCarBrandImage = filePathImageAccept;
                    }


                    var contract = _mapper.Map<CrMasSupCarBrand>(model);

                    _unitOfWork.CrMasSupCarBrand.Update(contract);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107004", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "CarBrand");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupCarBrand.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupCarBrandStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    int CountCarBrands = 0;
                    CountCarBrands = _carBrand.GetOneBrandCount(code);
                    if (CountCarBrands == 0)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        Contract.CrMasSupCarBrandStatus = Status.Deleted;
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
                    Contract.CrMasSupCarBrandStatus = Status.Active;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107004", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "CarBrand");
            }


            return View(Contract);

        }

        [HttpPost]
        public async Task<IActionResult> CheckChangedField(string Exist_lang,string dataField)
        {
            var All_CarBrands = await _unitOfWork.CrMasSupCarBrand.GetAllAsync();
            
            if (dataField != null && All_CarBrands != null)
            {
                if (Exist_lang == "ExistAr")
                {
                    var existingCarBrand_Ar = All_CarBrands.FirstOrDefault(x =>
                        x.CrMasSupCarBrandArName == dataField);
                    if (existingCarBrand_Ar != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }
                else if (Exist_lang == "ExistEn")
                {
                    var existingCarBrand_En = All_CarBrands.FirstOrDefault(x =>
                        x.CrMasSupCarBrandEnName == dataField);
                    if (existingCarBrand_En != null)
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
