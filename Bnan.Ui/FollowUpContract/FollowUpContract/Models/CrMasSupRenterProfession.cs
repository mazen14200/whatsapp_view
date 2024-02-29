using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupRenterProfession
    {
        public CrMasSupRenterProfession()
        {
            CrCasRenterContractStatistics = new HashSet<CrCasRenterContractStatistic>();
            CrCasRenterLessors = new HashSet<CrCasRenterLessor>();
            CrMasRenterInformations = new HashSet<CrMasRenterInformation>();
        }

        public string CrMasSupRenterProfessionsCode { get; set; } = null!;
        public string? CrMasSupRenterProfessionsGroupCode { get; set; }
        public string? CrMasSupRenterProfessionsArName { get; set; }
        public string? CrMasSupRenterProfessionsEnName { get; set; }
        public string? CrMasSupRenterProfessionsStatus { get; set; }
        public string? CrMasSupRenterProfessionsReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupRenterProfessionsGroupCodeNavigation { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic> CrCasRenterContractStatistics { get; set; }
        public virtual ICollection<CrCasRenterLessor> CrCasRenterLessors { get; set; }
        public virtual ICollection<CrMasRenterInformation> CrMasRenterInformations { get; set; }
    }
}
