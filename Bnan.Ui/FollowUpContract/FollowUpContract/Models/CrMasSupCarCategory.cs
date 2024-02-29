using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarCategory
    {
        public CrMasSupCarCategory()
        {
            CrCasCarAdvantages = new HashSet<CrCasCarAdvantage>();
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
            CrCasPriceCarBasics = new HashSet<CrCasPriceCarBasic>();
            CrCasRenterContractStatistics = new HashSet<CrCasRenterContractStatistic>();
            CrMasSupCarDistributions = new HashSet<CrMasSupCarDistribution>();
        }

        public string CrMasSupCarCategoryCode { get; set; } = null!;
        public string? CrMasSupCarCategoryGroup { get; set; }
        public string? CrMasSupCarCategoryArName { get; set; }
        public string? CrMasSupCarCategoryEnName { get; set; }
        public string? CrMasSupCarCategoryStatus { get; set; }
        public string? CrMasSupCarCategoryReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupCarCategoryGroupNavigation { get; set; }
        public virtual ICollection<CrCasCarAdvantage> CrCasCarAdvantages { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
        public virtual ICollection<CrCasPriceCarBasic> CrCasPriceCarBasics { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatistics { get; set; }
        public virtual ICollection<CrMasSupCarDistribution> CrMasSupCarDistributions { get; set; }
    }
}
