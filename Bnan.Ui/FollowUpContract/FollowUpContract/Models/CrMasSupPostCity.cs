using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupPostCity
    {
        public CrMasSupPostCity()
        {
            CrCasBranchPosts = new HashSet<CrCasBranchPost>();
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
            CrCasRenterContractStatisticCrCasRenterContractStatisticsBranchCityNavigations = new HashSet<CrCasRenterContractStatistic>();
            CrCasRenterContractStatisticCrCasRenterContractStatisticsRenterCityNavigations = new HashSet<CrCasRenterContractStatistic>();
            CrCasRenterLessors = new HashSet<CrCasRenterLessor>();
            CrMasRenterPosts = new HashSet<CrMasRenterPost>();
        }

        public string CrMasSupPostCityCode { get; set; } = null!;
        public string? CrMasSupPostCityGroupCode { get; set; }
        public string? CrMasSupPostCityRegionsCode { get; set; }
        public string? CrMasSupPostCityArName { get; set; }
        public string? CrMasSupPostCityEnName { get; set; }
        public string? CrMasSupPostCityConcatenateArName { get; set; }
        public string? CrMasSupPostCityConcatenateEnName { get; set; }
        public string? CrMasSupPostCityLocation { get; set; }
        public decimal? CrMasSupPostCityLongitude { get; set; }
        public decimal? CrMasSupPostCityLatitude { get; set; }
        public int? CrMasSupPostCityCounter { get; set; }
        public string? CrMasSupPostCityRegionsStatus { get; set; }
        public string? CrMasSupPostCityStatus { get; set; }
        public string? CrMasSupPostCityReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupPostCityGroupCodeNavigation { get; set; }
        public virtual CrMasSupPostRegion? CrMasSupPostCityRegionsCodeNavigation { get; set; }
        public virtual ICollection<CrCasBranchPost> CrCasBranchPosts { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatisticCrCasRenterContractStatisticsBranchCityNavigations { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatisticCrCasRenterContractStatisticsRenterCityNavigations { get; set; }
        public virtual ICollection<CrCasRenterLessor> CrCasRenterLessors { get; set; }
        public virtual ICollection<CrMasRenterPost> CrMasRenterPosts { get; set; }
    }
}
