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
    public class RenterContractController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly UserManager<CrMasUserInformation> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IRenterContract _RenterContract;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<RenterContractController> _localizer;


        public RenterContractController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IRenterContract RenterContract,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IStringLocalizer<RenterContractController> localizer) : base(userManager, unitOfWork, mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userService = userService;
            _RenterContract = RenterContract;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "1";
            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203002", "2");

            var titles = await setTitle("203", "2203002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterContractAll = _unitOfWork.CrCasRenterLessor.FindAll(x => x.CrCasRenterLessorCode == currentUser.CrMasUserInformationLessor && x.CrCasRenterLessorContractCount > 0,new[] { "CrCasRenterLessorNavigation", "CrCasRenterLessorMembershipNavigation" }).ToList();


            var rates = _unitOfWork.CrMasSysEvaluation.FindAll(x => x.CrMasSysEvaluationsClassification == "1" && x.CrMasSysEvaluationsStatus == "A").ToList();

            ViewData["Rates"] = rates;


            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            return View(RenterContractAll);
        }


        [HttpGet]
        public async Task<PartialViewResult> GetRenterContractByStatusAsync(string status)
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "1";
            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203002", "2");

            if (!string.IsNullOrEmpty(status))
            {
                

                if (status == Status.All)
                {

                    var RenterContractbyStatusAll = _unitOfWork.CrCasRenterLessor.FindAll(l => l.CrCasRenterLessorCode == currentUser.CrMasUserInformationLessor && l.CrCasRenterLessorContractCount > 0, new[] { "CrCasRenterLessorNavigation", "CrCasRenterLessorMembershipNavigation" }).ToList();
                    return PartialView("_DataTableRenterContract", RenterContractbyStatusAll);
                }
                var RenterContractbyStatus = _unitOfWork.CrCasRenterLessor.FindAll(x => x.CrCasRenterLessorStatus == status && x.CrCasRenterLessorCode == currentUser.CrMasUserInformationLessor && x.CrCasRenterLessorContractCount > 0, new[] { "CrCasRenterLessorNavigation", "CrCasRenterLessorMembershipNavigation" }).ToList();
                return PartialView("_DataTableRenterContract", RenterContractbyStatus);
            }
            return PartialView();
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "1";

            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203002", "2");

            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("203", "2203002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == id, new[] { "CrCasRenterLessorNavigation", "CrCasRenterLessorMembershipNavigation" });
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var Mechanism = _unitOfWork.CrMasSysEvaluation.FindAll(x => x.CrMasSysEvaluationsClassification == "1" && x.CrMasSysEvaluationsStatus == "A").ToList();
            ViewData["Mechanism"] = Mechanism;

            var workPlace = _unitOfWork.CrMasSupRenterEmployer.Find(x => x.CrMasSupRenterEmployerCode == contract.CrCasRenterLessorNavigation.CrMasRenterInformationEmployer);
            ViewBag.workPlaceAr = workPlace?.CrMasSupRenterEmployerArName;
            ViewBag.workPlaceEn = workPlace?.CrMasSupRenterEmployerEnName;

            var Bank = _unitOfWork.CrMasSupAccountBanks.Find(x => x.CrMasSupAccountBankCode == contract.CrCasRenterLessorNavigation.CrMasRenterInformationBank);
            ViewBag.BankAr = Bank?.CrMasSupAccountBankArName;
            ViewBag.BankEn = Bank?.CrMasSupAccountBankEnName;

            var DrivingType = _unitOfWork.CrElmLicense.Find(x => x.CrElmLicenseNo == contract.CrCasRenterLessorNavigation.CrMasRenterInformationDrivingLicenseNo);
            ViewBag.DrivingTypeAr = DrivingType.CrElmLicenseArName;
            ViewBag.DrivingTypeEn = DrivingType.CrElmLicenseEnName;


            var RenterContract_address = _unitOfWork.CrMasRenterPost.Find(x => x.CrMasRenterPostArShortConcatenate != null);
            ViewBag.AddressAr = RenterContract_address?.CrMasRenterPostArShortConcatenate;
            ViewBag.AddressEn = RenterContract_address?.CrMasRenterPostEnShortConcatenate;

            //ViewBag.Mechanism = contract?.CrCasRenterLessorDealingMechanism;

            int countRenterContracts = 0;
            ViewBag.RenterContracts_Count = countRenterContracts;
            var model = _mapper.Map<CasRenterContractVM>(contract);

            var RenterContract_Basic_All = _unitOfWork.CrCasRenterContractBasic.FindAll(x=> x.CrCasRenterContractBasicRenterId == model.CrCasRenterLessorId && currentUser.CrMasUserInformationLessor == x.CrCasRenterContractBasicLessor && x.CrCasRenterContractBasicStatus != Status.Extension, new[] {  "CrCasRenterContractBasicCarSerailNoNavigation"}).ToList();

            ViewData["Data_Table"] = RenterContract_Basic_All;

            return View(model);
        }

        public async Task<IActionResult> FailedMessageReport_NoData()
        {
            //sidebar Active
            ViewBag.id = "#sidebarRenter";
            ViewBag.no = "1";
            var (mainTask, subTask, system, currentUser) = await SetTrace("203", "2203002", "2");

            var RenterContractAll = _unitOfWork.CrCasRenterLessor.GetAll(new[] { "CrCasRenterLessorNavigation", "CrCasRenterLessorMembershipNavigation" }).Where(x => x.CrCasRenterLessorCode == currentUser.CrMasUserInformationLessor && x.CrCasRenterLessorContractCount > 0);

            if (RenterContractAll?.Count() < 1)
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
