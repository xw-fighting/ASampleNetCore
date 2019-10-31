using ASample.NetCore.Domain;
using System;

namespace ASample.NetCore.Auths.Domains
{
    public class TOrganization:AggregateRoot
    {
        public Guid? ParentId { get; set; }
        public string OrgName { get; set; }
        public string Description { get; set; }
    }
}
