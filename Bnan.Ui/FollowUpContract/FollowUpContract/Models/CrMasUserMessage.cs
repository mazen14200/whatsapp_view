using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrMasUserMessage
    {
        public int CrMasUserMessageNo { get; set; }
        public string? CrMasUserMessageLessor { get; set; }
        public string? CrMasUserMessageUserSender { get; set; }
        public string? CrMasUserMessageUserReceiver { get; set; }
        public DateTime? CrMasUserMessageDateWasSent { get; set; }
        public TimeSpan? CrMasUserMessageTimeWasSent { get; set; }
        public DateTime? CrMasUserMessageDateWasReceived { get; set; }
        public TimeSpan? CrMasUserMessageTimeWasReceived { get; set; }
        public string? CrMasUserMessageContent { get; set; }
        public string? CrMasUserMessageStatus { get; set; }

        public virtual CrMasUserInformation? CrMasUserMessageUserReceiverNavigation { get; set; }
        public virtual CrMasUserInformation? CrMasUserMessageUserSenderNavigation { get; set; }
    }
}
