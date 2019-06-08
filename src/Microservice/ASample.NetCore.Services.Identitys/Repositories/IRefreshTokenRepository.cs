using ASample.NetCore.Services.Identitys.Domain;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Identitys.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
