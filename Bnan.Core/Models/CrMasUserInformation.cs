using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasUserInformation : IdentityUser
    {
        public CrMasUserInformation()
        {
            CrCasSysAdministrativeProcedures = new HashSet<CrCasSysAdministrativeProcedure>();
            CrMasUserBranchValidities = new HashSet<CrMasUserBranchValidity>();
            CrMasUserLogins = new HashSet<CrMasUserLogin>();
            CrMasUserMainValidations = new HashSet<CrMasUserMainValidation>();
            CrMasUserMessageCrMasUserMessageUserReceiverNavigations = new HashSet<CrMasUserMessage>();
            CrMasUserMessageCrMasUserMessageUserSenderNavigations = new HashSet<CrMasUserMessage>();
            CrMasUserProceduresValidations = new HashSet<CrMasUserProceduresValidation>();
            CrMasUserSubValidations = new HashSet<CrMasUserSubValidation>();
        }

        public string CrMasUserInformationCode { get; set; } = null!;
        public string? CrMasUserInformationPassWord { get; set; }
        public string? CrMasUserInformationRemindMe { get; set; }
        public string? CrMasUserInformationLessor { get; set; }
        public string? CrMasUserInformationDefaultBranch { get; set; }
        public string? CrMasUserInformationDefaultLanguage { get; set; }
        public bool? CrMasUserInformationAuthorizationBnan { get; set; }
        public bool? CrMasUserInformationAuthorizationAdmin { get; set; }
        public bool? CrMasUserInformationAuthorizationBranch { get; set; }
        public bool? CrMasUserInformationAuthorizationOwner { get; set; }
        public bool? CrMasUserInformationAuthorizationFoolwUp { get; set; }
        public string? CrMasUserInformationArName { get; set; }
        public string? CrMasUserInformationEnName { get; set; }
        public string? CrMasUserInformationTasksArName { get; set; }
        public string? CrMasUserInformationTasksEnName { get; set; }
        public decimal? CrMasUserInformationReservedBalance { get; set; }
        public decimal? CrMasUserInformationTotalBalance { get; set; }
        public decimal? CrMasUserInformationAvailableBalance { get; set; }
        public decimal? CrMasUserInformationCreditLimit { get; set; }
        public string? CrMasUserInformationCallingKey { get; set; }
        public string? CrMasUserInformationMobileNo { get; set; }
        public string? CrMasUserInformationEmail { get; set; }
        public DateTime? CrMasUserInformationChangePassWordLastDate { get; set; }
        public DateTime? CrMasUserInformationEntryLastDate { get; set; }
        public TimeSpan? CrMasUserInformationEntryLastTime { get; set; }
        public DateTime? CrMasUserInformationExitLastDate { get; set; }
        public TimeSpan? CrMasUserInformationExitLastTime { get; set; }
        public int? CrMasUserInformationExitTimer { get; set; }
        public string? CrMasUserInformationPicture { get; set; }
        public string? CrMasUserInformationSignature { get; set; }
        public bool? CrMasUserInformationOperationStatus { get; set; }
        public string? CrMasUserInformationStatus { get; set; }
        public string? CrMasUserInformationReasons { get; set; }
        public virtual CrMasLessorInformation? CrMasUserInformationLessorNavigation { get; set; }
        public virtual CrMasUserContractValidity? CrMasUserContractValidity { get; set; } = null!;
        public virtual ICollection<CrCasSysAdministrativeProcedure> CrCasSysAdministrativeProcedures { get; set; }
        public virtual ICollection<CrMasUserBranchValidity> CrMasUserBranchValidities { get; set; }
        public virtual ICollection<CrMasUserLogin> CrMasUserLogins { get; set; }
        public virtual ICollection<CrMasUserMainValidation> CrMasUserMainValidations { get; set; }
        public virtual ICollection<CrMasUserMessage> CrMasUserMessageCrMasUserMessageUserReceiverNavigations { get; set; }
        public virtual ICollection<CrMasUserMessage> CrMasUserMessageCrMasUserMessageUserSenderNavigations { get; set; }
        public virtual ICollection<CrMasUserProceduresValidation> CrMasUserProceduresValidations { get; set; }
        public virtual ICollection<CrMasUserSubValidation> CrMasUserSubValidations { get; set; }
    }
}
