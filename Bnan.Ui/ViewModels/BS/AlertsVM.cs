namespace Bnan.Ui.ViewModels.BS
{
    public class AlertsVM
    {
        public int BranchDocumentsAboutExpire { get; set; }
        public int BranchDocumentsExpireAndRenewed{ get; set; }
        public int DocumentsCarsAboutExpire { get; set; }
        public int DocumentsCarExpiredAndRenewed { get; set; }
        public int MaintainceCarAboutExpire { get; set; }
        public int MaintainceCarExpireAndRenewed { get; set; }
        public int PriceCarAboutExpire { get; set; }
        public int PriceCarExpireAndRenewed { get; set; }
        public int AlertOrNot { get; set; }
    }
}
