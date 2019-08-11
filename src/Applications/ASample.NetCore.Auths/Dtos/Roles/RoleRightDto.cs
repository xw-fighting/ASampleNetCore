using System;

namespace ASample.NetCore.Auths.Dtos.Roles
{
    public class RoleRightDto
    {
        public Guid Id { get; set; }

        public string RightName { get; set; }

        public bool IsAdd { get; set; }
    }
}
