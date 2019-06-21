using System.Threading.Tasks;
using ASample.NetCore.Authentications.Attributes;
using ASample.NetCore.Mvc;
using ASample.NetCore.Services.Identitys.Messages.Commands;
using ASample.NetCore.Services.Identitys.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Services.Identitys.Controllers
{
    [Route("")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenService _refreshTokenService;

        public IdentityController(IIdentityService identityService,
            IRefreshTokenService refreshTokenService)
        {
            _identityService = identityService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpGet("me")]
        [JwtAuth]
        public IActionResult Get() => Content($"Your id: '{UserId:N}'.");

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpCommand command)
        {
            command.BindId(c => c.Id);
            await _identityService.SignUpAsync(command.Id,
                command.Email, command.Password, command.Role);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInCommand command)
            => Ok(await _identityService.SignInAsync(command.Email, command.Password));

        [HttpPut("me/password")]
        [JwtAuth]
        public async Task<ActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await _identityService.ChangePasswordAsync(command.Bind(c => c.UserId, UserId).UserId,
                command.CurrentPassword, command.NewPassword);

            return NoContent();
        }
    }
}