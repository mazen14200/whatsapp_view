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
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;

namespace Bnan.Ui.Areas.CAS.Controllers
{

    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class ContractStatisticsController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IFinancialTransactionOfRenter _CarContract;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<ContractStatisticsController> _localizer;


        public ContractStatisticsController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IFinancialTransactionOfRenter CarContract, 
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<ContractStatisticsController> localizer) : base(userManager, unitOfWork, mapper)
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
            ViewBag.no = "14";

            var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205015", "2");

            var titles = await setTitle("205", "2205015", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllContracts = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor ).OrderByDescending(x=>x.CrCasRenterContractStatisticsDate).ToList();
            ViewBag.StartDate = AllContracts?.LastOrDefault()?.CrCasRenterContractStatisticsDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            ViewBag.EndDate = AllContracts?.FirstOrDefault()?.CrCasRenterContractStatisticsDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);


            if (AllContracts?.Count() < 1)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }


            var Branch_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsBranch).Count();
            var Day_Create_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsDayCreate).Count();
            var Time_Create_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsTimeCreate).Count();
            var Day_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsDayCount).Count();
            var Value_Contract_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsValueNo).Count();
            var KM_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsKm).Count();
            var Days_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisicsDays).Count();

            if (Branch_count < 2 && Day_Create_count < 2 && Time_Create_count < 2 && Day_count < 2 && Value_Contract_count < 2 && KM_count < 2 && Days_count < 2 )
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }
            string concate_DropDown = "";
            if (Branch_count > 1) concate_DropDown = concate_DropDown + "0";
            if (Day_Create_count > 1) concate_DropDown = concate_DropDown + "1";
            if (Time_Create_count > 1) concate_DropDown = concate_DropDown + "2";
            if (Day_count > 1) concate_DropDown = concate_DropDown + "3";
            if (Value_Contract_count > 1) concate_DropDown = concate_DropDown + "4";
            if (KM_count > 1) concate_DropDown = concate_DropDown + "5";
            if (Days_count > 1) concate_DropDown = concate_DropDown + "6";
            ViewBag.concate_DropDown = concate_DropDown;



            var AllBranches = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor, new[] { "CrCasRenterContractStatistics" }).Where(x => x.CrCasRenterContractStatistics?.CrCasBranchInformationStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsBranch).ToList();


            List<ChartBranchDataVM> chartBranchDataVMs = new List<ChartBranchDataVM>();
            var count_Renters = 0;
            foreach (var branch in AllBranches)
            {
                var BranchCount = 0;
                BranchCount = AllContracts.Count(x=>x.CrCasRenterContractStatisticsBranch == branch.CrCasRenterContractStatistics?.CrCasBranchInformationCode);
                ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                chartBranchDataVM.ArName = branch.CrCasRenterContractStatistics?.CrCasBranchInformationArShortName;
                chartBranchDataVM.EnName = branch.CrCasRenterContractStatistics?.CrCasBranchInformationEnShortName;
                chartBranchDataVM.Code = branch.CrCasRenterContractStatistics?.CrCasBranchInformationCode;
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
            ViewBag.no = "14";
            ViewBag.concate_DropDown = listDrop;
            ViewBag.singleType = singleNo;
            ViewBag.StartDate = DateTime.Parse(startDate).Date.ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Parse(endDate).Date.ToString("yyyy-MM-dd");
            var _max = DateTime.Parse(endDate).Date.AddDays(1);
            var _mini = DateTime.Parse(startDate).Date;

            var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205015", "2");

            var titles = await setTitle("205", "2205015", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllContracts = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor && x.CrCasRenterContractStatisticsDate < _max && x.CrCasRenterContractStatisticsDate >= _mini).ToList();
            
            if (AllContracts?.Count() < 1)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }

            List<ChartBranchDataVM> chartBranchDataVMs = new List<ChartBranchDataVM>();
            var count_Renters = 0;

            if (Type == "Branch")
            {
                var AllBranches = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor, new[] { "CrCasRenterContractStatistics" }).Where(x => x.CrCasRenterContractStatistics?.CrCasBranchInformationStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsBranch).ToList();

                foreach (var single in AllBranches)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllContracts.Count(x => x.CrCasRenterContractStatisticsBranch == single.CrCasRenterContractStatistics?.CrCasBranchInformationCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterContractStatistics?.CrCasBranchInformationArShortName;
                    chartBranchDataVM.EnName = single.CrCasRenterContractStatistics?.CrCasBranchInformationEnShortName;
                    chartBranchDataVM.Code = single.CrCasRenterContractStatistics?.CrCasBranchInformationCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "Day_Create")
            {
                var AllContracts2_Day_Create = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsDayCreate).ToList();

                foreach (var single in AllContracts2_Day_Create)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllContracts.Count(x => x.CrCasRenterContractStatisticsDayCreate == single.CrCasRenterContractStatisticsDayCreate);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    switch (single.CrCasRenterContractStatisticsDayCreate)
                    {
                        case "1":
                            chartBranchDataVM.ArName = "السبت";
                            chartBranchDataVM.EnName = "Saturday";
                            break;
                        case "2":
                            chartBranchDataVM.ArName = "الأحد";
                            chartBranchDataVM.EnName = "Sunday";
                            break;
                        case "3":
                            chartBranchDataVM.ArName = "الإثنين";
                            chartBranchDataVM.EnName = "Monday";
                            break;
                        case "4":
                            chartBranchDataVM.ArName = "الثلاثاء";
                            chartBranchDataVM.EnName = "Tuesday";
                            break;
                        case "5":
                            chartBranchDataVM.ArName = "الأربعاء";
                            chartBranchDataVM.EnName = "Wednesday";
                            break;
                        case "6":
                            chartBranchDataVM.ArName = "الخميس";
                            chartBranchDataVM.EnName = "Thursday";
                            break;
                        case "7":
                            chartBranchDataVM.ArName = "الجمعة";
                            chartBranchDataVM.EnName = "Friday";
                            break;
                        default:
                            chartBranchDataVM.ArName = "السبت";
                            chartBranchDataVM.EnName = "Saturday";
                            break;
                    }

                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsDayCreate;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }


            if (Type == "Time_Create")
            {
                var AllContracts2_Time_Create = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor ).DistinctBy(x => x.CrCasRenterContractStatisticsTimeCreate).ToList();

                foreach (var single in AllContracts2_Time_Create)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllContracts.Count(x => x.CrCasRenterContractStatisticsTimeCreate == single.CrCasRenterContractStatisticsTimeCreate);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    switch (single.CrCasRenterContractStatisticsTimeCreate)
                    {
                        case "1":
                            chartBranchDataVM.ArName = "من 00:00 إلى 02:59";
                            chartBranchDataVM.EnName = "From 00:00 To 02:59";
                            break;
                        case "2":
                            chartBranchDataVM.ArName = "من 03:00 إلى 05:59";
                            chartBranchDataVM.EnName = "From 03:00 To 05:59";
                            break;
                        case "3":
                            chartBranchDataVM.ArName = "من 06:00 إلى 08:59";
                            chartBranchDataVM.EnName = "From 06:00 To 08:59";
                            break;
                        case "4":
                            chartBranchDataVM.ArName = "من 09:00 إلى 11:59";
                            chartBranchDataVM.EnName = "From 09:00 To 11:59";
                            break;
                        case "5":
                            chartBranchDataVM.ArName = "من 12:00 إلى 14:59";
                            chartBranchDataVM.EnName = "From 12:00 To 14:59";
                            break;
                        case "6":
                            chartBranchDataVM.ArName = "من 15:00 إلى 17:59";
                            chartBranchDataVM.EnName = "From 15:00 To 17:59";
                            break;
                        case "7":
                            chartBranchDataVM.ArName = "من 18:00 إلى 20:59";
                            chartBranchDataVM.EnName = "From 18:00 To 20:59";
                            break;
                        case "8":
                            chartBranchDataVM.ArName = "من 21:00 إلى 23:59";
                            chartBranchDataVM.EnName = "From 21:00 To 23:59";
                            break;
                        default:
                            chartBranchDataVM.ArName = "من 00:00 إلى 02:59";
                            chartBranchDataVM.EnName = "From 00:00 To 02:59";
                            break;
                    }

                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsTimeCreate;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }


            if (Type == "Day_Count")
            {
                var AllContracts2_Day_Count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsDayCount).ToList();

                foreach (var single in AllContracts2_Day_Count)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllContracts.Count(x => x.CrCasRenterContractStatisticsDayCount == single.CrCasRenterContractStatisticsDayCount);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    switch (single.CrCasRenterContractStatisticsDayCount)
                    {
                        case "1":
                            chartBranchDataVM.ArName = "من 1 إلى 3";
                            chartBranchDataVM.EnName = "From 1 To 3";
                            break;
                        case "2":
                            chartBranchDataVM.ArName = "من 4 إلى 7";
                            chartBranchDataVM.EnName = "From 4 To 7";
                            break;
                        case "3":
                            chartBranchDataVM.ArName = "من 8 إلى 10";
                            chartBranchDataVM.EnName = "From 8 To 10";
                            break;
                        case "4":
                            chartBranchDataVM.ArName = "من 11 إلى 15";
                            chartBranchDataVM.EnName = "From 11 To 15";
                            break;
                        case "5":
                            chartBranchDataVM.ArName = "من 16 إلى 20";
                            chartBranchDataVM.EnName = "From 16 To 20";
                            break;
                        case "6":
                            chartBranchDataVM.ArName = "من 21 إلى 25";
                            chartBranchDataVM.EnName = "From 21 To 25";
                            break;
                        case "7":
                            chartBranchDataVM.ArName = "من 26 إلى 30";
                            chartBranchDataVM.EnName = "From 26 To 30";
                            break;
                        case "8":
                            chartBranchDataVM.ArName = "أكثر من 30";
                            chartBranchDataVM.EnName = "More Than 30";
                            break;
                        default:
                            chartBranchDataVM.ArName = "من 1 إلى 3";
                            chartBranchDataVM.EnName = "From 1 To 3";
                            break;
                    }

                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsDayCount;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
        
            
            
            if (Type == "Value_No")
            {
                var AllContracts2_Contract_Value = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor ).DistinctBy(x => x.CrCasRenterContractStatisticsValueNo).ToList();

                foreach (var single in AllContracts2_Contract_Value)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllContracts.Count(x => x.CrCasRenterContractStatisticsValueNo == single.CrCasRenterContractStatisticsValueNo);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    switch (single.CrCasRenterContractStatisticsValueNo)
                    {
                        case "1":
                            chartBranchDataVM.ArName = "أقل من 300";
                            chartBranchDataVM.EnName = "Less Than 300";
                            break;
                        case "2":
                            chartBranchDataVM.ArName = "من 301 إلى 500";
                            chartBranchDataVM.EnName = "From 301 To 500";
                            break;
                        case "3":
                            chartBranchDataVM.ArName = "من 501 إلى 1000";
                            chartBranchDataVM.EnName = "From 501 To 1000";
                            break;
                        case "4":
                            chartBranchDataVM.ArName = "من 1001 إلى 1500";
                            chartBranchDataVM.EnName = "From 1001 To 1500";
                            break;
                        case "5":
                            chartBranchDataVM.ArName = "من 1501 إلى 2000";
                            chartBranchDataVM.EnName = "From 1501 To 2000";
                            break;
                        case "6":
                            chartBranchDataVM.ArName = "من 2001 إلى 3000";
                            chartBranchDataVM.EnName = "From 2001 To 3000";
                            break;
                        case "7":
                            chartBranchDataVM.ArName = "من 3001 إلى 4000";
                            chartBranchDataVM.EnName = "From 3001 To 4000";
                            break;
                        case "8":
                            chartBranchDataVM.ArName = "من 4001 إلى 5000";
                            chartBranchDataVM.EnName = "From 4001 To 5000";
                            break;
                        case "9":
                            chartBranchDataVM.ArName = "أكثر من 5000";
                            chartBranchDataVM.EnName = "More Than 5000";
                            break;
                        default:
                            chartBranchDataVM.ArName = "أقل من 300";
                            chartBranchDataVM.EnName = "Less Than 300";
                            break;
                    }

                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsValueNo;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            
            
            if (Type == "KM")
            {
                var AllContracts2_KM = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsKm).ToList();

                foreach (var single in AllContracts2_KM)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllContracts.Count(x => x.CrCasRenterContractStatisticsKm == single.CrCasRenterContractStatisticsKm);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    switch (single.CrCasRenterContractStatisticsKm)
                    {
                        case "1":
                            chartBranchDataVM.ArName = "أقل من 100";
                            chartBranchDataVM.EnName = "Less Than 100";
                            break;
                        case "2":
                            chartBranchDataVM.ArName = "من 101 إلى 200";
                            chartBranchDataVM.EnName = "From 101 To 200";
                            break;
                        case "3":
                            chartBranchDataVM.ArName = "من 201 إلى 300";
                            chartBranchDataVM.EnName = "From 201 To 300";
                            break;
                        case "4":
                            chartBranchDataVM.ArName = "من 301 إلى 400";
                            chartBranchDataVM.EnName = "From 301 To 400";
                            break;
                        case "5":
                            chartBranchDataVM.ArName = "أكثر من 400";
                            chartBranchDataVM.EnName = "More Than 400";
                            break;
                        default:
                            chartBranchDataVM.ArName = "أقل من 100";
                            chartBranchDataVM.EnName = "Less Than 100";
                            break;
                    }

                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsKm;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }



            //if (Type == "KM")
            //{
            //    var AllRenters2_Age = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsAgeNo).ToList();

            //    foreach (var single in AllRenters2_Age)
            //    {
            //        var CategoryCount = 0;
            //        CategoryCount = AllContracts.Count(x => x.CrCasRenterContractStatisticsAgeNo == single.CrCasRenterContractStatisticsAgeNo);
            //        ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
            //        switch (single.CrCasRenterContractStatisticsAgeNo)
            //        {
            //            case "1":
            //                chartBranchDataVM.ArName = "أقل من 20";
            //                chartBranchDataVM.EnName = "Less Than 20";
            //                break;
            //            case "2":
            //                chartBranchDataVM.ArName = "من 21 إلى 30";
            //                chartBranchDataVM.EnName = "From 21 To 30";
            //                break;
            //            case "3":
            //                chartBranchDataVM.ArName = "من 31 إلى 40";
            //                chartBranchDataVM.EnName = "From 31 To 40";
            //                break;
            //            case "4":
            //                chartBranchDataVM.ArName = "من 41 إلى 50";
            //                chartBranchDataVM.EnName = "From 41 To 50";
            //                break;
            //            case "5":
            //                chartBranchDataVM.ArName = "من 51 إلى 60";
            //                chartBranchDataVM.EnName = "From 51 To 60";
            //                break;
            //            case "6":
            //                chartBranchDataVM.ArName = "أكثر من 60";
            //                chartBranchDataVM.EnName = "More Than 60";
            //                break;
            //            default:
            //                chartBranchDataVM.ArName = "أقل من 20";
            //                chartBranchDataVM.EnName = "Less Than 20";
            //                break;
            //        }

            //        chartBranchDataVM.Code = single.CrCasRenterContractStatisticsAgeNo;
            //        chartBranchDataVM.Value = CategoryCount;
            //        chartBranchDataVMs.Add(chartBranchDataVM);
            //        count_Renters = CategoryCount + count_Renters;
            //    }
            //    ViewBag.count_Renters = count_Renters;
            //}


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
        ViewBag.no = "14";
        var titles = await setTitle("205", "2205015", "2");
        await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
        var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205015", "2");
        ViewBag.Data = "0";
        return View();

    }
}
}

