using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class UserContractVM
    {
        public List<CrCasRenterContractBasic> crCasRenterContractBasics { get; set; }
        public List<CrMasUserInformation> crMasUserInformation { get; set; }
    }
}
