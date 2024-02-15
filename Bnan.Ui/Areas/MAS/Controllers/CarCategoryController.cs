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
    public class CarCategoryController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IMasCarCategory _carCategory;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<CarCategoryController> _localizer;


        public CarCategoryController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IMasCarCategory carCategory,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<CarCategoryController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _carCategory = carCategory;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {

            var titles = await setTitle("107", "1107006", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupCarCategory.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupCarCategoryStatus == "A").ToList();
            var CarsInfo_count_all = _carCategory.GetAllCarCategorysCount();
            Tuple<IEnumerable<CrMasSupCarCategory>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarCategory>, List<List<string>>>(contract, CarsInfo_count_all);
            return View(tb);
        }

        [HttpGet]
        public PartialViewResult GetCarCategoryByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    //var CarCategorybyStatusAll = _unitOfWork.CrMasSupCarCategory.GetAll();
                    //return PartialView("_DataTableCarCategory", CarCategorybyStatusAll);

                    var CarCategorybyStatusAll = _unitOfWork.CrMasSupCarCategory.FindAll(l => l.CrMasSupCarCategoryStatus == Status.Hold || l.CrMasSupCarCategoryStatus == Status.Active);
                    var CarsInfo_count_all1 = _carCategory.GetAllCarCategorysCount();
                    Tuple<IEnumerable<CrMasSupCarCategory>, List<List<string>>> tb1 = new Tuple<IEnumerable<CrMasSupCarCategory>, List<List<string>>>(CarCategorybyStatusAll, CarsInfo_count_all1);
                    return PartialView("_DataTableCarCategory", tb1);
                }
                var CarCategorybyStatus = _unitOfWork.CrMasSupCarCategory.FindAll(l => l.CrMasSupCarCategoryStatus == status).ToList();
                var CarsInfo_count_all = _carCategory.GetAllCarCategorysCount();
                Tuple<IEnumerable<CrMasSupCarCategory>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupCarCategory>, List<List<string>>>(CarCategorybyStatus, CarsInfo_count_all);
                return PartialView("_DataTableCarCategory", tb);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddCarCategory()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("107", "1107006", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var CarCategoryCode = "";
            var CarCategorys = await _unitOfWork.CrMasSupCarCategory.GetAllAsync();
            if (CarCategorys.Count() != 0)
            {
                CarCategoryCode = (BigInteger.Parse(CarCategorys.LastOrDefault().CrMasSupCarCategoryCode) + 1).ToString();
            }
            else
            {
                CarCategoryCode = "3400000001";
            }
            ViewBag.CarCategoryCode = CarCategoryCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCarCategory(CarCategoryVM CarCategorys)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;

            if (ModelState.IsValid)
            {
                if (CarCategorys != null)
                {
                    var CarCategoryVMT = _mapper.Map<CrMasSupCarCategory>(CarCategorys);
                    var All_CarCategorys = await _unitOfWork.CrMasSupCarCategory.GetAllAsync();
                    var existingCarCategory_En = All_CarCategorys.FirstOrDefault(x =>
                        x.CrMasSupCarCategoryEnName == CarCategoryVMT.CrMasSupCarCategoryEnName);
                    var existingCarCategory_Ar = All_CarCategorys.FirstOrDefault(x =>
                        x.CrMasSupCarCategoryArName == CarCategoryVMT.CrMasSupCarCategoryArName);

                    // Generate code for the second time
                    var CarCategoryCode = (BigInteger.Parse(All_CarCategorys.LastOrDefault().CrMasSupCarCategoryCode) + 1).ToString();
                    CarCategorys.CrMasSupCarCategoryCode = CarCategoryCode;
                    ViewBag.CarCategoryCode = CarCategoryCode;
                    if (CarCategoryVMT.CrMasSupCarCategoryArName != null && CarCategoryVMT.CrMasSupCarCategoryEnName != null)
                    {
                        if (existingCarCategory_Ar != null && existingCarCategory_En != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarCategorys);
                        }
                        else if (existingCarCategory_En != null)
                        {
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(CarCategorys);
                        }
                        else if (existingCarCategory_Ar != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            return View(CarCategorys);
                        }
                    }

                    CarCategoryVMT.CrMasSupCarCategoryStatus = "A";
                    CarCategoryVMT.CrMasSupCarCategoryGroup = "33";
                    await _unitOfWork.CrMasSupCarCategory.AddAsync(CarCategoryVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107006", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddCarCategory", CarCategorys);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("107", "1107006", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupCarCategory.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            int countCarCategorys = 0;
            countCarCategorys = _carCategory.GetOneCarCategoryCount(id);
            ViewBag.CarCategorys_Count = countCarCategorys;
            var model = _mapper.Map<CarCategoryVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CarCategoryVM model)
        {
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);

            if (user != null)
            {
                if (model != null)
                {

                    var contract = _mapper.Map<CrMasSupCarCategory>(model);

                    _unitOfWork.CrMasSupCarCategory.Update(contract);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107006", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "CarCategory");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupCarCategory.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupCarCategoryStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    int CountCarCategorys = 0;
                    CountCarCategorys = _carCategory.GetOneCarCategoryCount(code);
                    if (CountCarCategorys == 0)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        Contract.CrMasSupCarCategoryStatus = Status.Deleted;
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
                    Contract.CrMasSupCarCategoryStatus = Status.Active;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107006", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "CarCategory");
            }


            return View(Contract);

        }

        [HttpPost]
        public async Task<IActionResult> CheckChangedField(string Exist_lang, string dataField)
        {
            var All_CarCategorys = await _unitOfWork.CrMasSupCarCategory.GetAllAsync();

            if (dataField != null && All_CarCategorys != null)
            {
                if (Exist_lang == "ExistAr")
                {
                    var existingCarCategory_Ar = All_CarCategorys.FirstOrDefault(x =>
                        x.CrMasSupCarCategoryArName == dataField);
                    if (existingCarCategory_Ar != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }
                else if (Exist_lang == "ExistEn")
                {
                    var existingCarCategory_En = All_CarCategorys.FirstOrDefault(x =>
                        x.CrMasSupCarCategoryEnName == dataField);
                    if (existingCarCategory_En != null)
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
