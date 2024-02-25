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
    public class RenterIdTypeController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IRenterIdType _RenterIdType;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<RenterIdTypeController> _localizer;


        public RenterIdTypeController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IRenterIdType RenterIdType,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<RenterIdTypeController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _RenterIdType = RenterIdType;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {

            var titles = await setTitle("106", "1106002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupRenterIdtype.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupRenterIdtypeStatus == "A").ToList();
            var CarsInfo_count_all = _RenterIdType.GetAllRenterIdTypesCount();
            Tuple<IEnumerable<CrMasSupRenterIdtype>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupRenterIdtype>, List<List<string>>>(contract, CarsInfo_count_all);
            return View(tb);
        }

        [HttpGet]
        public PartialViewResult GetRenterIdTypeByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    //var RenterIdTypebyStatusAll = _unitOfWork.CrMasSupRenterIdtype.GetAll();
                    //return PartialView("_DataTableRenterIdType", RenterIdTypebyStatusAll);

                    var RenterIdTypebyStatusAll = _unitOfWork.CrMasSupRenterIdtype.FindAll(l => l.CrMasSupRenterIdtypeStatus == Status.Hold || l.CrMasSupRenterIdtypeStatus == Status.Active);
                    var CarsInfo_count_all1 = _RenterIdType.GetAllRenterIdTypesCount();
                    Tuple<IEnumerable<CrMasSupRenterIdtype>, List<List<string>>> tb1 = new Tuple<IEnumerable<CrMasSupRenterIdtype>, List<List<string>>>(RenterIdTypebyStatusAll, CarsInfo_count_all1);
                    return PartialView("_DataTableRenterIdType", tb1);
                }
                var RenterIdTypebyStatus = _unitOfWork.CrMasSupRenterIdtype.FindAll(l => l.CrMasSupRenterIdtypeStatus == status).ToList();
                var CarsInfo_count_all = _RenterIdType.GetAllRenterIdTypesCount();
                Tuple<IEnumerable<CrMasSupRenterIdtype>, List<List<string>>> tb = new Tuple<IEnumerable<CrMasSupRenterIdtype>, List<List<string>>>(RenterIdTypebyStatus, CarsInfo_count_all);
                return PartialView("_DataTableRenterIdType", tb);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddRenterIdType()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("106", "1106002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterIdTypeCode = "";
            var RenterIdTypes = await _unitOfWork.CrMasSupRenterIdtype.GetAllAsync();
            if (RenterIdTypes.Count() != 0)
            {
                RenterIdTypeCode = (BigInteger.Parse(RenterIdTypes.LastOrDefault().CrMasSupRenterIdtypeCode) + 1).ToString();
            }
            else
            {
                RenterIdTypeCode = "0";
            }
            ViewBag.RenterIdTypeCode = RenterIdTypeCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRenterIdType(RenterIdTypeVM RenterIdTypes)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;

            if (ModelState.IsValid)
            {
                if (RenterIdTypes != null)
                {
                    var RenterIdTypeVMT = _mapper.Map<CrMasSupRenterIdtype>(RenterIdTypes);
                    var All_RenterIdTypes = await _unitOfWork.CrMasSupRenterIdtype.GetAllAsync();
                    var existingRenterIdType_En = All_RenterIdTypes.FirstOrDefault(x =>
                        x.CrMasSupRenterIdtypeEnName == RenterIdTypeVMT.CrMasSupRenterIdtypeEnName);
                    var existingRenterIdType_Ar = All_RenterIdTypes.FirstOrDefault(x =>
                        x.CrMasSupRenterIdtypeArName == RenterIdTypeVMT.CrMasSupRenterIdtypeArName);

                    // Generate code for the second time
                    var RenterIdTypeCode = (BigInteger.Parse(All_RenterIdTypes.LastOrDefault().CrMasSupRenterIdtypeCode) + 1).ToString();
                    RenterIdTypes.CrMasSupRenterIdtypeCode = RenterIdTypeCode;
                    ViewBag.RenterIdTypeCode = RenterIdTypeCode;
                    if (RenterIdTypeVMT.CrMasSupRenterIdtypeArName != null && RenterIdTypeVMT.CrMasSupRenterIdtypeEnName != null)
                    {
                        if (existingRenterIdType_Ar != null && existingRenterIdType_En != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(RenterIdTypes);
                        }
                        else if (existingRenterIdType_En != null)
                        {
                            ModelState.AddModelError("ExistEn", _localizer["Existing"]);
                            return View(RenterIdTypes);
                        }
                        else if (existingRenterIdType_Ar != null)
                        {
                            ModelState.AddModelError("ExistAr", _localizer["Existing"]);
                            return View(RenterIdTypes);
                        }
                    }

                    RenterIdTypeVMT.CrMasSupRenterIdtypeStatus = "A";
                    await _unitOfWork.CrMasSupRenterIdtype.AddAsync(RenterIdTypeVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("106", "1106002", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddRenterIdType", RenterIdTypes);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("106", "1106002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupRenterIdtype.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            int countRenterIdTypes = 0;
            countRenterIdTypes = _RenterIdType.GetOneRenterIdTypeCount(id);
            ViewBag.RenterIdTypes_Count = countRenterIdTypes;
            var model = _mapper.Map<RenterIdTypeVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RenterIdTypeVM model)
        {
          
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);

            if (user != null)
            {
                if (model != null)
                {

                    var contract = _mapper.Map<CrMasSupRenterIdtype>(model);

                    _unitOfWork.CrMasSupRenterIdtype.Update(contract);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("106", "1106002", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "RenterIdType");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupRenterIdtype.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupRenterIdtypeStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    int CountRenterIdTypes = 0;
                    CountRenterIdTypes = _RenterIdType.GetOneRenterIdTypeCount(code);
                    if (CountRenterIdTypes == 0)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        Contract.CrMasSupRenterIdtypeStatus = Status.Deleted;
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
                    Contract.CrMasSupRenterIdtypeStatus = Status.Active;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("106", "1106002", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "RenterIdType");
            }


            return View(Contract);

        }

        [HttpPost]
        public async Task<IActionResult> CheckChangedField(string Exist_lang, string dataField)
        {
            var All_RenterIdTypes = await _unitOfWork.CrMasSupRenterIdtype.GetAllAsync();

            if (dataField != null && All_RenterIdTypes != null)
            {
                if (Exist_lang == "ExistAr")
                {
                    var existingRenterIdType_Ar = All_RenterIdTypes.FirstOrDefault(x =>
                        x.CrMasSupRenterIdtypeArName == dataField);
                    if (existingRenterIdType_Ar != null)
                    {
                        ModelState.AddModelError(Exist_lang, _localizer["Existing"]);
                        return View();
                    }
                }
                else if (Exist_lang == "ExistEn")
                {
                    var existingRenterIdType_En = All_RenterIdTypes.FirstOrDefault(x =>
                        x.CrMasSupRenterIdtypeEnName == dataField);
                    if (existingRenterIdType_En != null)
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
