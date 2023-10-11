using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasLessorClassification
    {
        public CrCasLessorClassification()
        {
            CrMasLessorInformations = new HashSet<CrMasLessorInformation>();
        }

        public string CrCasLessorClassificationCode { get; set; } = null!;
        public string? CrCasLessorClassificationArName { get; set; }
        public string? CrCasLessorClassificationEnName { get; set; }
        public string? CrMasLessorClassificationStatus { get; set; }
        public string? CrMasLessorClassificationReasons { get; set; }

        public virtual ICollection<CrMasLessorInformation> CrMasLessorInformations { get; set; }
    }
}
