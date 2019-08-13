using ASample.NetCore.Services.IdentityServers.Domain;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.IdentityServers.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string role = Role.User);
        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}
