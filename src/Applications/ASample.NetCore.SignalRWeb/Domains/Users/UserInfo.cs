using ASample.NetCore.Domain;

namespace ASample.NetCore.SignalRWeb.Domains.Users
{
    public class UserInfo:AggregateRoot
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
