using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.Auths.Models.IdentityUsers;
using ASample.NetCore.Auths.Repositories;
using ASample.NetCore.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASample.NetCore.Auths.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ASampleUser> _userManager;
        private readonly SignInManager<ASampleUser> _signInManager;
        private readonly ITUserRepository _iTUserRepository;

        public UserController(UserManager<ASampleUser> userManager
            , SignInManager<ASampleUser> signInManager
            , ITUserRepository tUserRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _iTUserRepository = tUserRepository;
        }

        public async Task<ApiRequestResult> QueryAsync()
        {
            var result = await _iTUserRepository.QueryAsync();
            return ApiRequestResult.Success(result, "");
        }

        [HttpGet("QueryPaged")]
        public async Task<string> QueryPagedAsync([FromQuery] LayuiPagedParam param)
        {
            //var total = _userManager.Users.Count();
            var result = await _iTUserRepository.QueryPagedAsync(param.Page,param.Limit,c=>c.CreateTime,c=>true);
            var pageData = new LayuiTableDto<TUser>
            {
                Code = "0",
                Msg = "获取数据成功",
                Count = result.TotalResults,
                Data = result.Items.ToList()
            };
            var json = JsonConvert.SerializeObject(pageData);
            return json;
        }

        [HttpPost]
        public async Task<ApiRequestResult> AddAsync([FromBody]UserParam param)
        {
            var user = new TUser
            {
                UserName = param.UserName,
                Email = param.Email,
                PhoneNumber = param.PhoneNumber,
                Password = param.Password,
                OrgId = param.OrgId,
                LoginName = param.LoginName,
            };
            await _iTUserRepository.AddAsync(user);

            var asampleUser = new ASampleUser
            {
                Id = user.Id.ToString(),
                UserName = param.UserName,
                Email = param.Email,
                PhoneNumber = param.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(asampleUser, param.Password);
            if (result.Succeeded)
            {
                return ApiRequestResult.Success("添加成功");
            }
            return ApiRequestResult.Error(result.Errors.FirstOrDefault().Description);
        }

        [HttpPut]
        public async Task<ApiRequestResult> UpdateAsync([FromBody]UserParam param)
        {
            var user = new TUser
            {
                UserName = param.UserName,
                Email = param.Email,
                PhoneNumber = param.PhoneNumber,
                Password = param.Password,
                OrgId = param.OrgId,
                LoginName = param.LoginName,
            };

            await _iTUserRepository.UpdateAsync(user);

            var asampleUser = new ASampleUser
            {
                UserName = param.UserName,
                Email = param.Email,
                PhoneNumber = param.PhoneNumber,
            };
            var result = await _userManager.UpdateAsync(asampleUser);
            if (result.Succeeded)
            {
                return ApiRequestResult.Success("修改成功");
            }
            return ApiRequestResult.Error(result.Errors.FirstOrDefault().Description);
        }

        [HttpDelete]
        public async Task<ApiRequestResult> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return ApiRequestResult.Success("删除成功");
            }
            return ApiRequestResult.Error("删除失败");
        }
    }
}