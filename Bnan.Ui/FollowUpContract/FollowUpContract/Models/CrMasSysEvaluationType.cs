using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSysEvaluationType
    {
        public CrMasSysEvaluationType()
        {
            CrMasSysServiceEvaluations = new HashSet<CrMasSysServiceEvaluation>();
        }

        public string CrMasSysEvaluationTypesCode { get; set; } = null!;
        public string? CrMasSysEvaluationTypesKind { get; set; }
        public string? CrMasSysEvaluationTypesArDescription { get; set; }
        public string? CrMasSysEvaluationTypesEnDescription { get; set; }
        public long? CrMasSysServiceEvaluationTypesValue { get; set; }
        public string? CrMasSysEvaluationTypesImage { get; set; }
        public string? CrMasSysEvaluationTypesStatus { get; set; }
        public string? CrMasSysEvaluationTypesReasons { get; set; }

        public virtual ICollection<CrMasSysServiceEvaluation> CrMasSysServiceEvaluations { get; set; }
    }
}
