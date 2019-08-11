using ASample.NetCore.Common;
using System;

namespace ASample.NetCore.Auths.Models.Roles
{
    public class RoleRightParam:LayuiPagedParam
    {
        public Guid? RoleId { get; set; }
    }
}
