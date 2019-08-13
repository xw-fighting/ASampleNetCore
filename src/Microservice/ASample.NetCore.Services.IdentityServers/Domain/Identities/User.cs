using ASample.NetCore.Domain.AggregateRoots;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;

namespace ASample.NetCore.Services.IdentityServers.Domain
{
    public class User:AggregateRoot
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public string PasswordHash { get; private set; }

        protected User()
        {

        }

        public User(Guid id, string name,string email, string role)
        {
            if (!EmailRegex.IsMatch(email))
            {
                throw new ASampleException(IdentityServerCodes.InvalidEmail,
                    $"Invalid email: '{email}'.");
            }
            if (!Domain.Role.IsValid(role))
            {
                throw new ASampleException(IdentityServerCodes.InvalidRole,
                    $"Invalid role: '{role}'.");
            }
            //if (string.IsNullOrEmpty(name))
            //{
            //    throw new ASampleException(IdentityServerCodes.InvalidRole,
            //       $"Invalid role: '{name}'.");
            //}
            Id = id;
            Name = name.ToLowerInvariant();
            Email = email.ToLowerInvariant();
            Role = role.ToLowerInvariant();
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ASampleException(IdentityServerCodes.InvalidPassword,
                    "Password can not be empty.");
            }
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, PasswordHash, password) != PasswordVerificationResult.Failed;
    }
}
