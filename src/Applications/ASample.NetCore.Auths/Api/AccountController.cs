using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.Auths.Models;
using ASample.NetCore.Auths.Models.Account;
using ASample.NetCore.Auths.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASample.NetCore.Auths.Api
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<ASampleUser> _userManager;
        private readonly SignInManager<ASampleUser> _signInManager;
        private readonly ITUserRepository _userRepository;
        private readonly ILogger<AccountController> _log;

        
        public AccountController(UserManager<ASampleUser> userManager
            , SignInManager<ASampleUser> signInManager
            , ITUserRepository userRepository
            , ILogger<AccountController> log
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _log = log;
        }

        //[Authorize]
        [HttpPost("sign-in")]
        public async Task<ApiRequestResult> SignIn([FromBody] SignInParam param)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(param.UserName, param.Password, true, false);
            if (signInResult.Succeeded)
            {
                //var test = HttpContext.Current.User.Identity.GetUserId();
                var userInfo = await _userManager.FindByNameAsync(param.UserName);
                HttpContext.User = await _signInManager.CreateUserPrincipalAsync(userInfo);
                _log.LogInformation("登录成功");
                // If we have no return URL...
                return ApiRequestResult.Success(param.UserName,"登录成功");
                // Otherwise, go to the return url
            }
            _log.LogError("登录失败");
            return ApiRequestResult.Error("登录失败");
           
        }


        [HttpPost("sign-up")]
        public async Task<ApiRequestResult> SignUp([FromBody]SignUpParam param)
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
                return ApiRequestResult.Success("注册成功");
            }
            return ApiRequestResult.Error(result.Errors.FirstOrDefault().Description);
        }

        [HttpPost("sign-out")]
        public async Task<ApiRequestResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return ApiRequestResult.Success("退出登录成功");
        }

        [HttpPost("profile")]
        public async Task<ApiRequestResult> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return ApiRequestResult.Error("未登录");
            return ApiRequestResult.Success(user.UserName, "获取用户信息成功");
        }


        [HttpPost("password")]
        public async Task<ApiRequestResult> UpdatePassword(UpdatePasswordModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return ApiRequestResult.Error("未登录");
            var checkResult = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            if (!checkResult)
            {
                return ApiRequestResult.Error("原密码输入错误");
            }

            var tUser = await _userRepository.GetAsync(c => c.Id == Guid.Parse(user.Id));
            if(tUser == null)
                return ApiRequestResult.Error("获取用户信息失败");

            //修改密码
            var passwordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
            user.PasswordHash = passwordHash;

            tUser.Password = passwordHash;
            await _userRepository.UpdateAsync(tUser);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return ApiRequestResult.Success("修改成功");
            }
            var err = result.Errors.FirstOrDefault();
            return ApiRequestResult.Error(err.Code+":"+ err.Description);
        }
    }
}