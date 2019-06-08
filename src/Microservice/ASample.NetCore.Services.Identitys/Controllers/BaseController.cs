using System;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Services.Identitys.Controllers
{
    [Route("")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected bool IsAdmin
           => User.IsInRole("admin");

        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ?
                Guid.Empty :
                Guid.Parse(User.Identity.Name);
    }
}