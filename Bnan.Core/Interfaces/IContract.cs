using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IContract
    {
        Task<CrMasRenterInformation> AddRenterFromElmToMasRenter(string RenterID, CrElmEmployer crElmEmployer , CrElmPersonal crElmPersonal, CrElmLicense crElmLicense);
        Task<CrMasRenterPost> AddRenterFromElmToMasRenterPost(string RenterID, CrElmPost crElmPost);
        Task<CrCasRenterLessor> AddRenterFromElmToCasRenterLessor(string LessorCode, CrMasRenterInformation crMasRenterInformation, CrMasRenterPost crMasRenterPost);
        Task<bool> AddRenterContractChoice(string LessorCode,string ContractNo, string SerialNo, string PriceNo, string Choice);
        Task<bool> AddRenterContractAdditional(string LessorCode, string ContractNo, string SerialNo, string PriceNo, string Choice);
        Task<bool> AddRenterContractAdvantages(CrCasPriceCarAdvantage crCasPriceCarAdvantage, string ContractNo);
        Task<bool> AddRenterContractCheckUp(string LessorCode, string ContractNo, string SerialNo, string PriceNo, string CheckUpCode, string Reasons);
        Task<CrCasRenterContractBasic> AddRenterContractBasic(string LessorCode, string BranchCode, string ContractNo, string RenterId, string DriverId, string PrivateDriver,
                                                       string AdditionalDriver, string SerialNo, string PriceNo, string DaysNo, string UserFreeHour, string UserFreeKm,
                                                       string CurrentMeter, string OptionsTotal, string AdditionalTotal, string ContractValueAfterDiscount,
                                                       string DiscountValue, string ContractValueBeforeDiscount, string TaxValue, string TotalAmount, string UserInsert,
                                                       string Authrization,string UserDiscount, string AmountPayed, string Reasons);
        Task<bool> AddRenterContractAuthrization(string ContractNo,string LessorCode,string AuthrizationType,string AuthrizationValue);
        Task<bool> UpdateCarInformation(string SerialNo, string LessorCode, string BranchCode, int DaysNo, int CurrentMeter);
        Task<bool> UpdateCarDocMaintainance(string SerialNo, string LessorCode, string BranchCode, int CurrentMeter);
        Task<bool> UpdateRenterLessor(string RenterId, string LessorCode,DateTime LastContract,decimal TotalPayed);


    }
}
