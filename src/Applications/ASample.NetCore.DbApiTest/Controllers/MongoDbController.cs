using System;
using System.Threading.Tasks;
using ASample.NetCore.MySqlDb;
using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.DbApiTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.DbApiTest.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class MongoDbController : ControllerBase
    {
        private IMongoUserRepository _userRepository;
        public MongoDbController(IMongoUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<User> GetAsync(Guid id)
        {
            try
            {
                var result = await _userRepository.GetAsync(id);
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task AddAsync([FromBody]User user)
        {
            var user1 = user.BindId<User>(c => c.Id);
            await _userRepository.AddAsync(user1);
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] User user)
        {
            var userEntity = await _userRepository.GetAsync(user.Id);
            userEntity.Name = user.Name;
            userEntity.Age = user.Age;
            await _userRepository.UpdateAsync(userEntity);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}