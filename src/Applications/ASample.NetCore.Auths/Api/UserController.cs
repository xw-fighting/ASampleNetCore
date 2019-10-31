using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.Auths.Models.Rights;
using ASample.NetCore.Auths.Models.Users;
using ASample.NetCore.Auths.Models.Admins;
using ASample.NetCore.Auths.Models.IdentityUsers;
using ASample.NetCore.Auths.Repositories;
using ASample.NetCore.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ASample.NetCore.Extension;

namespace ASample.NetCore.Auths.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ASampleUser> _userManager;
        private readonly SignInManager<ASampleUser> _signInManager;
        private readonly ITUserRepository _iTUserRepository;
        private readonly ITRoleRepository _iTRoleRepository;
        private readonly ITRightRepository _iTRightRepository;

        public UserController(UserManager<ASampleUser> userManager
            , SignInManager<ASampleUser> signInManager
            , ITUserRepository tUserRepository
            , ITRoleRepository tRoleRepository
            , ITRightRepository tRightRepository
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _iTUserRepository = tUserRepository;
            _iTRoleRepository = tRoleRepository;
            _iTRightRepository = tRightRepository;
        }

        public async Task<ApiRequestResult> QueryAsync()
        {
            var result = await _iTUserRepository.QueryAsync();
            return ApiRequestResult.Success(result, "");
        }

        [HttpGet("QueryPaged")]
        public async Task<string> QueryPagedAsync([FromQuery] UserSearchParam param)
        {
            var filter = param.SearchLambda<TUser, UserSearchParam>();
            var result = await _iTUserRepository.QueryPagedAsync(param.Page, param.Limit,c=>c.CreateTime, filter);
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

        [HttpGet("QueryRole")]
        public async Task<string> GetUserRoleAsync([FromQuery] UserRoleParam param)
        {
            var userRoles = new List<TUserRoleRelation>();
            if (param.UserId != null)
                userRoles = await _iTUserRepository.GetUserRolesAsync(param.UserId.Value);
            var roles = await _iTRoleRepository.QueryAsync();
            var roleDtos = new List<UserRoleDto>();
            foreach (var item in roles)
            {
                var roleDto = new UserRoleDto
                {
                    Id = item.Id,
                    RoleName = item.RoleName,
                    Description = item.Description,
                    IsAdd = false
                };
                if (userRoles.Exists(c => c.RoleId == item.Id))
                    roleDto.IsAdd = true;
                roleDtos.Add(roleDto);
            }

            var pageData = new LayuiTableDto<UserRoleDto>
            {
                Code = "0",
                Msg = "获取数据成功",
                Count = roleDtos.Count,
                Data = roleDtos
            };
            var json = JsonConvert.SerializeObject(pageData);
            return json;
        }

        [HttpGet("UserMenu")]
        public async Task<string> GetUserRightAsync()
        {
            //获取当前用户的角色
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //获取当前用户的 角色
            var roles = await _iTUserRepository.GetUserRolesAsync(Guid.Parse("A0C2A91A-AB72-4FC2-9CF6-13332A499F6C"));
            if (roles == null || roles.Count <= 0)
                return ApiRequestResult.Error("用户暂无任何菜单权限").ToString();
            //获取角色菜单
            var userRights = new List<TRight>();
            foreach (var item in roles)
            {
                var roleRights =await _iTRoleRepository.GetRoleRightsAsync(item.RoleId);
                if (roleRights == null || roleRights.Count <= 0)
                    continue;
                foreach (var roleRight in roleRights)
                {
                    var right = await _iTRightRepository.GetAsync(c => c.Id == roleRight.RightId);
                    userRights.Add(right);
                }
            }

            var list = userRights.Distinct(new RightComparer());

            //父菜单
            var parents = list.Where(c => c.ParentId == null);
            var rightDtos = new List<MenuDto>();
            foreach (var parent in parents)
            {
                var rightDto = new MenuDto
                {
                    //Id = parent.Id,
                    Icon = parent.RightIcon,
                    Name = parent.RightName,
                    Title = parent.RightName,
                    Jump = parent.RightUrl,
                };
                //获取子菜单
                var subRights = list.Where(c => c.ParentId == parent.Id).Select(c => 
                new MenuDto
                {
                    //Id = c.Id,
                    Icon = c.RightIcon,
                    Name = c.RightName,
                    Title = c.RightName,
                    Jump = c.RightUrl,
                }).ToList();
                rightDto.List = subRights;
                rightDtos.Add(rightDto);
            }
            return JsonConvert.SerializeObject(
                new MenuResult(){
                    Code = 0,
                    Msg = "",
                    Data = rightDtos
                }
             );
        }

        [HttpPost]
        public async Task<ApiRequestResult> AddAsync([FromBody]UserParam param)
        {
            var userId = Guid.NewGuid();
            var asampleUser = new ASampleUser
            {
                Id = userId.ToString(),
                UserName = param.UserName,
                Email = param.Email,
                PhoneNumber = param.PhoneNumber,
            };
            var hashedNewPassword = _userManager.PasswordHasher.HashPassword(asampleUser, param.Password);
            var user = new TUser
            {
                Id = userId,
                UserName = param.UserName,
                Email = param.Email,
                PhoneNumber = param.PhoneNumber,
                Password = hashedNewPassword,
                OrgId = param.OrgId,
                LoginName = param.LoginName,
            };
            await _iTUserRepository.AddAsync(user);
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
            var user = await _iTUserRepository.GetAsync(c => c.Id == param.Id);
            user = param.UpdateEntity<TUser, UserParam>(user);
            await _iTUserRepository.UpdateAsync(user);

            var identityUser = await _userManager.FindByNameAsync(param.UserName);
            identityUser.UserName = param.UserName;
            identityUser.Email = param.Email;
            identityUser.PhoneNumber = param.PhoneNumber;

            var result = await _userManager.UpdateAsync(identityUser);
            if (result.Succeeded)
            {
                return ApiRequestResult.Success("修改成功");
            }
            return ApiRequestResult.Error(result.Errors.FirstOrDefault().Description);
        }

        [HttpPut("UserRoles")]
        public async Task<ApiRequestResult> UpdateUserRoleAsync([FromBody] UpdateUserRoleParam param)
        {
            var deleteResult = await _iTUserRepository.DeleteUserRoleAsync(param.UserId);
            if (!deleteResult)
                return ApiRequestResult.Error("更新用户角色失败");
            var updateResult = await _iTUserRepository.UpdateUserRoleAsync(param.UserId, param.RoleIds);
            if (updateResult)
                return ApiRequestResult.Success("更新用户角色成功");
            else
                return ApiRequestResult.Error("更新用户角色失败");
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