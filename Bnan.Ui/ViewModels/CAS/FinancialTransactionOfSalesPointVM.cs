using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class FinancialTransactionOfSalesPointVM
    {
        public List<CrCasAccountReceipt>? crCasAccountReceipt { get; set; }
        public List<CrCasAccountSalesPoint>? crCasAccountSalesPoint { get; set; }
        public List<CrCasRenterContractBasic>? crCasRenterContractBasics { get; set; }

        public List<List<string>>? All_Counts = new List<List<string>>(); 
        public List<CrCasAccountReceipt>? FinancialTransactionOfSalesPoints_Filtered { get; set; }


    }
}
