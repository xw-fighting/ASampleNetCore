using ASample.NetCore.Authentications;
using ASample.NetCore.Services.Identitys.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Identitys.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string role = Role.User);
        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);

    }
}
