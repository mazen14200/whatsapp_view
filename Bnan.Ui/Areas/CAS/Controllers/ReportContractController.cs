using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.MAS;
using Bnan.Ui.ViewModels.CAS;
using MessagePack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Numerics;
namespace Bnan.Ui.Areas.CAS.Controllers
{

    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class ReportContractController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly ICompnayContract _compnayContract;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<ReportContractController> _localizer;


        public ReportContractController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, ICompnayContract compnayContract,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<ReportContractController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _compnayContract = compnayContract;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            //
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "2";
            //ViewBag.StartDate = "2024-02-15";
            //ViewBag.EndDate = "2024-02-15";
            ViewBag.StartDate = "";
            ViewBag.EndDate = "";

            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205003", "2");


            var titles = await setTitle("205", "2205003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterContract_Basic_All = _unitOfWork.CrCasRenterContractBasic.FindAll(x => (x.CrCasRenterContractBasicStatus == Status.Active || x.CrCasRenterContractBasicStatus == Status.Expire) && x.CrCasRenterContractBasicLessor == currentUser.CrMasUserInformationLessor, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic4", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).OrderByDescending(x => x.CrCasRenterContractBasicRenterId).ThenByDescending(y => y.CrCasRenterContractBasicIssuedDate).ToList();

            //--------------------------------

            var RenterContract_Basic_All_Date = _unitOfWork.CrCasRenterContractBasic.FindAll(x => (x.CrCasRenterContractBasicStatus == Status.Active || x.CrCasRenterContractBasicStatus == Status.Expire) && x.CrCasRenterContractBasicLessor == currentUser.CrMasUserInformationLessor, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic4", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).OrderByDescending(y => y.CrCasRenterContractBasicIssuedDate).ToList();

            if (RenterContract_Basic_All_Date.Count > 1)
            {
                var lastDate = RenterContract_Basic_All_Date.FirstOrDefault(x => x.CrCasRenterContractBasicIssuedDate != null)?.CrCasRenterContractBasicIssuedDate;
                var startDate = RenterContract_Basic_All_Date.LastOrDefault(x => x.CrCasRenterContractBasicIssuedDate != null)?.CrCasRenterContractBasicIssuedDate;
                if (lastDate != null && startDate != null)
                {
                    //var startDate = lastDate.Value.AddMonths(-1);
                    ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
                    ViewBag.EndDate = lastDate?.ToString("yyyy-MM-dd");

                    //var UserLoginbyDate_filtered = UserLoginbyDateAll1.Where(x => x.CrMasUserLoginEntryDate <= lastDate && x.CrMasUserLoginEntryDate >= startDate);
                }
            }
            var query_Alert = _unitOfWork.CrCasRenterContractAlert.FindAll(x => x.CrCasRenterContractAlertLessor == currentUser.CrMasUserInformationLessor).ToList();

            ReportActiveContractVM reportActiveContractVM = new ReportActiveContractVM();
            reportActiveContractVM.crCasRenterContractBasic = RenterContract_Basic_All;
            reportActiveContractVM.crCasRenterContractAlert = query_Alert;

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            return View("Index", reportActiveContractVM);



        }

        [HttpGet]
        public async Task<PartialViewResult> GetAllContractsByDate_statusAsync(string _max, string _mini, string status)
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "2";

            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205003", "2");

            var query_Alert_All = _unitOfWork.CrCasRenterContractAlert.FindAll(x => x.CrCasRenterContractAlertLessor == currentUser.CrMasUserInformationLessor).ToList();


