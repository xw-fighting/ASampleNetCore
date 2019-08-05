using System;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Auths.DbConexts;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.Auths.Models;
using ASample.NetCore.Auths.Repositories;
using ASample.NetCore.Common;
using ASample.NetCore.EntityFramwork;
using Microsoft.AspNetCore.Authorization;
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

        public RoleController(
            UserManager<ASampleUser> userManager
            , ITRoleRepository tRoleRepository
            )
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
                    RoleName = param.RoleName,
                    ParentId = (param.ParentId != null ? param.ParentId.Value : param.ParentId),
                    Description = param.Description,
                };
                await _iTRoleRepository.AddAsync(add);
                //_unitOfWork.SaveChanges();
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
                    Id = param.Id.Value,
                    RoleName = param.RoleName,
                    ParentId = (param.ParentId != null? param.ParentId.Value: param.ParentId),
                    Description = param.Description,
                };

                await _iTRoleRepository.UpdateAsync(update);
                //_unitOfWork.SaveChanges();
                return ApiRequestResult.Success("修改成功");
            }
            catch (Exception ex)
            {
                return ApiRequestResult.Error(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ApiRequestResult> DeleteAsync(Guid id)
        {
            await _iTRoleRepository.DeleteAsync(id);
            //_unitOfWork.SaveChanges();
            return ApiRequestResult.Success("删除成功");
        }
    }
}