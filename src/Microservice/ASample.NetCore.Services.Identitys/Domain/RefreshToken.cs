using ASample.NetCore.AuthenticationService.Dto;
using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace ASample.NetCore.AuthenticationService.Domain
{
    public class RefreshToken:AggregateRoot
    {
        //public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime? RevokedTime { get; private set; }
        public bool Revoked => RevokedTime.HasValue;

        protected RefreshToken()
        {
        }

        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            Token = CreateToken(user, passwordHasher);
        }

        public void Revoke()
        {
            if (Revoked)
            {
                throw new ASampleException(Codes.RefreshTokenAlreadyRevoked,
                    $"Refresh token: '{Id}' was already revoked at '{RevokedTime}'.");
            }
            RevokedTime = DateTime.Now;
        }

        private static string CreateToken(User user, IPasswordHasher<User> passwordHasher)
        {
            var token = passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
            return token;
        }
    }
}
