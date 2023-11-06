using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarYear
    {
        public string CrMasSupCarYearCode { get; set; } = null!;
        public string? CrMasSupCarYearGroup { get; set; }
        public string? CrMasSupCarYearNo { get; set; }

        public virtual CrMasSysGroup? CrMasSupCarYearGroupNavigation { get; set; }
    }
}
