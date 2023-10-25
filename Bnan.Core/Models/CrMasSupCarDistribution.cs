using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarDistribution
    {
        public CrMasSupCarDistribution()
        {
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
            CrCasPriceCarBasics = new HashSet<CrCasPriceCarBasic>();
        }

        public string CrMasSupCarDistributionCode { get; set; } = null!;
        public string CrMasSupCarDistributionBrand { get; set; } = null!;
        public string CrMasSupCarDistributionModel { get; set; } = null!;
        public string CrMasSupCarDistributionCategory { get; set; } = null!;
        public string CrMasSupCarDistributionYear { get; set; } = null!;
        public int? CrMasSupCarDistributionDoor { get; set; }
        public int? CrMasSupCarDistributionBagBags { get; set; }
        public int? CrMasSupCarDistributionSmallBags { get; set; }
        public int? CrMasSupCarDistributionPassengers { get; set; }
        public int? CrMasSupCarDistributionCount { get; set; }
        public string? CrMasSupCarDistributionConcatenateArName { get; set; }
        public string? CrMasSupCarDistributionConcatenateEnName { get; set; }
        public string? CrMasSupCarDistributionImage { get; set; }
        public string? CrMasSupCarDistributionStatus { get; set; }
        public string? CrMasSupCarDistributionReasons { get; set; }

        public virtual CrMasSupCarBrand CrMasSupCarDistributionBrandNavigation { get; set; } = null!;
        public virtual CrMasSupCarCategory CrMasSupCarDistributionCategoryNavigation { get; set; } = null!;
        public virtual CrMasSupCarModel CrMasSupCarDistributionModelNavigation { get; set; } = null!;
        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
        public virtual ICollection<CrCasPriceCarBasic> CrCasPriceCarBasics { get; set; }
    }
}
