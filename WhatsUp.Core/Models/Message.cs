using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsUp.Core.Models
{
    public partial class Message
    {
        public Message() 
        {
        }

        public string MessageId { get; set; } = null!;

        public string? MessageConnectId { get; set; }

        public string? MessageRenterId { get; set; }
        public DateTime? Message_Sent_DateTime { get; set; }
        public DateTime? Message_Received_DateTime { get; set; }
        public DateTime? Message_Read_DateTime { get; set; }
        public DateTime? Message_Deleted_DateTime { get; set; }

        public string? MessageType { get; set; }

        public string? MessagePhoneNumberFull { get; set; }
        public string? MessageText { get; set; }
        public string? MessageStatus { get; set; }

    }
}
