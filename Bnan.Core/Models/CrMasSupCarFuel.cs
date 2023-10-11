using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarFuel
    {
        public string CrMasSupCarFuelCode { get; set; } = null!;
        public string? CrMasSupCarFuelArName { get; set; }
        public string? CrMasSupCarFuelEnName { get; set; }
        public string? CrMasSupCarFuelImage { get; set; }
        public string? CrMasSupCarFuelStatus { get; set; }
        public string? CrMasSupCarFuelReasons { get; set; }
    }
}
