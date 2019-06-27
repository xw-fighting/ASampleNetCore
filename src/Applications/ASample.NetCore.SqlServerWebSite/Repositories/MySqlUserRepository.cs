﻿using ASample.NetCore.EntityFramwork;
using ASample.NetCore.MySqlDb.Repositories;
using ASample.NetCore.SqlServerWebSite.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.SqlServerWebSite.Repositories
{
    public class MySqlUserRepository:IMySqlUserRepository
    {
        private IMySqlRepository<User> _mySqlRepository;
        private IUnitOfWork _unitOfWork;
        public MySqlUserRepository(IMySqlRepository<User> mySqlRepository, IUnitOfWork unitOfWork)
        {
            _mySqlRepository = mySqlRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(User user)
        {
            await _mySqlRepository.AddAsync(user);
            _unitOfWork.SaveChanges();
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await _mySqlRepository.GetAsync(c => c.Id == id);
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            await _mySqlRepository.UpdateAsync(user);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mySqlRepository.DeleteAsync(id);
            _unitOfWork.SaveChanges();
        }
    }
}