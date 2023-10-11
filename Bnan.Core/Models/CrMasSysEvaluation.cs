using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSysEvaluation
    {
        public string CrMasSysEvaluationsCode { get; set; } = null!;
        public string? CrMasSysEvaluationsArDescription { get; set; }
        public string? CrMasSysEvaluationsEnDescription { get; set; }
        public int? CrMasSysServiceEvaluationsValue { get; set; }
        public string? CrMasSysEvaluationsImage { get; set; }
        public string? CrMasSysEvaluationsStatus { get; set; }
        public string? CrMasSysEvaluationsReasons { get; set; }
    }
}
