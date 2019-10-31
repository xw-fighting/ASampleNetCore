using ASample.NetCore.Domain;

namespace ASample.NetCore.Services.Orders.Domain
{
    public class Customer:AggregateRoot
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
