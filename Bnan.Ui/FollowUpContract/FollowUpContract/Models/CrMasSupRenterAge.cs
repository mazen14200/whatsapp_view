using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupRenterAge
    {
        public string CrMasSupRenterAgeCode { get; set; } = null!;
        public string? CrMasSupRenterAgeGroupCode { get; set; }
        public int? CrMasSupRenterAgeNo { get; set; }

        public virtual CrMasSysGroup? CrMasSupRenterAgeGroupCodeNavigation { get; set; }
    }
}
