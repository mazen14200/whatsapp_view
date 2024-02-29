using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupRenterNationality
    {
        public CrMasSupRenterNationality()
        {
            CrCasRenterContractStatistics = new HashSet<CrCasRenterContractStatistic>();
            CrCasRenterLessors = new HashSet<CrCasRenterLessor>();
            CrCasRenterPrivateDriverInformations = new HashSet<CrCasRenterPrivateDriverInformation>();
            CrMasRenterInformations = new HashSet<CrMasRenterInformation>();
        }

        public string CrMasSupRenterNationalitiesCode { get; set; } = null!;
        public string? CrMasSupRenterNationalitiesGroupCode { get; set; }
        public string? CrMasSupRenterNationalitiesArName { get; set; }
        public string? CrMasSupRenterNationalitiesEnName { get; set; }
        public string? CrMasSupRenterNationalitiesFlag { get; set; }
        public int? CrMasSupRenterNationalitiesCounter { get; set; }
        public string? CrMasSupRenterNationalitiesStatus { get; set; }
        public string? CrMasSupRenterNationalitiesReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupRenterNationalitiesGroupCodeNavigation { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatistics { get; set; }
        public virtual ICollection<CrCasRenterLessor> CrCasRenterLessors { get; set; }
        public virtual ICollection<CrCasRenterPrivateDriverInformation> CrCasRenterPrivateDriverInformations { get; set; }
        public virtual ICollection<CrMasRenterInformation> CrMasRenterInformations { get; set; }
    }
}
