using ASample.NetCore.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Auths.Domains
{
    public class TRight:AggregateRoot
    {
        /// <summary>
        /// 父权限
        /// </summary>
        public string ParentTd { get; set; }

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
