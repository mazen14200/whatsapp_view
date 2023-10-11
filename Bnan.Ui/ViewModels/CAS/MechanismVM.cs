using Bnan.Core.Models;
using Bnan.Ui.ViewModels.MAS;

namespace Bnan.Ui.ViewModels.CAS
{
    public class MechanismVM
    {
        public string? CrCasLessorMechanismCode { get; set; } = null!;
        public string? CrCasLessorMechanismProcedures { get; set; } = null!;
        public string? CrCasLessorMechanismProceduresClassification { get; set; }
        public bool? CrCasLessorMechanismActivate { get; set; }
        public int? CrCasLessorMechanismDaysAlertAboutExpire { get; set; }
        public int? CrCasLessorMechanismKmAlertAboutExpire { get; set; }

        public  CrMasLessorInformationVM? CrCasLessorMechanismCodeNavigation { get; set; }
        public  CrMasSysProcedureVM? CrCasLessorMechanismProceduresNavigation { get; set; }
    }
}
