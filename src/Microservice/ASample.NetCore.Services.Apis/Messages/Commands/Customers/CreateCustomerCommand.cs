using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Apis.Messages.Commands.Customers
{

    [MessageNamespace("customers")]
    public class CreateCustomerCommand:ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        [JsonConstructor]
        public CreateCustomerCommand(Guid id, string email, string firstName, string lastName, string address, string country)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Country = country;
        }
    }
}
