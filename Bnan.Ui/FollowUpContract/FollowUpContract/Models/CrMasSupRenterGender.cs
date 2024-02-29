using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupRenterGender
    {
        public CrMasSupRenterGender()
        {
            CrCasRenterContractStatistics = new HashSet<CrCasRenterContractStatistic>();
            CrCasRenterLessors = new HashSet<CrCasRenterLessor>();
            CrCasRenterPrivateDriverInformations = new HashSet<CrCasRenterPrivateDriverInformation>();
            CrMasRenterInformations = new HashSet<CrMasRenterInformation>();
        }

        public string CrMasSupRenterGenderCode { get; set; } = null!;
        public string? CrMasSupRenterGenderGroupCode { get; set; }
        public string? CrMasSupRenterGenderArName { get; set; }
        public string? CrMasSupRenterGenderEnName { get; set; }
        public string? CrMasSupRenterGenderStatus { get; set; }
        public string? CrMasSupRenterGenderReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupRenterGenderGroupCodeNavigation { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatistics { get; set; }
        public virtual ICollection<CrCasRenterLessor> CrCasRenterLessors { get; set; }
        public virtual ICollection<CrCasRenterPrivateDriverInformation> CrCasRenterPrivateDriverInformations { get; set; }
        public virtual ICollection<CrMasRenterInformation> CrMasRenterInformations { get; set; }
    }
}
