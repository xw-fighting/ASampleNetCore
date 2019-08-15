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
        private readonly IUnitOfWork<ASampleSqlServerDbContext> _unitOfWork;

        public SqlServerController(ISqlServerUserRepository userRepository, IUnitOfWork<ASampleSqlServerDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<User> GetAsync(Guid id)
        {
            var result = await _userRepository.GetAsync(id);
            return result;
        }

        [HttpGet("query")]
        public async Task<List<User>> QueryAsync()
        {
            var result = await _userRepository.QueryAsync();
            return result.ToList();
        }

        [HttpPost]
        public async Task AddAsync([FromBody]User user)
        {
            await _userRepository.AddAsync(user);
            _unitOfWork.SaveChanges();
        }
    }
}