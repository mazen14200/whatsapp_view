using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;


namespace Bnan.Ui.Areas.CAS.Controllers
{
        [Area("CAS")]
        [Authorize(Roles = "CAS")]
        public class UserContractController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IUserContract _UserContract;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<UserContractController> _localizer;


        public UserContractController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IUserContract UserContract,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<UserContractController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _UserContract = UserContract;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "6";

            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205007", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            var titles = await setTitle("205", "2205007", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            //var UserContractAll = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).Where(x => x.CrCasRenterContractBasic3?.CrCasRenterPrivateUserInformationContractCount > 0 && x.CrCasRenterContractBasicPrivateUserId != null);
            var UserContractAll = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).Where(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor).ToList();
            var AllUsers = _unitOfWork.CrMasUserInformation.FindAll(x => UserContractAll.Any(y => y.CrCasRenterContractBasicUserInsert == x.CrMasUserInformationCode)).ToList();

            UserContractVM userContractVM = new UserContractVM();
            userContractVM.crMasUserInformation = AllUsers;
            userContractVM.crCasRenterContractBasics = UserContractAll;



            return View(userContractVM);
        }


        [HttpGet]
        public async Task<PartialViewResult> GetUserContractByStatusAsync(string status)
        {

            if (!string.IsNullOrEmpty(status))
            {
                var UserContractAllA = _unitOfWork.CrElmPost.FindAll(x => x.CrElmPostRegionsArName != null).ToList();

                var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205007", "2");

                if (status == Status.All)
                {

                    var UserContractbyStatusAll = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).Where(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor).ToList();

                    return PartialView("_DataTableUserContract", UserContractbyStatusAll);
                }
                var UserContractbyStatus = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).Where(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor).ToList();

                return PartialView("_DataTableUserContract", UserContractbyStatus);
            }
            return PartialView();
        }





        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "6";
            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205007", "2");

            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("205", "2205007", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            //var contract = _unitOfWork.CrCasRenterContractBasic.FindAll(l => l.CrCasRenterContractBasic3?.CrCasRenterPrivateUserInformationId == id, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).Where(x => x.CrCasRenterContractBasicPrivateUserId != null).Where(x => x.CrCasRenterContractBasic3?.CrCasRenterPrivateUserInformationContractCount > 0).ToList();
            var contract = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).Where(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor && x.CrCasRenterContractBasicStatus != Status.Extension ).OrderBy(x => x.CrCasRenterContractBasicNo).ThenByDescending(t => t.CrCasRenterContractBasicCopy).ToList();
            contract = contract.Where(x => x.CrCasRenterContractBasicCopy > 0).ToList();
            var AllUsers = _unitOfWork.CrMasUserInformation.FindAll(x => contract.Any(y=>y.CrCasRenterContractBasicUserInsert == x.CrMasUserInformationCode  )).ToList();

             
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }

            ViewBag.CountRecord = contract.Count;

            //var queryMax = _unitOfWork.CrCasRenterContractBasic.GetAll().Where(x => x.CrCasRenterContractBasicPrivateUserId == id && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor).GroupBy(x => x.CrCasRenterContractBasicNo).Select(x => x.OrderByDescending(t => t.CrCasRenterContractBasicCopy));
            var Single_data = _unitOfWork.CrMasUserInformation.Find(x => currentUser.CrMasUserInformationLessor == x.CrMasUserInformationLessor );

            ViewBag.Single_UserId = Single_data.CrMasUserInformationCode;
            ViewBag.Single_UserNameAr = Single_data.CrMasUserInformationArName;
            ViewBag.Single_UserNameEn = Single_data.CrMasUserInformationEnName;

            return View(contract);
        }


        [HttpGet]
        public async Task<IActionResult> Edit2Date(string _max, string _mini, string id)
        {

            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205007", "2");

            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "6";
            ViewBag.startDate = DateTime.Parse(_mini).Date.ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Parse(_max).Date.ToString("yyyy-MM-dd");
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("205", "2205007", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            if (!string.IsNullOrEmpty(_max) && !string.IsNullOrEmpty(_mini) && _max.Length > 0)
            {
                // "today"  "tomorrow"   "after_longTime" 

                _max = DateTime.Parse(_max).Date.AddDays(1).ToString("yyyy-MM-dd");
                //var contract = _unitOfWork.CrCasRenterContractBasic.FindAll(l => l.CrCasRenterContractBasic3?.CrCasRenterPrivateUserInformationId == id, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).Where(x => x.CrCasRenterContractBasicPrivateUserId != null).Where(x => x.CrCasRenterContractBasic3?.CrCasRenterPrivateUserInformationContractCount > 0).ToList();
                var contract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicIssuedDate < DateTime.Parse(_max).Date && x.CrCasRenterContractBasicIssuedDate >= DateTime.Parse(_mini).Date, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).Where(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor && x.CrCasRenterContractBasicStatus != Status.Extension ).OrderBy(x => x.CrCasRenterContractBasicNo).ThenByDescending(t => t.CrCasRenterContractBasicCopy).ToList();

                contract = contract.Where(x => x.CrCasRenterContractBasicCopy > 0).ToList();
                var AllUsers = _unitOfWork.CrMasUserInformation.FindAll(x => contract.Any(y => y.CrCasRenterContractBasicUserInsert == x.CrMasUserInformationCode)).ToList();
                
                if (contract == null)
                {
                    ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                    return View("Index");
                }

                ViewBag.CountRecord = contract.Count;

                //var queryMax = _unitOfWork.CrCasRenterContractBasic.GetAll().Where(x => x.CrCasRenterContractBasicPrivateUserId == id && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor).GroupBy(x => x.CrCasRenterContractBasicNo).Select(x => x.OrderByDescending(t => t.CrCasRenterContractBasicCopy));
                var Single_data = _unitOfWork.CrMasUserInformation.Find(x => currentUser.CrMasUserInformationLessor == x.CrMasUserInformationLessor );

                ViewBag.Single_UserId = Single_data.CrMasUserInformationCode;
                ViewBag.Single_UserNameAr = Single_data.CrMasUserInformationArName;
                ViewBag.Single_UserNameEn = Single_data.CrMasUserInformationEnName;

                return View(contract);

            }

            return View();
        }

    }
}

