using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSysGroup
    {
        public CrMasSysGroup()
        {
            CrMasSupCarCategories = new HashSet<CrMasSupCarCategory>();
            CrMasSupCarModels = new HashSet<CrMasSupCarModel>();
            CrMasSupCarYears = new HashSet<CrMasSupCarYear>();
            CrMasSupContractAdditionals = new HashSet<CrMasSupContractAdditional>();
            CrMasSupContractOptions = new HashSet<CrMasSupContractOption>();
            CrMasSupPostCities = new HashSet<CrMasSupPostCity>();
            CrMasSupRenterAges = new HashSet<CrMasSupRenterAge>();
            CrMasSupRenterEmployers = new HashSet<CrMasSupRenterEmployer>();
            CrMasSupRenterGenders = new HashSet<CrMasSupRenterGender>();
            CrMasSupRenterMemberships = new HashSet<CrMasSupRenterMembership>();
            CrMasSupRenterNationalities = new HashSet<CrMasSupRenterNationality>();
            CrMasSupRenterProfessions = new HashSet<CrMasSupRenterProfession>();
        }

        public string CrMasSysGroupCode { get; set; } = null!;
        public bool? CrMasSysGroupClassified { get; set; }
        public bool? CrMasSysGroupIndependent { get; set; }
        public string? CrMasSysGroupArName { get; set; }
        public string? CrMasSysGroupEnName { get; set; }
        public string? CrMasSysGroupStatus { get; set; }
        public string? CrMasSysGroupReasons { get; set; }

        public virtual ICollection<CrMasSupCarCategory> CrMasSupCarCategories { get; set; }
        public virtual ICollection<CrMasSupCarModel> CrMasSupCarModels { get; set; }
        public virtual ICollection<CrMasSupCarYear> CrMasSupCarYears { get; set; }
        public virtual ICollection<CrMasSupContractAdditional> CrMasSupContractAdditionals { get; set; }
        public virtual ICollection<CrMasSupContractOption> CrMasSupContractOptions { get; set; }
        public virtual ICollection<CrMasSupPostCity> CrMasSupPostCities { get; set; }
        public virtual ICollection<CrMasSupRenterAge> CrMasSupRenterAges { get; set; }
        public virtual ICollection<CrMasSupRenterEmployer> CrMasSupRenterEmployers { get; set; }
        public virtual ICollection<CrMasSupRenterGender> CrMasSupRenterGenders { get; set; }
        public virtual ICollection<CrMasSupRenterMembership> CrMasSupRenterMemberships { get; set; }
        public virtual ICollection<CrMasSupRenterNationality> CrMasSupRenterNationalities { get; set; }
        public virtual ICollection<CrMasSupRenterProfession> CrMasSupRenterProfessions { get; set; }
    }
}
