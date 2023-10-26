using Bnan.Core.Models;
using Bnan.Ui.ViewModels.MAS;

namespace Bnan.Ui.ViewModels.CAS
{
    public class LayoutVM
    {
        public List<CrCasBranchDocument>? DocumentsCompany { get; set; }
        public List<CrCasCarDocumentsMaintenance>? DocumentsCar { get; set; }
        public List<CrCasCarDocumentsMaintenance>? MaintainceCar { get; set; }
        public List<CrMasContractCompany>? ContractCompany { get; set; }
    }
}
