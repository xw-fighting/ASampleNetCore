using System;
using System.Threading.Tasks;
using ASample.NetCore.MySqlDb;
using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.DbApiTest.Dtos;
using ASample.NetCore.DbApiTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.DbApiTest.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class PostgreController : ControllerBase
    {
        private IPostgreUserRepository _userRepository;
        public PostgreController(IPostgreUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取用户信息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<User> GetAsync(Guid id)
        {
            var result = await _userRepository.GetAsync(id);
            return result;
        }

        [HttpPost]
        public async Task AddAsync([FromBody]User user)
        {
            var user1 = user.BindId<User>(c => c.Id);
            await _userRepository.AddAsync(user);
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] UserUpdateDto userUpdate)
        {
            try
            {

                var user = await _userRepository.GetAsync(userUpdate.Id);
                user.Name = userUpdate.Name;
                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}