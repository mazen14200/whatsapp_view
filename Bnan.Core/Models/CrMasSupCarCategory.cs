using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarCategory
    {
        public CrMasSupCarCategory()
        {
            CrMasSupCarDistributions = new HashSet<CrMasSupCarDistribution>();
        }

        public string CrMasSupCarCategoryCode { get; set; } = null!;
        public string? CrMasSupCarCategoryGroup { get; set; }
        public string? CrMasSupCarCategoryArName { get; set; }
        public string? CrMasSupCarCategoryEnName { get; set; }
        public string? CrMasSupCarCategoryStatus { get; set; }
        public string? CrMasSupCarCategoryReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupCarCategoryGroupNavigation { get; set; }
        public virtual ICollection<CrMasSupCarDistribution> CrMasSupCarDistributions { get; set; }
    }
}
