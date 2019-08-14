﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Services.IdentityServers.Domain;
using ASample.NetCore.Services.IdentityServers.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ASample.NetCore.Services.IdentityServers.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            throw new NotImplementedException();
            var user =await _userRepository.GetAsync(email);
            if(user == null && !user.ValidatePassword(password, _passwordHasher))
            {
                //sign in failed
                throw new ASampleException(IdentityServerCodes.InvalidCredentials,
                   "Invalid credentials.");
            }

            var refreshToken = new RefreshToken(user, _passwordHasher);

        }

        public Task SignUpAsync(Guid id, string email, string password, string role = "user")
        {
            throw new NotImplementedException();
        }
    }
}