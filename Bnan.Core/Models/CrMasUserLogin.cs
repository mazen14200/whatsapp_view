using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasUserLogin
    {
        public int CrMasUserLoginNo { get; set; }
        public DateTime? CrMasUserLoginEntryDate { get; set; }
        public TimeSpan? CrMasUserLoginEntryTime { get; set; }
        public string? CrMasUserLoginUser { get; set; }
        public string? CrMasUserLoginLessor { get; set; }
        public string? CrMasUserLoginBranch { get; set; }
        public string? CrMasUserLoginSystem { get; set; }
        public string? CrMasUserLoginMainTask { get; set; }
        public string? CrMasUserLoginSubTask { get; set; }
        public string? CrMasUserLoginArOperation { get; set; }
        public string? CrMasUserLoginEnOperation { get; set; }
        public string? CrMasUserLoginSubComputerType { get; set; }
        public string? CrMasUserLoginSubComputerCode { get; set; }
        public string? CrMasUserLoginConcatenateOperationArDescription { get; set; }
        public string? CrMasUserLoginConcatenateOperationEnDescription { get; set; }

        public virtual CrMasLessorInformation? CrMasUserLoginLessorNavigation { get; set; }
        public virtual CrMasSysMainTask? CrMasUserLoginMainTaskNavigation { get; set; }
        public virtual CrMasSysSubTask? CrMasUserLoginSubTaskNavigation { get; set; }
        public virtual CrMasSysSystem? CrMasUserLoginSystemNavigation { get; set; }
        public virtual CrMasUserInformation? CrMasUserLoginUserNavigation { get; set; }
    }
}
