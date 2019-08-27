using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Services.IdentityServers.Domain;
using ASample.NetCore.Services.IdentityServers.Repositories;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ASample.NetCore.Services.IdentityServers.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IClientStore _clientStore;

        public IdentityService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IClientStore clientStore)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _clientStore = clientStore;
        }

        public Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            //throw new NotImplementedException();
            var user = await _userRepository.GetAsync(c => c.Email == email.ToLowerInvariant());
            if (user == null && !user.ValidatePassword(password, _passwordHasher))
            {
                //sign in failed
                throw new ASampleException(IdentityServerCodes.InvalidCredentials,
                   "Invalid credentials.");
            }


            var refreshToken = new RefreshToken(user, _passwordHasher);
            //var claims = await _claimsProvider.GetAsync(user.Id);
            //var jwt = _jwtHandler.CreateToken(user.Id.ToString("N"), user.Role, claims);
            //jwt.RefreshToken = refreshToken.Token;
            //await _refreshTokenRepository.AddAsync(refreshToken);

            //return jwt;
            return new JsonWebToken("");

        }

        public Task SignUpAsync(Guid id, string email, string password, string role = "user")
        {
            throw new NotImplementedException();
        }
    }
}
