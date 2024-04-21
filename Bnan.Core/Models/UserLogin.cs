using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class UserLogin
    {
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }
        public string UserId { get; set; } = null!;

        public virtual CrMasUserInformation User { get; set; } = null!;
    }
}
