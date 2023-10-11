using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.Identitiy
{
    public class RegisterViewModel
    {
        [Required]
        public string CrMasUserInformationCode { get; set; } = null!;
        public string? CrMasUserInformationPassWord { get; set; }
        public string? CrMasUserInformationLessor { get; set; }
        public bool? CrMasUserInformationAuthorizationBnan { get; set; }
        public bool? CrMasUserInformationAuthorizationAdmin { get; set; }
        public bool? CrMasUserInformationAuthorizationBranch { get; set; }
        public bool? CrMasUserInformationAuthorizationOwner { get; set; }
        public bool? CrMasUserInformationAuthorizationFoolwUp { get; set; }
        [Required]
        [MinLength(3)]
        public string? CrMasUserInformationArName { get; set; }
        [Required]
        [MinLength(3)]
        public string? CrMasUserInformationEnName { get; set; }
        public decimal? CrMasUserInformationTotalBalance { get; set; }
        public decimal? CrMasUserInformationReservedBalance { get; set; }
        public decimal? CrMasUserInformationAvailableBalance { get; set; }
        public decimal? CrMasUserInformationCreditLimit { get; set; }
        [Required]
        [MinLength(3)]
        public string? CrMasUserInformationTasksArName { get; set; }
        [Required]
        [MinLength(3)]
        public string? CrMasUserInformationTasksEnName { get; set; }
        public string? CrMasUserInformationRemindMe { get; set; }
        public string? CrMasUserInformationDefaultBranch { get; set; }
        public string? CrMasUserInformationDefaultLanguage { get; set; }
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

        public List<CrMasUserMainValidation>? CrMasUserMainValidations { get; set; }
        public List<CrMasSysMainTask>? CrMasSysMainTasks { get; set; }

        public List<CrMasUserSubValidation>? CrMasUserSubValidations { get; set; }
        public List<CrMasSysSubTask>? CrMasSysSubTasks { get; set; }

        public List<CrMasUserProceduresValidation>? ProceduresValidations { get; set; }
        public List<CrMasSysProceduresTask>? CrMasSysProceduresTasks { get; set; }
        public List<CrCasBranchInformation>? CrCasBranchInformations { get; set; }
        public List<CrMasUserBranchValidity>? CrMasUserBranchValidities { get; set; }
        public CrMasUserContractValidity? CrMasUserContractValidity { get; set; } 



    }
}
