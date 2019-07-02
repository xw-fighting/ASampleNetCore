using ASample.NetCore.Handlers;
using ASample.NetCore.SignalRWeb.Domains.Users;
using ASample.NetCore.SignalRWeb.Dtos;
using ASample.NetCore.SignalRWeb.Queries;
using ASample.NetCore.SignalRWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.SignalRWeb.Handlers.Users
{
    public class GetUserHandler : IQueryHandler<GetUser, UserInfoDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserInfoDto> HandleAsync(GetUser query)
        {
            var result = await _userRepository.GetAsync(query.Id);
            var userInfo = new UserInfoDto
            {
                Id = result.Id,
                Name = result.Name,
                Address = result.Address
            };
            return userInfo;
        }
    }
}
