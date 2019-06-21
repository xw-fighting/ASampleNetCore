using System.Threading.Tasks;
using ASample.NetCore.Authentications;
using ASample.NetCore.Mvc;
using ASample.NetCore.Services.Identitys.Messages.Commands;
using ASample.NetCore.Services.Identitys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Services.Identitys.Controllers
{
    [Route("")]
    [ApiController]
    public class TokensController : BaseController
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public TokensController(IAccessTokenService accessTokenService,
            IRefreshTokenService refreshTokenService)
        {
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("access-tokens/{refreshToken}/refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAccessToken(string refreshToken, RefreshAccessTokenCommand command)
            => Ok(await _refreshTokenService.CreateAccessTokenAsync(command.Bind(c => c.Token, refreshToken).Token));

        [HttpPost("access-tokens/revoke")]
        public async Task<IActionResult> RevokeAccessToken(RevokeAccessTokenCommand command)
        {
            await _accessTokenService.DeactivateCurrentAsync(
                command.Bind(c => c.UserId, UserId).UserId.ToString("N"));

            return NoContent();
        }

        [HttpPost("refresh-tokens/{refreshToken}/revoke")]
        public async Task<IActionResult> RevokeRefreshToken(string refreshToken, RevokeRefreshTokenCommand command)
        {
            await _refreshTokenService.RevokeAsync(command.Bind(c => c.Token, refreshToken).Token,
                command.Bind(c => c.UserId, UserId).UserId);

            return NoContent();
        }
    }
}