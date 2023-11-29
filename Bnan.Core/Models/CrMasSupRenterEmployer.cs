using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupRenterEmployer
    {
        public CrMasSupRenterEmployer()
        {
            CrMasRenterInformations = new HashSet<CrMasRenterInformation>();
        }

        public string CrMasSupRenterEmployerCode { get; set; } = null!;
        public string? CrMasSupRenterEmployerGroupCode { get; set; }
        public string? CrMasSupRenterEmployerSectorCode { get; set; }
        public string? CrMasSupRenterEmployerArName { get; set; }
        public string? CrMasSupRenterEmployerEnName { get; set; }
        public int? CrMasSupRenterEmployerCounter { get; set; }
        public string? CrMasSupRenterEmployerStatus { get; set; }
        public string? CrMasSupRenterEmployerReasons { get; set; }

        public virtual CrMasSysGroup? CrMasSupRenterEmployerGroupCodeNavigation { get; set; }
        public virtual CrMasSupRenterSector? CrMasSupRenterEmployerSectorCodeNavigation { get; set; }
        public virtual ICollection<CrMasRenterInformation> CrMasRenterInformations { get; set; }
    }
}
