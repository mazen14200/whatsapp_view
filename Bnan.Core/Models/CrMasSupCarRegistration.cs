using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupCarRegistration
    {
        public string CrMasSupCarRegistrationCode { get; set; } = null!;
        public string? CrMasSupCarRegistrationArName { get; set; }
        public string? CrMasSupCarRegistrationEnName { get; set; }
        public string? CrMasSupCarRegistrationStatus { get; set; }
        public string? CrMasSupCarRegistrationReasons { get; set; }
    }
}
