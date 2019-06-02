using ASample.NetCore.Handlers;
using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.WebApi.Domain;
using ASample.NetCore.WebApi.Dto.Users;
using ASample.NetCore.WebApi.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.WebApi.Handlers.User
{
    public class SelectUserInfoHandler : IQueryHandler<SelectUserInfo, IEnumerable<UserInfoDto>>
    {
        private readonly IMongoRepository<UserInfo> _repository;
        public SelectUserInfoHandler(IMongoRepository<UserInfo> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<UserInfoDto>> HandleAsync(SelectUserInfo query)
        {
            var userInfos = await _repository.SelectAsync(c => true);
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
