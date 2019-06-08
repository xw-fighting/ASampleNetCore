using ASample.NetCore.Services.Identitys.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Identitys.Services
{
    public interface IIdentityService
    {

        Task SignUp(Guid id,string email,string password,string role = Role.User);

        Task SignIn(string email,string password);
    }
}
