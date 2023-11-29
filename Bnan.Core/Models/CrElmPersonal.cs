using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrElmPersonal
    {
        public string CrElmPersonalCode { get; set; } = null!;
        public int? CrElmPersonalIdCopy { get; set; }
        public string? CrElmPersonalPersonalArName { get; set; }
        public string? CrElmPersonalPersonalEnName { get; set; }
        public string? CrElmPersonalPersonalSector { get; set; }
        public DateTime? CrElmPersonalPersonalDate { get; set; }
        public DateTime? CrElmPersonalIssuedIdDate { get; set; }
        public DateTime? CrElmPersonalExpiryIdDate { get; set; }
        public string? CrElmPersonalSource { get; set; }
        public string? CrElmPersonalArNationality { get; set; }
        public string? CrElmPersonalEnNationality { get; set; }
        public string? CrElmPersonalArGender { get; set; }
        public string? CrElmPersonalEnGender { get; set; }
        public string? CrElmPersonalArProfessions { get; set; }
        public string? CrElmPersonalEnProfessions { get; set; }
        public string? CrElmPersonalCountryKey { get; set; }
        public string? CrElmPersonalMobile { get; set; }
        public string? CrElmPersonalEmail { get; set; }
    }
}
