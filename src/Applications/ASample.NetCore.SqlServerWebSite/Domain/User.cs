using ASample.NetCore.Domain.AggregateRoots;

namespace ASample.NetCore.SqlServerWebSite.Domain
{
    public class User:AggregateRoot
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
