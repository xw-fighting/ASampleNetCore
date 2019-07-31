using ASample.NetCore.Domain.AggregateRoots;
using System;

namespace ASample.NetCore.Auths.Domains
{
    public class TUser:AggregateRoot
    {
        /// <summary>
        /// 所属组织
        /// </summary>
        public Guid OrgId { get; set; }

        /// <summary>
        /// 登录帐号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int Count { get; set; }
    }
}
