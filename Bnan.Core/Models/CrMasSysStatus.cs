using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSysStatus
    {
        public string CrMasSysStatusCode { get; set; } = null!;
        public string? CrMasSysStatusArName { get; set; }
        public string? CrMasSysStatusEnName { get; set; }
        public string? CrMasSysStatusStatus { get; set; }
        public string? CrMasSysStatusReasons { get; set; }
    }
}
