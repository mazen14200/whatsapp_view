using Bnan.Core.Models;
using Microsoft.CodeAnalysis.Options;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bnan.Ui.ViewModels.BS
{
    public class BSLayoutVM
    {
        public List<CrCasBranchInformation>? CrCasBranchInformations { get; set; }
        public List<CrCasCarInformation>? AvaliableCars { get; set; }
        public List<CrCasCarInformation>? UnAvaliableCars { get; set; }
        public List<CrCasCarInformation>? Cars { get; set; }
        public List<CrCasCarInformation>? RentedCars { get; set; }
        public List<CrCasCarDocumentsMaintenance>? DocumentsMaintenances { get; set; }
        public List<CrCasRenterPrivateDriverInformation>? Drivers { get; set; }
        public List<CrMasSupCarCategory>? CarCategories { get; set; }
        public List<CrCasCarInformation>? CarsFilter { get; set; }
        public List<CrMasSupContractCarCheckup>? CarsCheckUp { get; set; }
        public List<CrCasAccountBank>? AccountBanks { get; set; }
        public List<CrCasAccountSalesPoint>? SalesPoint { get; set; }
        public List<CrMasSupAccountPaymentMethod>? PaymentMethods { get; set; }
        public List<CrCasRenterContractBasic>? BasicContracts { get; set; }
        public List<CrCasRenterContractAlert>? AlertContract { get; set; }
        //Renter Lessor PAge
        public List<CrCasRenterLessor>? RentersLessor { get; set; }
        public CrCasRenterLessor? Renter { get; set; }
        public List<CrCasRenterContractBasic>? RenterContracts { get; set; }
        public List<CrMasSysEvaluation>? Evaluations { get; set; }

        //Report PAge
        public List<CrCasAccountReceipt>? AccountReceipts { get; set; }
        public decimal? TotalDebit { get; set; }
        public decimal? TotalCreditor { get; set; }
        // Custody Page
        public List<CrCasAccountSalesPoint>? SalesPointHaveBalance { get; set; }
        public decimal? SalesPointBalanceTotal { get; set; }
        public decimal? SalesPointBalanceResereved { get; set; }
        public decimal? SalesPointBalanceAvaliable { get; set; }
        public decimal? UserBalanceTotal { get; set; }
        public decimal? UserBalanceResereved { get; set; }
        public decimal? UserBalanceAvaliable { get; set; }
        public decimal? CreditorTotal { get; set; }
        public decimal? DebitTotal { get; set; }
        public decimal? TransaferTotal { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? SalesPointSelected { get; set; }
        public List<CrCasAccountReceipt>? CustodyReceipts { get; set; }
        public List<PaymentMethodBranchDataVM>? PaymentMethodsBranch { get; set; }
        public List<PaymentMethodBranchDataVM>? PaymentMethodsUser { get; set; }
        public CrCasBranchInformation? CrCasBranchInformation { get; set; }
        public CrMasUserBranchValidity? CrMasUserBranchValidity { get; set; }
        public CrCasSysAdministrativeProcedure? CrCasSysAdministrativeProcedure { get; set; }
        public Contract? Contract { get; set; }

        public string? SelectedBranch { get; set; }


    }
}
