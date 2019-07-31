using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.Auths.Models;
using ASample.NetCore.Auths.Models.Admins;
using ASample.NetCore.Auths.Models.IdentityUsers;
using ASample.NetCore.Auths.Repositories;
using ASample.NetCore.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASample.NetCore.Auths.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<ASampleUser> _userManager;
        private readonly ITRoleRepository _iTRoleRepository;

        public RoleController(UserManager<ASampleUser> userManager
            , SignInManager<ASampleUser> signInManager
            , ITRoleRepository tRoleRepository)
        {
            _userManager = userManager;
            _iTRoleRepository = tRoleRepository;
        }

        public async Task<ApiRequestResult> QueryAsync()
        {
            var result = await _iTRoleRepository.QueryAsync();
            return ApiRequestResult.Success(result, "");
        }

        [HttpGet("QueryPaged")]
        public async Task<string> QueryPagedAsync([FromQuery] RoleSearchParam param)
        {
            var filter = param.SearchFilter<TRole, RoleSearchParam>();
            var result = await _iTRoleRepository.QueryPagedAsync(param.Page,param.Limit, filter);
            var pageData = new LayuiTableDto<TRole>
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
        public async Task<ApiRequestResult> AddAsync([FromBody]RoleParam param)
        {
            try
            {
                var add = new TRole
                {

                };
                await _iTRoleRepository.AddAsync(add);
                return ApiRequestResult.Success("添加成功");
            }
            catch (Exception ex)
            {
                return ApiRequestResult.Error(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ApiRequestResult> UpdateAsync([FromBody]RoleParam param)
        {
            try
            {
                var update = new TRole
                {
                    
                };

                await _iTRoleRepository.UpdateAsync(update);
                return ApiRequestResult.Success("修改成功");
            }
            catch (Exception ex)
            {
                return ApiRequestResult.Error(ex.Message);
            }
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