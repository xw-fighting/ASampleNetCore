using System;
using System.Collections.Generic;

namespace ASample.NetCore.MvcWeb.Models.SignalRs
{
    public class VoteParam
    {
        public int Id { get; set; }
        public string VoteName { get; set; }
        public string Creator { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Dictionary<string, int> VoteSelect { get; set; }
    }
}
