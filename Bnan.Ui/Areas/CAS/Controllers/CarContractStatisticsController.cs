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
    public class CarContractStatisticsController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IFinancialTransactionOfRenter _CarContract;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<CarContractStatisticsController> _localizer;


        public CarContractStatisticsController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IFinancialTransactionOfRenter CarContract, 
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<CarContractStatisticsController> localizer) : base(userManager, unitOfWork, mapper)
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
            ViewBag.no = "15";

            var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205016", "2");

            var titles = await setTitle("205", "2205016", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllCarContracts = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor ).OrderByDescending(x=>x.CrCasRenterContractStatisticsDate).ToList();
            ViewBag.StartDate = AllCarContracts?.LastOrDefault()?.CrCasRenterContractStatisticsDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            ViewBag.EndDate = AllCarContracts?.FirstOrDefault()?.CrCasRenterContractStatisticsDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);


            if (AllCarContracts?.Count() < 1)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }

            var Brand_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsBrand).Count();
            var Category_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsCategory).Count();
            var Model_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsModel).Count();
            var Car_year_count = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsCarYear).Count();

            if (Brand_count < 2 && Category_count < 2 && Model_count < 2 && Car_year_count < 2 )
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }

            string concate_DropDown = "";
            if (Brand_count > 1) concate_DropDown = concate_DropDown + "0";
            if (Category_count > 1) concate_DropDown = concate_DropDown + "1";
            if (Model_count > 1) concate_DropDown = concate_DropDown + "2";
            if (Car_year_count > 1) concate_DropDown = concate_DropDown + "3";
            ViewBag.concate_DropDown = concate_DropDown;



            var AllBrands = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor, new[] { "CrCasRenterContractStatisticsBrandNavigation" }).Where(x => x.CrCasRenterContractStatisticsBrandNavigation?.CrMasSupCarBrandStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsBrand).ToList();


            List<ChartBranchDataVM> chartBranchDataVMs = new List<ChartBranchDataVM>();
            var count_Renters = 0;
            foreach (var branch in AllBrands)
            {
                var BranchCount = 0;
                BranchCount = AllCarContracts.Count(x=>x.CrCasRenterContractStatisticsBrand == branch.CrCasRenterContractStatisticsBrandNavigation?.CrMasSupCarBrandCode);
                ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                chartBranchDataVM.ArName = branch.CrCasRenterContractStatisticsBrandNavigation?.CrMasSupCarBrandArName;
                chartBranchDataVM.EnName = branch.CrCasRenterContractStatisticsBrandNavigation?.CrMasSupCarBrandEnName;
                chartBranchDataVM.Code = branch.CrCasRenterContractStatisticsBrandNavigation?.CrMasSupCarBrandCode;
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
            ViewBag.no = "15";
            ViewBag.concate_DropDown = listDrop;
            ViewBag.singleType = singleNo;
            ViewBag.StartDate = DateTime.Parse(startDate).Date.ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Parse(endDate).Date.ToString("yyyy-MM-dd");
            var _max = DateTime.Parse(endDate).Date.AddDays(1);
            var _mini = DateTime.Parse(startDate).Date;

            var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205016", "2");

            var titles = await setTitle("205", "2205016", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllCarContracts = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor && x.CrCasRenterContractStatisticsDate < _max && x.CrCasRenterContractStatisticsDate >= _mini).ToList();
            
            if (AllCarContracts?.Count() < 1)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }

            List<ChartBranchDataVM> chartBranchDataVMs = new List<ChartBranchDataVM>();
            var count_Renters = 0;

            if (Type == "Brand")
            {
                var AllBrands = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor, new[] { "CrCasRenterContractStatisticsBrandNavigation" }).Where(x => x.CrCasRenterContractStatisticsBrandNavigation?.CrMasSupCarBrandStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsBrand).ToList();

                foreach (var single in AllBrands)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCarContracts.Count(x => x.CrCasRenterContractStatisticsBrand == single.CrCasRenterContractStatisticsBrandNavigation?.CrMasSupCarBrandCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterContractStatisticsBrandNavigation?.CrMasSupCarBrandArName;
                    chartBranchDataVM.EnName = single.CrCasRenterContractStatisticsBrandNavigation?.CrMasSupCarBrandEnName;
                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsBrandNavigation?.CrMasSupCarBrandCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "Model")
            {
                var AllCarContracts2_Model = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor, new[] { "CrCasRenterContractStatisticsModelNavigation" }).Where(x => x.CrCasRenterContractStatisticsModelNavigation?.CrMasSupCarModelStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsModel).ToList();

                foreach (var single in AllCarContracts2_Model)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCarContracts.Count(x => x.CrCasRenterContractStatisticsModel == single.CrCasRenterContractStatisticsModelNavigation?.CrMasSupCarModelCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterContractStatisticsModelNavigation?.CrMasSupCarModelArName;
                    chartBranchDataVM.EnName = single.CrCasRenterContractStatisticsModelNavigation?.CrMasSupCarModelEnName;
                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsModelNavigation?.CrMasSupCarModelCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "Category")
            {
                var AllCarContracts2_Category = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor , new[] { "CrCasRenterContractStatisticsCategoryNavigation" } ).Where(x => x.CrCasRenterContractStatisticsCategoryNavigation?.CrMasSupCarCategoryStatus != Status.Deleted).DistinctBy(x => x.CrCasRenterContractStatisticsCategory).ToList();

                foreach (var single in AllCarContracts2_Category)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCarContracts.Count(x => x.CrCasRenterContractStatisticsCategory == single.CrCasRenterContractStatisticsCategoryNavigation?.CrMasSupCarCategoryCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterContractStatisticsCategoryNavigation?.CrMasSupCarCategoryArName;
                    chartBranchDataVM.EnName = single.CrCasRenterContractStatisticsCategoryNavigation?.CrMasSupCarCategoryEnName;
                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsCategoryNavigation?.CrMasSupCarCategoryCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Renters = CategoryCount + count_Renters;
                }
                ViewBag.count_Renters = count_Renters;
            }
            if (Type == "Car_year")
            {
                var AllCarContracts2_Car_year = _unitOfWork.CrCasRenterContractStatistic.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasRenterContractStatisticsLessor).DistinctBy(x => x.CrCasRenterContractStatisticsCarYear).ToList();

                foreach (var single in AllCarContracts2_Car_year)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCarContracts.Count(x => x.CrCasRenterContractStatisticsCarYear == single.CrCasRenterContractStatisticsCarYear);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasRenterContractStatisticsCarYear;
                    chartBranchDataVM.EnName = single.CrCasRenterContractStatisticsCarYear;
                    chartBranchDataVM.Code = single.CrCasRenterContractStatisticsCarYear;
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
        ViewBag.no = "15";
        var titles = await setTitle("205", "2205016", "2");
        await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
        var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205016", "2");
        ViewBag.Data = "0";
        return View();

    }
}
}

