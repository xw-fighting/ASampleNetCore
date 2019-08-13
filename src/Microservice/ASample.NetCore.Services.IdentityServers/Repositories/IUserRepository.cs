﻿using ASample.NetCore.Services.IdentityServers.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
