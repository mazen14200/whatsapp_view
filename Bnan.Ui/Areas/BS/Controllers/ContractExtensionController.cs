using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class ContractExtensionController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<ContractExtensionController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IContractExtension _contractExtension;

        public ContractExtensionController(IStringLocalizer<ContractExtensionController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IAdminstritiveProcedures adminstritiveProcedures, IContractExtension contractExtension) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _adminstritiveProcedures = adminstritiveProcedures;
            _contractExtension = contractExtension;
        }
        public async Task<IActionResult> Index()
        {
            //To Set Title 
            var titles = await setTitle("501", "5501002", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicStatus == Status.Active &&
                                                                           x.CrCasRenterContractBasicBranch == bsLayoutVM.SelectedBranch &&
                                                                           x.CrCasRenterContractBasicLessor == lessorCode, new[] { "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasic5.CrCasRenterLessorNavigation" });
            var contractMap = _mapper.Map<List<ContractForExtensionVM>>(contracts);
            foreach (var contract in contractMap)
            {
                var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode &&
                x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
                if (authContract != null) contract.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;
            }
            bsLayoutVM.ExtensionContracts = contractMap.Where(x => x.AuthEndDate > DateTime.Now).ToList();
            return View(bsLayoutVM);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetContractBySearch(string search)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicStatus == Status.Active &&
                                                                           x.CrCasRenterContractBasicBranch == bsLayoutVM.SelectedBranch &&
                                                                           x.CrCasRenterContractBasicLessor == lessorCode, new[] { "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasic4.CrCasRenterLessorNavigation" });
            var contractMap = _mapper.Map<List<ContractForExtensionVM>>(contracts);
            foreach (var contract in contractMap)
            {
                var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode &&
                x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
                if (authContract != null) contract.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;
            }

            if (!string.IsNullOrEmpty(search))
            {

                bsLayoutVM.ExtensionContracts = contractMap.Where(x => x.AuthEndDate > DateTime.Now &&
                                                                                               (x.CrCasRenterContractBasicNo.Contains(search) ||
                                                                                                x.CrCasRenterContractBasic5.CrCasRenterLessorNavigation.CrMasRenterInformationArName.Contains(search) ||
                                                                                                x.CrCasRenterContractBasicCarSerailNoNavigation.CrCasCarInformationConcatenateArName.Contains(search) ||
                                                                                                x.CrCasRenterContractBasic5.CrCasRenterLessorNavigation.CrMasRenterInformationEnName.ToLower().Contains(search) ||
                                                                                                x.CrCasRenterContractBasicCarSerailNoNavigation.CrCasCarInformationConcatenateEnName.ToLower().Contains(search))).ToList();
                return PartialView("_ContractExtension", bsLayoutVM);
            }
            bsLayoutVM.ExtensionContracts = contractMap.Where(x => x.AuthEndDate > DateTime.Now).ToList();
            return PartialView("_ContractExtension", bsLayoutVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            //To Set Title 
            var titles = await setTitle("501", "5501002", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == id,
                                                                                     new[] { "CrCasRenterContractBasic5.CrCasRenterLessorNavigation",
                                                                                             "CrCasRenterContractBasicCarSerailNoNavigation"}).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();
            var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode &&
                x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
            var contractMap = _mapper.Map<ContractForExtensionVM>(contract);
            var PaymentMethod = _unitOfWork.CrMasSupAccountPaymentMethod.FindAll(x => x.CrMasSupAccountPaymentMethodStatus == Status.Active && x.CrMasSupAccountPaymentMethodClassification != "4").ToList();
            var SalesPoint = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == lessorCode &&
                                                                             x.CrCasAccountSalesPointBrn == bsLayoutVM.SelectedBranch &&
                                                                             x.CrCasAccountSalesPointBankStatus == Status.Active &&
                                                                             x.CrCasAccountSalesPointStatus == Status.Active &&
                                                                             x.CrCasAccountSalesPointBranchStatus == Status.Active).ToList();

            contractMap.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;
            contractMap.AuthType = authContract.CrCasRenterContractAuthorizationType;
            contractMap.CasRenterPreviousBalance = contract.CrCasRenterContractBasic5?.CrCasRenterLessorAvailableBalance;
            bsLayoutVM.ExtensionContract = contractMap;
            bsLayoutVM.SalesPoint = SalesPoint;
            bsLayoutVM.PaymentMethods = PaymentMethod;
            return View(bsLayoutVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BSLayoutVM bSLayoutVM, string ExtensionValue, string reasons)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var ContractInfo = bSLayoutVM.ExtensionContract;
            var Contract = await _unitOfWork.CrCasRenterContractBasic.FindAsync(x => x.CrCasRenterContractBasicNo == ContractInfo.CrCasRenterContractBasicNo && x.CrCasRenterContractBasicStatus == Status.Active);
            if (Contract != null)
            {
                var Renter = _unitOfWork.CrMasRenterInformation.Find(x => x.CrMasRenterInformationId == Contract.CrCasRenterContractBasicRenterId);
                var RenterLessor = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == Contract.CrCasRenterContractBasicRenterId);
                var Car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == Contract.CrCasRenterContractBasicCarSerailNo);
                var CarPrice = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicNo == Contract.CrCasRenterContractPriceReference);
                var Branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == bSLayoutVM.SelectedBranch && x.CrCasBranchInformationLessor == lessorCode);
                if (string.IsNullOrEmpty(ContractInfo.AmountPayed)) ContractInfo.AmountPayed = "0";
                var NewContract = await _contractExtension.AddRenterExtensionContract(Contract.CrCasRenterContractBasicNo, ContractInfo.DaysNo, userLogin.CrMasUserInformationCode, ContractInfo.AmountPayed, reasons);
                if (NewContract != null)
                {
                    var CheckUpdateContractStatus = true;
                    CheckUpdateContractStatus = await _contractExtension.UpdateStatusOldContract(Contract.CrCasRenterContractBasicNo);

                    //Account Receipt
                    var CheckAccountReceipt = true;
                    var CheckRenterLessor = true;
                    var CheckBranch = true;
                    var CheckSalesPoint = true;
                    var CheckBranchValidity = true;
                    var CheckUserInformation = true;

                    var passing = "";
                    if (decimal.Parse(ContractInfo.AmountPayed, CultureInfo.InvariantCulture) !=null&& decimal.Parse(ContractInfo.AmountPayed, CultureInfo.InvariantCulture) > 0)
                    {

                        if (ContractInfo.PaymentMethod == "30")
                        {
                            passing = "4";
                            ContractInfo.AccountNo = ContractInfo.SalesPoint;
                        }
                        else
                        {
                            passing = "1";
                        }

                        CheckAccountReceipt = await _contractExtension.AddAccountReceipt(NewContract.CrCasRenterContractBasicNo, lessorCode, NewContract.CrCasRenterContractBasicBranch,
                                                                                      ContractInfo.PaymentMethod, ContractInfo.AccountNo, NewContract.CrCasRenterContractBasicCarSerailNo,
                                                                                      ContractInfo.SalesPoint, decimal.Parse(ContractInfo.AmountPayed, CultureInfo.InvariantCulture),
                                                                                      NewContract.CrCasRenterContractBasicRenterId, userLogin.CrMasUserInformationCode, passing, reasons);
                        //Update Branch Balance , But first Check if passing equal 4 or not 
                        if (passing != "4") CheckBranch = await _contractExtension.UpdateBranchBalance(Branch.CrCasBranchInformationCode, lessorCode, decimal.Parse(ContractInfo.AmountPayed, CultureInfo.InvariantCulture));
                        //Update SalesPoint Balance , But first Check if passing equal 4 or not 
                        if (!string.IsNullOrEmpty(ContractInfo.SalesPoint) && passing != "4") CheckSalesPoint = await _contractExtension.UpdateSalesPointBalance(Branch.CrCasBranchInformationCode, lessorCode, ContractInfo.SalesPoint, decimal.Parse(ContractInfo.AmountPayed, CultureInfo.InvariantCulture));
                        // UpdateBranchValidity
                        if (passing != "4") CheckBranchValidity = await _contractExtension.UpdateBranchValidity(Branch.CrCasBranchInformationCode, lessorCode, userLogin.CrMasUserInformationCode, ContractInfo.PaymentMethod, decimal.Parse(ContractInfo.AmountPayed, CultureInfo.InvariantCulture));
                        // UpdateUserBalance
                        if (passing != "4") CheckUserInformation = await _contractExtension.UpdateUserBalance(Branch.CrCasBranchInformationCode, lessorCode, userLogin.CrMasUserInformationCode, ContractInfo.PaymentMethod, decimal.Parse(ContractInfo.AmountPayed, CultureInfo.InvariantCulture));
                    }
                    //Update RenterLessor Of Car Renter
                    CheckRenterLessor = await _contractExtension.UpdateRenterLessor(NewContract.CrCasRenterContractBasicRenterId, lessorCode, decimal.Parse(ContractInfo.AmountPayed, CultureInfo.InvariantCulture), decimal.Parse(ExtensionValue, CultureInfo.InvariantCulture));

                    // Add Renter Alert
                    var CheckRenterAlert = true;
                    CheckRenterAlert = await _contractExtension.UpdateAlertContract(NewContract.CrCasRenterContractBasicNo);

                    if (NewContract != null && CheckUpdateContractStatus && CheckRenterLessor && CheckAccountReceipt && CheckBranch && CheckSalesPoint &&
                        CheckUserInformation && CheckBranchValidity && CheckRenterAlert)
                    {
                        try
                        {
                            if (await _unitOfWork.CompleteAsync() > 0)
                            {
                                _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                                return RedirectToAction("Index");
                            }
                        }
                        catch (Exception ex)
                        {
                            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                            return RedirectToAction("Index");
                            throw;
                        }
                    }
                }
            }
            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesPoint(string PaymentMethod, string BranchCode)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            List<SalesPointsVM> SalesPointVMList = new List<SalesPointsVM>();
            List<AccountBankVM> AccountBankVMList = new List<AccountBankVM>();
            var Type = "0";
            if (PaymentMethod != null)
            {
                if (PaymentMethod == "10")
                {
                    var SalesPoints = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == lessorCode && x.CrCasAccountSalesPointBrn == BranchCode &&
                                                                           x.CrCasAccountSalesPointStatus == Status.Active && x.CrCasAccountSalesPointBankStatus == Status.Active &&
                                                                           x.CrCasAccountSalesPointBranchStatus == Status.Active && x.CrCasAccountSalesPointBank == "00").ToList();
                    Type = "1";

                    foreach (var item in SalesPoints)
                    {
                        SalesPointsVM SalesPointVM = new SalesPointsVM
                        {
                            CrCasAccountSalesPointNo = item.CrCasAccountSalesPointNo,
                            CrCasAccountSalesPointCode = item.CrCasAccountSalesPointCode,
                            CrCasAccountSalesPointArName = item.CrCasAccountSalesPointArName,
                            CrCasAccountSalesPointEnName = item.CrCasAccountSalesPointEnName,
                            CrCasAccountSalesPointBank = item.CrCasAccountSalesPointBank,
                            CrCasAccountSalesPointAccountBank = item.CrCasAccountSalesPointAccountBank
                        };
                        SalesPointVMList.Add(SalesPointVM);
                    }
                }
                else if (PaymentMethod == "20" || PaymentMethod == "22" || PaymentMethod == "21" || PaymentMethod == "23")
                {
                    var SalesPoints = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == lessorCode && x.CrCasAccountSalesPointBrn == BranchCode &&
                                                                           x.CrCasAccountSalesPointStatus == Status.Active && x.CrCasAccountSalesPointBankStatus == Status.Active &&
                                                                           x.CrCasAccountSalesPointBranchStatus == Status.Active && x.CrCasAccountSalesPointBank != "00").ToList();
                    Type = "1";
                    foreach (var item in SalesPoints)
                    {
                        SalesPointsVM SalesPointVM = new SalesPointsVM
                        {
                            CrCasAccountSalesPointNo = item.CrCasAccountSalesPointNo,
                            CrCasAccountSalesPointCode = item.CrCasAccountSalesPointCode,
                            CrCasAccountSalesPointArName = item.CrCasAccountSalesPointArName,
                            CrCasAccountSalesPointEnName = item.CrCasAccountSalesPointEnName,
                            CrCasAccountSalesPointBank = item.CrCasAccountSalesPointBank,
                            CrCasAccountSalesPointAccountBank = item.CrCasAccountSalesPointAccountBank
                        };
                        SalesPointVMList.Add(SalesPointVM);
                    }
                }
                else
                {
                    var AccountBanks = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankLessor == lessorCode && x.CrCasAccountBankStatus == Status.Active &&
                                                                         x.CrCasAccountBankNo != "00").ToList();
                    Type = "2";
                    foreach (var item in AccountBanks)
                    {
                        AccountBankVM AccountBankVM = new AccountBankVM
                        {
                            CrCasAccountBankNo = item.CrCasAccountBankNo,
                            CrCasAccountBankArName = item.CrCasAccountBankArName,
                            CrCasAccountBankEnName = item.CrCasAccountBankEnName,
                            CrCasAccountBankCode = item.CrCasAccountBankCode,
                        };
                        AccountBankVMList.Add(AccountBankVM);
                    }
                }
                return Json(new { SalesPoints = SalesPointVMList, AccountBank = AccountBankVMList, Type = Type });
            }
            return Json(null);
        }
    }

}
