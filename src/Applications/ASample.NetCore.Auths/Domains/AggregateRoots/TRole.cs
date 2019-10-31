using ASample.NetCore.Domain;
using System;

namespace ASample.NetCore.Auths.Domains
{
    public class TRole:AggregateRoot
    {
        /// <summary>
        /// 父级角色ID
        /// </summary>
        public Guid ?ParentId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
    }
}
