using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.Auths.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Auths.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ASampleUser> _userManager;
        private readonly SignInManager<ASampleUser> _signInManager;

        public AccountController(UserManager<ASampleUser> userManager, SignInManager<ASampleUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInParam param)
        {
            var asampleUser = new ASampleUser
            {
                UserName = param.UserName,
                
            };
            var signInResult = await _signInManager.PasswordSignInAsync(param.UserName, param.Password, true, false);
            if (signInResult.Succeeded)
            {
                // If we have no return URL...
                if (string.IsNullOrEmpty(param.ReturnUrl))
                    // Go to home
                    return Content($"{HttpContext.User} is sign in");
                return RedirectToAction(param.ReturnUrl);

                // Otherwise, go to the return url
            }
            return Content($"user signed in failed");
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody]SignUpParam param)
        {
            var asampleUser = new ASampleUser
            {
                UserName = param.UserName,
                Email = param.Email,
                PhoneNumber = param.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(asampleUser, param.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("/Home/SignIn");
            }
            return Redirect("/sign-in.html");
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Content($"{HttpContext.User} is sign out");
        }
    }
}