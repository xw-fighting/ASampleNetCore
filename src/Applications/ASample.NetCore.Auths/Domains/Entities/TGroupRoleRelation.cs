using ASample.NetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Auths.Domains
{
    public class TGroupRoleRelation:Entity
    {
        public Guid GroupId { get; set; }
        public Guid RoleId { get; set; }
    }
}
