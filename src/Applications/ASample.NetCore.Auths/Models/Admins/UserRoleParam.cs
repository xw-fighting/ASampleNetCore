using ASample.NetCore.Common;
using System;

namespace ASample.NetCore.Auths.Models.Admins
{
    public class UserRoleParam: LayuiPagedParam
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid? UserId { get; set; }
    }
}
