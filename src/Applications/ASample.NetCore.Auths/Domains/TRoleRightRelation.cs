using ASample.NetCore.Auths.Domains.Values;
using ASample.NetCore.Domain.AggregateRoots;
using System;

namespace ASample.NetCore.Auths.Domains
{
    public class TRoleRightRelation:Entity
    {
        public Guid RoleId { get; set; }
        public Guid RightId { get; set; }
        public RightType RightType { get; set; }
    }
}
