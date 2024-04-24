﻿using Bnan.Core.Extensions;
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
            crCasAccountContractTaxOwed.CrCasAccountContractTaxOwedValue = crCasAccountContractTaxOwed.CrCasAccountContractTaxOwedContractValue * OldContract.CrCasRenterContractBasicTaxRate;
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
            if (UserValidity == null)
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

        public async Task<bool> UpdateRenterLessor(string ContractNo, decimal AmountRequired, decimal AmountPaid, decimal TotalContractValue)
        {
            var OldContract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == ContractNo).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();
            var RenterLessor = await _unitOfWork.CrCasRenterLessor.FindAsync(x => x.CrCasRenterLessorId == OldContract.CrCasRenterContractBasicRenterId && x.CrCasRenterLessorCode == OldContract.CrCasRenterContractBasicLessor);

            var ResevedAndAvaliableBalance = RenterLessor.CrCasRenterLessorAvailableBalance + RenterLessor.CrCasRenterLessorReservedBalance;

            if (ResevedAndAvaliableBalance == TotalContractValue)
            {
                RenterLessor.CrCasRenterLessorBalance = 0;
                RenterLessor.CrCasRenterLessorReservedBalance = 0;
                RenterLessor.CrCasRenterLessorAvailableBalance = 0;
            }
            else if (TotalContractValue > ResevedAndAvaliableBalance)
            {
                RenterLessor.CrCasRenterLessorBalance -= TotalContractValue - AmountPaid; // 400 > 400 + 100 - 497.9 = 2.10
                RenterLessor.CrCasRenterLessorReservedBalance = 0; // 397.9 > 397.9 + 100 - 500=2.10
                RenterLessor.CrCasRenterLessorAvailableBalance = RenterLessor.CrCasRenterLessorBalance; // 2.10 > -10-0=-10
            }
            else if (TotalContractValue < ResevedAndAvaliableBalance)
            {
                RenterLessor.CrCasRenterLessorBalance -= TotalContractValue + AmountPaid; // 12.10 +397.9 =400-410=-10
                RenterLessor.CrCasRenterLessorReservedBalance = 0;  // 0
                RenterLessor.CrCasRenterLessorAvailableBalance = RenterLessor.CrCasRenterLessorBalance; // 0 > -10-0=-10
            }
            //if (AmountPaid == 0 && AmountRequired == 0)
            //{
            //    RenterLessor.CrCasRenterLessorBalance = 0;
            //    RenterLessor.CrCasRenterLessorReservedBalance = 0; 
            //    RenterLessor.CrCasRenterLessorAvailableBalance = 0; 
            //}
            //// AmountRequired = "-2.10";
            //// AmountPaid ="0";
            //// var totalBalance = "400";
            //// var ResevedBalance = "397.9";
            //// var AvaBalance = "2.10";
            //// له فلوس ومدفعناش 
            //else if (AmountPaid == 0 && AmountRequired < 0)
            //{
            //    RenterLessor.CrCasRenterLessorBalance -= TotalContractValue; // 2.10
            //    RenterLessor.CrCasRenterLessorReservedBalance = 0;  // 0
            //    RenterLessor.CrCasRenterLessorAvailableBalance = RenterLessor.CrCasRenterLessorBalance; // 2.10
            //}
            //// AmountRequired = "-2.10";
            //// AmountPaid = "2.10";
            //// var totalBalance = "400";
            //// var ResevedBalance = "397.9";
            //// var AvaBalance = "2.10";
            //// له فلوس او ملوش ودفعناله 
            //else if (AmountPaid >= 0 && AmountRequired <= 0)
            //{
            //    RenterLessor.CrCasRenterLessorBalance = TotalContractValue - AmountPaid; // 12.10 +397.9 =400-410=-10
            //    RenterLessor.CrCasRenterLessorReservedBalance = 0;  // 0
            //    RenterLessor.CrCasRenterLessorAvailableBalance = RenterLessor.CrCasRenterLessorBalance; // 0 > -10-0=-10
            //}
            //// AmountRequired = "-2.10";
            //// AmountPaid = "2.10";
            //// var totalBalance = "400";
            //// var ResevedBalance = "397.9";
            //// var AvaBalance = "2.10";
            //// var AmountRequired = "100";
            //// var TotalContractValue = "497.9";
            //// عليه فلوس ودفع 
            //else if (AmountPaid > 0 && AmountRequired > 0)
            //{
            //    RenterLessor.CrCasRenterLessorBalance = AmountRequired - AmountPaid; // 400 > 400 + 100 - 497.9 = 2.10
            //    RenterLessor.CrCasRenterLessorReservedBalance = 0; // 397.9 > 397.9 + 100 - 500=2.10
            //    RenterLessor.CrCasRenterLessorAvailableBalance = RenterLessor.CrCasRenterLessorBalance; // 2.10 > -10-0=-10
            //}



            if (_unitOfWork.CrCasRenterLessor.Update(RenterLessor) != null) return true;
            return false;
        }

        public async Task<CrCasRenterContractBasic> UpdateRenterSettlementContract(string ContractNo, string UserInsert, string ActualDaysNo, string Mechanizm, string CurrentMeter, string AdditionalKm,
                                                                              string TaxValue, string DiscountValue, string RequiredValue, string AmountPaid, string ExpensesValue, string ExpensesReasons, string CompensationValue,
                                                                             string CompensationReasons, string MaxHours, string MaxMinutes, string ExtraValueHours, string PrivateDriverValueTotal, string ChoicesValueTotal, string ContractValue,
                                                                             string ContractValueAfterDiscount, string TotalContract, decimal PreviousBalance)
        {
            var OldContract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == ContractNo).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();
            var User = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == UserInsert);

            OldContract.CrCasRenterContractBasicActualCloseDateTime = OldContract.CrCasRenterContractBasicExpectedStartDate?.AddDays(int.Parse(ActualDaysNo));
            OldContract.CrCasRenterContractBasicDelayMechanism = Mechanizm;
            double actualDays = Convert.ToDouble(ActualDaysNo);
            OldContract.CrCasRenterContractBasicActualDays = (int)Math.Floor(actualDays);
            OldContract.CrCasRenterContractBasicActualExtraHours = int.Parse(MaxHours);
            OldContract.CrCasRenterContractBasicActualCurrentReadingMeter = int.Parse(CurrentMeter);
            OldContract.CrCasRenterContractBasicActualFreeKm = int.Parse(ActualDaysNo) * OldContract.CrCasRenterContractBasicDailyFreeKm;
            OldContract.CrCasRenterContractBasicActualExtraKm = int.Parse(AdditionalKm);
            OldContract.CrCasRenterContractBasicActualDailyRent = OldContract.CrCasRenterContractBasicDailyRent;
            OldContract.CrCasRenterContractBasicActualDailyRent = int.Parse(ActualDaysNo) * OldContract.CrCasRenterContractBasicDailyRent;
            OldContract.CrCasRenterContractBasicActualExtraHoursValue = int.Parse(MaxHours) * OldContract.CrCasRenterContractBasicHourValue;
            OldContract.CrCasRenterContractBasicActualExtraKmValue = int.Parse(AdditionalKm) * OldContract.CrCasRenterContractBasicKmValue;
            OldContract.CrCasRenterContractBasicActualPrivateDriverValue = decimal.Parse(PrivateDriverValueTotal, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualValueBeforDiscount = decimal.Parse(ContractValue, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualDiscountValue = decimal.Parse(DiscountValue, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualValueAfterDiscount = decimal.Parse(ContractValueAfterDiscount, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualTaxValue = decimal.Parse(TaxValue, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicActualTotal = decimal.Parse(TotalContract, CultureInfo.InvariantCulture);
            OldContract.CrCasRenterContractBasicClosePreviousBalance = PreviousBalance;
            if (string.IsNullOrEmpty(CompensationValue)) CompensationValue = "0";
            if (string.IsNullOrEmpty(ExpensesValue)) ExpensesValue = "0";
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
            var car = await _unitOfWork.CrCasCarInformation.FindAsync(x => x.CrCasCarInformationSerailNo == SerialNo && x.CrCasCarInformationLessor == LessorCode && x.CrCasCarInformationBranch == BranchCode);
            if (car != null)
            {
                car.CrCasCarInformationStatus = Status.Active;
                car.CrCasCarInformationCurrentMeter = CurrentMeter;
                if (ExpireMaintainceCount != null && ExpireMaintainceCount != "0") car.CrCasCarInformationMaintenanceStatus = false;
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
            var car = await _unitOfWork.CrCasCarInformation.FindAsync(x => x.CrCasCarInformationSerailNo == SerialNo && x.CrCasCarInformationLessor == LessorCode && x.CrCasCarInformationBranch == BranchCode);
            var CarDocMaintainances = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(x => x.CrCasCarDocumentsMaintenanceSerailNo == car.CrCasCarInformationSerailNo && x.CrCasCarDocumentsMaintenanceLessor == LessorCode &&
                                                                                            x.CrCasCarDocumentsMaintenanceBranch == BranchCode && x.CrCasCarDocumentsMaintenanceStatus == Status.Active).ToList();

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
                return CarDocMaintainances.Where(x => x.CrCasCarDocumentsMaintenanceProceduresClassification == "13").Count().ToString();
            }
            return null;
        }
        public async Task<bool> AddAccountContractCompanyOwed(string ContractNo, string DaysNo, decimal DailyRentValue)
        {
            var OldContract = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicNo == ContractNo).OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault();
            var CompanyContract = await _unitOfWork.CrMasContractCompany.FindAsync(x => x.CrMasContractCompanyLessor == OldContract.CrCasRenterContractBasicLessor && x.CrMasContractCompanyProcedures == "112");//ForBnan Contract
            var CompanyContractDetailed = _unitOfWork.CrMasContractCompanyDetailed.FindAll(x => x.CrMasContractCompanyDetailedNo == CompanyContract.CrMasContractCompanyNo);
            CrCasAccountContractCompanyOwed crCasAccountContractCompany = new CrCasAccountContractCompanyOwed();
            if (CompanyContract.CrMasContractCompanyActivation != "1") // Subscribtion
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
            }
            if (await _unitOfWork.CrCasAccountContractCompanyOwed.AddAsync(crCasAccountContractCompany) != null) return true;
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

        
    }
}
