using ASample.NetCore.Domain.AggregateRoots;

namespace ASample.NetCore.RelationalDb.Test.Model
{
    public class User:AggregateRoot
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
