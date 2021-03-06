﻿using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Identitys.Messages.Events
{
    public class AccessTokenRefreshedEvent : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public AccessTokenRefreshedEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