            if (!string.IsNullOrEmpty(_max) && !string.IsNullOrEmpty(_mini))
            {
                // "today"  "tomorrow"   "after_longTime" 

                _max = DateTime.Parse(_max).Date.AddDays(1).ToString("yyyy-MM-dd");

                if (status == "All")
                {
                    var RenterContract_Basic_All1 = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicIssuedDate < DateTime.Parse(_max).Date && x.CrCasRenterContractBasicIssuedDate >= DateTime.Parse(_mini).Date && (x.CrCasRenterContractBasicStatus == Status.Active || x.CrCasRenterContractBasicStatus == Status.Expire) && x.CrCasRenterContractBasicLessor == currentUser.CrMasUserInformationLessor, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic4", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).OrderByDescending(x => x.CrCasRenterContractBasicRenterId).ThenByDescending(y => y.CrCasRenterContractBasicIssuedDate).ToList();

                    ReportActiveContractVM reportActiveContractVM = new ReportActiveContractVM();
                    reportActiveContractVM.crCasRenterContractBasic = RenterContract_Basic_All1;
                    reportActiveContractVM.crCasRenterContractAlert = query_Alert_All;

                    return PartialView("_DataTableReportContract", reportActiveContractVM);

                }
                else
                {
                    var today = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    var tomorrow = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");

                    var RenterContract_Basic_All = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicIssuedDate < DateTime.Parse(_max).Date && x.CrCasRenterContractBasicIssuedDate >= DateTime.Parse(_mini).Date && (x.CrCasRenterContractBasicStatus == Status.Active || x.CrCasRenterContractBasicStatus == Status.Expire) && x.CrCasRenterContractBasicLessor == currentUser.CrMasUserInformationLessor, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic4", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic5" }).OrderByDescending(x => x.CrCasRenterContractBasicRenterId).ThenByDescending(y => y.CrCasRenterContractBasicIssuedDate).ToList();

                    if (status == "today")
                    {
                        var query_Alert = _unitOfWork.CrCasRenterContractAlert.FindAll(x => x.CrCasRenterContractAlertLessor == currentUser.CrMasUserInformationLessor && x.CrCasRenterContractAlertContractActiviteStatus == "0").ToList();
                        RenterContract_Basic_All = RenterContract_Basic_All.Where(x => query_Alert.Any(y => y.CrCasRenterContractAlertNo == x.CrCasRenterContractBasicNo && x.CrCasRenterContractBasicStatus == "A")).ToList();
                    }
                    else if (status == "tomorrow")
                    {
                        var query_Alert = _unitOfWork.CrCasRenterContractAlert.FindAll(x => x.CrCasRenterContractAlertLessor == currentUser.CrMasUserInformationLessor && x.CrCasRenterContractAlertContractActiviteStatus == "1").ToList();
                        RenterContract_Basic_All = RenterContract_Basic_All.Where(x => query_Alert.Any(y => y.CrCasRenterContractAlertNo == x.CrCasRenterContractBasicNo)).ToList();
                    }
                    else if (status == "after_longTime")
                    {
                        var query_Alert = _unitOfWork.CrCasRenterContractAlert.FindAll(x => x.CrCasRenterContractAlertLessor == currentUser.CrMasUserInformationLessor && x.CrCasRenterContractAlertContractActiviteStatus == "2").ToList();
                        RenterContract_Basic_All = RenterContract_Basic_All.Where(x => query_Alert.Any(y => y.CrCasRenterContractAlertNo == x.CrCasRenterContractBasicNo)).ToList();
                    }
                    else
                    {
                        RenterContract_Basic_All = RenterContract_Basic_All.Where(x => x.CrCasRenterContractBasicStatus == status).ToList(); // E to ended
                    }

                    ReportActiveContractVM reportActiveContractVM = new ReportActiveContractVM();
                    reportActiveContractVM.crCasRenterContractBasic = RenterContract_Basic_All;
                    reportActiveContractVM.crCasRenterContractAlert = query_Alert_All;

                    return PartialView("_DataTableReportContract", reportActiveContractVM);

                }

            }
            return PartialView();
        }


        public IActionResult SuccesssMessageReport1()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index");
        }
        public IActionResult FailedMessageReport1()
        {
            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index");

        }




    }
}

