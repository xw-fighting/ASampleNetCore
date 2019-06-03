using ASample.NetCore.AuthenticationService.Domain;
using System.Threading.Tasks;

namespace ASample.NetCore.AuthenticationService.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
