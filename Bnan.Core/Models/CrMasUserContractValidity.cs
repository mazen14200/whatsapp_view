using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasUserContractValidity
    {
        public string CrMasUserContractValidityUserId { get; set; } = null!;
        public string? CrMasUserContractValidityAdmin { get; set; }
        public bool CrMasUserContractValidityRegister { get; set; }
        public bool? CrMasUserContractValidityChamber { get; set; }
        public bool? CrMasUserContractValidityTransferPermission { get; set; }
        public bool? CrMasUserContractValidityLicenceMunicipale { get; set; }
        public bool? CrMasUserContractValidityCompanyAddress { get; set; }
        public bool? CrMasUserContractValidityTrafficLicense { get; set; }
        public bool? CrMasUserContractValidityInsurance { get; set; }
        public bool? CrMasUserContractValidityOperatingCard { get; set; }
        public bool? CrMasUserContractValidityChkecUp { get; set; }
        public bool? CrMasUserContractValidityId { get; set; }
        public bool? CrMasUserContractValidityDrivingLicense { get; set; }
        public bool? CrMasUserContractValidityRenterAddress { get; set; }
        public bool? CrMasUserContractValidityEmployer { get; set; }
        public bool? CrMasUserContractValidityAge { get; set; }
        public bool? CrMasUserContractValidityTires { get; set; }
        public bool? CrMasUserContractValidityOil { get; set; }
        public bool? CrMasUserContractValidityMaintenance { get; set; }
        public bool? CrMasUserContractValidityFbrake { get; set; }
        public bool? CrMasUserContractValidityBbrake { get; set; }
        public decimal? CrMasUserContractValidityDiscountRate { get; set; }
        public int? CrMasUserContractValidityKm { get; set; }
        public int? CrMasUserContractValidityHour { get; set; }
        public bool? CrMasUserContractValidityCreate { get; set; }
        public bool? CrMasUserContractValidityCancel { get; set; }
        public bool? CrMasUserContractValidityExtension { get; set; }
        public bool? CrMasUserContractValidityLessContractValue { get; set; }
        public bool? CrMasUserContractValidityEnd { get; set; }

        public virtual CrCasSysAdministrativeProcedure? CrMasUserContractValidityAdminNavigation { get; set; }
        public virtual CrMasUserInformation CrMasUserContractValidityUser { get; set; } = null!;
    }
}
