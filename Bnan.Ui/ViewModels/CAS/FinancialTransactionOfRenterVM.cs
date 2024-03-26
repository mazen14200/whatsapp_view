using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class FinancialTransactionOfRenterVM
    {
        public List<CrCasAccountReceipt>? crCasAccountReceipt { get; set; }
        public List<CrCasRenterLessor>? crCasRenterLessor { get; set; }
        public List<CrCasRenterContractBasic>? crCasRenterContractBasics { get; set; }
        public List<CrMasRenterInformation>? crMasRenterInformation { get; set; }

        public List<List<string>>? All_Counts = new List<List<string>>(); 
        public List<CrCasAccountReceipt>? FinancialTransactionOfRente_Filtered { get; set; }


    }
}
