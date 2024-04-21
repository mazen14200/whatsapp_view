using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class UserToken
    {
        public string UserId { get; set; } = null!;
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }

        public virtual CrMasUserInformation User { get; set; } = null!;
    }
}
