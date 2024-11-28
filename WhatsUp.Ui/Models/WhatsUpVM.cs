using Bnan.Core.Models;
using WhatsUp.Core.Models;

namespace WhatsUp3.Ui.Models
{
    public class WhatsUpVM
    {

        public string? Account_ApiToken { get; set; }
        public string ImageForLogo { get; set; } = "";
        public string ImageForSend { get; set; } = "";
        public string FileForSend { get; set; } = "";

        public Connect Single_connect { get; set; } = new Connect();
        public string Single_connect_id { get; set; } = "00";

        public Renter single_Renter { get; set; } = new Renter();
        public Message single_Message { get; set; } = new Message();
        public List<Company> Company_List { get; set; } = new List<Company>();
        public List<Connect> Connect_List { get; set; } = new List<Connect>();
        public List<Renter> Renter_List { get; set; } = new List<Renter>();
        public List<Message> Message_List { get; set; } = new List<Message>();
        public List<CrMasSupPostRegion_x> postRegion_updated { get; set; } = new List<CrMasSupPostRegion_x>();
        public List<CrMasSupPostRegion> postRegion_old { get; set; } = new List<CrMasSupPostRegion>();



    }
}
