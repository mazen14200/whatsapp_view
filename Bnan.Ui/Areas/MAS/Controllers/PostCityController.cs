using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.MAS;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System;
using System.Globalization;
using System.Numerics;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class PostCityController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IPostCity _PostCityService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<PostCityController> _localizer;



        public PostCityController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, 
            IMapper mapper, IUserService userService, IPostCity PostCityService,
            IUserLoginsService userLoginsService, IToastNotification toastNotification,
           IStringLocalizer<PostCityController> localizer) :
            base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _PostCityService = PostCityService;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var titles = await setTitle("108", "1108002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var PostCitys = await _unitOfWork.CrMasSupPostCity.GetAllAsync();
            var PostCity = PostCitys.Where(x => x.CrMasSupPostCityStatus == "A").ToList();

            return View(PostCity);
        }


        [HttpGet]
        public PartialViewResult GetPostCityByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var PostCitybyStatusAll = _unitOfWork.CrMasSupPostCity.GetAll();
                    return PartialView("_DataTablePostCity", PostCitybyStatusAll);
                }
                var PostCitybyStatus = _unitOfWork.CrMasSupPostCity.FindAll(l => l.CrMasSupPostCityStatus == status).ToList();
                return PartialView("_DataTablePostCity", PostCitybyStatus);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddPostCity()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("108", "1108002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var PostCity = await _unitOfWork.CrMasSupPostCity.GetAllAsync();

            var PostCityCode = (BigInteger.Parse(PostCity.LastOrDefault().CrMasSupPostCityCode) + 1).ToString();
            ViewBag.PostCityCode = PostCityCode;
            return View();
        }

        [HttpGet]
        public JsonResult GetPostCityRegionNameAr(string? prefix)
        {

            var res = _unitOfWork.CrMasSupPostRegion.GetAll();
            var list = res.ToList();
            var CountriesArabic = (from c in list
                                   where c.CrMasSupPostRegionsArName.Contains(prefix) &&c.CrMasSupPostRegionsStatus=="A"
                                   select new
                                   {
                                       label = c.CrMasSupPostRegionsArName,
                                       val = c.CrMasSupPostRegionsArName
                                   }).ToList();

            return Json(CountriesArabic);
        }
        [HttpGet]
        public JsonResult GetPostCityRegionNameEn(string? prefix)
        {

            var res = _unitOfWork.CrMasSupPostRegion.GetAll();
            var list = res.ToList();
            var CountriesEnglish = (from c in list
                                    where c.CrMasSupPostRegionsEnName.Contains(prefix) && c.CrMasSupPostRegionsStatus=="A"
                                    select new
                                    {
                                        label = c.CrMasSupPostRegionsEnName,
                                        val = c.CrMasSupPostRegionsEnName
                                    }).ToList();
            return Json(CountriesEnglish);
        }

        [HttpGet]
        public ActionResult GetCode(string selectedOption)
        {

            var res = _unitOfWork.CrMasSupPostRegion.GetAll();
            var list = res.ToList();
            var CountriesCode = (from c in list
                                 where c.CrMasSupPostRegionsArName == selectedOption || c.CrMasSupPostRegionsEnName == selectedOption
                                 select c.CrMasSupPostRegionsCode).ToList()[0].ToString();

            var list2 = res.ToList();
            var data2 = (from c in list
                         where c.CrMasSupPostRegionsCode == CountriesCode
                         select new
                         {
                             c.CrMasSupPostRegionsStatus
                         }).ToList();

            ViewBag.PostCityRegionCode = CountriesCode;
            ViewBag.PostCityRegionStatus = data2[0].CrMasSupPostRegionsStatus.ToString();
            var Status = data2[0].CrMasSupPostRegionsStatus.ToString();

            Console.WriteLine("Stataus    ...     "+Status);
            return Json(new { data1 = CountriesCode, data2 = Status });
        }


        [HttpGet]
        public ActionResult GetData(string code, string CitynameAr)
        {

            var res = _unitOfWork.CrMasSupPostRegion.GetAll();
            var list = res.ToList();
            var data = (from c in list
                        where c.CrMasSupPostRegionsCode == code
                        select new
                        {
                            c.CrMasSupPostRegionsArName
                        }).ToList();

            ViewBag.PostCityConcateAr = CitynameAr + " - " + data[0].CrMasSupPostRegionsArName.ToString();
            var concate = CitynameAr + " - " + data[0].CrMasSupPostRegionsArName.ToString();
            return Json(new { data1 = concate });
        }

        [HttpGet]
        public ActionResult GetDataEn(string code, string CitynameEn)
        {

            var res = _unitOfWork.CrMasSupPostRegion.GetAll();
            var list = res.ToList();
            var data = (from c in list
                        where c.CrMasSupPostRegionsCode == code
                        select new
                        {
                            c.CrMasSupPostRegionsEnName
                        }).ToList();

            ViewBag.PostCityConcateEn = CitynameEn + " - " + data[0].CrMasSupPostRegionsEnName.ToString();
            var concate = CitynameEn + " - " + data[0].CrMasSupPostRegionsEnName.ToString();
            return Json(new { data1 = concate });
        }

        [HttpPost]
        public async Task<IActionResult> AddPostCity(PostCityVM PostCitymodel)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            if (ModelState.IsValid)
            {
                if (PostCitymodel != null)
                {
                    var PostCitys = await _unitOfWork.CrMasSupPostCity.GetAllAsync();
                    var existingPostCity = PostCitys.FirstOrDefault(x =>
                        x.CrMasSupPostCityEnName == PostCitymodel.CrMasSupPostCityEnName ||
                        x.CrMasSupPostCityArName == PostCitymodel.CrMasSupPostCityArName);

                    // Generate code for the second time
                    var PostCityCode = (int.Parse(PostCitys.LastOrDefault().CrMasSupPostCityCode) + 1).ToString();
                    PostCitymodel.CrMasSupPostCityCode = PostCityCode;
                    ViewBag.PostCityCode = PostCityCode;

                    if (existingPostCity != null)
                    {
                        if (existingPostCity.CrMasSupPostCityArName != null &&
                            existingPostCity.CrMasSupPostCityArName == PostCitymodel.CrMasSupPostCityArName &&
                             existingPostCity.CrMasSupPostCityEnName != PostCitymodel.CrMasSupPostCityEnName)
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
                        if (existingPostCity.CrMasSupPostCityEnName != null &&
                            existingPostCity.CrMasSupPostCityEnName == PostCitymodel.CrMasSupPostCityEnName &&
                             existingPostCity.CrMasSupPostCityArName != PostCitymodel.CrMasSupPostCityArName)
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
                        if (existingPostCity.CrMasSupPostCityArName != null && existingPostCity.CrMasSupPostCityEnName != null
                            && existingPostCity.CrMasSupPostCityEnName == PostCitymodel.CrMasSupPostCityEnName &&
                           existingPostCity.CrMasSupPostCityArName == PostCitymodel.CrMasSupPostCityArName)
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
                        return View(PostCitymodel);
                    }
                    PostCitymodel.CrMasSupPostCityStatus = "A";
                    PostCitymodel.CrMasSupPostCityCounter = 0;
                    PostCitymodel.CrMasSupPostCityGroupCode = "17";

                    var PostCityVMTPostCity = _mapper.Map<CrMasSupPostCity>(PostCitymodel);
                    await _unitOfWork.CrMasSupPostCity.AddAsync(PostCityVMTPostCity);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("108", "1108002", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة مدينة", "Add city", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddPostCity", PostCitymodel);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("108", "1108002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var PostCity = await _unitOfWork.CrMasSupPostCity.GetByIdAsync(id);
            if (PostCity == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            if (id == "00")
            {
                ModelState.AddModelError("Exist", "You Can't Edit This.");
                var PostCity2 = await _unitOfWork.CrMasSupPostCity.GetAllAsync();
                return View("Index", PostCity2);

            }
            var model = _mapper.Map<PostCityVM>(PostCity);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PostCityVM model)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            var PostCity = _mapper.Map<CrMasSupPostCity>(model);

            if (user != null)
            {
                if (PostCity != null)
                {
                    _unitOfWork.CrMasSupPostCity.Update(PostCity);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("108", "1108002", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "PostCity");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var PostCity = await _unitOfWork.CrMasSupPostCity.GetByIdAsync(code);
            if (PostCity != null)
            {
                if (await CheckUserSubValidationProcdures("1108002", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "PostCity";
                        PostCity.CrMasSupPostCityStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "PostCity";
                        PostCity.CrMasSupPostCityStatus = Status.Deleted;
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع";
                        sEn = "PostCity";
                        PostCity.CrMasSupPostCityStatus = Status.Acive;
                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("108", "1108002", "1");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    return RedirectToAction("Index", "PostCity");
                }
            }


            return View(PostCity);

        }

    }
}
