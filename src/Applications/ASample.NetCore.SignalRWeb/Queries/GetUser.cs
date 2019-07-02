using ASample.NetCore.Domain;
using ASample.NetCore.SignalRWeb.Dtos;
using System;

namespace ASample.NetCore.SignalRWeb.Queries
{
    public class GetUser:IQuery<UserInfoDto>
    {
        public Guid Id { get; set; }
        public string ConnectUserId { get; set; }
    }
}
