using System;

namespace ASample.NetCore.Services.Apis.Dtos.Customers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Completed { get; set; }

    }
}
