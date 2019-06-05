using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.SqlServerWebSite.Domain;
using ASample.NetCore.SqlServerWebSite.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.SqlServerWebSite.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class SqlServerController : ControllerBase
    {
        private IUserRepository _userRepository;
        public SqlServerController(IUserRepository userRepository)
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