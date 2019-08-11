using System;
using System.Collections.Generic;

namespace ASample.NetCore.Auths.Models.Roles
{
    public class UpdateRoleRightParam
    {
        public Guid RoleId { get; set; }
        public List<Guid> RightIds { get; set; }
    }
}
