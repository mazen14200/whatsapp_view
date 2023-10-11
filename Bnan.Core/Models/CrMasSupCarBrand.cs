using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarBrand
    {
        public CrMasSupCarBrand()
        {
            CrMasSupCarDistributions = new HashSet<CrMasSupCarDistribution>();
            CrMasSupCarModels = new HashSet<CrMasSupCarModel>();
        }

        public string CrMasSupCarBrandCode { get; set; } = null!;
        public string? CrMasSupCarBrandArName { get; set; }
        public string? CrMasSupCarBrandEnName { get; set; }
        public string? CrMasSupCarBrandStatus { get; set; }
        public string? CrMasSupCarBrandReasons { get; set; }

        public virtual ICollection<CrMasSupCarDistribution> CrMasSupCarDistributions { get; set; }
        public virtual ICollection<CrMasSupCarModel> CrMasSupCarModels { get; set; }
    }
}
