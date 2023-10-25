using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasCarAdvantage
    {
        public string CrCasCarAdvantagesSerialNo { get; set; } = null!;
        public string CrCasCarAdvantagesCode { get; set; } = null!;
        public string? CrCasCarAdvantagesLessor { get; set; }
        public string? CrCasCarAdvantagesBrand { get; set; }
        public string? CrCasCarAdvantagesModel { get; set; }
        public string? CrCasCarAdvantagesCarYear { get; set; }
        public string CrCasCarAdvantagesCategory { get; set; } = null!;
        public string? CrCasCarAdvantagesStatus { get; set; }

        public virtual CrMasSupCarBrand? CrCasCarAdvantagesBrandNavigation { get; set; }
        public virtual CrMasSupCarCategory CrCasCarAdvantagesCategoryNavigation { get; set; } = null!;
        public virtual CrMasSupCarAdvantage CrCasCarAdvantagesCodeNavigation { get; set; } = null!;
        public virtual CrMasLessorInformation? CrCasCarAdvantagesLessorNavigation { get; set; }
        public virtual CrMasSupCarModel? CrCasCarAdvantagesModelNavigation { get; set; }
        public virtual CrCasCarInformation CrCasCarAdvantagesSerialNoNavigation { get; set; } = null!;
    }
}
