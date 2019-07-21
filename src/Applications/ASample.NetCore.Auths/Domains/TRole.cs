using ASample.NetCore.Domain.AggregateRoots;

namespace ASample.NetCore.Auths.Domains
{
    public class TRole:AggregateRoot
    {
        /// <summary>
        /// 父级角色ID
        /// </summary>
        public string ParentId { get; set; }

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
