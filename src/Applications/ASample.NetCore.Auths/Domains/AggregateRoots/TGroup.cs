using ASample.NetCore.Domain;
using System;

namespace ASample.NetCore.Auths.Domains
{
    public class TGroup:AggregateRoot
    {
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 父组
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 组描述
        /// </summary>
        public string Description { get; set; }
    }
}
