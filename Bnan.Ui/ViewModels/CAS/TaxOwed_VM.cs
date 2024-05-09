using Bnan.Core.Models;

namespace Bnan.Ui.ViewModels.CAS
{
    public class TaxOwed_VM
    {
        public List<List<string>>? Contract_Count { get; set; }
        
        public int Amount_will_Pay = 0;
        public string? New_TaxOwed_Tax_no  { get; set; }
    public List<CrCasAccountContractTaxOwed>? CrCasAccountContractTaxOwed { get; set; }
        public List<CrCasAccountContractTaxOwed>? CrCasAccountContractTaxOwed_Filtered { get; set; }
        public List<CrCasSysAdministrativeProcedure>? CrCasSysAdministrativeProcedure { get; set; }

    }
}
