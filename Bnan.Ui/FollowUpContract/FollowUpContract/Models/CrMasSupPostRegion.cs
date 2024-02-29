using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupPostRegion
    {
        public CrMasSupPostRegion()
        {
            CrCasBranchPosts = new HashSet<CrCasBranchPost>();
            CrCasCarInformations = new HashSet<CrCasCarInformation>();
            CrCasRenterContractStatisticCrCasRenterContractStatisticsBranchRegionsNavigations = new HashSet<CrCasRenterContractStatistic>();
            CrCasRenterContractStatisticCrCasRenterContractStatisticsRenterRegionsNavigations = new HashSet<CrCasRenterContractStatistic>();
            CrCasRenterLessors = new HashSet<CrCasRenterLessor>();
            CrMasRenterPosts = new HashSet<CrMasRenterPost>();
            CrMasSupPostCities = new HashSet<CrMasSupPostCity>();
        }

        public string CrMasSupPostRegionsCode { get; set; } = null!;
        public string? CrMasSupPostRegionsArName { get; set; }
        public string? CrMasSupPostRegionsEnName { get; set; }
        public string? CrMasSupPostRegionsLocation { get; set; }
        public decimal? CrMasSupPostRegionsLongitude { get; set; }
        public decimal? CrMasSupPostRegionsLatitude { get; set; }
        public string? CrMasSupPostRegionsStatus { get; set; }
        public string? CrMasSupPostRegionsReasons { get; set; }

        public virtual ICollection<CrCasBranchPost> CrCasBranchPosts { get; set; }
        public virtual ICollection<CrCasCarInformation> CrCasCarInformations { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatisticCrCasRenterContractStatisticsBranchRegionsNavigations { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatisticCrCasRenterContractStatisticsRenterRegionsNavigations { get; set; }
        public virtual ICollection<CrCasRenterLessor> CrCasRenterLessors { get; set; }
        public virtual ICollection<CrMasRenterPost> CrMasRenterPosts { get; set; }
        public virtual ICollection<CrMasSupPostCity> CrMasSupPostCities { get; set; }
    }
}
