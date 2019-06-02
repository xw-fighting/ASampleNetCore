using ASample.NetCore.Domain;
using ASample.NetCore.WebApi.Dto.Users;
using System;
using System.Collections.Generic;

namespace ASample.NetCore.WebApi.Queries
{
    public class SelectUserInfo:IQuery<IEnumerable<UserInfoDto>>
    {
        public Guid Id { get; set; }
    }
}
