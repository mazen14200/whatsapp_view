using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarBrand
    {
        public CrMasSupCarBrand()
        {
            CrCasCarAdvantages = new HashSet<CrCasCarAdvantage>();
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
            CrCasPriceCarBasics = new HashSet<CrCasPriceCarBasic>();
            CrCasRenterContractStatistics = new HashSet<CrCasRenterContractStatistic>();
            CrMasSupCarDistributions = new HashSet<CrMasSupCarDistribution>();
            CrMasSupCarModels = new HashSet<CrMasSupCarModel>();
        }

        public string CrMasSupCarBrandCode { get; set; } = null!;
        public string? CrMasSupCarBrandArName { get; set; }
        public string? CrMasSupCarBrandEnName { get; set; }
        public string? CrMasSupCarBrandImage { get; set; }
        public string? CrMasSupCarBrandStatus { get; set; }
        public string? CrMasSupCarBrandReasons { get; set; }

        public virtual ICollection<CrCasCarAdvantage> CrCasCarAdvantages { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
        public virtual ICollection<CrCasPriceCarBasic> CrCasPriceCarBasics { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatistics { get; set; }
        public virtual ICollection<CrMasSupCarDistribution> CrMasSupCarDistributions { get; set; }
        public virtual ICollection<CrMasSupCarModel> CrMasSupCarModels { get; set; }
    }
}
