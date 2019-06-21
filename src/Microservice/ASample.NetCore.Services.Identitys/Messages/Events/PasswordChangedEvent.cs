using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Identitys.Messages.Events
{
    public class PasswordChangedEvent : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public PasswordChangedEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
