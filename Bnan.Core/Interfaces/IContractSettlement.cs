using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IContractSettlement
    {
        Task<CrCasRenterContractBasic> UpdateRenterSettlementContract(string ContractNo, string UserInsert, string ActualDaysNo, string Mechanizm, string CurrentMeter, string AdditionalKm,
                                                                          string TaxValue, string DiscountValue, string RequiredValue, string AmountPaid, string ExpensesValue, string ExpensesReasons, string CompensationValue,
                                                                             string CompensationReasons, string MaxHours, string MaxMinutes, string ExtraValueHours, string PrivateDriverValueTotal, string ChoicesValueTotal, string ContractValue,
                                                                             string ContractValueAfterDiscount, string TotalContract, decimal PreviousBalance);


        Task<bool> AddAccountReceipt(string ContractNo, string LessorCode, string BranchCode, string PaymentMethod, string Account, string SerialNo, string SalesPointNo,
                                    decimal TotalPayed, string RenterId, string UserId, string PassingType, string Reasons, string pdfPathAr, string pdfPathEn, string procedureCode);
        Task<bool> AddAccountContractTaxOwed(string ContractNo,decimal ContractValue);
        Task<bool> AddAccountContractCompanyOwed(string ContractNo,string DaysNo,decimal DailyRentValue);
        Task<bool> UpdateAuthrization(string ContractNo);
        Task<bool> UpdateAlert(string ContractNo);
        Task<bool> UpdateRenterLessor(string ContractNo,decimal AmountRequired , decimal AmountPaid, decimal ContractValue, decimal TotalContractValue, int DaysNo);
        Task<bool> UpdateBranchBalance(string BranchCode, string LessorCode, decimal AmountPaid,decimal AmountRequired);
        Task<bool> UpdateSalesPointBalance(string BranchCode, string LessorCode, string SalesPointCode, decimal AmountPaid, decimal AmountRequired);
        Task<bool> UpdateBranchValidity(string BranchCode, string LessorCode, string UserId, string PaymentMethod, decimal AmountPaid, decimal AmountRequired);
        Task<bool> UpdateUserBalance(string BranchCode, string LessorCode, string UserId, string PaymentMethod, decimal AmountPaid, decimal AmountRequired);
        Task<bool> UpdateMasRenter(string RenterId);
        Task<bool> UpdateDriverStatus(string DriverId, string LessorCode);
        Task<bool> UpdatePrivateDriverStatus(string PrivateDriverId, string LessorCode);
        Task<bool> UpdateCarInformation(string SerialNo, string LessorCode, string BranchCode, int CurrentMeter, string ExpireMaintainceCount);
        Task<string> UpdateCarDocMaintainance(string SerialNo, string LessorCode, string BranchCode, int CurrentMeter);
        Task<bool> UpdateRenterContractCheckUp(string LessorCode, string ContractNo, string SerialNo, string PriceNo, string CheckUpCode, string Reasons);
        Task<bool> UpdateRenterStatistics(CrCasRenterContractBasic Contract);

    }
}
