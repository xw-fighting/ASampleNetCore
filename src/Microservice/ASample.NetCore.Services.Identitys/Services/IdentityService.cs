using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Services.Identitys.Domain;
using ASample.NetCore.Services.Identitys.Dto;
using ASample.NetCore.Services.Identitys.Messages.Commands;
using ASample.NetCore.Services.Identitys.Repositories;
using ASample.NetCore.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace ASample.NetCore.Services.Identitys.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IClaimsProvider _claimsProvider;
        public IdentityService(IUserRepository userRepository
            ,IPasswordHasher<User> passwordHasher
            , IClaimsProvider claimsProvider

            )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _claimsProvider = claimsProvider;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task SignIn(string email,string password)
        {
            var user = await _userRepository.GetAsync(email);
            if(user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new ASampleException(Codes.InvalidCredentials, "invalid credentials");
            }

            var refreshToken = new RefreshToken(user, _passwordHasher);
            var claims = await _claimsProvider.GetAsync(user.Id);



            throw new NotImplementedException();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task SignUp(Guid id,string email,string password,string role = Role.User)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new ASampleException(Codes.EmailInUse,
                   $"Email: '{email}' is already in use.");
            }

            user = new User(id, email, role);
            user.SetPassword(password, _passwordHasher);

            await _userRepository.AddAsync(user);
        }
    }
}
