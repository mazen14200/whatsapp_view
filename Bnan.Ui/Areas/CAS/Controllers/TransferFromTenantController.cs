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
    public class TransferFromTenantController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<TransferFromTenantController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly ITransferToFromRenter _tranferToRenter;


        public TransferFromTenantController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
             IMapper mapper, IUserService userService, IAccountBank accountBank,
             IUserLoginsService userLoginsService, IToastNotification toastNotification,
             IStringLocalizer<TransferFromTenantController> localizer, IAdminstritiveProcedures adminstritiveProcedures, ITransferToFromRenter tranferToRenter) :
             base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _adminstritiveProcedures = adminstritiveProcedures;
            _tranferToRenter = tranferToRenter;
        }
        public async Task<IActionResult> Index()
        {
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "3";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("204", "2204004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterAvaiable = _unitOfWork.CrCasRenterLessor.FindAll(x => x.CrCasRenterLessorCode == lessorCode, new[] {"CrCasRenterContractBasicCrCasRenterContractBasic4s",
                                                                                                                           "CrCasRenterLessorNavigation" }).ToList();
            var RenterAvaiableVM = _mapper.Map<List<RenterLessorVM>>(RenterAvaiable);
            foreach (var item in RenterAvaiableVM)
            {
                item.CrMasSysEvaluation = _unitOfWork.CrMasSysEvaluation.Find(x => x.CrMasSysEvaluationsCode == item.CrCasRenterLessorDealingMechanism);
            }
            return View(RenterAvaiableVM);
        }

        [HttpGet]
        public async Task<IActionResult> TransferFrom(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "3";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("204", "2204004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تحويل", "Transfer", titles[3]);

            var Renter = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == id && x.CrCasRenterLessorCode == lessorCode, new[] {"CrCasRenterContractBasicCrCasRenterContractBasic4s",
                                                                                                                           "CrCasRenterLessorNavigation" });
            var RenterVM = _mapper.Map<RenterLessorVM>(Renter);

            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var Lrecord = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresLessor == lessorCode &&
                x.CrCasSysAdministrativeProceduresCode == "306"
                && x.CrCasSysAdministrativeProceduresSector == "1"
                && x.CrCasSysAdministrativeProceduresYear == y).Max(x => x.CrCasSysAdministrativeProceduresNo.Substring(x.CrCasSysAdministrativeProceduresNo.Length - 6, 6));
            string Serial;
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                Serial = val.ToString("000000");
            }
            else
            {
                Serial = "000001";
            }
            RenterVM.AdminstritiveNo = y + "-" + "1" + "306" + "-" + lessorCode + "100" + "-" + Serial;
            RenterVM.CrMasSysEvaluation = _unitOfWork.CrMasSysEvaluation.Find(x => x.CrMasSysEvaluationsCode == RenterVM.CrCasRenterLessorDealingMechanism);
            RenterVM.Banks = _unitOfWork.CrMasSupAccountBanks.FindAll(x => x.CrMasSupAccountBankStatus == Status.Active && x.CrMasSupAccountBankCode != "00").ToList();
            RenterVM.AccountBanks = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankStatus == Status.Active && x.CrCasAccountBankNo != "00" && x.CrCasAccountBankLessor == lessorCode).ToList();
            RenterVM.RenterInformationIban = Renter.CrCasRenterLessorNavigation.CrMasRenterInformationIban;
            RenterVM.BankSelected = Renter.CrCasRenterLessorNavigation.CrMasRenterInformationBank;
            return View(RenterVM);
        }


    }
}
