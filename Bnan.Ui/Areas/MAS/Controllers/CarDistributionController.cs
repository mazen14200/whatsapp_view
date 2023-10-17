using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.Areas.CAS.Controllers;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Bnan.Ui.Areas.MAS.Controllers
{

    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class CarDistributionController : BaseController
    {
        private readonly IStringLocalizer<LessorsKSAController> _localizer;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICarDistribution _CarDistribution;
        private readonly IToastNotification _toastNotification;
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _UserService;


        public CarDistributionController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<LessorsKSAController> localizer, IWebHostEnvironment webHostEnvironment, ICarDistribution carDistribution, IToastNotification toastNotification, IUserLoginsService userLoginsService, IUserService userService) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _webHostEnvironment = webHostEnvironment;
            _CarDistribution = carDistribution;
            _toastNotification = toastNotification;
            _userLoginsService = userLoginsService;
            _UserService = userService;
        }

        public async Task<IActionResult> CarDistribution()
        {
            //sidebar active
            ViewBag.id = "#sidebarcarsservices";
            ViewBag.no = "5";

            // set title
            var titles = await setTitle("107", "1107007", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            //check user sub validation
            var uservalidation = await CheckUserSubValidation("1107007");
            if (uservalidation == false) return RedirectToAction("index", "home", new { area = "mas" });

            var cardistribution = await _unitOfWork.CrMasSupCarDistribution.GetAllAsync();
            return View(cardistribution);

        }

        [HttpGet]
        public async Task<IActionResult> AddCarDistribution()
        {
            // Pass the category Arabic key to the view 
            var categoryAr = await _unitOfWork.CrMasSupCarCategory.GetAllAsync();
            var categoryDropDownAr = categoryAr.Select(c => new SelectListItem { Value = c.CrMasSupCarCategoryCode?.ToString(), Text = c.CrMasSupCarCategoryArName }).ToList();
            categoryDropDownAr.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            ViewData["categoryAr"] = categoryDropDownAr;

            // Pass the category Arabic key to the view 
            var categoryEn = await _unitOfWork.CrMasSupCarCategory.GetAllAsync();
            var categoryDropDownEn = categoryEn.Select(c => new SelectListItem { Value = c.CrMasSupCarCategoryCode?.ToString(), Text = c.CrMasSupCarCategoryEnName }).ToList();
            categoryDropDownEn.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            ViewData["categoryEn"] = categoryDropDownEn;

            // Pass the Model English  key to the view 
            var ModelEn = await _unitOfWork.CrMasSupCarModel.GetAllAsync();
            var ModelDropDownEn = ModelEn.Select(c => new SelectListItem { Value = c.CrMasSupCarModelCode?.ToString(), Text = c.CrMasSupCarModelConcatenateEnName }).ToList();
            ModelDropDownEn.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            ViewData["ModelEn"] = ModelDropDownEn;

            // Pass the Model English key to the view 
            var ModelAr = await _unitOfWork.CrMasSupCarModel.GetAllAsync();
            var ModelDropDownAr = ModelAr.Select(c => new SelectListItem { Value = c.CrMasSupCarModelCode?.ToString(), Text = c.CrMasSupCarModelArConcatenateName }).ToList();
            ModelDropDownAr.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            ViewData["ModelAr"] = ModelDropDownAr;

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddCarDistribution(CrMasSupCarDistributionVM crMasSupCarDistributionVM, IFormFile? CarDistributionFile)
        {
            if (crMasSupCarDistributionVM.CrMasSupCarDistributionModel == "") ModelState.AddModelError("CrMasSupCarDistributionModel", _localizer["requiredFiled"]);
            if (crMasSupCarDistributionVM.CrMasSupCarDistributionCategory == "") ModelState.AddModelError("CrMasSupCarDistributionCategory", _localizer["requiredFiled"]);
            if (crMasSupCarDistributionVM.CrMasSupCarDistributionYear == "") ModelState.AddModelError("CrMasSupCarDistributionYear", _localizer["requiredFiled"]);
            bool isExists =  _unitOfWork.CrMasSupCarDistribution.FindAll(x => x.CrMasSupCarDistributionModel == crMasSupCarDistributionVM.CrMasSupCarDistributionModel &&
            x.CrMasSupCarDistributionYear == crMasSupCarDistributionVM.CrMasSupCarDistributionYear).Count() > 0;
            if (isExists) ModelState.AddModelError("IsExist", _localizer["IsExistModel"]);

            if (ModelState.IsValid)
            {
                var CarDistribution = _unitOfWork.CrMasSupCarDistribution.FindAll(l => l.CrMasSupCarDistributionYear == crMasSupCarDistributionVM.CrMasSupCarDistributionYear)
               .Max(x => x.CrMasSupCarDistributionCode.Substring(x.CrMasSupCarDistributionCode.Length - 5, 5));
                string Serial;
                if (CarDistribution != null)
                {
                    Int64 val = Int64.Parse(CarDistribution) + 1;
                    Serial = val.ToString("000000");
                }
                else
                {
                    Serial = "000001";
                }

                var crMasSupCarDistribution = _mapper.Map<CrMasSupCarDistribution>(crMasSupCarDistributionVM);
                crMasSupCarDistribution.CrMasSupCarDistributionCode = crMasSupCarDistributionVM.CrMasSupCarDistributionYear + Serial;
                string foldername = $"{"images\\Bnan\\Models"}";
                string filePathImage;

                if (CarDistributionFile != null)
                {
                    string fileNameImg = crMasSupCarDistribution.CrMasSupCarDistributionCode;
                    //string fileNameImgExtenstion = Path.GetExtension(UserImgFile.FileName);
                    filePathImage = await CarDistributionFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    crMasSupCarDistribution.CrMasSupCarDistributionImage = filePathImage;
                }
                else
                {
                    crMasSupCarDistribution.CrMasSupCarDistributionImage = "~/images/common/DefaultCar.png";
                }

                await _CarDistribution.AddCarDisribtion(crMasSupCarDistribution);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("CarDistribution");
            }

            // Pass the category Arabic key to the view 
            var categoryAr = await _unitOfWork.CrMasSupCarCategory.GetAllAsync();
            var categoryDropDownAr = categoryAr.Select(c => new SelectListItem { Value = c.CrMasSupCarCategoryCode?.ToString(), Text = c.CrMasSupCarCategoryArName }).ToList();
            //categoryDropDownAr.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            ViewData["categoryAr"] = categoryDropDownAr;

            // Pass the category Arabic key to the view 
            var categoryEn = await _unitOfWork.CrMasSupCarCategory.GetAllAsync();
            var categoryDropDownEn = categoryEn.Select(c => new SelectListItem { Value = c.CrMasSupCarCategoryCode?.ToString(), Text = c.CrMasSupCarCategoryEnName }).ToList();
            //categoryDropDownEn.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            ViewData["categoryEn"] = categoryDropDownEn;

            // Pass the Model English  key to the view 
            var ModelEn = await _unitOfWork.CrMasSupCarModel.GetAllAsync();
            var ModelDropDownEn = ModelEn.Select(c => new SelectListItem { Value = c.CrMasSupCarModelCode?.ToString(), Text = c.CrMasSupCarModelConcatenateEnName }).ToList();
            //ModelDropDownEn.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            ViewData["ModelEn"] = ModelDropDownEn;

            // Pass the Model English key to the view 
            var ModelAr = await _unitOfWork.CrMasSupCarModel.GetAllAsync();
            var ModelDropDownAr = ModelAr.Select(c => new SelectListItem { Value = c.CrMasSupCarModelCode?.ToString(), Text = c.CrMasSupCarModelArConcatenateName }).ToList();
            //ModelDropDownAr.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            ViewData["ModelAr"] = ModelDropDownAr;

            // SaveTracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107007", "1");
            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "إضافه", "Add", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
            return View(crMasSupCarDistributionVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            var DistruibtionCar = await _unitOfWork.CrMasSupCarDistribution.GetByIdAsync(id);
            var DistruibtionCarVM = _mapper.Map<CrMasSupCarDistributionVM>(DistruibtionCar);
            return View(DistruibtionCarVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CrMasSupCarDistributionVM CrMasSupCarDistributionVM, IFormFile? CarDistributionFile)
        {
            if (ModelState.IsValid)
            {
                var CrMasSupCarDistribution = _mapper.Map<CrMasSupCarDistribution>(CrMasSupCarDistributionVM);
                string foldername = $"{"images\\Bnan\\Models"}";
                string filePathImage;

                if (CarDistributionFile != null)
                {
                    string fileNameImg = CrMasSupCarDistribution.CrMasSupCarDistributionModel;
                    filePathImage = await CarDistributionFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    CrMasSupCarDistribution.CrMasSupCarDistributionImage = filePathImage;
                }
                else if (CrMasSupCarDistributionVM.CrMasSupCarDistributionImage != null)
                {
                    CrMasSupCarDistribution.CrMasSupCarDistributionImage = CrMasSupCarDistributionVM.CrMasSupCarDistributionImage;
                }
                else {
                    CrMasSupCarDistribution.CrMasSupCarDistributionImage = "~/images/common/DefaultCar.png";
                }

                await _CarDistribution.editCarDisribtion(CrMasSupCarDistribution);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                // SaveTracing
                var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107007", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                return RedirectToAction("CarDistribution");
            }

            return View(CrMasSupCarDistributionVM);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetCarDistributionByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var LessorbyStatusAll = await _unitOfWork.CrMasSupCarDistribution.GetAllAsync();
                    return PartialView("_DataTableDistribution", LessorbyStatusAll);
                }
                var LessorbyStatus = _unitOfWork.CrMasSupCarDistribution.FindAll(l => l.CrMasSupCarDistributionStatus == status).ToList();
                return PartialView("_DataTableDistribution", LessorbyStatus);
            }
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var CarDistribution = await _unitOfWork.CrMasSupCarDistribution.GetByIdAsync(code);
            if (CarDistribution != null)
            {

                if (await CheckUserSubValidationProcdures("1107007", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Hold";
                        CarDistribution.CrMasSupCarDistributionStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Remove ";
                        CarDistribution.CrMasSupCarDistributionStatus = Status.Deleted; 
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع";
                        sEn = "Retrive";
                        CarDistribution.CrMasSupCarDistributionStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("107", "1107007", "1");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    return RedirectToAction("Index", "LessorsKSA");
                }
            }


            return View(CarDistribution);

        }

    }
}
