using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasRenterContractStatistic
    {
        public string CrCasRenterContractStatisticsNo { get; set; } = null!;
        public string? CrCasRenterContractStatisticsLessor { get; set; }
        public string? CrCasRenterContractStatisticsBranch { get; set; }
        public DateTime? CrCasRenterContractStatisticsDate { get; set; }
        public string? CrCasRenterContractStatisticsBranchRegions { get; set; }
        public string? CrCasRenterContractStatisticsBranchCity { get; set; }
        public string? CrCasRenterContractStatisticsRenterRegions { get; set; }
        public string? CrCasRenterContractStatisticsRenterCity { get; set; }
        public string? CrCasRenterContractStatisticsNationalities { get; set; }
        public string? CrCasRenterContractStatisticsGender { get; set; }
        public string? CrCasRenterContractStatisticsJobs { get; set; }
        public string? CrCasRenterContractStatisticsMembership { get; set; }
        public string? CrCasRenterContractStatisticsBrand { get; set; }
        public string? CrCasRenterContractStatisticsModel { get; set; }
        public string? CrCasRenterContractStatisticsCategory { get; set; }
        public string? CrCasRenterContractStatisticsCarYear { get; set; }
        public string? CrCasRenterContractStatisticsHmonthCreate { get; set; }
        public string? CrCasRenterContractStatisticsGmonthCreate { get; set; }
        public string? CrCasRenterContractStatisticsDayCreate { get; set; }
        public string? CrCasRenterContractStatisticsDayClose { get; set; }
        public string? CrCasRenterContractStatisticsTimeCreate { get; set; }
        public string? CrCasRenterContractStatisticsTimeClose { get; set; }
        public string? CrCasRenterContractStatisticsDayCount { get; set; }
        public string? CrCasRenterContractStatisticsAgeNo { get; set; }
        public string? CrCasRenterContractStatisticsValueNo { get; set; }
        public string? CrCasRenterContractStatisticsKm { get; set; }
        public string? CrCasRenterContractStatisticsRenter { get; set; }
        public string? CrCasRenterContractStatisticsCarSerialNo { get; set; }
        public string? CrCasRenterContractStatisticsUserOpen { get; set; }
        public string? CrCasRenterContractStatisticsUserClose { get; set; }
        public int? CrCasRenterContractStatisicsDays { get; set; }
        public decimal? CrCasRenterContractStatisticsRentValue { get; set; }
        public decimal? CrCasRenterContractStatisticsAdditionsValue { get; set; }
        public decimal? CrCasRenterContractStatisticsOptionsValue { get; set; }
        public decimal? CrCasRenterContractStatisticsAuthorizationValue { get; set; }
        public decimal? CrCasRenterContractStatisticsAdditionsKmValue { get; set; }
        public decimal? CrCasRenterContractStatisticsAdditionsHourValue { get; set; }
        public decimal? CrCasRenterContractStatisticsContractValue { get; set; }
        public decimal? CrCasRenterContractStatisticsDiscountValue { get; set; }
        public decimal? CrCasRenterContractStatisticsContractAfterValue { get; set; }
        public decimal? CrCasRenterContractStatisticsTaxValue { get; set; }
        public decimal? CrCasRenterContractStatisticsExpensesValue { get; set; }
        public decimal? CrCasRenterContractStatisticsCompensationValue { get; set; }
        public decimal? CrCasRenterContractStatisticsBnanValue { get; set; }

        public virtual CrCasBranchInformation? CrCasRenterContractStatistics { get; set; }
        public virtual CrMasSupPostCity? CrCasRenterContractStatisticsBranchCityNavigation { get; set; }
        public virtual CrMasSupPostRegion? CrCasRenterContractStatisticsBranchRegionsNavigation { get; set; }
        public virtual CrMasSupCarBrand? CrCasRenterContractStatisticsBrandNavigation { get; set; }
        public virtual CrCasCarInformation? CrCasRenterContractStatisticsCarSerialNoNavigation { get; set; }
        public virtual CrMasSupCarCategory? CrCasRenterContractStatisticsCategoryNavigation { get; set; }
        public virtual CrMasSupRenterGender? CrCasRenterContractStatisticsGenderNavigation { get; set; }
        public virtual CrMasSupRenterProfession? CrCasRenterContractStatisticsJobsNavigation { get; set; }
        public virtual CrMasSupRenterMembership? CrCasRenterContractStatisticsMembershipNavigation { get; set; }
        public virtual CrMasSupCarModel? CrCasRenterContractStatisticsModelNavigation { get; set; }
        public virtual CrMasSupRenterNationality? CrCasRenterContractStatisticsNationalitiesNavigation { get; set; }
        public virtual CrCasRenterLessor? CrCasRenterContractStatisticsNavigation { get; set; }
        public virtual CrMasSupPostCity? CrCasRenterContractStatisticsRenterCityNavigation { get; set; }
        public virtual CrMasSupPostRegion? CrCasRenterContractStatisticsRenterRegionsNavigation { get; set; }
        public virtual CrMasUserInformation? CrCasRenterContractStatisticsUserCloseNavigation { get; set; }
        public virtual CrMasUserInformation? CrCasRenterContractStatisticsUserOpenNavigation { get; set; }
    }
}
