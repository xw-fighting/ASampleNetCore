using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Auths.DbConexts;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.Auths.Models.Roles;
using ASample.NetCore.Auths.Models;
using ASample.NetCore.Auths.Repositories;
using ASample.NetCore.Common;
using ASample.NetCore.EntityFramwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ASample.NetCore.Auths.Dtos.Roles;

namespace ASample.NetCore.Auths.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<ASampleUser> _userManager;
        private readonly ITRoleRepository _iTRoleRepository;
        private readonly ITRightRepository _iTRightRepository;

        public RoleController(
            UserManager<ASampleUser> userManager
            , ITRoleRepository tRoleRepository
            , ITRightRepository tRightRepository
            )
        {
            _userManager = userManager;
            _iTRoleRepository = tRoleRepository;
            _iTRightRepository = tRightRepository;
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
                    ParentId = param.ParentId,
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
                var tRole = await _iTRoleRepository.GetAsync(c => c.Id == param.Id);
                tRole = param.UpdateEntity<TRole, RoleParam>(tRole);
                await _iTRoleRepository.UpdateAsync(tRole);
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

        [HttpGet("RoleRight")]
        public async Task<string> GetRoleRightsAsync([FromQuery] RoleRightParam param)
        {
            var rights = await _iTRightRepository.QueryAsync();
            var roleRight = new List<TRoleRightRelation>();
            if(param.RoleId != null)
                roleRight = await _iTRoleRepository.GetRoleRightsAsync(param.RoleId.Value);
            var roleRightDtos = new List<RoleRightDto>();
            foreach (var item in rights)
            {
                var roleRightDto = new RoleRightDto
                {
                    Id = item.Id,
                    RightName = item.RightName,
                    IsAdd = false
                };

                if (roleRight.Exists(c => c.RightId == item.Id))
                    roleRightDto.IsAdd = true;
                roleRightDtos.Add(roleRightDto);
            }

            var pageData = new LayuiTableDto<RoleRightDto>
            {
                Code = "0",
                Msg = "获取数据成功",
                Count = roleRightDtos.Count,
                Data = roleRightDtos
            };
            var json = JsonConvert.SerializeObject(pageData);
            return json;
        }

        [HttpPost("RoleRight")]
        public async Task<ApiRequestResult> UpdateRoleRightAsync([FromBody] UpdateRoleRightParam param)
        {
            var deleteResult = await _iTRoleRepository.DeleteRoleRightAsync(param.RoleId);
            if (!deleteResult)
                return ApiRequestResult.Error("更新用户权限失败");
            var updateResult = await _iTRoleRepository.UpdateRoleRightAsync(param.RoleId, param.RightIds);
            if(!updateResult)
                return ApiRequestResult.Error("更新用户权限失败");
            else
                return ApiRequestResult.Success("更新用户权限成功");
        }
    }
}