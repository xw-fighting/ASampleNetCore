using System;

namespace ASample.NetCore.Auths.Models.Users
{
    public class UserRoleDto
    {
        public Guid Id { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public bool IsAdd { get; set; }
    }
}
