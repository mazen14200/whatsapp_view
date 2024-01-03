using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarModel
    {
        public CrMasSupCarModel()
        {
            CrCasCarAdvantages = new HashSet<CrCasCarAdvantage>();
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
            CrCasPriceCarBasics = new HashSet<CrCasPriceCarBasic>();
            CrCasRenterContractStatistics = new HashSet<CrCasRenterContractStatistic>();
            CrMasSupCarDistributions = new HashSet<CrMasSupCarDistribution>();
        }

        public string CrMasSupCarModelCode { get; set; } = null!;
        public string? CrMasSupCarModelGroup { get; set; }
        public string? CrMasSupCarModelBrand { get; set; }
        public string? CrMasSupCarModelArName { get; set; }
        public string? CrMasSupCarModelEnName { get; set; }
        public string? CrMasSupCarModelArConcatenateName { get; set; }
        public string? CrMasSupCarModelConcatenateEnName { get; set; }
        public int? CrMasSupCarModelCounter { get; set; }
        public string? CrMasSupCarModelStatus { get; set; }
        public string? CrMasSupCarModelReasons { get; set; }

        public virtual CrMasSupCarBrand? CrMasSupCarModelBrandNavigation { get; set; }
        public virtual CrMasSysGroup? CrMasSupCarModelGroupNavigation { get; set; }
        public virtual ICollection<CrCasCarAdvantage> CrCasCarAdvantages { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
        public virtual ICollection<CrCasPriceCarBasic> CrCasPriceCarBasics { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatistics { get; set; }
        public virtual ICollection<CrMasSupCarDistribution> CrMasSupCarDistributions { get; set; }
    }
}
