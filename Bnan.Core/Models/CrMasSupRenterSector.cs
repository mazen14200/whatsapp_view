﻿using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasSupRenterSector
    {
        public CrMasSupRenterSector()
        {
            CrCasBeneficiaries = new HashSet<CrCasBeneficiary>();
            CrCasOwners = new HashSet<CrCasOwner>();
            CrCasSysAdministrativeProcedures = new HashSet<CrCasSysAdministrativeProcedure>();
            CrMasContractCompanies = new HashSet<CrMasContractCompany>();
            CrMasSupRenterEmployers = new HashSet<CrMasSupRenterEmployer>();
        }

        public string CrMasSupRenterSectorCode { get; set; } = null!;
        public string? CrMasSupRenterSectorArName { get; set; }
        public string? CrMasSupRenterSectorEnName { get; set; }
        public string? CrMasSupRenterSectorStatus { get; set; }
        public string? CrMasSupRenterSectorReasons { get; set; }

        public virtual ICollection<CrCasBeneficiary> CrCasBeneficiaries { get; set; }
        public virtual ICollection<CrCasOwner> CrCasOwners { get; set; }
        public virtual ICollection<CrCasSysAdministrativeProcedure> CrCasSysAdministrativeProcedures { get; set; }
        public virtual ICollection<CrMasContractCompany> CrMasContractCompanies { get; set; }
        public virtual ICollection<CrMasSupRenterEmployer> CrMasSupRenterEmployers { get; set; }
    }
}
