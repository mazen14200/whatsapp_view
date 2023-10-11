using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarColor
    {
        public string CrMasSupCarColorCode { get; set; } = null!;
        public string? CrMasSupCarColorArName { get; set; }
        public string? CrMasSupCarColorEnName { get; set; }
        public int? CrMasSupCarColorCounter { get; set; }
        public string? CrMasSupCarColorImage { get; set; }
        public string? CrMasSupCarColorStatus { get; set; }
        public string? CrMasSupCarColorReasons { get; set; }
    }
}
