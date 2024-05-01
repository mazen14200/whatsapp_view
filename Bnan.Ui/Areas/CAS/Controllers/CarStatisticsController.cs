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
    public class CarStatisticsController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IFinancialTransactionOfRenter _CarContract;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<CarStatisticsController> _localizer;


        public CarStatisticsController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IFinancialTransactionOfRenter CarContract,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<CarStatisticsController> localizer) : base(userManager, unitOfWork, mapper)
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
            ViewBag.no = "12";

            var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205013", "2");

            var titles = await setTitle("205", "2205013", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllCars = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor , new[] { "CrCasCarInformation1" }).ToList();

            if (AllCars?.Count() < 1)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }
            var Branch_count = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor && x.CrCasCarInformationBranchStatus != Status.Deleted).DistinctBy(x => x.CrCasCarInformationBranch).Count();
            var City_count = Branch_count;
            var rigon_count = Branch_count;
            var Brand_count = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor).DistinctBy(x => x.CrCasCarInformationBrand).Count();
            var year_count = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor ).DistinctBy(x => x.CrCasCarInformationYear).Count();
            var Category_count = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor ).DistinctBy(x => x.CrCasCarInformationCategory).Count();
            var Model_count = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor).DistinctBy(x => x.CrCasCarInformationModel).Count();
            var Status_count = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor ).DistinctBy(x => x.CrCasCarInformationStatus).Count();

            if (Branch_count < 2 && Brand_count <2 && year_count < 2 && Category_count < 2 && Model_count < 2 && Status_count < 2 )
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }
            if (Status_count < 2)
            {
                return RedirectToAction("FailedMessageReport_NoData");
            }
            string concate_DropDown = "";
            if (Status_count > 1) concate_DropDown = concate_DropDown + "0";
            if (Branch_count > 1) concate_DropDown = concate_DropDown + "167";
            if (Brand_count > 1) concate_DropDown = concate_DropDown + "2";
            if (year_count > 1) concate_DropDown = concate_DropDown + "3";
            if (Category_count > 1) concate_DropDown = concate_DropDown + "4";
            if (Model_count > 1) concate_DropDown = concate_DropDown + "5";
            ViewBag.concate_DropDown = concate_DropDown;


            var AllBranches = _unitOfWork.CrCasBranchInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasBranchInformationLessor && x.CrCasBranchInformationStatus != Status.Deleted).ToList();

            List<ChartBranchDataVM> chartBranchDataVMs = new List<ChartBranchDataVM>();
            var count_Cars = 0;
            foreach (var branch in AllBranches)
            {
                var BranchCount = 0;
                BranchCount = AllCars.Count(x=>x.CrCasCarInformationBranch == branch.CrCasBranchInformationCode);
                ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                chartBranchDataVM.ArName = branch.CrCasBranchInformationArShortName;
                chartBranchDataVM.EnName = branch.CrCasBranchInformationEnShortName;
                chartBranchDataVM.Code = branch.CrCasBranchInformationCode;
                chartBranchDataVM.Value = BranchCount;
                chartBranchDataVMs.Add(chartBranchDataVM);
                count_Cars = BranchCount + count_Cars;

            }
            chartBranchDataVMs = chartBranchDataVMs.OrderByDescending(x => x.Value).ToList();
            var Type_Avarage = chartBranchDataVMs.Average(x => x.Value);
            var Type_Sum = chartBranchDataVMs.Sum(x => x.Value);
            var Type_Count = chartBranchDataVMs.Count();
            var Type_Avarage_percentage = Type_Avarage/Type_Sum;
            var Static_Percentage_rate = 0.10;

            ViewBag.count_Cars = count_Cars;
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
            ViewBag.singleType =  "1";
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
            ViewBag.no = "12";
            ViewBag.concate_DropDown = listDrop;
            ViewBag.singleType = singleNo;



            var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205013", "2");

            var titles = await setTitle("205", "2205013", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var AllCars = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor, new[] { "CrCasCarInformation1" }).ToList();

            List<ChartBranchDataVM> chartBranchDataVMs = new List<ChartBranchDataVM>();
            var count_Cars = 0;

            if (Type == "Category")
            {
                var AllCategories = _unitOfWork.CrMasSupCarCategory.FindAll(x => x.CrMasSupCarCategoryStatus != Status.Deleted).ToList();

                foreach (var single in AllCategories)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCars.Count(x => x.CrCasCarInformationCategory == single.CrMasSupCarCategoryCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrMasSupCarCategoryArName;
                    chartBranchDataVM.EnName = single.CrMasSupCarCategoryEnName;
                    chartBranchDataVM.Code = single.CrMasSupCarCategoryCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Cars = CategoryCount + count_Cars;
                }
                ViewBag.count_Cars = count_Cars;
            }
            if (Type == "Branches")
            {
                var AllBranches = _unitOfWork.CrCasBranchInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasBranchInformationLessor && x.CrCasBranchInformationStatus != Status.Deleted).ToList();

                foreach (var single in AllBranches)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCars.Count(x => x.CrCasCarInformationBranch == single.CrCasBranchInformationCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasBranchInformationArShortName;
                    chartBranchDataVM.EnName = single.CrCasBranchInformationEnShortName;
                    chartBranchDataVM.Code = single.CrCasBranchInformationCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Cars = CategoryCount + count_Cars;
                }
                ViewBag.count_Cars = count_Cars;
            }
            if (Type == "Brand")
            {
                var AllCars2_Brand = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor, new[] { "CrCasCarInformationBrandNavigation" }).Where(x => x.CrCasCarInformationBrandNavigation?.CrMasSupCarBrandStatus != Status.Deleted).DistinctBy(x => x.CrCasCarInformationBrand).ToList();
                
                foreach (var single in AllCars2_Brand)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCars.Count(x => x.CrCasCarInformationBrand == single.CrCasCarInformationBrandNavigation?.CrMasSupCarBrandCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasCarInformationBrandNavigation?.CrMasSupCarBrandArName;
                    chartBranchDataVM.EnName = single.CrCasCarInformationBrandNavigation?.CrMasSupCarBrandEnName;
                    chartBranchDataVM.Code = single.CrCasCarInformationBrandNavigation?.CrMasSupCarBrandCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Cars = CategoryCount + count_Cars;
                }
                ViewBag.count_Cars = count_Cars;
            }
            if (Type == "year")
            {
                var AllCars2_year = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor && x.CrCasCarInformationStatus != Status.Deleted).DistinctBy(x=>x.CrCasCarInformationYear).ToList();

                foreach (var single in AllCars2_year)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCars.Count(x => x.CrCasCarInformationYear == single.CrCasCarInformationYear);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasCarInformationYear;
                    chartBranchDataVM.EnName = single.CrCasCarInformationYear;
                    chartBranchDataVM.Code = single.CrCasCarInformationYear;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Cars = CategoryCount + count_Cars;
                }
                ViewBag.count_Cars = count_Cars;
            }
            if (Type == "Model")
            {
                var AllCars2_Model = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor, new[] { "CrCasCarInformationModelNavigation" }).Where(x => x.CrCasCarInformationModelNavigation?.CrMasSupCarModelStatus != Status.Deleted).DistinctBy(x => x.CrCasCarInformationModel).ToList();

                foreach (var single in AllCars2_Model)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCars.Count(x => x.CrCasCarInformationModel == single.CrCasCarInformationModelNavigation?.CrMasSupCarModelCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasCarInformationModelNavigation?.CrMasSupCarModelArName;
                    chartBranchDataVM.EnName = single.CrCasCarInformationModelNavigation?.CrMasSupCarModelEnName;
                    chartBranchDataVM.Code = single.CrCasCarInformationModelNavigation?.CrMasSupCarModelCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Cars = CategoryCount + count_Cars;
                }
                ViewBag.count_Cars = count_Cars;
            }
            if (Type == "City")
            {
                var AllCars2_City = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor, new[] { "CrCasCarInformationCityNavigation" }).Where(x => x.CrCasCarInformationCityNavigation?.CrMasSupPostCityStatus != Status.Deleted).DistinctBy(x => x.CrCasCarInformationCity).ToList();

                foreach (var single in AllCars2_City)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCars.Count(x => x.CrCasCarInformationCity == single.CrCasCarInformationCityNavigation?.CrMasSupPostCityCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasCarInformationCityNavigation?.CrMasSupPostCityArName;
                    chartBranchDataVM.EnName = single.CrCasCarInformationCityNavigation?.CrMasSupPostCityEnName;
                    chartBranchDataVM.Code = single.CrCasCarInformationCityNavigation?.CrMasSupPostCityCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Cars = CategoryCount + count_Cars;
                }
                ViewBag.count_Cars = count_Cars;
            }
            if (Type == "Rigon")
            {
                var AllCars2_Rigon = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor, new[] { "CrCasCarInformationRegionNavigation" }).Where(x => x.CrCasCarInformationRegionNavigation?.CrMasSupPostRegionsStatus != Status.Deleted).DistinctBy(x => x.CrCasCarInformationRegion).ToList();

                foreach (var single in AllCars2_Rigon)
                {
                    var CategoryCount = 0;
                    CategoryCount = AllCars.Count(x => x.CrCasCarInformationRegion == single.CrCasCarInformationRegionNavigation?.CrMasSupPostRegionsCode);
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.ArName = single.CrCasCarInformationRegionNavigation?.CrMasSupPostRegionsArName;
                    chartBranchDataVM.EnName = single.CrCasCarInformationRegionNavigation?.CrMasSupPostRegionsEnName;
                    chartBranchDataVM.Code = single.CrCasCarInformationRegionNavigation?.CrMasSupPostRegionsCode;
                    chartBranchDataVM.Value = CategoryCount;
                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Cars = CategoryCount + count_Cars;
                }
                ViewBag.count_Cars = count_Cars;
            }
            if (Type == "Status")
            {
                List<ChartBranchDataVM> chartBranchDataVMs_Alls = new List<ChartBranchDataVM>();

                var AllCars2_Rigon = _unitOfWork.CrCasCarInformation.FindAll(x => currentCar.CrMasUserInformationLessor == x.CrCasCarInformationLessor).ToList();

                foreach (var single in AllCars2_Rigon)
                {
                    ChartBranchDataVM chartBranchDataVM_All = new ChartBranchDataVM();

                    if (single.CrCasCarInformationStatus == "H" || single.CrCasCarInformationOwnerStatus == "H" || single.CrCasCarInformationBranchStatus == "H" )
                    {
                        chartBranchDataVM_All.ArName = "موقوفة";
                        chartBranchDataVM_All.EnName = "Hold";
                    }
                    else if (single.CrCasCarInformationStatus == "A" && single.CrCasCarInformationForSaleStatus == "A" && single.CrCasCarInformationOwnerStatus == "A" && single.CrCasCarInformationBranchStatus == "A" && single.CrCasCarInformationPriceStatus == true)
                    {
                        chartBranchDataVM_All.ArName = "نشطة";
                        chartBranchDataVM_All.EnName = "Active";
                    }
                    else if (single.CrCasCarInformationStatus == "A" && (single.CrCasCarInformationForSaleStatus == "T" || single.CrCasCarInformationForSaleStatus == "V"))
                    {
                        chartBranchDataVM_All.ArName = "للبيع";
                        chartBranchDataVM_All.EnName = "ForSale";
                    }
                    else if (single.CrCasCarInformationStatus == "A" && single.CrCasCarInformationOwnerStatus == "A" && single.CrCasCarInformationBranchStatus == "A" && single.CrCasCarInformationPriceStatus == false )
                    {
                        chartBranchDataVM_All.ArName = "بدون سعر";
                        chartBranchDataVM_All.EnName = "Without Price";
                    }
                    else if (single.CrCasCarInformationStatus == "R")
                    {
                        chartBranchDataVM_All.ArName = "مؤجرة";
                        chartBranchDataVM_All.EnName = "Rented";
                    }
                    else if (single.CrCasCarInformationStatus == "M")
                    {
                        chartBranchDataVM_All.ArName = "إصلاح";
                        chartBranchDataVM_All.EnName = "Fix";
                    }

                    chartBranchDataVM_All.Code = single.CrCasCarInformationStatus;
                    chartBranchDataVM_All.Value = 0;
                    chartBranchDataVMs_Alls.Add(chartBranchDataVM_All);

                }

                var AllStatus_Type = chartBranchDataVMs_Alls.DistinctBy(x=>x.EnName).ToList();
                
                foreach (var ssingl in AllStatus_Type)
                {
                    ChartBranchDataVM chartBranchDataVM = new ChartBranchDataVM();
                    chartBranchDataVM.EnName = ssingl.EnName;
                    chartBranchDataVM.ArName = ssingl.ArName;
                    chartBranchDataVM.Code = ssingl.Code;
                    var TypeStatusCount = 0;
                    TypeStatusCount = chartBranchDataVMs_Alls.Count(x => x.EnName == ssingl.EnName);
                    chartBranchDataVM.Value = (decimal)TypeStatusCount;

                    chartBranchDataVMs.Add(chartBranchDataVM);
                    count_Cars = TypeStatusCount + count_Cars;


                }
                ViewBag.count_Cars = count_Cars;
            }

            chartBranchDataVMs = chartBranchDataVMs.OrderByDescending(x => x.Value).ToList();
            var Type_Avarage = chartBranchDataVMs.Average(x => x.Value);
            var Type_Sum = chartBranchDataVMs.Sum(x => x.Value);
            var Type_Count = chartBranchDataVMs.Count();
            var Type_Avarage_percentage = Type_Avarage / Type_Sum;
            var Static_Percentage_rate = 0.10;

            //ViewBag.count_Cars = count_Cars;
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
            var count_bar = 0;
            count_bar = chartBranchDataVMs.Count();
            var WidthChart = (count_bar * 50) + 200;
            ViewBag.WidthChart = WidthChart;
            ViewBag.Width_bar = WidthChart / count_bar ;

            return View(casStatisticLayoutVM);
        }


    public async Task<IActionResult> FailedMessageReport_NoData()
    {
        //sidebar Active
        ViewBag.id = "#sidebarReport";
        ViewBag.no = "12";
        var titles = await setTitle("205", "2205013", "2");
        await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
        var (mainTask, subTask, system, currentCar) = await SetTrace("205", "2205013", "2");
        ViewBag.Data = "0";
        return View();

    }
}
}

