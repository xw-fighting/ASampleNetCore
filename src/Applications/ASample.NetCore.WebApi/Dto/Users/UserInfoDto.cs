using System;

namespace ASample.NetCore.WebApi.Dto.Users
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
    }
}
