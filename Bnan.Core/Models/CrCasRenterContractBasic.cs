﻿using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasRenterContractBasic
    {
        public CrCasRenterContractBasic()
        {
            CrCasRenterContractAdditionals = new HashSet<CrCasRenterContractAdditional>();
            CrCasRenterContractAdvantages = new HashSet<CrCasRenterContractAdvantage>();
            CrCasRenterContractCarCheckups = new HashSet<CrCasRenterContractCarCheckup>();
            CrCasRenterContractChoices = new HashSet<CrCasRenterContractChoice>();
        }

        public string CrCasRenterContractBasicNo { get; set; } = null!;
        public int? CrCasRenterContractBasicCopy { get; set; }
        public string? CrCasRenterContractBasicYear { get; set; }
        public string? CrCasRenterContractBasicSector { get; set; }
        public string? CrCasRenterContractBasicProcedures { get; set; }
        public string? CrCasRenterContractBasicLessor { get; set; }
        public string? CrCasRenterContractBasicBranch { get; set; }
        public DateTime? CrCasRenterContractBasicIssuedDate { get; set; }
        public DateTime? CrCasRenterContractBasicAllowCanceled { get; set; }
        public DateTime? CrCasRenterContractBasicExpectedStartDate { get; set; }
        public DateTime? CrCasRenterContractBasicExpectedEndDate { get; set; }
        public int? CrCasRenterContractBasicExpectedRentalDays { get; set; }
        public string? CrCasRenterContractBasicRenterId { get; set; }
        public string? CrCasRenterContractBasicDriverId { get; set; }
        public string? CrCasRenterContractBasicAdditionalDriverId { get; set; }
        public string? CrCasRenterContractBasicPrivateDriverId { get; set; }
        public string? CrCasRenterContractBasicCarSerailNo { get; set; }
        public int? CrCasRenterContractBasicFreeHours { get; set; }
        public int? CrCasRenterContractBasicUserFreeHours { get; set; }
        public int? CrCasRenterContractBasicTotalFreeHours { get; set; }
        public int? CrCasRenterContractBasicHourMax { get; set; }
        public decimal? CrCasRenterContractBasicHourValue { get; set; }
        public int? CrCasRenterContractBasicDailyFreeKm { get; set; }
        public int? CrCasRenterContractBasicDailyFreeKmUser { get; set; }
        public int? CrCasRenterContractBasicTotalDailyFreeKm { get; set; }
        public decimal? CrCasRenterContractBasicKmValue { get; set; }
        public decimal? CrCasRenterContractBasicDailyRent { get; set; }
        public decimal? CrCasRenterContractBasicWeeklyRent { get; set; }
        public decimal? CrCasRenterContractBasicMonthlyRent { get; set; }
        public decimal? CrCasRenterContractBasicYearlyRent { get; set; }
        public decimal? CrCasRenterContractBasicAdditionalDriverValue { get; set; }
        public decimal? CrCasRenterContractBasicPrivateDriverValue { get; set; }
        public decimal? CrCasRenterContractBasicAuthorizationValue { get; set; }
        public decimal? CrCasRenterContractBasicTaxRate { get; set; }
        public decimal? CrCasRenterContractBasicUserDiscountRate { get; set; }
        public int? CrCasRenterContractBasicCurrentReadingMeter { get; set; }
        public decimal? CrCasRenterContractBasicExpectedRentValue { get; set; }
        public decimal? CrCasRenterContractBasicExpectedOptionsValue { get; set; }
        public decimal? CrCasRenterContractBasicAdditionalValue { get; set; }
        public decimal? CrCasRenterContractBasicExpectedPrivateDriverValue { get; set; }
        public decimal? CrCasRenterContractBasicExpectedValueBeforDiscount { get; set; }
        public decimal? CrCasRenterContractBasicExpectedDiscountValue { get; set; }
        public decimal? CrCasRenterContractBasicExpectedValueAfterDiscount { get; set; }
        public decimal? CrCasRenterContractBasicExpectedTaxValue { get; set; }
        public decimal? CrCasRenterContractBasicExpectedTotal { get; set; }
        public decimal? CrCasRenterContractBasicPreviousBalance { get; set; }
        public decimal? CrCasRenterContractBasicAmountRequired { get; set; }
        public decimal? CrCasRenterContractBasicAmountPaidAdvance { get; set; }
        public string? CrCasRenterContractBasicArPdfFile { get; set; }
        public string? CrCasRenterContractBasicEnPdfFile { get; set; }
        public string? CrCasRenterContractBasicArTga { get; set; }
        public string? CrCasRenterContractBasicEnTga { get; set; }
        public string? CrCasRenterContractPriceReference { get; set; }
        public string? CrCasRenterContractOffersReference { get; set; }
        public string? CrCasRenterContractUserReference { get; set; }
        public string? CrCasRenterContractBasicUserInsert { get; set; }
        public string? CrCasRenterContractBasicStatus { get; set; }
        public string? CrCasRenterContractBasicReasons { get; set; }

        public virtual CrCasBranchInformation? CrCasRenterContractBasic1 { get; set; }
        public virtual CrCasRenterLessor? CrCasRenterContractBasic2 { get; set; }
        public virtual CrCasRenterPrivateDriverInformation? CrCasRenterContractBasic3 { get; set; }
        public virtual CrCasRenterLessor? CrCasRenterContractBasic4 { get; set; }
        public virtual CrCasCarInformation? CrCasRenterContractBasicCarSerailNoNavigation { get; set; }
        public virtual CrCasRenterLessor? CrCasRenterContractBasicNavigation { get; set; }
        public virtual CrMasSupRenterSector? CrCasRenterContractBasicSectorNavigation { get; set; }
        public virtual CrCasRenterContractAlert CrCasRenterContractAlert { get; set; } = null!;
        public virtual CrCasRenterContractAuthorization CrCasRenterContractAuthorization { get; set; } = null!;
        public virtual CrCasRenterContractStatistic CrCasRenterContractStatistic { get; set; } = null!;
        public virtual ICollection<CrCasRenterContractAdditional> CrCasRenterContractAdditionals { get; set; }
        public virtual ICollection<CrCasRenterContractAdvantage> CrCasRenterContractAdvantages { get; set; }
        public virtual ICollection<CrCasRenterContractCarCheckup> CrCasRenterContractCarCheckups { get; set; }
        public virtual ICollection<CrCasRenterContractChoice> CrCasRenterContractChoices { get; set; }
    }
}
