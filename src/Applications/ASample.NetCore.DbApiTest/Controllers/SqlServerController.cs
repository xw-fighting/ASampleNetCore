using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.DbApiTest.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.DbApiTest.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class SqlServerController : ControllerBase
    {
        private ISqlServerUserRepository _userRepository;
        public SqlServerController(ISqlServerUserRepository userRepository)
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
            await _userRepository.AddAsync(user);
        }
    }
}