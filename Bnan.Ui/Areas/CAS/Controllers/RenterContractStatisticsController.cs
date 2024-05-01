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
    public class RenterContractStatisticsController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IFinancialTransactionOfRenter _CarContract;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<RenterContractStatisticsController> _localizer;


        public RenterContractStatisticsController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IFinancialTransactionOfRenter CarContract, 
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<RenterContractStatisticsController> localizer) : base(userManager, unitOfWork, mapper)
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
            ViewBag.no = "16";

            var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205017", "2");

            var titles = await setTitle("205", "2205017", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllCarContracts = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor ).OrderByDescending(x=>x.CrCasRenterContractStatisticsDate).ToList();
            ViewBag.StartDate = AllCarContracts?.LastOrDefault()?.CrCasRenterContractStatisticsDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            ViewBag.EndDate = AllCarContracts?.FirstOrDefault()?.CrCasRenterContractStatisticsDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);


            if (AllCarContracts?.Count() < 1)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }


            var Nationality_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsNationalities).Count();
            var MemperShip_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsMembership).Count();
            var profession_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsJobs).Count();
            var Rigon_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsRenterRegions).Count();
            var City_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsRenterCity).Count();
            var Age_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsAgeNo).Count();
            
            if (Nationality_count < 2 && MemperShip_count < 2 && profession_count < 2 && Rigon_count < 2 && City_count < 2 && Age_count < 2 && City_count < 2 )
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
            ViewBag.concate_DropDown = concate_DropDown;



            var AllNationalities = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor, new[] { "CrCasRenterContractStatisticsNationalitiesNavigation" }).Where(x => x.CrCasRenterContractStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsNationalities).ToList();


            List<ChartBranchDataVM> chartBranchDataVMs = new List<ChartBranchDataVM>();
            var count_Renters = 0;
            foreach (var branch in AllNationalities)
            {
                var BranchCount = 0;
                BranchCount = AllCarContracts.Count(x=>x.CrCasRenterContractStatisticsNationalities == branch.CrCasRenterContractStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesCode);
                ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                chartBranchDataVM.ArName = branch.CrCasRenterContractStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesArName;
                chartBranchDataVM.EnName = branch.CrCasRenterContractStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesEnName;
                chartBranchDataVM.Code = branch.CrCasRenterContractStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesCode;
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
        public async Task<IActionResult> GetAllByType(string Type ,string listDrop ,string singleNo ,string startDate,string endDate)
        {
            if (listDrop == "" || listDrop == null)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "16";
            ViewBag.concate_DropDown = listDrop;
            ViewBag.singleType = singleNo;
            ViewBag.StartDate = DateTime.Parse(startDate).Date.ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Parse(endDate).Date.ToString("yyyy-MM-dd");
            var _max = DateTime.Parse(endDate).Date.AddDays(1);
            var _mini = DateTime.Parse(startDate).Date;

            var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205017", "2");

            var titles = await setTitle("205", "2205017", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllCarContracts = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor && x.CrCasRenterContractStatisticsDate < _max && x.CrCasRenterContractStatisticsDate >= _mini).ToList();
            
            if (AllCarContracts?.Count() < 1)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }

            List<ChartBranchDataVM> chartBranchDataVMs = new List<ChartBranchDataVM>();
            var count_Renters = 0;

            if (Type == "Nationality")
            {
                var AllNationalities = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor, new[] { "CrCasRenterContractStatisticsNationalitiesNavigation" }).Where(x => x.CrCasRenterContractStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsNationalities).ToList();

                foreach (var single in AllNationalities)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCarContracts.Count(x => x.CrCasRenterContractStatisticsNationalities == single.CrCasRenterContractStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterContractStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesArName;
                    chartBranchDataVM.EnName = single.CrCasRenterContractStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesEnName;
                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsNationalitiesNavigation?.CrMasSupRenterNationalitiesCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "MemperShip")
            {
                var AllCarContracts2_MemperShip = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor, new[] { "CrCasRenterContractStatisticsMembershipNavigation" }).Where(x => x.CrCasRenterContractStatisticsMembershipNavigation?.CrMasSupRenterMembershipStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsMembership).ToList();

                foreach (var single in AllCarContracts2_MemperShip)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCarContracts.Count(x => x.CrCasRenterContractStatisticsMembership == single.CrCasRenterContractStatisticsMembershipNavigation?.CrMasSupRenterMembershipCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterContractStatisticsMembershipNavigation?.CrMasSupRenterMembershipArName;
                    chartBranchDataVM.EnName = single.CrCasRenterContractStatisticsMembershipNavigation?.CrMasSupRenterMembershipEnName;
                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsMembershipNavigation?.CrMasSupRenterMembershipCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "profession")
            {
                var AllCarContracts2_profession = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor , new[] { "CrCasRenterContractStatisticsJobsNavigation" } ).Where(x => x.CrCasRenterContractStatisticsJobsNavigation?.CrMasSupRenterProfessionsStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsJobs).ToList();

                foreach (var single in AllCarContracts2_profession)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCarContracts.Count(x => x.CrCasRenterContractStatisticsJobs == single.CrCasRenterContractStatisticsJobsNavigation?.CrMasSupRenterProfessionsCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterContractStatisticsJobsNavigation?.CrMasSupRenterProfessionsArName;
                    chartBranchDataVM.EnName = single.CrCasRenterContractStatisticsJobsNavigation?.CrMasSupRenterProfessionsEnName;
                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsJobsNavigation?.CrMasSupRenterProfessionsCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "Rigon")
            {
                var AllCarContracts2_Rigon = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor, new[] { "CrCasRenterContractStatisticsRenterRegionsNavigation" }).Where(x => x.CrCasRenterContractStatisticsRenterRegionsNavigation?.CrMasSupPostRegionsStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsRenterRegions).ToList();

                foreach (var single in AllCarContracts2_Rigon)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCarContracts.Count(x => x.CrCasRenterContractStatisticsRenterRegions == single.CrCasRenterContractStatisticsRenterRegionsNavigation?.CrMasSupPostRegionsCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterContractStatisticsRenterRegionsNavigation?.CrMasSupPostRegionsArName;
                    chartBranchDataVM.EnName = single.CrCasRenterContractStatisticsRenterRegionsNavigation?.CrMasSupPostRegionsEnName;
                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsRenterRegionsNavigation?.CrMasSupPostRegionsCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "City")
            {
                var AllCarContracts2_City = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor, new[] { "CrCasRenterContractStatisticsRenterCityNavigation" }).Where(x => x.CrCasRenterContractStatisticsRenterCityNavigation?.CrMasSupPostCityStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsRenterCity).ToList();

                foreach (var single in AllCarContracts2_City)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCarContracts.Count(x => x.CrCasRenterContractStatisticsRenterCity == single.CrCasRenterContractStatisticsRenterCityNavigation?.CrMasSupPostCityCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterContractStatisticsRenterCityNavigation?.CrMasSupPostCityArName;
                    chartBranchDataVM.EnName = single.CrCasRenterContractStatisticsRenterCityNavigation?.CrMasSupPostCityEnName;
                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsRenterCityNavigation?.CrMasSupPostCityCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "Age")
            {
                var AllRenters2_Age = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsAgeNo).ToList();

                foreach (var single in AllRenters2_Age)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCarContracts.Count(x => x.CrCasRenterContractStatisticsAgeNo == single.CrCasRenterContractStatisticsAgeNo);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    switch (single.CrCasRenterContractStatisticsAgeNo)
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

                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsAgeNo;
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
        ViewBag.no = "16";
        var titles = await setTitle("205", "2205017", "2");
        await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
        var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205017", "2");
        ViewBag.Data = "0";
        return View();

    }
}
}

