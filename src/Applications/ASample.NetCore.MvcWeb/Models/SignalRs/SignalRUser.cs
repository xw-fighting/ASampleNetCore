using System;

namespace ASample.NetCore.MvcWeb.Models.SignalRs
{
    public class SignalRUser
    {
        public string ConnectID { get; set; }
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public DateTime OnlineTime { get; set; }
    }
}
