using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasUserEvaluationRenter
    {
        public string CrMasUserEvaluationRenterContractNo { get; set; } = null!;
        public string? CrMasUserEvaluationRenterLessor { get; set; }
        public string? CrMasUserEvaluationRenterRenter { get; set; }
        public string? CrMasUserEvaluationRenterUser { get; set; }
        public string? CrMasUserEvaluationRenterCode { get; set; }
        public int? CrMasUserEvaluationRenterValue { get; set; }
        public DateTime? CrMasUserEvaluationRenterDate { get; set; }
        public TimeSpan? CrMasUserEvaluationRenterTime { get; set; }
        public string? CrMasUserEvaluationRenterReasons { get; set; }

        public virtual CrMasSysEvaluationType? CrMasUserEvaluationRenterCodeNavigation { get; set; }
        public virtual CrMasLessorInformation? CrMasUserEvaluationRenterLessorNavigation { get; set; }
        public virtual CrMasUserInformation? CrMasUserEvaluationRenterUserNavigation { get; set; }
    }
}
