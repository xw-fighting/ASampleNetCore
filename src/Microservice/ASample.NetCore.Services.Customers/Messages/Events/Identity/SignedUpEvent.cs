using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events
{
    [MessageNamespace("identity")]
    public class SignedUpEvent:IEvent
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        [JsonConstructor]
        public SignedUpEvent(Guid userId, string email, string role)
        {
            UserId = userId;
            Email = email;
            Role = role;
        }
    }
}
