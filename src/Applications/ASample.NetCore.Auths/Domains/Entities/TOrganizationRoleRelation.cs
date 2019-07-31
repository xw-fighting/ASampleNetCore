using ASample.NetCore.Domain.AggregateRoots;
using System;

namespace ASample.NetCore.Auths.Domains
{
    public class TOrganizationRoleRelation:Entity
    {
        public Guid OrgId { get; set; }
        public Guid RoleId { get; set; }
    }
}
