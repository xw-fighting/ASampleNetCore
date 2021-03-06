﻿using System;
using System.Threading.Tasks;
using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.DbApiTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.Mvc;

namespace ASample.NetCore.DbApiTest.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class MySqlController : ControllerBase
    {
        private IMySqlUserRepository _userRepository;
        private readonly IUnitOfWork<ASampleMySqlDbContext> _unitOfWork;
        public MySqlController(IMySqlUserRepository userRepository, IUnitOfWork<ASampleMySqlDbContext> unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
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
            _unitOfWork.SaveChanges();
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] User use)
        {
            await _userRepository.UpdateAsync(use);
            _unitOfWork.SaveChanges();
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
            _unitOfWork.SaveChanges();
        }
    }
}