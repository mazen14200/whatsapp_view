using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsUp.Core.Models
{
    public partial class Connect
    {
        public Connect() 
        {
        }

        //public string SourceCompanyId { get; set; } = null!;

        //public string? SourceApiToken { get; set; }
        //public string? SourceDeviceSerial { get; set; }
        //public string? SourceStatus { get; set; }


        public string connectId { get; set; } = null!;
        
        public string? connectSerial { get; set; }
        public string? connectName { get; set; }
        public string? connectMobile { get; set; }
        public string? connectDeviceType { get; set; }
        public string? connectIsBussenis { get; set; }
        public DateTime? connect_Login_Date_time { get; set; }
        public DateTime? connect_LogOut_Date_time { get; set; }
        public string? connectStatus { get; set; }
    }
}
