using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;
using System.Numerics;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class CarCheckupController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly ICarCheckup _carCheckup;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<CarCheckupController> _localizer;


        public CarCheckupController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, 
            IMapper mapper, IUserService userService, ICarCheckup carCheckup, IUserLoginsService userLoginsService, 
            IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment,
            IStringLocalizer<CarCheckupController> localizer) : base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _carCheckup = carCheckup;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {

            var titles = await setTitle("110", "1110003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupContractCarCheckup.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupContractCarCheckupStatus == "A").ToList();
            return View(contract);
        }

        [HttpGet]
        public PartialViewResult GetCarCheckupByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var CarCheckupbyStatusAll = _unitOfWork.CrMasSupContractCarCheckup.GetAll();
                    return PartialView("_DataTableCarCheckup", CarCheckupbyStatusAll);
                }
                var CarCheckupbyStatus = _unitOfWork.CrMasSupContractCarCheckup.FindAll(l => l.CrMasSupContractCarCheckupStatus == status).ToList();
                return PartialView("_DataTableCarCheckup", CarCheckupbyStatus);
            }
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> AddCarCheckup()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("110", "1110003", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var CarCheckupCode = "";
            var CarCheckups = await _unitOfWork.CrMasSupContractCarCheckup.GetAllAsync();
            if (CarCheckups.Count() != 0)
            {
                CarCheckupCode = (BigInteger.Parse(CarCheckups.LastOrDefault().CrMasSupContractCarCheckupCode) + 1).ToString();

            }
            else
            {
                CarCheckupCode = "20";
            }
            ViewBag.CarCheckupCode = CarCheckupCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCarCheckup(CarCheckupVM CarCheckup, IFormFile? AcceptImg, IFormFile? RejectImg, IFormFile? BlockImg)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";
            string filePathImageReject = "";
            string filePathImageBlock = "";


            if (ModelState.IsValid)
            {
                if (CarCheckup != null)
                {
                    var CarCheckupVMT = _mapper.Map<CrMasSupContractCarCheckup>(CarCheckup);
                    var CarCheckups = await _unitOfWork.CrMasSupContractCarCheckup.GetAllAsync();
                    var existingCarCheckup = CarCheckups.FirstOrDefault(x =>
                        x.CrMasSupContractCarCheckupEnName == CarCheckupVMT.CrMasSupContractCarCheckupEnName ||
                        x.CrMasSupContractCarCheckupArName == CarCheckupVMT.CrMasSupContractCarCheckupArName);

                    // Generate code for the second time
                    var CarCheckupCode = (BigInteger.Parse(CarCheckups.LastOrDefault().CrMasSupContractCarCheckupCode) + 1).ToString();
                    CarCheckup.CrMasSupContractCarCheckupCode = CarCheckupCode;
                    ViewBag.CarCheckupCode = CarCheckupCode;

                    if (existingCarCheckup != null)
                    {
                        if (existingCarCheckup.CrMasSupContractCarCheckupArName != null &&
                            existingCarCheckup.CrMasSupContractCarCheckupArName == CarCheckup.CrMasSupContractCarCheckupArName &&
                             existingCarCheckup.CrMasSupContractCarCheckupEnName != CarCheckup.CrMasSupContractCarCheckupEnName)
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
                        if (existingCarCheckup.CrMasSupContractCarCheckupEnName != null &&
                            existingCarCheckup.CrMasSupContractCarCheckupEnName == CarCheckup.CrMasSupContractCarCheckupEnName &&
                             existingCarCheckup.CrMasSupContractCarCheckupArName != CarCheckup.CrMasSupContractCarCheckupArName)
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
                        if (existingCarCheckup.CrMasSupContractCarCheckupArName != null && existingCarCheckup.CrMasSupContractCarCheckupEnName != null
                            && existingCarCheckup.CrMasSupContractCarCheckupEnName == CarCheckup.CrMasSupContractCarCheckupEnName &&
                           existingCarCheckup.CrMasSupContractCarCheckupArName == CarCheckup.CrMasSupContractCarCheckupArName)
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
                        return View(CarCheckup);
                    }


                    if (AcceptImg != null)
                    {
                        string fileNameImg = CarCheckup.CrMasSupContractCarCheckupEnName + "_Accept_" + CarCheckup.CrMasSupContractCarCheckupCode.ToString();
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }
                    if (RejectImg != null)
                    {
                        string fileNameImg = CarCheckup.CrMasSupContractCarCheckupEnName + "_Reject_" + CarCheckup.CrMasSupContractCarCheckupCode.ToString();
                        filePathImageReject = await RejectImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }
                    if (BlockImg != null)
                    {
                        string fileNameImg = CarCheckup.CrMasSupContractCarCheckupEnName + "_Block_" + CarCheckup.CrMasSupContractCarCheckupCode.ToString();
                        filePathImageBlock = await BlockImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }

                    CarCheckupVMT.CrMasSupContractCarCheckupRejectImage = filePathImageReject;
                    CarCheckupVMT.CrMasSupContractCarCheckupAcceptImage = filePathImageAccept;
                    CarCheckupVMT.CrMasSupContractCarCheckupBlockImage = filePathImageBlock;
                    CarCheckupVMT.CrMasSupContractCarCheckupStatus = "A";
                    await _unitOfWork.CrMasSupContractCarCheckup.AddAsync(CarCheckupVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("110", "1110003", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة فحص", "Add Checkup", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddCarCheckup", CarCheckup);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("110", "1110001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupContractCarCheckup.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var model = _mapper.Map<CarCheckupVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CarCheckupVM model, IFormFile? AcceptImg, IFormFile? RejectImg, IFormFile? BlockImg)
        {
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";
            string filePathImageReject = "";
            string filePathImageBlock = "";
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);


            if (user != null)
            {
                if (model != null)
                {

                    if (AcceptImg != null)
                    {
                        string fileNameImg = model.CrMasSupContractCarCheckupEnName + "_Accept_" + model.CrMasSupContractCarCheckupCode.ToString();
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupContractCarCheckupAcceptImage = filePathImageAccept;
                    }

                    if (RejectImg != null)
                    {
                        string fileNameImg = model.CrMasSupContractCarCheckupEnName + "_Reject_" + model.CrMasSupContractCarCheckupCode.ToString();
                        filePathImageReject = await RejectImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupContractCarCheckupRejectImage = filePathImageReject;

                    }

                    if (BlockImg != null)
                    {
                        string fileNameImg = model.CrMasSupContractCarCheckupEnName + "_Block_" + model.CrMasSupContractCarCheckupCode.ToString();
                        filePathImageBlock = await BlockImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupContractCarCheckupBlockImage = filePathImageBlock;
                    }


                    var contract = _mapper.Map<CrMasSupContractCarCheckup>(model);

                    _unitOfWork.CrMasSupContractCarCheckup.Update(contract);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("110", "1110001", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "CarCheckup");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupContractCarCheckup.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupContractCarCheckupStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    sAr = "حذف";
                    sEn = "Remove";
                    Contract.CrMasSupContractCarCheckupStatus = Status.Deleted;
                }
                else if (status == "Reactivate")
                {
                    sAr = "استرجاع";
                    sEn = "Retrive";
                    Contract.CrMasSupContractCarCheckupStatus = Status.Acive;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("110", "1110003", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "CarCheckup");
            }


            return View(Contract);

        }


    }
}
