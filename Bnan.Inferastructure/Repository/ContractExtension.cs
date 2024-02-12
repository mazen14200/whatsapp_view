using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class ContractExtension : IContractExtension
    {
        private IUnitOfWork _unitOfWork;

        public ContractExtension(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CrCasRenterContractBasic> AddRenterExtensionContract(string ContractNo, string DaysNo, string UserInsert, string AmountPayed, string Reasons)
        {
            var Contract = await _unitOfWork.CrCasRenterContractBasic.FindAsync(x => x.CrCasRenterContractBasicNo == ContractNo);
            var Renter = await _unitOfWork.CrCasRenterLessor.FindAsync(x => x.CrCasRenterLessorId == Contract.CrCasRenterContractBasicRenterId);
            CrCasRenterContractBasic renterContractBasic = new CrCasRenterContractBasic();

            // Constant Valuess 
            renterContractBasic.CrCasRenterContractBasicNo = Contract.CrCasRenterContractBasicNo;
            renterContractBasic.CrCasRenterContractBasicYear = Contract.CrCasRenterContractBasicYear;
            renterContractBasic.CrCasRenterContractBasicSector = Contract.CrCasRenterContractBasicSector;
            renterContractBasic.CrCasRenterContractBasicProcedures = Contract.CrCasRenterContractBasicProcedures;
            renterContractBasic.CrCasRenterContractBasicLessor = Contract.CrCasRenterContractBasicLessor;
            renterContractBasic.CrCasRenterContractBasicBranch = Contract.CrCasRenterContractBasicBranch;
            renterContractBasic.CrCasRenterContractBasicIssuedDate = Contract.CrCasRenterContractBasicIssuedDate;
            renterContractBasic.CrCasRenterContractBasicExpectedStartDate = Contract.CrCasRenterContractBasicExpectedStartDate;
            renterContractBasic.CrCasRenterContractBasicAllowCanceled = Contract.CrCasRenterContractBasicAllowCanceled;
            renterContractBasic.CrCasRenterContractBasicRenterId = Contract.CrCasRenterContractBasicRenterId;
            renterContractBasic.CrCasRenterContractBasicAdditionalDriverId = Contract.CrCasRenterContractBasicAdditionalDriverId;
            renterContractBasic.CrCasRenterContractBasicPrivateDriverId = Contract.CrCasRenterContractBasicPrivateDriverId;
            renterContractBasic.CrCasRenterContractBasicDriverId = Contract.CrCasRenterContractBasicDriverId;
            renterContractBasic.CrCasRenterContractBasicFreeHours = Contract.CrCasRenterContractBasicFreeHours;
            renterContractBasic.CrCasRenterContractBasicHourMax = Contract.CrCasRenterContractBasicHourMax;
            renterContractBasic.CrCasRenterContractBasicHourValue = Contract.CrCasRenterContractBasicHourValue;
            renterContractBasic.CrCasRenterContractBasicDailyFreeKm = Contract.CrCasRenterContractBasicDailyFreeKm;
            renterContractBasic.CrCasRenterContractBasicKmValue = Contract.CrCasRenterContractBasicKmValue;
            renterContractBasic.CrCasRenterContractBasicDailyRent = Contract.CrCasRenterContractBasicDailyRent;
            renterContractBasic.CrCasRenterContractBasicWeeklyRent = Contract.CrCasRenterContractBasicWeeklyRent;
            renterContractBasic.CrCasRenterContractBasicMonthlyRent = Contract.CrCasRenterContractBasicMonthlyRent;
            renterContractBasic.CrCasRenterContractBasicYearlyRent = Contract.CrCasRenterContractBasicYearlyRent;
            renterContractBasic.CrCasRenterContractBasicAdditionalDriverValue = Contract.CrCasRenterContractBasicAdditionalDriverValue;
            renterContractBasic.CrCasRenterContractBasicPrivateDriverValue = Contract.CrCasRenterContractBasicPrivateDriverValue;
            renterContractBasic.CrCasRenterContractBasicAuthorizationValue = Contract.CrCasRenterContractBasicAuthorizationValue;
            renterContractBasic.CrCasRenterContractBasicTaxRate = Contract.CrCasRenterContractBasicTaxRate;
            renterContractBasic.CrCasRenterContractBasicUserDiscountRate = Contract.CrCasRenterContractBasicUserDiscountRate;
            renterContractBasic.CrCasRenterContractBasicCurrentReadingMeter = Contract.CrCasRenterContractBasicCurrentReadingMeter;
            renterContractBasic.CrCasRenterContractBasicAdditionalValue = Contract.CrCasRenterContractBasicAdditionalValue;
            renterContractBasic.CrCasRenterContractPriceReference = Contract.CrCasRenterContractPriceReference;
            renterContractBasic.CrCasRenterContractBasicCarSerailNo = Contract.CrCasRenterContractBasicCarSerailNo;
            ////////////////
            ///       
            renterContractBasic.CrCasRenterContractBasicCopy += 1;
            renterContractBasic.CrCasRenterContractBasicExpectedRentalDays = int.Parse(DaysNo);
            renterContractBasic.CrCasRenterContractBasicExpectedEndDate = renterContractBasic.CrCasRenterContractBasicExpectedStartDate?.AddDays(int.Parse(DaysNo));
            renterContractBasic.CrCasRenterContractBasicExpectedRentValue = int.Parse(DaysNo) * Contract.CrCasRenterContractBasicDailyRent;
            renterContractBasic.CrCasRenterContractBasicExpectedOptionsValue = int.Parse(DaysNo) * (Contract.CrCasRenterContractBasicExpectedOptionsValue / Contract.CrCasRenterContractBasicExpectedRentalDays);
            renterContractBasic.CrCasRenterContractBasicExpectedPrivateDriverValue = int.Parse(DaysNo) * Contract.CrCasRenterContractBasicPrivateDriverValue;

            renterContractBasic.CrCasRenterContractBasicExpectedValueBeforDiscount = renterContractBasic.CrCasRenterContractBasicExpectedRentValue +
                                                                                     renterContractBasic.CrCasRenterContractBasicExpectedOptionsValue +
                                                                                     renterContractBasic.CrCasRenterContractBasicExpectedPrivateDriverValue +
                                                                                     renterContractBasic.CrCasRenterContractBasicAdditionalValue +
                                                                                     renterContractBasic.CrCasRenterContractBasicAuthorizationValue;

            renterContractBasic.CrCasRenterContractBasicExpectedDiscountValue = renterContractBasic.CrCasRenterContractBasicExpectedValueBeforDiscount * Contract.CrCasRenterContractBasicUserDiscountRate;

            renterContractBasic.CrCasRenterContractBasicExpectedValueAfterDiscount = renterContractBasic.CrCasRenterContractBasicExpectedValueBeforDiscount +
                                                                                     renterContractBasic.CrCasRenterContractBasicExpectedDiscountValue;
            renterContractBasic.CrCasRenterContractBasicExpectedTaxValue = renterContractBasic.CrCasRenterContractBasicExpectedValueAfterDiscount * (Contract.CrCasRenterContractBasicTaxRate/100);

            renterContractBasic.CrCasRenterContractBasicExpectedTotal = renterContractBasic.CrCasRenterContractBasicExpectedValueAfterDiscount +
                                                                        renterContractBasic.CrCasRenterContractBasicExpectedTaxValue;

            renterContractBasic.CrCasRenterContractBasicPreviousBalance = Renter.CrCasRenterLessorBalance;
            renterContractBasic.CrCasRenterContractBasicAmountRequired = renterContractBasic.CrCasRenterContractBasicExpectedTotal + renterContractBasic.CrCasRenterContractBasicPreviousBalance;
            renterContractBasic.CrCasRenterContractBasicAmountPaidAdvance = decimal.Parse(AmountPayed, CultureInfo.InvariantCulture);
            renterContractBasic.CrCasRenterContractBasicUserInsert = UserInsert;
            renterContractBasic.CrCasRenterContractBasicReasons = Reasons;
            renterContractBasic.CrCasRenterContractBasicStatus = Status.Active;
            if (await _unitOfWork.CrCasRenterContractBasic.AddAsync(renterContractBasic) != null) return renterContractBasic;
            return null;
        }
        public async Task<bool> UpdateAlertContract(string ContractNo)
        {
            var ContractAlert = await _unitOfWork.CrCasRenterContractAlert.FindAsync(x => x.CrCasRenterContractAlertNo == ContractNo);
            var Contract = await _unitOfWork.CrCasRenterContractBasic.FindAsync(x => x.CrCasRenterContractBasicNo == ContractNo && x.CrCasRenterContractBasicStatus==Status.Active);
            if (ContractAlert != null)
            {
                ContractAlert.CrCasRenterContractAlertDays = Contract.CrCasRenterContractBasicExpectedRentalDays;
                ContractAlert.CrCasRenterContractAlertDayDate = Contract.CrCasRenterContractBasicExpectedEndDate?.AddDays(-1);
                ContractAlert.CrCasRenterContractAlertHourDate = Contract.CrCasRenterContractBasicExpectedEndDate?.AddHours(-4);
                ContractAlert.CrCasRenterContractAlertEndDate = Contract.CrCasRenterContractBasicExpectedEndDate;
                ContractAlert.CrCasRenterContractAlertStatus = "0";
                ContractAlert.CrCasRenterContractAlertContractStatus = Status.Active;
                if (Contract.CrCasRenterContractBasicExpectedRentalDays == 1) ContractAlert.CrCasRenterContractAlertContractActiviteStatus = "1";
                else if (Contract.CrCasRenterContractBasicExpectedRentalDays >= 2) ContractAlert.CrCasRenterContractAlertContractActiviteStatus = "2";
                if (_unitOfWork.CrCasRenterContractAlert.Update(ContractAlert) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateRenterLessor(string RenterId, string LessorCode, decimal TotalPayed)
        {
            var RenterLessor = await _unitOfWork.CrCasRenterLessor.FindAsync(x => x.CrCasRenterLessorId == RenterId && x.CrCasRenterLessorCode == LessorCode);
            if (RenterLessor != null)
            {
                if (RenterLessor.CrCasRenterLessorContractCount != null) RenterLessor.CrCasRenterLessorContractCount += 1;
                else RenterLessor.CrCasRenterLessorContractCount = 1;
                RenterLessor.CrCasRenterLessorBalance -= (TotalPayed);
                RenterLessor.CrCasRenterLessorContractExtension += 1;
                if (_unitOfWork.CrCasRenterLessor.Update(RenterLessor) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateStatusOldContract(string ContractNo)
        {
            var Contract = await _unitOfWork.CrCasRenterContractBasic.FindAsync(x => x.CrCasRenterContractBasicNo == ContractNo&&x.CrCasRenterContractBasicStatus==Status.Active);
            if (Contract != null)
            {
                Contract.CrCasRenterContractBasicStatus = Status.Extension;
                if (_unitOfWork.CrCasRenterContractBasic.Update(Contract) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateBranchBalance(string BranchCode, string LessorCode, decimal AmountPaid)
        {
            var Branch = await _unitOfWork.CrCasBranchInformation.FindAsync(x => x.CrCasBranchInformationCode == BranchCode && x.CrCasBranchInformationLessor == LessorCode);
            if (Branch != null)
            {
                if (Branch.CrCasBranchInformationAvailableBalance != null) Branch.CrCasBranchInformationAvailableBalance += AmountPaid;
                else Branch.CrCasBranchInformationAvailableBalance = AmountPaid;
                if (Branch.CrCasBranchInformationTotalBalance != null) Branch.CrCasBranchInformationTotalBalance += AmountPaid;
                else Branch.CrCasBranchInformationTotalBalance = AmountPaid;

                if (_unitOfWork.CrCasBranchInformation.Update(Branch) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateSalesPointBalance(string BranchCode, string LessorCode, string SalesPointCode, decimal AmountPaid)
        {
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointCode == SalesPointCode &&
                                                                                     x.CrCasAccountSalesPointLessor == LessorCode &&
                                                                                     x.CrCasAccountSalesPointBrn == BranchCode);
            if (SalesPoint != null)
            {
                if (SalesPoint.CrCasAccountSalesPointTotalAvailable != null) SalesPoint.CrCasAccountSalesPointTotalAvailable += AmountPaid;
                else SalesPoint.CrCasAccountSalesPointTotalAvailable = AmountPaid;
                if (SalesPoint.CrCasAccountSalesPointTotalBalance != null) SalesPoint.CrCasAccountSalesPointTotalBalance += AmountPaid;
                else SalesPoint.CrCasAccountSalesPointTotalBalance = AmountPaid;
                if (_unitOfWork.CrCasAccountSalesPoint.Update(SalesPoint) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateBranchValidity(string BranchCode, string LessorCode, string UserId, string PaymentMethod, decimal AmountPaid)
        {
            var UserValidity = await _unitOfWork.CrMasUserBranchValidity.FindAsync(x => x.CrMasUserBranchValidityId == UserId && x.CrMasUserBranchValidityBranch == BranchCode && x.CrMasUserBranchValidityLessor == LessorCode);
            if (PaymentMethod == "10")
            {
                if (UserValidity.CrMasUserBranchValidityBranchCashAvailable != null) UserValidity.CrMasUserBranchValidityBranchCashAvailable += AmountPaid;
                else UserValidity.CrMasUserBranchValidityBranchCashAvailable = AmountPaid;
                if (UserValidity.CrMasUserBranchValidityBranchCashBalance != null) UserValidity.CrMasUserBranchValidityBranchCashBalance += AmountPaid;
                else UserValidity.CrMasUserBranchValidityBranchCashBalance = AmountPaid;
            }
            else if (PaymentMethod != "40" && PaymentMethod != "10")
            {
                if (UserValidity.CrMasUserBranchValidityBranchSalesPointBalance != null) UserValidity.CrMasUserBranchValidityBranchSalesPointAvailable += AmountPaid;
                else UserValidity.CrMasUserBranchValidityBranchSalesPointAvailable = AmountPaid;
                if (UserValidity.CrMasUserBranchValidityBranchSalesPointBalance != null) UserValidity.CrMasUserBranchValidityBranchSalesPointBalance += AmountPaid;
                else UserValidity.CrMasUserBranchValidityBranchSalesPointBalance = AmountPaid;
            }
            if (_unitOfWork.CrMasUserBranchValidity.Update(UserValidity) != null) return true;
            return false;
        }
        public async Task<bool> UpdateUserBalance(string BranchCode, string LessorCode, string UserId, string PaymentMethod, decimal AmountPaid)
        {
            var UserInformation = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == UserId && x.CrMasUserInformationLessor == LessorCode);
            if (UserInformation != null)
            {
                if (UserInformation.CrMasUserInformationAvailableBalance != null) UserInformation.CrMasUserInformationAvailableBalance += AmountPaid;
                else UserInformation.CrMasUserInformationAvailableBalance = AmountPaid;
                if (UserInformation.CrMasUserInformationTotalBalance != null) UserInformation.CrMasUserInformationTotalBalance += AmountPaid;
                else UserInformation.CrMasUserInformationTotalBalance = AmountPaid;
                if (_unitOfWork.CrMasUserInformation.Update(UserInformation) != null) return true;
            }
            return false;
        }
        public async Task<bool> AddAccountReceipt(string ContractNo, string LessorCode, string BranchCode, string PaymentMethod, string Account, string SerialNo, string SalesPointNo, decimal TotalPayed, string RenterId, string UserId, string PassingType, string Reasons)
        {
            CrCasAccountReceipt crCasAccountReceipt = new CrCasAccountReceipt();
            var User = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == UserId && x.CrMasUserInformationLessor == LessorCode);
            var Renter = await _unitOfWork.CrCasRenterLessor.FindAsync(x => x.CrCasRenterLessorId == RenterId && x.CrCasRenterLessorCode == LessorCode);
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointCode == SalesPointNo && x.CrCasAccountSalesPointLessor == LessorCode && x.CrCasAccountSalesPointBrn == BranchCode,
                                                                                new[] { "CrCasAccountSalesPointBankNavigation", "CrCasAccountSalesPointAccountBankNavigation" });
            var AccountBank = await _unitOfWork.CrCasAccountBank.FindAsync(x => x.CrCasAccountBankCode == Account && x.CrCasAccountBankLessor == LessorCode, new[] { "CrCasAccountBankNoNavigation" });
            //Get ContractCode
            DateTime now = DateTime.Now;
            var y = now.ToString("yy");
            var sector = Renter.CrCasRenterLessorSector;
            var autoinc = GetContractAccountReceipt(LessorCode, BranchCode).CrCasAccountReceiptNo;
            var AccountReceiptNo = y + "-" + sector + "301" + "-" + LessorCode + BranchCode + "-" + autoinc;

            crCasAccountReceipt.CrCasAccountReceiptNo = AccountReceiptNo;
            crCasAccountReceipt.CrCasAccountReceiptYear = y;
            crCasAccountReceipt.CrCasAccountReceiptType = "301"; //Create Contract
            crCasAccountReceipt.CrCasAccountReceiptLessorCode = LessorCode;
            crCasAccountReceipt.CrCasAccountReceiptBranchCode = BranchCode;
            crCasAccountReceipt.CrCasAccountReceiptDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            crCasAccountReceipt.CrCasAccountReceiptPaymentMethod = PaymentMethod;
            crCasAccountReceipt.CrCasAccountReceiptReferenceType = "11";
            crCasAccountReceipt.CrCasAccountReceiptReferenceNo = ContractNo;
            crCasAccountReceipt.CrCasAccountReceiptCar = SerialNo;
            crCasAccountReceipt.CrCasAccountReceiptUser = UserId;

            if (PassingType != "4")
            {
                crCasAccountReceipt.CrCasAccountReceiptBank = SalesPoint?.CrCasAccountSalesPointBankNavigation?.CrMasSupAccountBankCode;
                crCasAccountReceipt.CrCasAccountReceiptAccount = SalesPoint?.CrCasAccountSalesPointAccountBankNavigation?.CrCasAccountBankCode;
                crCasAccountReceipt.CrCasAccountReceiptSalesPoint = SalesPointNo;

                if (SalesPoint.CrCasAccountSalesPointTotalBalance != null) crCasAccountReceipt.CrCasAccountReceiptSalesPointPreviousBalance = SalesPoint.CrCasAccountSalesPointTotalBalance;
                else crCasAccountReceipt.CrCasAccountReceiptSalesPointPreviousBalance = 0;

                if (User.CrMasUserInformationTotalBalance != null) crCasAccountReceipt.CrCasAccountReceiptUserPreviousBalance = User.CrMasUserInformationTotalBalance;
                else crCasAccountReceipt.CrCasAccountReceiptUserPreviousBalance = 0;
            }
            else
            {
                crCasAccountReceipt.CrCasAccountReceiptBank = AccountBank?.CrCasAccountBankNoNavigation?.CrMasSupAccountBankCode;
                crCasAccountReceipt.CrCasAccountReceiptAccount = AccountBank?.CrCasAccountBankCode;
                if (User.CrMasUserInformationTotalBalance != null) crCasAccountReceipt.CrCasAccountReceiptUserPreviousBalance = User.CrMasUserInformationTotalBalance;
                else crCasAccountReceipt.CrCasAccountReceiptUserPreviousBalance = 0;
            }


            crCasAccountReceipt.CrCasAccountReceiptRenterId = RenterId;
            if (Renter.CrCasRenterLessorBalance != null) crCasAccountReceipt.CrCasAccountReceiptRenterPreviousBalance = Renter.CrCasRenterLessorBalance;
            else crCasAccountReceipt.CrCasAccountReceiptRenterPreviousBalance = 0;
            crCasAccountReceipt.CrCasAccountReceiptPayment = TotalPayed;
            crCasAccountReceipt.CrCasAccountReceiptReceipt = 0;
            crCasAccountReceipt.CrCasAccountReceiptIsPassing = PassingType;
            crCasAccountReceipt.CrCasAccountReceiptReasons = Reasons;

            if (await _unitOfWork.CrCasAccountReceipt.AddAsync(crCasAccountReceipt) != null) return true;
            return false;
        }
        private CrCasAccountReceipt GetContractAccountReceipt(string LessorCode, string BranchCode)
        {
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var Lrecord = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptLessorCode == LessorCode &&
                                                                       x.CrCasAccountReceiptYear == y && x.CrCasAccountReceiptBranchCode == BranchCode)
                                                             .Max(x => x.CrCasAccountReceiptNo.Substring(x.CrCasAccountReceiptNo.Length - 6, 6));

            CrCasAccountReceipt c = new CrCasAccountReceipt();
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                c.CrCasAccountReceiptNo = val.ToString("000000");
            }
            else
            {
                c.CrCasAccountReceiptNo = "000001";
            }

            return c;
        }
    }
}
