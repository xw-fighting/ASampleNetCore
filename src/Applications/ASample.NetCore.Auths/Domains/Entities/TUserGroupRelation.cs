using ASample.NetCore.Domain;
using System;

namespace ASample.NetCore.Auths.Domains
{
    public class TUserGroupRelation:Entity
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }
}
