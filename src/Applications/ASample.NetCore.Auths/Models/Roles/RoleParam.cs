using System;

namespace ASample.NetCore.Auths.Models
{
    public class RoleParam
    {
        public Guid? Id { get; set; }
        public string  RoleName { get; set; }
        public Guid?  ParentId { get; set; }
        public string  Description { get; set; }
    }
}
