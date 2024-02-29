using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSysServiceEvaluation
    {
        public string CrMasSysServiceEvaluationContractNo { get; set; } = null!;
        public string? CrMasSysServiceEvaluationLessor { get; set; }
        public string? CrMasSysServiceEvaluationRenter { get; set; }
        public string? CrMasSysServiceEvaluationUser { get; set; }
        public string? CrMasSysServiceEvaluationCode { get; set; }
        public long? CrMasSysServiceEvaluationValue { get; set; }
        public DateTime? CrMasSysServiceEvaluationDate { get; set; }
        public TimeSpan? CrMasSysServiceEvaluationTime { get; set; }
        public string? CrMasSysServiceEvaluationReasons { get; set; }

        public virtual CrMasSysEvaluationType? CrMasSysServiceEvaluationCodeNavigation { get; set; }
        public virtual CrMasLessorInformation? CrMasSysServiceEvaluationLessorNavigation { get; set; }
        public virtual CrMasUserInformation? CrMasSysServiceEvaluationUserNavigation { get; set; }
    }
}
