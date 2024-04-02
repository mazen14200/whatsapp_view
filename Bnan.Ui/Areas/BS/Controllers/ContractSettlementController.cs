using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class ContractSettlementController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<ContractSettlementController> _localizer;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ContractSettlementController(IStringLocalizer<ContractSettlementController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IContract contractServices, IWebHostEnvironment hostingEnvironment) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            //To Set Title 
            var titles = await setTitle("501", "5501004", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => (x.CrCasRenterContractBasicStatus == Status.Active ||x.CrCasRenterContractBasicStatus==Status.Expire) &&
                                                                           
                                                                           x.CrCasRenterContractBasicLessor == lessorCode, new[] { "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasic1" });
            var contractMap = _mapper.Map<List<ContractSettlementVM>>(contracts);
            foreach (var contract in contractMap)
            {
                var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode &&
                x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
                if (authContract != null) contract.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;
            }
            bsLayoutVM.ContractSettlements = contractMap.Where(x => x.AuthEndDate > DateTime.Now).ToList();
            return View(bsLayoutVM);
        }
        [HttpGet]
        public async Task<PartialViewResult> GetContractBySearch(string search)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => (x.CrCasRenterContractBasicStatus == Status.Active || x.CrCasRenterContractBasicStatus == Status.Expire)&&
                                                                               x.CrCasRenterContractBasicLessor == lessorCode, new[] { "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation", "CrCasRenterContractBasic1" });
            var contractMap = _mapper.Map<List<ContractSettlementVM>>(contracts);
            foreach (var contract in contractMap)
            {
                var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode &&
                x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
                if (authContract != null) contract.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;
            }

            if (!string.IsNullOrEmpty(search))
            {

                bsLayoutVM.ContractSettlements = contractMap.Where(x => x.AuthEndDate > DateTime.Now &&
                                                                                               (x.CrCasRenterContractBasicNo.Contains(search) ||
                                                                                                x.CrCasRenterContractBasic5.CrCasRenterLessorNavigation.CrMasRenterInformationArName.Contains(search) ||
                                                                                                x.CrCasRenterContractBasicCarSerailNoNavigation.CrCasCarInformationConcatenateArName.Contains(search) ||
                                                                                                x.CrCasRenterContractBasic5.CrCasRenterLessorNavigation.CrMasRenterInformationEnName.ToLower().Contains(search) ||
                                                                                                x.CrCasRenterContractBasicCarSerailNoNavigation.CrCasCarInformationConcatenateEnName.ToLower().Contains(search))).ToList();
                return PartialView("_ContractSettlement", bsLayoutVM);
            }
            bsLayoutVM.ContractSettlements = contractMap.Where(x => x.AuthEndDate > DateTime.Now).ToList();
            return PartialView("_ContractSettlement", bsLayoutVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            //To Set Title 
            var titles = await setTitle("501", "5501004", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == id,
                                                                                     new[] { "CrCasRenterContractBasic5.CrCasRenterLessorNavigation",
                                                                                             "CrCasRenterContractBasicCarSerailNoNavigation.CrCasCarAdvantages",
                                                                                             "CrCasRenterContractBasic1"}).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();
            var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode &&
                x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
            var contractMap = _mapper.Map<ContractSettlementVM>(contract);
            var PaymentMethod = _unitOfWork.CrMasSupAccountPaymentMethod.FindAll(x => x.CrMasSupAccountPaymentMethodStatus == Status.Active && x.CrMasSupAccountPaymentMethodClassification != "4").ToList();
            var SalesPoint = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == lessorCode &&
                                                                             x.CrCasAccountSalesPointBrn == bsLayoutVM.SelectedBranch &&
                                                                             x.CrCasAccountSalesPointBankStatus == Status.Active &&
                                                                             x.CrCasAccountSalesPointStatus == Status.Active &&
                                                                             x.CrCasAccountSalesPointBranchStatus == Status.Active).ToList();

            contractMap.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;
            contractMap.AuthType = authContract.CrCasRenterContractAuthorizationType;
            contractMap.CasRenterPreviousBalance = contract.CrCasRenterContractBasic5?.CrCasRenterLessorAvailableBalance;
            contractMap.AdvantagesValue = (_unitOfWork.CrCasRenterContractAdvantage.FindAll(x => x.CrCasRenterContractAdvantagesNo == contract.CrCasRenterContractBasicNo).Sum(x=>x.CrCasContractAdvantagesValue)*contractMap.CrCasRenterContractBasicExpectedRentalDays)?.ToString("N2", CultureInfo.InvariantCulture);
            bsLayoutVM.ContractSettlement = contractMap;
            bsLayoutVM.SalesPoint = SalesPoint;
            bsLayoutVM.PaymentMethods = PaymentMethod;
            return View(bsLayoutVM);
        }

    }
}
