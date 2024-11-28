using WhatsUp.Core.Models;

namespace WhatsUp3.Ui.Models
{
    public class MessageVM
    {

        //public string message_id { get; set; } = "";
        //public string source_id { get; set; } = "";
        //public string? status_No { get; set; }
        //public string benefactor_id { get; set; } = "";

        public string destination { get; set; } = "";
        public string message { get; set; } = "";
        public string message_id { get; set; } = "";
        public string receipt { get; set; } = "";
        public string receipt_t { get; set; } = "";
        public string response { get; set; } = "";
        public string source { get; set; } = "";
        public string status { get; set; } = "";
        public bool t { get; set; }
        public string type { get; set; } = "";

    }
}
