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
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class TransferToTenantController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<TransferToTenantController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly ICustody _Custody;


        public TransferToTenantController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
             IMapper mapper, IUserService userService, IAccountBank accountBank,
             IUserLoginsService userLoginsService, IToastNotification toastNotification,
             IStringLocalizer<TransferToTenantController> localizer, IAdminstritiveProcedures adminstritiveProcedures, ICustody custody) :
             base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _adminstritiveProcedures = adminstritiveProcedures;
            _Custody = custody;
        }
        public async Task<IActionResult> Index()
        {
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "2";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("204", "2204003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterAvaiable = _unitOfWork.CrCasRenterLessor.FindAll(x => x.CrCasRenterLessorCode == lessorCode &&
                                                                          x.CrCasRenterLessorBalance < 0 &&
                                                                          x.CrCasRenterLessorStatus == Status.Active, new[] {"CrCasRenterContractBasicCrCasRenterContractBasic4s",
                                                                                                                           "CrCasRenterLessorNavigation" }).ToList();
            var RenterAvaiableVM = _mapper.Map<List<RenterLessorVM>>(RenterAvaiable);
            foreach (var item in RenterAvaiableVM)
            {
                item.CrMasSysEvaluation = _unitOfWork.CrMasSysEvaluation.Find(x => x.CrMasSysEvaluationsCode == item.CrCasRenterLessorDealingMechanism);
            }
            return View(RenterAvaiableVM);
        }
        public async Task<IActionResult> TransferTo(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "2";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("204", "2204003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تحويل", "Transfer", titles[3]);

            var Renter = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == id && x.CrCasRenterLessorCode == lessorCode, new[] {"CrCasRenterContractBasicCrCasRenterContractBasic4s",
                                                                                                                           "CrCasRenterLessorNavigation" });
            var RenterVM = _mapper.Map<RenterLessorVM>(Renter);

            RenterVM.CrMasSysEvaluation = _unitOfWork.CrMasSysEvaluation.Find(x => x.CrMasSysEvaluationsCode == RenterVM.CrCasRenterLessorDealingMechanism);
            RenterVM.Banks = _unitOfWork.CrMasSupAccountBanks.FindAll(x => x.CrMasSupAccountBankStatus == Status.Active&&x.CrMasSupAccountBankCode!="00").ToList();
            RenterVM.AccountBanks = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankStatus == Status.Active&&x.CrCasAccountBankNo!="00" && x.CrCasAccountBankLessor == lessorCode).ToList();
            return View(RenterVM);
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountBankNo(string AccountNo)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var account= await _unitOfWork.CrCasAccountBank.FindAsync(x=>x.CrCasAccountBankCode == AccountNo&&x.CrCasAccountBankLessor== lessorCode, new[] { "CrCasAccountBankNoNavigation" });
            var result = new
            {
                accountNo = account.CrCasAccountBankCode,
                accountIban = account.CrCasAccountBankIban,
                bankNo = account.CrCasAccountBankNoNavigation?.CrMasSupAccountBankCode,
                arBank = account.CrCasAccountBankNoNavigation?.CrMasSupAccountBankArName,
                enBank = account.CrCasAccountBankNoNavigation?.CrMasSupAccountBankEnName,
            };
            return Json(result);
        }
    }
}
