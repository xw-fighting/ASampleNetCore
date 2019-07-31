using ASample.NetCore.Domain.AggregateRoots;
using System;

namespace ASample.NetCore.Auths.Domains
{
    public class TRight:AggregateRoot
    {
        /// <summary>
        /// 父权限
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string RightName { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; set; }
    }
}
