using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.Identitiy;
using Bnan.Ui.ViewModels.MAS.UserValiditySystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.ViewModels.MAS;
using NToastNotify;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class PostRegionController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IPostRegion _postRegion;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<PostRegionController> _localizer;


        public PostRegionController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper,
            IUserService userService, IPostRegion postRegion, IUserLoginsService userLoginsService,
            IToastNotification toastNotification, IStringLocalizer<PostRegionController> localizer) : 
            base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _postRegion = postRegion;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var (mainTask, subTask, system, currentUser) = await SetTrace("108", "1108001", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            var titles = await setTitle("108", "1108001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var PostRegions = await _unitOfWork.CrMasSupPostRegion.GetAllAsync();
            var PostRegion = PostRegions.Where(x =>  x.CrMasSupPostRegionsStatus == "A").ToList();

            return View(PostRegion);
        }


        [HttpGet]
        public PartialViewResult GetPostRegionsByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var PostRegionbyStatusAll = _unitOfWork.CrMasSupPostRegion.GetAll();
                    return PartialView("_DataTablePostRegion", PostRegionbyStatusAll);
                }
                var PostRegionbyStatus = _unitOfWork.CrMasSupPostRegion.FindAll(l => l.CrMasSupPostRegionsStatus == status).ToList();
                return PartialView("_DataTablePostRegion", PostRegionbyStatus);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddPostRegion()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("108", "1108001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var PostRegions = await _unitOfWork.CrMasSupPostRegion.GetAllAsync();

            var PostRegionCode = (int.Parse(PostRegions.LastOrDefault().CrMasSupPostRegionsCode) + 1).ToString();
            ViewBag.PostRegionCode = PostRegionCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPostRegion(PostRegionVM PostRegionmodel)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            if (ModelState.IsValid)
            {
                if (PostRegionmodel != null)
                {
                    var PostRegions = await _unitOfWork.CrMasSupPostRegion.GetAllAsync();
                    var existingPostRegion = PostRegions.FirstOrDefault(x =>
                        x.CrMasSupPostRegionsEnName == PostRegionmodel.CrMasSupPostRegionsEnName ||
                        x.CrMasSupPostRegionsArName == PostRegionmodel.CrMasSupPostRegionsArName);

                    // Generate code for the second time
                    var PostRegionCode = (int.Parse(PostRegions.LastOrDefault().CrMasSupPostRegionsCode) + 1).ToString();
                    PostRegionmodel.CrMasSupPostRegionsCode = PostRegionCode;
                    ViewBag.PostRegionCode = PostRegionCode;

                    if (existingPostRegion != null)
                    {
                        if (existingPostRegion.CrMasSupPostRegionsArName != null &&
                            existingPostRegion.CrMasSupPostRegionsArName == PostRegionmodel.CrMasSupPostRegionsArName &&
                             existingPostRegion.CrMasSupPostRegionsEnName != PostRegionmodel.CrMasSupPostRegionsEnName)
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
                        if (existingPostRegion.CrMasSupPostRegionsEnName != null &&
                            existingPostRegion.CrMasSupPostRegionsEnName == PostRegionmodel.CrMasSupPostRegionsEnName &&
                             existingPostRegion.CrMasSupPostRegionsArName != PostRegionmodel.CrMasSupPostRegionsArName)
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
                        if (existingPostRegion.CrMasSupPostRegionsArName != null && existingPostRegion.CrMasSupPostRegionsEnName != null
                            && existingPostRegion.CrMasSupPostRegionsEnName == PostRegionmodel.CrMasSupPostRegionsEnName &&
                           existingPostRegion.CrMasSupPostRegionsArName == PostRegionmodel.CrMasSupPostRegionsArName)
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
                        return View(PostRegionmodel);
                    }
                    PostRegionmodel.CrMasSupPostRegionsStatus = "A";
                    var PostRegionVMTPostRegion = _mapper.Map<CrMasSupPostRegion>(PostRegionmodel);
                    await _unitOfWork.CrMasSupPostRegion.AddAsync(PostRegionVMTPostRegion);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("108", "1108001", "1");
                    var RecordAr = PostRegionVMTPostRegion.CrMasSupPostRegionsArName;
                    var RecordEn = PostRegionVMTPostRegion.CrMasSupPostRegionsEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });


                }
                return RedirectToAction("Index");
            }
            return View("AddPostRegion", PostRegionmodel);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("108", "1108001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var PostRegion = await _unitOfWork.CrMasSupPostRegion.GetByIdAsync(id);
            if (PostRegion == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            if (id == "00")
            {
                ModelState.AddModelError("Exist", "You Can't Edit This.");
                var PostRegion2 = await _unitOfWork.CrMasSupPostRegion.GetAllAsync();
                return View("Index", PostRegion2);

            }
            var model = _mapper.Map<PostRegionVM>(PostRegion);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PostRegionVM model)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            var PostRegion = _mapper.Map<CrMasSupPostRegion>(model);

            if (user != null)
            {
                if (PostRegion != null)
                {
                    _unitOfWork.CrMasSupPostRegion.Update(PostRegion);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("108", "1108001", "1");
                    var RecordAr = PostRegion.CrMasSupPostRegionsArName;
                    var RecordEn = PostRegion.CrMasSupPostRegionsEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "PostRegion");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var PostRegion = await _unitOfWork.CrMasSupPostRegion.GetByIdAsync(code);
            if (PostRegion != null)
            {
                if (await CheckUserSubValidationProcdures("1108001", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Hold";
                        PostRegion.CrMasSupPostRegionsStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        PostRegion.CrMasSupPostRegionsStatus = Status.Deleted;
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع";
                        sEn = "Retrive";
                        PostRegion.CrMasSupPostRegionsStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("108", "1108001", "1");
                    var RecordAr = PostRegion.CrMasSupPostRegionsArName;
                    var RecordEn = PostRegion.CrMasSupPostRegionsEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                return RedirectToAction("Index", "PostRegion");
            }
        }


            return View(PostRegion);

    }
}
}
