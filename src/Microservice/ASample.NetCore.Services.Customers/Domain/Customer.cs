using ASample.NetCore.Domain.AggregateRoots;
using System;

namespace ASample.NetCore.Services.Customers.Domain
{
    public class Customer:AggregateRoot
    {
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string Country { get; private set; }
        public bool Completed => CompletedTime.HasValue;
        public DateTime? CompletedTime { get; private set; }

        protected Customer()
        {
        }

        public Customer(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        public void Complete(string firstName, string lastName,
            string address, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Country = country;
            CompletedTime = DateTime.Now;
        }
    }
}
