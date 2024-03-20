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
        public class DriverContractController : BaseController
        {
            private readonly IUserLoginsService _userLoginsService;
            private readonly UserManager<CrMasUserInformation> userManager;
            private readonly IUnitOfWork unitOfWork;
            private readonly IMapper mapper;
            private readonly IUserService _userService;
            private readonly IDriverContract _DriverContract;
            private readonly IToastNotification _toastNotification;
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly IStringLocalizer<DriverContractController> _localizer;


            public DriverContractController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
                IMapper mapper, IUserService userService, IDriverContract DriverContract,
                IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<DriverContractController> localizer) : base(userManager, unitOfWork, mapper)
            {
                this.userManager = userManager;
                this.unitOfWork = unitOfWork;
                this.mapper = mapper;
                _userService = userService;
                _DriverContract = DriverContract;
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
                ViewBag.no = "5";
                var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205006", "2");

                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                var titles = await setTitle("205", "2205006", "2");
                await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

                //var DriverContractAll = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic4", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).Where(x => x.CrCasRenterContractBasic4?.CrCasRenterPrivateDriverInformationContractCount > 0 && x.CrCasRenterContractBasicPrivateDriverId != null);
                var DriverContractAll = _unitOfWork.CrCasRenterContractBasic.GetAll(new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic4", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic3" }).Where(x => x.CrCasRenterContractBasicPrivateDriverId != null && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor);



                return View(DriverContractAll);
            }


        [HttpGet]
            public async Task<PartialViewResult> GetDriverContractByStatusAsync(string status)
            {

                if (!string.IsNullOrEmpty(status))
                {
                    var DriverContractAllA = _unitOfWork.CrElmPost.FindAll(x => x.CrElmPostRegionsArName != null).ToList();

                var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205006", "2");

                if (status == Status.All)
                    {

                        var DriverContractbyStatusAll = _unitOfWork.CrCasRenterContractBasic.FindAll(l => l.CrCasRenterContractBasic4.CrCasRenterPrivateDriverInformationStatus == Status.Hold || l.CrCasRenterContractBasicStatus == Status.Active || l.CrCasRenterContractBasicStatus == Status.Rented, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic4", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).Where(x => x.CrCasRenterContractBasicPrivateDriverId != null && x.CrCasRenterContractBasic4?.CrCasRenterPrivateDriverInformationContractCount > 0 && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor).ToList();

                        return PartialView("_DataTableDriverContract", DriverContractbyStatusAll);
                    }
                    var DriverContractbyStatus = _unitOfWork.CrCasRenterContractBasic.FindAll(l => l.CrCasRenterContractBasic4.CrCasRenterPrivateDriverInformationStatus == status , new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic4", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).Where(x => x.CrCasRenterContractBasicPrivateDriverId != null && x.CrCasRenterContractBasic4?.CrCasRenterPrivateDriverInformationContractCount > 0 && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor).ToList();

                    return PartialView("_DataTableDriverContract", DriverContractbyStatus);
                }
                return PartialView();
            }





        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "5";
            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205006", "2");

            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("205", "2205006", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            //var contract = _unitOfWork.CrCasRenterContractBasic.FindAll(l => l.CrCasRenterContractBasic4?.CrCasRenterPrivateDriverInformationId == id, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic4", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).Where(x => x.CrCasRenterContractBasicPrivateDriverId != null).Where(x => x.CrCasRenterContractBasic4?.CrCasRenterPrivateDriverInformationContractCount > 0).ToList();
            var contract = _unitOfWork.CrCasRenterContractBasic.FindAll(l => l.CrCasRenterContractBasic4.CrCasRenterPrivateDriverInformationId == id, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic4", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).Where(x => x.CrCasRenterContractBasicPrivateDriverId == id && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor && x.CrCasRenterContractBasicStatus != Status.Extension && x.CrCasRenterContractBasic4?.CrCasRenterPrivateDriverInformationContractCount > 0).OrderBy(x => x.CrCasRenterContractBasicNo).ThenByDescending(t => t.CrCasRenterContractBasicCopy).ToList();
            //contract = contract.Where(x => x.CrCasRenterContractBasicCopy > 0).ToList();

            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
    
            ViewBag.CountRecord = contract.Count;

            //var queryMax = _unitOfWork.CrCasRenterContractBasic.GetAll().Where(x => x.CrCasRenterContractBasicPrivateDriverId == id && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor).GroupBy(x => x.CrCasRenterContractBasicNo).Select(x => x.OrderByDescending(t => t.CrCasRenterContractBasicCopy));
            var Single_data = _unitOfWork.CrCasRenterPrivateDriverInformation.Find(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterPrivateDriverInformationLessor && x.CrCasRenterPrivateDriverInformationId == id);

            ViewBag.Single_DriverId = Single_data.CrCasRenterPrivateDriverInformationId;
            ViewBag.Single_DriverNameAr = Single_data.CrCasRenterPrivateDriverInformationArName;
            ViewBag.Single_DriverNameEn = Single_data.CrCasRenterPrivateDriverInformationEnName;

            return View(contract);
        }


        [HttpGet]
        public async Task<IActionResult> Edit2Date(string _max, string _mini, string id)
        {

            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205006", "2");
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "5";

            ViewBag.startDate = DateTime.Parse(_mini).Date.ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Parse(_max).Date.ToString("yyyy-MM-dd");
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("205", "2205006", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            if (!string.IsNullOrEmpty(_max) && !string.IsNullOrEmpty(_mini) && _max.Length > 0 )
            {
                // "today"  "tomorrow"   "after_longTime" 

                _max = DateTime.Parse(_max).Date.AddDays(1).ToString("yyyy-MM-dd");
                //var contract = _unitOfWork.CrCasRenterContractBasic.FindAll(l => l.CrCasRenterContractBasic4?.CrCasRenterPrivateDriverInformationId == id, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic4", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).Where(x => x.CrCasRenterContractBasicPrivateDriverId != null).Where(x => x.CrCasRenterContractBasic4?.CrCasRenterPrivateDriverInformationContractCount > 0).ToList();
                var contract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicIssuedDate < DateTime.Parse(_max).Date && x.CrCasRenterContractBasicIssuedDate >= DateTime.Parse(_mini).Date &&
                x.CrCasRenterContractBasicPrivateDriverId == id &&
                currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor && x.CrCasRenterContractBasicStatus != Status.Extension  
                && x.CrCasRenterContractBasic4.CrCasRenterPrivateDriverInformationContractCount > 0 , new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic4", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).OrderBy(x => x.CrCasRenterContractBasicNo).ThenByDescending(t => t.CrCasRenterContractBasicCopy).ToList();

                if (contract == null)
                {
                    ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                    return View("Index");
                }
              
                ViewBag.CountRecord = contract.Count;

                //var queryMax = _unitOfWork.CrCasRenterContractBasic.GetAll().Where(x => x.CrCasRenterContractBasicPrivateDriverId == id && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor).GroupBy(x => x.CrCasRenterContractBasicNo).Select(x => x.OrderByDescending(t => t.CrCasRenterContractBasicCopy));
                var Single_data = _unitOfWork.CrCasRenterPrivateDriverInformation.Find(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterPrivateDriverInformationLessor && x.CrCasRenterPrivateDriverInformationId == id);

                ViewBag.Single_DriverId = Single_data.CrCasRenterPrivateDriverInformationId;
                ViewBag.Single_DriverNameAr = Single_data.CrCasRenterPrivateDriverInformationArName;
                ViewBag.Single_DriverNameEn = Single_data.CrCasRenterPrivateDriverInformationEnName;

                return View(contract);

            }

            return View();
        }

    }
    }
