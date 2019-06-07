using ASample.NetCore.AuthenticationService.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.AuthenticationService.Services
{
    public interface IIdentityService
    {

        Task SignUp(Guid id,string email,string password,string role = Role.User);

        Task SignIn(string email,string password);
    }
}
