using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;

namespace Bnan.Ui.Areas.CAS.Controllers
{

    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class RenterStatisticsController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IFinancialTransactionOfRenter _CarContract;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<RenterStatisticsController> _localizer;


        public RenterStatisticsController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IFinancialTransactionOfRenter CarContract, 
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<RenterStatisticsController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _CarContract = CarContract;
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
            ViewBag.no = "13";

            var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205014", "2");

            var titles = await setTitle("205", "2205014", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllRenters = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode ).ToList();

            if (AllRenters?.Count() < 1)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }

            var Nationality_count = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).DistinctBy(x => x.CrCasRenterLessorStatisticsNationalities).Count();
            var MemperShip_count = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).DistinctBy(x => x.CrCasRenterLessorMembership).Count();
            var profession_count = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).DistinctBy(x => x.CrCasRenterLessorStatisticsJobs).Count();
            var Rigon_count = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).DistinctBy(x => x.CrCasRenterLessorStatisticsRegions).Count();
            var City_count = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).DistinctBy(x => x.CrCasRenterLessorStatisticsCity).Count();
            var Age_count = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).DistinctBy(x => x.CrCasRenterLessorStatisticsAge).Count();
            var Traded_count = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).DistinctBy(x => x.CrCasRenterLessorStatisticsTraded).Count();
            var Dealing_Mechanism_count = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).DistinctBy(x => x.CrCasRenterLessorDealingMechanism).Count();
            var Status_count = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode ).DistinctBy(x => x.CrCasRenterLessorStatus).Count();

            if (Nationality_count < 2 && MemperShip_count < 2 && profession_count < 2 && Rigon_count < 2 && City_count < 2 && Age_count < 2 && City_count < 2 && Traded_count < 2 && Dealing_Mechanism_count < 2 && Status_count < 2)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }
            if (Status_count < 2)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }
            string concate_DropDown = "";
            if (Nationality_count > 1) concate_DropDown = concate_DropDown + "0";
            if (MemperShip_count > 1) concate_DropDown = concate_DropDown + "1";
            if (profession_count > 1) concate_DropDown = concate_DropDown + "2";
            if (Rigon_count > 1) concate_DropDown = concate_DropDown + "3";
            if (City_count > 1) concate_DropDown = concate_DropDown + "4";
            if (Age_count > 1) concate_DropDown = concate_DropDown + "5";
            if (Traded_count > 1) concate_DropDown = concate_DropDown + "6";
            if (Dealing_Mechanism_count > 1) concate_DropDown = concate_DropDown + "7";
            if (Status_count > 1) concate_DropDown = concate_DropDown + "8";
            ViewBag.concate_DropDown = concate_DropDown;


            var AllNationality = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode, new[] { "CrCasRenterLessorStatisticsNationalitiesNavigation" }).Where(x => x.CrCasRenterLessorStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterLessorStatisticsNationalities).ToList();


            List<ChartBranchDataVM> chartBranchDataVMs = new List<ChartBranchDataVM>();
            var count_Renters = 0;
            foreach (var branch in AllNationality)
            {
                var BranchCount = 0;
                BranchCount = AllRenters.Count(x=>x.CrCasRenterLessorStatisticsNationalities == branch.CrCasRenterLessorStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesCode);
                ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                chartBranchDataVM.ArName = branch.CrCasRenterLessorStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesArName;
                chartBranchDataVM.EnName = branch.CrCasRenterLessorStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesEnName;
                chartBranchDataVM.Code = branch.CrCasRenterLessorStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesCode;
                chartBranchDataVM.Value = BranchCount;
                chartBranchDataVMs.Add(chartBranchDataVM);
                count_Renters = BranchCount + count_Renters;

            }
            chartBranchDataVMs = chartBranchDataVMs.OrderByDescending(x => x.Value).ToList();
            var Type_Avarage = chartBranchDataVMs.Average(x => x.Value);
            var Type_Sum = chartBranchDataVMs.Sum(x => x.Value);
            var Type_Count = chartBranchDataVMs.Count();
            var Type_Avarage_percentage = Type_Avarage/Type_Sum;
            var Static_Percentage_rate = 0.10;

            ViewBag.count_Renters = count_Renters;
            var max_Colomns = 15;
            var max = chartBranchDataVMs.Max(x => x.Value);
            var max1 = (int)max;
            ChartBranchDataVM other = new ChartBranchDataVM();
            other.Value = 0;
            other.ArName = "أخرى  ";
            other.EnName = "  Others";
            other.Code = "Aa";

            List<ChartBranchDataVM> chartBranchDataVMs_2 = new List<ChartBranchDataVM>(chartBranchDataVMs);
            int counter_For_max_Colomn = 0;
            foreach (var branch_1 in chartBranchDataVMs_2)
            {
                counter_For_max_Colomn ++;
                if(counter_For_max_Colomn <= max_Colomns)
                {
                    branch_1.IsTrue = true;
                }
                else
                {
                    branch_1.IsTrue = false;
                }
                if ((int)branch_1.Value <= max1 * (Static_Percentage_rate + (double)Type_Avarage_percentage) || (int)branch_1.Value <= max1 * (double)Type_Avarage_percentage)
                {
                    branch_1.IsTrue = false;
                }
            }
            foreach (var branch_1 in chartBranchDataVMs_2)
            {
                if (branch_1.IsTrue == false)
                {
                    other.Value = branch_1.Value + other.Value;
                    chartBranchDataVMs.Remove(branch_1);
                }
            }
            if ((int)other.Value > 0)
            {
                chartBranchDataVMs.Add(other);
                int listCount = 0;
                listCount = chartBranchDataVMs.Count() - 1;
                chartBranchDataVMs_2.Insert(listCount, other);
                //chartBranchDataVMs_2.Add(other);
                
            }
            ViewBag.singleType =  "0";
            CasStatisticLayoutVM casStatisticLayoutVM = new CasStatisticLayoutVM();
            casStatisticLayoutVM.ChartBranchDataVM = chartBranchDataVMs;
            //casStatisticLayoutVM.ChartBranchDataVM_2ForAll = chartBranchDataVMs_2;
            casStatisticLayoutVM.ChartBranchDataVM_2ForAll = chartBranchDataVMs;

            await _userLoginsService.SaveTracing(currentCar.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            return View(casStatisticLayoutVM);
        }




        [HttpGet]
        public async Task<IActionResult> GetAllByType(string Type ,string listDrop ,string singleNo)
        {
            if (listDrop == "" || listDrop == null)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "13";
            ViewBag.concate_DropDown = listDrop;
            ViewBag.singleType = singleNo;


            var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205014", "2");

            var titles = await setTitle("205", "2205014", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllRenters = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).ToList();


            List<ChartBranchDataVM> chartBranchDataVMs = new List<ChartBranchDataVM>();
            var count_Renters = 0;

            if (Type == "Nationality")
            {
                var AllNationality = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode, new[] { "CrCasRenterLessorStatisticsNationalitiesNavigation" }).Where(x => x.CrCasRenterLessorStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterLessorStatisticsNationalities).ToList();

                foreach (var single in AllNationality)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllRenters.Count(x => x.CrCasRenterLessorStatisticsNationalities == single.CrCasRenterLessorStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterLessorStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesArName;
                    chartBranchDataVM.EnName = single.CrCasRenterLessorStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesEnName;
                    chartBranchDataVM.Code = single.CrCasRenterLessorStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "MemperShip")
            {
                var AllRenters2_MemperShip = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode, new[] { "CrCasRenterLessorMembershipNavigation" }).Where(x => x.CrCasRenterLessorMembershipNavigation?.CrMasSupRenterMembershipStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterLessorMembership).ToList();

                foreach (var single in AllRenters2_MemperShip)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllRenters.Count(x => x.CrCasRenterLessorMembership == single.CrCasRenterLessorMembershipNavigation?.CrMasSupRenterMembershipCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterLessorMembershipNavigation?.CrMasSupRenterMembershipArName;
                    chartBranchDataVM.EnName = single.CrCasRenterLessorMembershipNavigation?.CrMasSupRenterMembershipEnName;
                    chartBranchDataVM.Code = single.CrCasRenterLessorMembershipNavigation?.CrMasSupRenterMembershipCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "profession")
            {
                var AllRenters2_profession = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode , new[] { "CrCasRenterLessorStatisticsJobsNavigation" } ).Where(x => x.CrCasRenterLessorStatisticsJobsNavigation?.CrMasSupRenterProfessionsStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterLessorStatisticsJobs).ToList();

                foreach (var single in AllRenters2_profession)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllRenters.Count(x => x.CrCasRenterLessorStatisticsJobs == single.CrCasRenterLessorStatisticsJobsNavigation?.CrMasSupRenterProfessionsCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterLessorStatisticsJobsNavigation?.CrMasSupRenterProfessionsArName;
                    chartBranchDataVM.EnName = single.CrCasRenterLessorStatisticsJobsNavigation?.CrMasSupRenterProfessionsEnName;
                    chartBranchDataVM.Code = single.CrCasRenterLessorStatisticsJobsNavigation?.CrMasSupRenterProfessionsCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "Rigon")
            {
                var AllRenters2_Rigon = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode, new[] { "CrCasRenterLessorStatisticsRegionsNavigation" }).Where(x => x.CrCasRenterLessorStatisticsRegionsNavigation?.CrMasSupPostRegionsStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterLessorStatisticsRegions).ToList();

                foreach (var single in AllRenters2_Rigon)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllRenters.Count(x => x.CrCasRenterLessorStatisticsRegions == single.CrCasRenterLessorStatisticsRegionsNavigation?.CrMasSupPostRegionsCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterLessorStatisticsRegionsNavigation?.CrMasSupPostRegionsArName;
                    chartBranchDataVM.EnName = single.CrCasRenterLessorStatisticsRegionsNavigation?.CrMasSupPostRegionsEnName;
                    chartBranchDataVM.Code = single.CrCasRenterLessorStatisticsRegionsNavigation?.CrMasSupPostRegionsCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "City")
            {
                var AllRenters2_City = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode, new[] { "CrCasRenterLessorStatisticsCityNavigation" }).Where(x => x.CrCasRenterLessorStatisticsCityNavigation?.CrMasSupPostCityStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterLessorStatisticsCity).ToList();

                foreach (var single in AllRenters2_City)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllRenters.Count(x => x.CrCasRenterLessorStatisticsCity == single.CrCasRenterLessorStatisticsCityNavigation?.CrMasSupPostCityCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterLessorStatisticsCityNavigation?.CrMasSupPostCityArName;
                    chartBranchDataVM.EnName = single.CrCasRenterLessorStatisticsCityNavigation?.CrMasSupPostCityEnName;
                    chartBranchDataVM.Code = single.CrCasRenterLessorStatisticsCityNavigation?.CrMasSupPostCityCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }

            if (Type == "Age")
            {
                var AllRenters2_Age = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode ).DistinctBy(x => x.CrCasRenterLessorStatisticsAge).ToList();

                foreach (var single in AllRenters2_Age)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllRenters.Count(x => x.CrCasRenterLessorStatisticsAge == single.CrCasRenterLessorStatisticsAge);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    switch (single.CrCasRenterLessorStatisticsAge)
                    {
                        case "1":
                            chartBranchDataVM.ArName = "أقل من 20";
                            chartBranchDataVM.EnName = "Less Than 20";
                            break;
                        case "2":
                            chartBranchDataVM.ArName = "من 21 إلى 30";
                            chartBranchDataVM.EnName = "From 21 To 30";
                            break;
                        case "3":
                            chartBranchDataVM.ArName = "من 31 إلى 40";
                            chartBranchDataVM.EnName = "From 31 To 40";
                            break;
                        case "4":
                            chartBranchDataVM.ArName = "من 41 إلى 50";
                            chartBranchDataVM.EnName = "From 41 To 50";
                            break;
                        case "5":
                            chartBranchDataVM.ArName = "من 51 إلى 60";
                            chartBranchDataVM.EnName = "From 51 To 60";
                            break;
                        case "6":
                            chartBranchDataVM.ArName = "أكثر من 60";
                            chartBranchDataVM.EnName = "More Than 60";
                            break;
                        default:
                            chartBranchDataVM.ArName = "أقل من 20";
                            chartBranchDataVM.EnName = "Less Than 20";
                            break;
                    }

                    chartBranchDataVM.Code = single.CrCasRenterLessorStatisticsAge;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }

            if (Type == "Traded")
            {
                var AllRenters2_Traded = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).DistinctBy(x => x.CrCasRenterLessorStatisticsTraded).ToList();

                foreach (var single in AllRenters2_Traded)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllRenters.Count(x => x.CrCasRenterLessorStatisticsTraded == single.CrCasRenterLessorStatisticsTraded);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    switch (single.CrCasRenterLessorStatisticsTraded)
                    {
                        case "1":
                            chartBranchDataVM.ArName = "أقل من 1000";
                            chartBranchDataVM.EnName = "Less Than 1000";
                            break;
                        case "2":
                            chartBranchDataVM.ArName = "من 1001 إلى 2000";
                            chartBranchDataVM.EnName = "From 1001 To 2000";
                            break;
                        case "3":
                            chartBranchDataVM.ArName = "من 2001 إلى 3000";
                            chartBranchDataVM.EnName = "From 2001 To 3000";
                            break;
                        case "4":
                            chartBranchDataVM.ArName = "من 3001 إلى 5000";
                            chartBranchDataVM.EnName = "From 3001 To 5000";
                            break;
                        case "5":
                            chartBranchDataVM.ArName = "من 5001 إلى 7000";
                            chartBranchDataVM.EnName = "From 5001 To 7000";
                            break;
                        case "6":
                            chartBranchDataVM.ArName = "من 7001 إلى 10,000";
                            chartBranchDataVM.EnName = "From 7001 To 10,000";
                            break;
                        case "7":
                            chartBranchDataVM.ArName = "من 10,001 إلى 15,000";
                            chartBranchDataVM.EnName = "From 10,001 To 15,000";
                            break;
                        case "8":
                            chartBranchDataVM.ArName = "من 15,001 إلى 20,000";
                            chartBranchDataVM.EnName = "From 15,001 To 20,000";
                            break;
                        case "9":
                            chartBranchDataVM.ArName = "أكثر من 20,000";
                            chartBranchDataVM.EnName = "More Than 20,000";
                            break;
                        default:
                            chartBranchDataVM.ArName = "أقل من 1000";
                            chartBranchDataVM.EnName = "Less Than 1000";
                            break;
                    }

                    chartBranchDataVM.Code = single.CrCasRenterLessorStatisticsTraded;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }

            if (Type == "Dealing_Mechanism")
            {                
                var AllDealing_Mechanism = _unitOfWork.CrMasSysEvaluation.GetAll().Where(x=>x.CrMasSysEvaluationsStatus != Status.Deleted).ToList();

                foreach (var single in AllDealing_Mechanism)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllRenters.Count(x => x.CrCasRenterLessorDealingMechanism == single.CrMasSysEvaluationsCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrMasSysEvaluationsArDescription;
                    chartBranchDataVM.EnName = single.CrMasSysEvaluationsEnDescription;
                    chartBranchDataVM.Code = single.CrMasSysEvaluationsCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }

            if (Type == "Status")
            {
                var AllRenters2_Status = _unitOfWork.CrCasRenterLessor.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterLessorCode).DistinctBy(x => x.CrCasRenterLessorStatus).ToList();

                foreach (var single in AllRenters2_Status)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllRenters.Count(x => x.CrCasRenterLessorStatus == single.CrCasRenterLessorStatus);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    switch (single.CrCasRenterLessorStatus)
                    {
                        case "A":
                            chartBranchDataVM.ArName = "نشط";
                            chartBranchDataVM.EnName = "Active";
                            break;
                        case "R":
                            chartBranchDataVM.ArName = "مؤجر";
                            chartBranchDataVM.EnName = "Rented";
                            break;
                        default:
                            chartBranchDataVM.ArName = "نشط";
                            chartBranchDataVM.EnName = "Active";
                            break;
                    }

                    chartBranchDataVM.Code = single.CrCasRenterLessorStatus;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }

            chartBranchDataVMs = chartBranchDataVMs.OrderByDescending(x => x.Value).ToList();
            var Type_Avarage = chartBranchDataVMs.Average(x => x.Value);
            var Type_Sum = chartBranchDataVMs.Sum(x => x.Value);
            var Type_Count = chartBranchDataVMs.Count();
            var Type_Avarage_percentage = Type_Avarage / Type_Sum;
            var Static_Percentage_rate = 0.10;

            //ViewBag.count_Renters = count_Renters;
            var max_Colomns = 15;
            var max = chartBranchDataVMs.Max(x => x.Value);
            var max1 = (int)max;
            ChartBranchDataVM other = new ChartBranchDataVM();
            other.Value = 0;
            other.ArName = "أخرى  ";
            other.EnName = "  Others";
            other.Code = "Aa";

            List<ChartBranchDataVM> chartBranchDataVMs_2 = new List<ChartBranchDataVM>(chartBranchDataVMs);
            int counter_For_max_Colomn = 0;

            foreach (var branch_1 in chartBranchDataVMs_2)
            {
                counter_For_max_Colomn++;
                if (counter_For_max_Colomn <= max_Colomns)
                {
                    branch_1.IsTrue = true;
                }
                else
                {
                    branch_1.IsTrue = false;
                }
                if (chartBranchDataVMs_2.Count() > 5)
                {
                    if ((int)branch_1.Value <= max1 * (Static_Percentage_rate + (double)Type_Avarage_percentage) || (int)branch_1.Value <= max1 * (double)Type_Avarage_percentage)
                    {
                        branch_1.IsTrue = false;
                    }
                }

            }
            foreach (var branch_1 in chartBranchDataVMs_2)
            {
                if (branch_1.IsTrue == false)
                {
                    other.Value = branch_1.Value + other.Value;
                    chartBranchDataVMs.Remove(branch_1);
                }
            }
            if ((int)other.Value > 0)
            {
                chartBranchDataVMs.Add(other);
                int listCount = 0;
                listCount = chartBranchDataVMs.Count() - 1;
                chartBranchDataVMs_2.Insert(listCount, other);
                //chartBranchDataVMs_2.Add(other);
            }

            CasStatisticLayoutVM casStatisticLayoutVM = new CasStatisticLayoutVM();
            casStatisticLayoutVM.ChartBranchDataVM = chartBranchDataVMs;
            //casStatisticLayoutVM.ChartBranchDataVM_2ForAll = chartBranchDataVMs_2;
            casStatisticLayoutVM.ChartBranchDataVM_2ForAll = chartBranchDataVMs;

            return View(casStatisticLayoutVM);
        }


    public async Task<IActionResult> FailedMessageReport_NoData()
    {
        //sidebar Active
        ViewBag.id = "#sidebarReport";
        ViewBag.no = "13";
        var titles = await setTitle("205", "2205014", "2");
        await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
        var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205014", "2");
        ViewBag.Data = "0";
        return View();

    }
}
}

