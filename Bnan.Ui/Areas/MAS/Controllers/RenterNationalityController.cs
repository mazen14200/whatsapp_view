using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
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
    public class RenterNationalityController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IRenterNationality _renterNationality;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<ContractAdditionalController> _localizer;
        private readonly IWebHostEnvironment _;


        public RenterNationalityController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IRenterNationality renterNationality, 
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<ContractAdditionalController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _renterNationality = renterNationality;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {

            var titles = await setTitle("106", "1106004", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupRenterNationality.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupRenterNationalitiesStatus == "A").ToList();
            return View(contract);
        }

        [HttpGet]
        public PartialViewResult GetRenterNationalityByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var RenterNationalitybyStatusAll = _unitOfWork.CrMasSupRenterNationality.GetAll();
                    return PartialView("_DataTableRenterNationality", RenterNationalitybyStatusAll);
                }
                var RenterNationalitybyStatus = _unitOfWork.CrMasSupRenterNationality.FindAll(l => l.CrMasSupRenterNationalitiesStatus == status).ToList();
                return PartialView("_DataTableRenterNationality", RenterNationalitybyStatus);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddRenterNationality()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("106", "1106004", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterNationalityCode = "";
            var RenterNationalitys = await _unitOfWork.CrMasSupRenterNationality.GetAllAsync();
            if (RenterNationalitys.Count() != 0)
            {
                RenterNationalityCode = (BigInteger.Parse(RenterNationalitys.LastOrDefault().CrMasSupRenterNationalitiesCode) + 1).ToString();
            }
            else
            {
                RenterNationalityCode = "1000000001";
            }
            ViewBag.RenterNationalityCode = RenterNationalityCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRenterNationality(RenterNationalityVM RenterNationalitys, IFormFile? AcceptImg)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";


            if (ModelState.IsValid)
            {
                if (RenterNationalitys != null)
                {
                    var RenterNationalityVMT = _mapper.Map<CrMasSupRenterNationality>(RenterNationalitys);
                    var All_RenterNationalitys = await _unitOfWork.CrMasSupRenterNationality.GetAllAsync();
                    var existingRenterNationality = All_RenterNationalitys.FirstOrDefault(x =>
                        x.CrMasSupRenterNationalitiesEnName == RenterNationalityVMT.CrMasSupRenterNationalitiesEnName ||
                        x.CrMasSupRenterNationalitiesArName == RenterNationalityVMT.CrMasSupRenterNationalitiesArName);

                    // Generate code for the second time
                    var RenterNationalityCode = (BigInteger.Parse(All_RenterNationalitys.LastOrDefault().CrMasSupRenterNationalitiesCode) + 1).ToString();
                    RenterNationalitys.CrMasSupRenterNationalitiesCode = RenterNationalityCode;
                    ViewBag.RenterNationalityCode = RenterNationalityCode;

                    if (existingRenterNationality != null)
                    {
                        if (existingRenterNationality.CrMasSupRenterNationalitiesArName != null &&
                            existingRenterNationality.CrMasSupRenterNationalitiesArName == RenterNationalitys.CrMasSupRenterNationalitiesArName &&
                                existingRenterNationality.CrMasSupRenterNationalitiesArName != RenterNationalitys.CrMasSupRenterNationalitiesEnName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistAr", "هذا الحقل مسجل من قبل.");
                            }
                            else
                            {
                                ModelState.AddModelError("ExistAr", "This field is Existed.");
                            }

                        }
                        if (existingRenterNationality.CrMasSupRenterNationalitiesEnName != null &&
                            existingRenterNationality.CrMasSupRenterNationalitiesEnName == RenterNationalitys.CrMasSupRenterNationalitiesEnName &&
                                existingRenterNationality.CrMasSupRenterNationalitiesArName != RenterNationalitys.CrMasSupRenterNationalitiesArName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistEn", "هذا الحقل مسجل من قبل.");
                            }
                            else
                            {
                                ModelState.AddModelError("ExistEn", "This field is Existed.");
                            }
                        }
                        if (existingRenterNationality.CrMasSupRenterNationalitiesArName != null && existingRenterNationality.CrMasSupRenterNationalitiesEnName != null
                            && existingRenterNationality.CrMasSupRenterNationalitiesEnName == RenterNationalitys.CrMasSupRenterNationalitiesEnName &&
                            existingRenterNationality.CrMasSupRenterNationalitiesArName == RenterNationalitys.CrMasSupRenterNationalitiesArName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistEn", "هذا الحقل مسجل من قبل.");
                                ModelState.AddModelError("ExistAr", "هذا الحقل مسجل من قبل.");

                            }
                            else
                            {
                                ModelState.AddModelError("ExistAr", "This field is Existed.");
                                ModelState.AddModelError("ExistEn", "This field is Existed.");
                            }
                        }
                        return View(RenterNationalitys);
                    }


                    if (AcceptImg != null)
                    {
                        string fileNameImg = RenterNationalitys.CrMasSupRenterNationalitiesEnName + "_Accept_" + RenterNationalitys.CrMasSupRenterNationalitiesCode.ToString().Substring(RenterNationalitys.CrMasSupRenterNationalitiesCode.ToString().Length - 3);
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }


                    //RenterNationalityVMT.CrMasSupRenterNationalityImage = filePathImageAccept;
                    RenterNationalityVMT.CrMasSupRenterNationalitiesStatus = "A";
                    await _unitOfWork.CrMasSupRenterNationality.AddAsync(RenterNationalityVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("106", "1106004", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة اضافات", "Add Additional", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddRenterNationality", RenterNationalitys);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("106", "1106004", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupRenterNationality.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var model = _mapper.Map<RenterNationalityVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RenterNationalityVM model)
        {
            //string foldername = $"{"images\\Common"}";
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);


            if (user != null)
            {
                if (model != null)
                {

                    var contract = _mapper.Map<CrMasSupRenterNationality>(model);

                    _unitOfWork.CrMasSupRenterNationality.Update(contract);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("106", "1106004", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "RenterNationality");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupRenterNationality.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupRenterNationalitiesStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    sAr = "حذف";
                    sEn = "Remove";
                    Contract.CrMasSupRenterNationalitiesStatus = Status.Deleted;
                }
                else if (status == "Reactivate")
                {
                    sAr = "استرجاع";
                    sEn = "Retrive";
                    Contract.CrMasSupRenterNationalitiesStatus = Status.Active;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("106", "1106004", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "RenterNationality");
            }


            return View(Contract);

        }


    }
}
