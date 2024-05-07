using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class ContractSettlement : IContractSettlement
    {
        private IUnitOfWork _unitOfWork;

        public ContractSettlement(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAccountContractTaxOwed(string ContractNo, decimal ContractValue)
        {
            var OldContract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == ContractNo).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();

            CrCasAccountContractTaxOwed crCasAccountContractTaxOwed = new CrCasAccountContractTaxOwed();
            crCasAccountContractTaxOwed.CrCasAccountContractTaxOwedContractNo = OldContract.CrCasRenterContractBasicNo;
            crCasAccountContractTaxOwed.CrCasAccountContractTaxOwedLessor = OldContract.CrCasRenterContractBasicLessor;
            crCasAccountContractTaxOwed.CrCasAccountContractTaxOwedContractValue = ContractValue;
            crCasAccountContractTaxOwed.CrCasAccountContractTaxOwedPercentage = OldContract.CrCasRenterContractBasicTaxRate;
            crCasAccountContractTaxOwed.CrCasAccountContractTaxOwedValue = crCasAccountContractTaxOwed.CrCasAccountContractTaxOwedContractValue * OldContract.CrCasRenterContractBasicTaxRate / 100;
            crCasAccountContractTaxOwed.CrCasAccountContractTaxOwedDate = DateTime.Now.Date;
            crCasAccountContractTaxOwed.CrCasAccountContractTaxOwedIsPaid = false;

            if (await _unitOfWork.CrCasAccountContractTaxOwed.AddAsync(crCasAccountContractTaxOwed) != null) return true;
            return false;
        }

        public async Task<bool> AddAccountReceipt(string ContractNo, string LessorCode, string BranchCode, string PaymentMethod, string Account, string SerialNo, string SalesPointNo,
                                                  decimal TotalPayed, string RenterId, string UserId, string PassingType, string Reasons, string pdfPathAr, string pdfPathEn, string procedureCode)
        {
            CrCasAccountReceipt crCasAccountReceipt = new CrCasAccountReceipt();
            var User = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == UserId && x.CrMasUserInformationLessor == LessorCode);
            var Renter = await _unitOfWork.CrCasRenterLessor.FindAsync(x => x.CrCasRenterLessorId == RenterId && x.CrCasRenterLessorCode == LessorCode);
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointCode == SalesPointNo && x.CrCasAccountSalesPointLessor == LessorCode && x.CrCasAccountSalesPointBrn == BranchCode,
                                                                                new[] { "CrCasAccountSalesPointBankNavigation", "CrCasAccountSalesPointAccountBankNavigation" });
            var AccountBank = await _unitOfWork.CrCasAccountBank.FindAsync(x => x.CrCasAccountBankCode == Account && x.CrCasAccountBankLessor == LessorCode, new[] { "CrCasAccountBankNoNavigation" });
            var userValidity = await _unitOfWork.CrMasUserBranchValidity.FindAsync(x => x.CrMasUserBranchValidityLessor == LessorCode && x.CrMasUserBranchValidityBranch == BranchCode && x.CrMasUserBranchValidityId == User.CrMasUserInformationCode);
            var userBranchValidityBalance = userValidity.CrMasUserBranchValidityBranchCashAvailable + userValidity.CrMasUserBranchValidityBranchSalesPointAvailable + userValidity.CrMasUserBranchValidityBranchTransferAvailable;
            //Get ContractCode
            DateTime now = DateTime.Now;
            var y = now.ToString("yy");
            var sector = Renter.CrCasRenterLessorSector;
            var autoinc = GetContractAccountReceipt(LessorCode, BranchCode, procedureCode).CrCasAccountReceiptNo;
            var AccountReceiptNo = y + "-" + sector + procedureCode + "-" + LessorCode + BranchCode + "-" + autoinc;

            crCasAccountReceipt.CrCasAccountReceiptNo = AccountReceiptNo;
            crCasAccountReceipt.CrCasAccountReceiptYear = y;
            crCasAccountReceipt.CrCasAccountReceiptType = procedureCode;
            crCasAccountReceipt.CrCasAccountReceiptLessorCode = LessorCode;
            crCasAccountReceipt.CrCasAccountReceiptBranchCode = BranchCode;
            crCasAccountReceipt.CrCasAccountReceiptDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            crCasAccountReceipt.CrCasAccountReceiptPaymentMethod = PaymentMethod;
            crCasAccountReceipt.CrCasAccountReceiptReferenceType = "13";
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
            crCasAccountReceipt.CrCasAccountReceiptBranchUserPreviousBalance = userBranchValidityBalance;
            crCasAccountReceipt.CrCasAccountReceiptRenterId = RenterId;
            if (Renter.CrCasRenterLessorBalance != null) crCasAccountReceipt.CrCasAccountReceiptRenterPreviousBalance = Renter.CrCasRenterLessorBalance;
            else crCasAccountReceipt.CrCasAccountReceiptRenterPreviousBalance = 0;
            if (procedureCode == "301")
            {
                crCasAccountReceipt.CrCasAccountReceiptPayment = TotalPayed;
                crCasAccountReceipt.CrCasAccountReceiptReceipt = 0;
            }
            else
            {
                crCasAccountReceipt.CrCasAccountReceiptPayment = 0;
                crCasAccountReceipt.CrCasAccountReceiptReceipt = TotalPayed;
            }




            crCasAccountReceipt.CrCasAccountReceiptIsPassing = PassingType;
            crCasAccountReceipt.CrCasAccountReceiptReasons = Reasons;
            crCasAccountReceipt.CrCasAccountReceiptArPdfFile = pdfPathAr;
            crCasAccountReceipt.CrCasAccountReceiptEnPdfFile = pdfPathEn;

            if (await _unitOfWork.CrCasAccountReceipt.AddAsync(crCasAccountReceipt) != null) return true;
            return false;
        }

        public async Task<bool> UpdateAlert(string ContractNo)
        {
            var Alert = await _unitOfWork.CrCasRenterContractAlert.FindAsync(x => x.CrCasRenterContractAlertNo == ContractNo);
            if (Alert != null)
            {
                Alert.CrCasRenterContractAlertContractStatus = Status.Closed;
                if (_unitOfWork.CrCasRenterContractAlert.Update(Alert) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateAuthrization(string ContractNo)
        {
            var Auth = await _unitOfWork.CrCasRenterContractAuthorization.FindAsync(x => x.CrCasRenterContractAuthorizationContractNo == ContractNo);
            if (Auth != null)
            {
                Auth.CrCasRenterContractAuthorizationEndDate = DateTime.Now;
                Auth.CrCasRenterContractAuthorizationAction = false;
                if (_unitOfWork.CrCasRenterContractAuthorization.Update(Auth) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateBranchBalance(string BranchCode, string LessorCode, decimal AmountPaid, decimal AmountRequired)
        {
            var Branch = await _unitOfWork.CrCasBranchInformation.FindAsync(x => x.CrCasBranchInformationCode == BranchCode && x.CrCasBranchInformationLessor == LessorCode);
            if (Branch != null)
            {
                if (AmountRequired < 0) AmountPaid = -AmountPaid;

                if (Branch.CrCasBranchInformationAvailableBalance != null) Branch.CrCasBranchInformationAvailableBalance += AmountPaid;
                else Branch.CrCasBranchInformationAvailableBalance = AmountPaid;
                if (Branch.CrCasBranchInformationTotalBalance != null) Branch.CrCasBranchInformationTotalBalance += AmountPaid;
                else Branch.CrCasBranchInformationTotalBalance = AmountPaid;

                if (_unitOfWork.CrCasBranchInformation.Update(Branch) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateBranchValidity(string BranchCode, string LessorCode, string UserId, string PaymentMethod, decimal AmountPaid, decimal AmountRequired)
        {
            var UserValidity = await _unitOfWork.CrMasUserBranchValidity.FindAsync(x => x.CrMasUserBranchValidityId == UserId && x.CrMasUserBranchValidityBranch == BranchCode && x.CrMasUserBranchValidityLessor == LessorCode);
            if (UserValidity != null)
            {

                if (AmountRequired < 0) AmountPaid = -AmountPaid;
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
            };
            return false;
        }

        public async Task<bool> UpdateMasRenter(string RenterId)
        {
            var Renter = await _unitOfWork.CrMasRenterInformation.FindAsync(x => x.CrMasRenterInformationId == RenterId);
            if (Renter != null)
            {
                Renter.CrMasRenterInformationStatus = Status.Active;
                if (_unitOfWork.CrMasRenterInformation.Update(Renter) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateDriverStatus(string DriverId, string LessorCode)
        {
            var Driver = await _unitOfWork.CrCasRenterLessor.FindAsync(x => x.CrCasRenterLessorId == DriverId && x.CrCasRenterLessorCode == LessorCode);

            if (Driver != null)
            {
                Driver.CrCasRenterLessorStatus = Status.Active;
                if (_unitOfWork.CrCasRenterLessor.Update(Driver) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdatePrivateDriverStatus(string PrivateDriverId, string LessorCode)
        {
            var PrivateDriver = await _unitOfWork.CrCasRenterPrivateDriverInformation.FindAsync(x => x.CrCasRenterPrivateDriverInformationId == PrivateDriverId && x.CrCasRenterPrivateDriverInformationLessor == LessorCode);

            if (PrivateDriver != null)
            {
                PrivateDriver.CrCasRenterPrivateDriverInformationStatus = Status.Active;
                if (_unitOfWork.CrCasRenterPrivateDriverInformation.Update(PrivateDriver) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateRenterLessor(string ContractNo, decimal AmountRequired, decimal AmountPaid, decimal ContractValue, decimal TotalContractValue, int DaysNo)
        {
            var OldContract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == ContractNo).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();
            var RenterLessor = await _unitOfWork.CrCasRenterLessor.FindAsync(x => x.CrCasRenterLessorId == OldContract.CrCasRenterContractBasicRenterId && x.CrCasRenterLessorCode == OldContract.CrCasRenterContractBasicLessor);

            var ResevedAndAvaliableBalance = RenterLessor.CrCasRenterLessorAvailableBalance + RenterLessor.CrCasRenterLessorReservedBalance;

            // In This Case , ReservedBalance equal Expected Total Contract , this have one contract
            if (RenterLessor.CrCasRenterLessorReservedBalance == OldContract?.CrCasRenterContractBasicExpectedTotal)
            {

                if (ResevedAndAvaliableBalance == TotalContractValue)
                {
                    RenterLessor.CrCasRenterLessorBalance = 0;
                }
                else if (TotalContractValue > ResevedAndAvaliableBalance)
                {
                    RenterLessor.CrCasRenterLessorBalance -= TotalContractValue - AmountPaid; 
                }
                else if (TotalContractValue < ResevedAndAvaliableBalance)
                {
                    RenterLessor.CrCasRenterLessorBalance -= TotalContractValue + AmountPaid;
                }
                RenterLessor.CrCasRenterLessorReservedBalance = 0;  // 0
                RenterLessor.CrCasRenterLessorAvailableBalance = RenterLessor.CrCasRenterLessorBalance;
            }
            //Have greater than 1 Contract
            else
            {
                RenterLessor.CrCasRenterLessorReservedBalance -= OldContract?.CrCasRenterContractBasicExpectedTotal;
                if (AmountRequired >= 0 && AmountPaid >= 0) RenterLessor.CrCasRenterLessorAvailableBalance -= (TotalContractValue - OldContract?.CrCasRenterContractBasicExpectedTotal) - AmountPaid;
                else RenterLessor.CrCasRenterLessorAvailableBalance -= (TotalContractValue - OldContract?.CrCasRenterContractBasicExpectedTotal) + AmountPaid;
                RenterLessor.CrCasRenterLessorBalance = RenterLessor.CrCasRenterLessorReservedBalance + RenterLessor.CrCasRenterLessorAvailableBalance;
            }
            if (RenterLessor.CrCasRenterLessorContractDays == null) RenterLessor.CrCasRenterLessorContractDays = 0;
            RenterLessor.CrCasRenterLessorContractDays += DaysNo;
            if (RenterLessor.CrCasRenterLessorContractTradedAmount == null) RenterLessor.CrCasRenterLessorContractTradedAmount = 0;
            RenterLessor.CrCasRenterLessorContractTradedAmount += TotalContractValue;
            RenterLessor.CrCasRenterLessorStatus = Status.Active;

            if (_unitOfWork.CrCasRenterLessor.Update(RenterLessor) != null) return true;
            return false;
        }

        public async Task<CrCasRenterContractBasic> UpdateRenterSettlementContract(string ContractNo, string UserInsert, string ActualDaysNo, string Mechanizm, string CurrentMeter, string AdditionalKm,
                                                                              string TaxValue, string DiscountValue, string RequiredValue, string AmountPaid, string ExpensesValue, string ExpensesReasons, string CompensationValue,
                                                                             string CompensationReasons, string MaxHours, string MaxMinutes, string ExtraValueHours, string PrivateDriverValueTotal, string ChoicesValueTotal, string AdvantagesValueTotal, string ContractValue,
                                                                             string ContractValueAfterDiscount, string TotalContract, decimal PreviousBalance)
        {
            var OldContract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == ContractNo).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();
            var User = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == UserInsert);
            if (string.IsNullOrEmpty(CompensationValue)) CompensationValue = "0";
            if (string.IsNullOrEmpty(ExpensesValue)) ExpensesValue = "0";
            if (string.IsNullOrEmpty(AmountPaid)) AmountPaid = "0";
            OldContract.CrCasRenterContractBasicActualCloseDateTime = OldContract.CrCasRenterContractBasicExpectedStartDate?.AddDays(int.Parse(ActualDaysNo));
            OldContract.CrCasRenterContractBasicDelayMechanism = Mechanizm;
            double actualDays = Convert.ToDouble(ActualDaysNo);
            OldContract.CrCasRenterContractBasicActualDays = (int)Math.Floor(actualDays);
            OldContract.CrCasRenterContractBasicActualExtraHours = int.Parse(MaxHours);
            OldContract.CrCasRenterContractBasicActualCurrentReadingMeter = int.Parse(CurrentMeter);
            OldContract.CrCasRenterContractBasicActualFreeKm = int.Parse(ActualDaysNo) * OldContract.CrCasRenterContractBasicDailyFreeKm;
            OldContract.CrCasRenterContractBasicActualExtraKm = int.Parse(AdditionalKm);
            OldContract.CrCasRenterContractBasicActualDailyRent = OldContract.CrCasRenterContractBasicDailyRent;
            OldContract.CrCasRenterContractBasicActualRentValue = int.Parse(ActualDaysNo) * OldContract.CrCasRenterContractBasicDailyRent;
            OldContract.CrCasRenterContractBasicActualExtraHoursValue = int.Parse(MaxHours) * OldContract.CrCasRenterContractBasicHourValue;
            OldContract.CrCasRenterContractBasicActualExtraKmValue = int.Parse(AdditionalKm) * OldContract.CrCasRenterContractBasicKmValue;
            OldContract.CrCasRenterContractBasicActualPrivateDriverValue = decimal.Parse(PrivateDriverValueTotal, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualOptionsValue = decimal.Parse(ChoicesValueTotal, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualAdvantagesValue = decimal.Parse(AdvantagesValueTotal, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualValueBeforDiscount = decimal.Parse(ContractValue, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualDiscountValue = decimal.Parse(DiscountValue, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualValueAfterDiscount = decimal.Parse(ContractValueAfterDiscount, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualTaxValue = decimal.Parse(TaxValue, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualTotal = decimal.Parse(TotalContract, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicClosePreviousBalance = PreviousBalance;

            OldContract.CrCasRenterContractBasicCompensationValue = decimal.Parse(CompensationValue, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicExpensesValue = decimal.Parse(ExpensesValue, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicCompensationDescription = CompensationReasons;
            OldContract.CrCasRenterContractBasicExpensesDescription = ExpensesReasons;
            OldContract.CrCasRenterContractBasicActualAmountRequired = decimal.Parse(RequiredValue, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicAmountPaid = decimal.Parse(AmountPaid, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicStatus = Status.Closed;
            if (_unitOfWork.CrCasRenterContractBasic.Update(OldContract) != null) return OldContract;
            return null;
        }

        public async Task<bool> UpdateSalesPointBalance(string BranchCode, string LessorCode, string SalesPointCode, decimal AmountPaid, decimal AmountRequired)
        {
            var SalesPoint = await _unitOfWork.CrCasAccountSalesPoint.FindAsync(x => x.CrCasAccountSalesPointCode == SalesPointCode &&
                                                                                     x.CrCasAccountSalesPointLessor == LessorCode &&
                                                                                     x.CrCasAccountSalesPointBrn == BranchCode);
            if (SalesPoint != null)
            {
                if (AmountRequired < 0) AmountPaid = -AmountPaid;

                if (SalesPoint.CrCasAccountSalesPointTotalAvailable != null) SalesPoint.CrCasAccountSalesPointTotalAvailable += AmountPaid;
                else SalesPoint.CrCasAccountSalesPointTotalAvailable = AmountPaid;
                if (SalesPoint.CrCasAccountSalesPointTotalBalance != null) SalesPoint.CrCasAccountSalesPointTotalBalance += AmountPaid;
                else SalesPoint.CrCasAccountSalesPointTotalBalance = AmountPaid;
                if (_unitOfWork.CrCasAccountSalesPoint.Update(SalesPoint) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserBalance(string BranchCode, string LessorCode, string UserId, string PaymentMethod, decimal AmountPaid, decimal AmountRequired)
        {
            var UserInformation = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == UserId && x.CrMasUserInformationLessor == LessorCode);
            if (UserInformation != null)
            {
                if (AmountRequired < 0) AmountPaid = -AmountPaid;

                if (UserInformation.CrMasUserInformationAvailableBalance != null) UserInformation.CrMasUserInformationAvailableBalance += AmountPaid;
                else UserInformation.CrMasUserInformationAvailableBalance = AmountPaid;
                if (UserInformation.CrMasUserInformationTotalBalance != null) UserInformation.CrMasUserInformationTotalBalance += AmountPaid;
                else UserInformation.CrMasUserInformationTotalBalance = AmountPaid;
                if (_unitOfWork.CrMasUserInformation.Update(UserInformation) != null) return true;
            }
            return false;
        }

        public async Task<bool> UpdateCarInformation(string SerialNo, string LessorCode, string BranchCode, int CurrentMeter, string ExpireMaintainceCount)
        {
            var car = await _unitOfWork.CrCasCarInformation.FindAsync(x => x.CrCasCarInformationSerailNo == SerialNo && x.CrCasCarInformationLessor == LessorCode);
            if (car != null)
            {
                car.CrCasCarInformationStatus = Status.Active;
                car.CrCasCarInformationCurrentMeter = CurrentMeter;
                if (ExpireMaintainceCount != null && ExpireMaintainceCount != "5") car.CrCasCarInformationMaintenanceStatus = false;
                if (_unitOfWork.CrCasCarInformation.Update(car) != null) return true;
            }
            return false;
        }
        public async Task<bool> UpdateRenterContractCheckUp(string LessorCode, string ContractNo, string SerialNo, string PriceNo, string CheckUpCode, string Reasons)
        {
            var oldChechUp = await _unitOfWork.CrCasRenterContractCarCheckup.FindAsync(x => x.CrCasRenterContractCarCheckupNo == ContractNo && x.CrCasRenterContractCarCheckupCode == CheckUpCode);
            var carInfo = await _unitOfWork.CrCasCarInformation.FindAsync(x => x.CrCasCarInformationSerailNo == SerialNo);
            var carPrice = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicNo == PriceNo);
            if (carInfo != null && carPrice != null && oldChechUp != null) oldChechUp.CrCasRenterContractCarCheckupReasons = Reasons;

            if (_unitOfWork.CrCasRenterContractCarCheckup.Update(oldChechUp) != null) return true;
            return false;
        }
        public async Task<string> UpdateCarDocMaintainance(string SerialNo, string LessorCode, string BranchCode, int CurrentMeter)
        {
            var car = await _unitOfWork.CrCasCarInformation.FindAsync(x => x.CrCasCarInformationSerailNo == SerialNo && x.CrCasCarInformationLessor == LessorCode);
            var CarDocMaintainances = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(x => x.CrCasCarDocumentsMaintenanceSerailNo == car.CrCasCarInformationSerailNo && x.CrCasCarDocumentsMaintenanceLessor == LessorCode &&
                                                                                            x.CrCasCarDocumentsMaintenanceStatus == Status.Active).ToList();

            if (CarDocMaintainances != null)
            {
                foreach (var item in CarDocMaintainances.Where(x => x.CrCasCarDocumentsMaintenanceProceduresClassification == "12"))
                {
                    item.CrCasCarDocumentsMaintenanceCarStatus = Status.Active;
                    _unitOfWork.CrCasCarDocumentsMaintenance.Update(item);
                }
                foreach (var item in CarDocMaintainances.Where(x => x.CrCasCarDocumentsMaintenanceProceduresClassification == "13"))
                {
                    item.CrCasCarDocumentsMaintenanceCurrentMeter = CurrentMeter;
                    item.CrCasCarDocumentsMaintenanceCarStatus = Status.Active;
                    if (CurrentMeter >= item.CrCasCarDocumentsMaintenanceKmEndsAt) item.CrCasCarDocumentsMaintenanceStatus = Status.Expire;
                    if (CurrentMeter >= item.CrCasCarDocumentsMaintenanceKmAboutToFinish && CurrentMeter < item.CrCasCarDocumentsMaintenanceKmEndsAt) item.CrCasCarDocumentsMaintenanceStatus = Status.AboutToExpire;
                    _unitOfWork.CrCasCarDocumentsMaintenance.Update(item);
                }
                return CarDocMaintainances.Where(x => x.CrCasCarDocumentsMaintenanceProceduresClassification == "13" && x.CrCasCarDocumentsMaintenanceStatus != Status.Expire).Count().ToString();
            }
            return null;
        }
        public async Task<bool> AddAccountContractCompanyOwed(string ContractNo, string DaysNo, decimal DailyRentValue)
        {
            var OldContract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == ContractNo).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();
            var CompanyContract = await _unitOfWork.CrMasContractCompany.FindAsync(x => x.CrMasContractCompanyLessor == OldContract.CrCasRenterContractBasicLessor && x.CrMasContractCompanyProcedures == "112");//ForBnan Contract
            var CompanyContractDetailed = _unitOfWork.CrMasContractCompanyDetailed.FindAll(x => x.CrMasContractCompanyDetailedNo == CompanyContract.CrMasContractCompanyNo);
            CrCasAccountContractCompanyOwed crCasAccountContractCompany = new CrCasAccountContractCompanyOwed();
            if (CompanyContractDetailed != null) // Subscribtion
            {
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedNo = OldContract.CrCasRenterContractBasicNo;
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedCompanyCode = OldContract.CrCasRenterContractBasicLessor;
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedContractCom = CompanyContract.CrMasContractCompanyNo;
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedDate = DateTime.Now;
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedType = CompanyContract.CrMasContractCompanyActivation;


                crCasAccountContractCompany.CrCasAccountContractCompanyOwedDaliayValue = OldContract.CrCasRenterContractBasicActualDailyRent;
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedContractValue = int.Parse(DaysNo) * OldContract.CrCasRenterContractBasicActualDailyRent;
                foreach (var company in CompanyContractDetailed)
                {
                    if (company.CrMasContractCompanyDetailedFromPrice <= crCasAccountContractCompany.CrCasAccountContractCompanyOwedDaliayValue &&
                        company.CrMasContractCompanyDetailedToPrice >= crCasAccountContractCompany.CrCasAccountContractCompanyOwedDaliayValue)
                    {
                        crCasAccountContractCompany.CrCasAccountContractCompanyOwedBeforeAmount = int.Parse(DaysNo) * company.CrMasContractCompanyDetailedValue;
                    }
                }
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedPercentage = CompanyContract.CrMasContractCompanyDiscountRate;
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedAfterAmount = crCasAccountContractCompany.CrCasAccountContractCompanyOwedBeforeAmount - (crCasAccountContractCompany.CrCasAccountContractCompanyOwedBeforeAmount * CompanyContract.CrMasContractCompanyDiscountRate / 100);

                crCasAccountContractCompany.CrCasAccountContractCompanyOwedTaxPercentage = CompanyContract.CrMasContractCompanyTaxRate;
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedTaxValue = crCasAccountContractCompany.CrCasAccountContractCompanyOwedAfterAmount * CompanyContract.CrMasContractCompanyTaxRate / 100;
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedAmount = crCasAccountContractCompany.CrCasAccountContractCompanyOwedAfterAmount + crCasAccountContractCompany.CrCasAccountContractCompanyOwedTaxValue;
                crCasAccountContractCompany.CrCasAccountContractCompanyOwedAccrualStatus = false;
                if (await _unitOfWork.CrCasAccountContractCompanyOwed.AddAsync(crCasAccountContractCompany) != null) return true;
            }
            return false;

        }

        private CrCasAccountReceipt GetContractAccountReceipt(string LessorCode, string BranchCode, string Procedure)
        {
            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var Lrecord = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceiptLessorCode == LessorCode &&
                                                                       x.CrCasAccountReceiptYear == y && x.CrCasAccountReceiptBranchCode == BranchCode && x.CrCasAccountReceiptType == Procedure)
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

        public async Task<bool> UpdateRenterStatistics(CrCasRenterContractBasic Contract)
        {
            var Statistic = await _unitOfWork.CrCasRenterContractStatistic.FindAsync(x => x.CrCasRenterContractStatisticsNo == Contract.CrCasRenterContractBasicNo);
            if (Contract != null)
            {
                // H Month 
                Statistic.CrCasRenterContractStatisticsDayCount = GetCountDaysCategory((int)Contract.CrCasRenterContractBasicActualDays);
                //Contract Value Category
                Statistic.CrCasRenterContractStatisticsValueNo = GetContractValueCategory((decimal)Contract.CrCasRenterContractBasicActualTotal);
                // Data From Contract 
                Statistic.CrCasRenterContractStatisicsDays = Contract.CrCasRenterContractBasicActualDays;
                Statistic.CrCasRenterContractStatisticsRentValue = Contract.CrCasRenterContractBasicActualRentValue;
                Statistic.CrCasRenterContractStatisticsAdditionsValue = Contract.CrCasRenterContractBasicAdditionalValue;
                Statistic.CrCasRenterContractStatisticsOptionsValue = Contract.CrCasRenterContractBasicActualOptionsValue;
                Statistic.CrCasRenterContractStatisticsAdditionsKmValue = Contract.CrCasRenterContractBasicActualExtraKmValue;
                Statistic.CrCasRenterContractStatisticsAdditionsHourValue = Contract.CrCasRenterContractBasicActualExtraHoursValue;
                Statistic.CrCasRenterContractStatisticsContractValue = Contract.CrCasRenterContractBasicActualValueBeforDiscount;
                Statistic.CrCasRenterContractStatisticsDiscountValue = Contract.CrCasRenterContractBasicActualDiscountValue;
                Statistic.CrCasRenterContractStatisticsContractAfterValue = Contract.CrCasRenterContractBasicActualValueAfterDiscount;
                Statistic.CrCasRenterContractStatisticsTaxValue = Contract.CrCasRenterContractBasicActualTaxValue;
                Statistic.CrCasRenterContractStatisticsExpensesValue = Contract.CrCasRenterContractBasicExpensesValue;
                Statistic.CrCasRenterContractStatisticsCompensationValue = Contract.CrCasRenterContractBasicCompensationValue;
                var CompanyContractBnan = await _unitOfWork.CrCasAccountContractCompanyOwed.FindAsync(x => x.CrCasAccountContractCompanyOwedNo == Contract.CrCasRenterContractBasicNo);
                if (CompanyContractBnan != null) Statistic.CrCasRenterContractStatisticsBnanValue = CompanyContractBnan.CrCasAccountContractCompanyOwedAmount;

                if (_unitOfWork.CrCasRenterContractStatistic.Update(Statistic) != null) return true;
                return false;
            }
            return false;
        }

        public string GetCountDaysCategory(int daysNo)
        {
            if (daysNo >= 1 && daysNo <= 3)
            {
                return "1"; // من 1 إلى 3
            }
            else if (daysNo >= 4 && daysNo <= 7)
            {
                return "2"; // من 4 إلى 7
            }
            else if (daysNo >= 8 && daysNo <= 10)
            {
                return "3"; // من 8 إلى 10
            }
            else if (daysNo >= 11 && daysNo <= 15)
            {
                return "4"; // من 11 إلى 15
            }
            else if (daysNo >= 16 && daysNo <= 20)
            {
                return "5"; // من 16 إلى 20
            }
            else if (daysNo >= 21 && daysNo <= 25)
            {
                return "6"; // من 21 إلى 25
            }
            else if (daysNo >= 26 && daysNo <= 30)
            {
                return "7"; // من 26 إلى 30
            }
            else if (daysNo > 30)
            {
                return "8"; // أكثر من 30
            }
            else
            {
                return "0";
            }
        }
        public string GetContractValueCategory(decimal value)
        {
            if (value < 300)
            {
                return "1";
            }
            else if (value > 300 && value <= 500)
            {
                return "2";
            }
            else if (value > 500 && value <= 1000)
            {
                return "3";
            }
            else if (value > 1000 && value <= 1500)
            {
                return "4";
            }
            else if (value > 1500 && value <= 2000)
            {
                return "5";
            }
            else if (value > 2000 && value <= 2500)
            {
                return "6";
            }
            else if (value > 2500 && value <= 3000)
            {
                return "7";
            }
            else if (value > 3000 && value <= 3500)
            {
                return "8";
            }
            else
            {
                return "9"; // العمر أكثر من 60
            }
        }
    }
}
