using System;
using System.Collections.Generic;

namespace ASample.NetCore.Auths.Models.Admins
{
    public class UpdateUserRoleParam
    {
        public Guid UserId { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
}
