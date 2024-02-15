using System.ComponentModel.DataAnnotations;


namespace Bnan.Ui.ViewModels.MAS
{
    public class UserLoginVM
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

    }
}
