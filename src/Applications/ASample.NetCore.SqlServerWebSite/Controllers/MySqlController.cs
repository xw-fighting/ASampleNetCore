using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.MySqlDb;
using ASample.NetCore.SqlServerWebSite.Domain;
using ASample.NetCore.SqlServerWebSite.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.SqlServerWebSite.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class MySqlController : ControllerBase
    {
        private IMySqlUserRepository _userRepository;
        public MySqlController(IMySqlUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
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
            await _userRepository.AddAsync(user1);
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] User use)
        {
            await _userRepository.UpdateAsync(use);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}