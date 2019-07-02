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
    public class QueryUsersHandler : IQueryHandler<QueryUsers, IEnumerable<UserInfoDto>>
    {
        private readonly IUserRepository _repository;
        public QueryUsersHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserInfoDto>> HandleAsync(QueryUsers query)
        {
            var userInfos = await _repository.QueryAsync();
            var result = userInfos.Select(c => new UserInfoDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address
            }).ToList();
            return result;
        }
    }
}
