using System;

namespace ASample.NetCore.Domain
{
    public class ServiceId
    {
        private static readonly string UniqueId = $"{Guid.NewGuid():N}";

        public string Id => UniqueId;
    }
}
