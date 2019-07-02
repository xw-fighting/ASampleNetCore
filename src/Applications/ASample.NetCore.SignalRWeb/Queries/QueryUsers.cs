using ASample.NetCore.Domain;
using ASample.NetCore.SignalRWeb.Dtos;
using System;
using System.Collections.Generic;

namespace ASample.NetCore.SignalRWeb.Queries
{
    public class QueryUsers : IQuery<IEnumerable<UserInfoDto>>
    {
        public Guid Id { get; set; }
        public string ConnectUserId { get; set; }
    }
}
