using ASample.NetCore.Authentication.Dto;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Identitys.Services
{
    public interface IRefreshTokenService
    {
        Task AddAsync(Guid userId);
        Task<JsonWebToken> CreateAccessTokenAsync(string refreshToken);
        Task RevokeAsync(string refreshToken, Guid userId);
    }
}
