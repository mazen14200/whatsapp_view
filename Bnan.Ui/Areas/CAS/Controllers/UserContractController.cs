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

            var titles = await setTitle("205", "2205007", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var UserContractAll = _unitOfWork.CrCasRenterContractBasic.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor && x.CrCasRenterContractBasicStatus != Status.Extension, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).OrderByDescending(x=>x.CrCasRenterContractBasicExpectedTotal).ToList();
            var AllUsers = _unitOfWork.CrMasUserInformation.GetAll(new[] { "CrMasUserContractValidity" } ).Where(x => UserContractAll.Any(y => y.CrCasRenterContractBasicUserInsert == x.CrMasUserInformationCode)).ToList();

            List<CrCasRenterContractBasic>? ContractBasic_Filtered = new List<CrCasRenterContractBasic>();

            List<List<string>>? contracts_Counts = new List<List<string>>();
            
            foreach ( var contract in UserContractAll)
            {
                var contract_count = 0;
                decimal? contract_Total = 0;
                var x = ContractBasic_Filtered.Find(x => x.CrCasRenterContractBasicUserInsert == contract.CrCasRenterContractBasicUserInsert);
                if (x == null) {
                    var counter = 0;
                    foreach (var contract_2 in UserContractAll)
                    {
                        if (contract.CrCasRenterContractBasicUserInsert == contract_2.CrCasRenterContractBasicUserInsert)
                        {
                            contract_Total = contract_2.CrCasRenterContractBasicExpectedTotal + contract_Total;
                            counter = counter +1; 
                        }

                    }
                    contracts_Counts.Add(new List<string> { contract.CrCasRenterContractBasicUserInsert, counter.ToString() , contract_Total.ToString() });
                    ContractBasic_Filtered.Add(contract);
                }
            }

            UserContractVM userContractVM = new UserContractVM();
            userContractVM.crCasRenterContractBasics = UserContractAll;
            userContractVM.crMasUserInformation = AllUsers;
            userContractVM.contracts_Counts = contracts_Counts;
            userContractVM.ContractBasic_Filtered = ContractBasic_Filtered;

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            return View(userContractVM);
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

            var UserContractAll = _unitOfWork.CrCasRenterContractBasic.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor && id == x.CrCasRenterContractBasicUserInsert && x.CrCasRenterContractBasicStatus != Status.Extension , new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).OrderByDescending(x => x.CrCasRenterContractBasicExpectedTotal).ToList();
            var AllUsers = _unitOfWork.CrMasUserInformation.GetAll(new[] { "CrMasUserContractValidity" }).Where(x => UserContractAll.Any(y => y.CrCasRenterContractBasicUserInsert == x.CrMasUserInformationCode)).ToList();

            List<CrCasRenterContractBasic>? ContractBasic_Filtered = new List<CrCasRenterContractBasic>();

            List<List<string>>? contracts_Counts = new List<List<string>>();

            foreach (var contract in UserContractAll)
            {
                var contract_count = 0;
                decimal? contract_Total = 0;
                var x = ContractBasic_Filtered.Find(x => x.CrCasRenterContractBasicUserInsert == contract.CrCasRenterContractBasicUserInsert);
                if (x == null)
                {
                    var counter = 0;
                    foreach (var contract_2 in UserContractAll)
                    {
                        if (contract.CrCasRenterContractBasicUserInsert == contract_2.CrCasRenterContractBasicUserInsert)
                        {
                            contract_Total = contract_2.CrCasRenterContractBasicExpectedTotal + contract_Total;
                            counter = counter + 1;
                        }

                    }
                    contracts_Counts.Add(new List<string> { contract.CrCasRenterContractBasicUserInsert, counter.ToString(), contract_Total.ToString() });
                    ContractBasic_Filtered.Add(contract);
                }
            }

            UserContractVM userContractVM = new UserContractVM();
            userContractVM.crCasRenterContractBasics = UserContractAll;
            userContractVM.crMasUserInformation = AllUsers;
            userContractVM.contracts_Counts = contracts_Counts;
            userContractVM.ContractBasic_Filtered = ContractBasic_Filtered;


            if (UserContractAll == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }

            ViewBag.CountRecord = UserContractAll.Count;

            var Single_data = _unitOfWork.CrMasUserInformation.Find(x => currentUser.CrMasUserInformationLessor == x.CrMasUserInformationLessor && id == x.CrMasUserInformationCode);

            ViewBag.Single_UserId = Single_data.CrMasUserInformationCode;
            ViewBag.Single_UserNameAr = Single_data.CrMasUserInformationArName;
            ViewBag.Single_UserNameEn = Single_data.CrMasUserInformationEnName;

            return View(userContractVM);
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
                _max = DateTime.Parse(_max).Date.AddDays(1).ToString("yyyy-MM-dd");
                //var UserContractAll = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicIssuedDate < DateTime.Parse(_max).Date && x.CrCasRenterContractBasicIssuedDate >= DateTime.Parse(_mini).Date && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor && id == x.CrCasRenterContractBasicUserInsert && x.CrCasRenterContractBasicStatus != Status.Extension && x.CrCasRenterContractBasic3.CrCasRenterPrivateUserInformationContractCount > 0, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).OrderByDescending(x => x.CrCasRenterContractBasicExpectedTotal).ToList();
                var UserContractAll = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicIssuedDate < DateTime.Parse(_max).Date && x.CrCasRenterContractBasicIssuedDate >= DateTime.Parse(_mini).Date && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor && id == x.CrCasRenterContractBasicUserInsert && x.CrCasRenterContractBasicStatus != Status.Extension, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).OrderByDescending(x => x.CrCasRenterContractBasicExpectedTotal).ToList();

                var AllUsers = _unitOfWork.CrMasUserInformation.GetAll(new[] { "CrMasUserContractValidity" }).Where(x => UserContractAll.Any(y => y.CrCasRenterContractBasicUserInsert == x.CrMasUserInformationCode)).ToList();

                List<CrCasRenterContractBasic>? ContractBasic_Filtered = new List<CrCasRenterContractBasic>();

                List<List<string>>? contracts_Counts = new List<List<string>>();

                foreach (var contract in UserContractAll)
                {
                    var contract_count = 0;
                    decimal? contract_Total = 0;
                    var x = ContractBasic_Filtered.Find(x => x.CrCasRenterContractBasicUserInsert == contract.CrCasRenterContractBasicUserInsert);
                    if (x == null)
                    {
                        var counter = 0;
                        foreach (var contract_2 in UserContractAll)
                        {
                            if (contract.CrCasRenterContractBasicUserInsert == contract_2.CrCasRenterContractBasicUserInsert)
                            {
                                contract_Total = contract_2.CrCasRenterContractBasicExpectedTotal + contract_Total;
                                counter = counter + 1;
                            }

                        }
                        contracts_Counts.Add(new List<string> { contract.CrCasRenterContractBasicUserInsert, counter.ToString(), contract_Total.ToString() });
                        ContractBasic_Filtered.Add(contract);
                    }
                }

                UserContractVM userContractVM = new UserContractVM();
                userContractVM.crCasRenterContractBasics = UserContractAll;
                userContractVM.crMasUserInformation = AllUsers;
                userContractVM.contracts_Counts = contracts_Counts;
                userContractVM.ContractBasic_Filtered = ContractBasic_Filtered;


                if (UserContractAll == null)
                {
                    ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                    return View("Index");
                }

                ViewBag.CountRecord = UserContractAll.Count;

                var Single_data = _unitOfWork.CrMasUserInformation.Find(x => currentUser.CrMasUserInformationLessor == x.CrMasUserInformationLessor && id == x.CrMasUserInformationCode);

                ViewBag.Single_UserId = Single_data.CrMasUserInformationCode;
                ViewBag.Single_UserNameAr = Single_data.CrMasUserInformationArName;
                ViewBag.Single_UserNameEn = Single_data.CrMasUserInformationEnName;

                return View(userContractVM);

            }

            return View();
        }
        public async Task<IActionResult> FailedMessageReport_NoData()
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "6";
            var titles = await setTitle("205", "2205007", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205007", "2");

            var UserContractAll = _unitOfWork.CrCasRenterContractBasic.FindAll(x => currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor && x.CrCasRenterContractBasicStatus != Status.Extension, new[] { "CrCasRenterContractBasic1", "CrCasRenterContractBasic2", "CrCasRenterContractBasic3", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasicNavigation", "CrCasRenterContractBasic4" }).OrderByDescending(x => x.CrCasRenterContractBasicExpectedTotal).ToList();
            if (UserContractAll?.Count() < 1)
            {
                ViewBag.Data = "0";
                //_toastNotification.AddErrorToastMessage(_localizer["NoDataToShow"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return View();
            }
            else
            {
                ViewBag.Data = "1";
                return RedirectToAction("Index");
            }

        }
    }
}

