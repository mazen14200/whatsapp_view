using Bnan.Core.Models;

namespace Bnan.Ui.ViewModels.BS
{
    public class BSCarsInformationVM
    {
        public CrCasCarInformation? CarInformation { get; set; }
        public CrCasPriceCarBasic? CarPrice { get; set; }
        public List<CrCasCarDocumentsMaintenance>? CarMaintenance { get; set; }
        //public string? Options { get; set; }
        //public string? Additionals { get; set; }

    }
}
