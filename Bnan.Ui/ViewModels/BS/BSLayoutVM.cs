using Bnan.Core.Models;
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
        public CrCasBranchInformation? CrCasBranchInformation { get; set; }
        public CrMasUserBranchValidity? CrMasUserBranchValidity { get; set; }
        public CrCasSysAdministrativeProcedure? CrCasSysAdministrativeProcedure { get; set; }
        public Contract? Contract { get; set; }
        public string? SelectedBranch { get; set; }


    }
}
