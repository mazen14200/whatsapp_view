using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasLessorImage
    {
        public string CrMasLessorImageCode { get; set; } = null!;
        public string? CrMasLessorImageLogo { get; set; }
        public string? CrMasLessorImageLogoPrint { get; set; }
        public string? CrMasLessorImageStamp { get; set; }
        public string? CrMasLessorImageStampOutsideCity { get; set; }
        public string? CrMasLessorImageStampOutsideCountry { get; set; }
        public string? CrMasLessorImageStampFullAmountPaid { get; set; }
        public string? CrMasLessorImageSignatureDirector { get; set; }
        public string? CrMasLessorImageCreateContractEmail { get; set; }
        public string? CrMasLessorImageCreateContractWhatUp { get; set; }
        public string? CrMasLessorImageTomorrowContractEmail { get; set; }
        public string? CrMasLessorImageTomorrowContractWhatUp { get; set; }
        public string? CrMasLessorImageHourContractEmail { get; set; }
        public string? CrMasLessorImageHourContractWhatUp { get; set; }
        public string? CrMasLessorImageEndContractEmail { get; set; }
        public string? CrMasLessorImageEndContractWhatUp { get; set; }
        public string? CrMasLessorImageCloseContractEmail { get; set; }
        public string? CrMasLessorImageCloseContractWhatUp { get; set; }
        public string? CrMasLessorImageContArConditions1 { get; set; }
        public string? CrMasLessorImageContEnConditions1 { get; set; }
        public string? CrMasLessorImageContArConditions2 { get; set; }
        public string? CrMasLessorImageContEnConditions2 { get; set; }
        public string? CrMasLessorImageContArConditions3 { get; set; }
        public string? CrMasLessorImageContEnConditions3 { get; set; }

        public virtual CrMasLessorInformation CrMasLessorImageCodeNavigation { get; set; } = null!;
    }
}
