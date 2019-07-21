
using System;

namespace ASample.NetCore.Auths.Models.IdentityUsers
{
    public class UserParam
    {
        public Guid Id { get; set; }
        public Guid OrgId { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

    }
}
