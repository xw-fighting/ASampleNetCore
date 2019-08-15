using System;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.Auths.Models;
using ASample.NetCore.Auths.Models.Rights;
using ASample.NetCore.Auths.Repositories;
using ASample.NetCore.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASample.NetCore.Auths.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RightController : ControllerBase
    {
        private readonly UserManager<ASampleUser> _userManager;
        private readonly ITRightRepository _iTRightRepository;

        public RightController(UserManager<ASampleUser> userManager
            , ITRightRepository tRightRepository)
        {
            _userManager = userManager;
            _iTRightRepository = tRightRepository;
        }

        public async Task<ApiRequestResult> QueryAsync()
        {
            var result = await _iTRightRepository.QueryAsync();
            return ApiRequestResult.Success(result, "");
        }

        [HttpGet("ParentRight")]
        public async Task<ApiRequestResult> GetParentRightAsync()
        {
            var result = await _iTRightRepository.QueryAsync(c => c.ParentId == null);
            return ApiRequestResult.Success(result, "");
        }

        [HttpGet("QueryPaged")]
        public async Task<string> QueryPagedAsync([FromQuery] RightSearchParam param)
        {
            var filter = param.SearchLambda<TRight, RightSearchParam>();
            var result = await _iTRightRepository.QueryPagedAsync(param.Page,param.Limit,null, filter);
            var pageData = new LayuiTableDto<TRight>
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
        public async Task<ApiRequestResult> AddAsync([FromBody]RightParam param)
        {
            try
            {
                var add = new TRight
                {
                    RightName = param.RightName,
                    ParentId = param.ParentId,
                    Description = param.Description,
                    RightIcon = param.RightIcon,
                    RightUrl = param.RightUrl,
                };
                await _iTRightRepository.AddAsync(add);
                return ApiRequestResult.Success("添加成功");
            }
            catch (Exception ex)
            {
                return ApiRequestResult.Error(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ApiRequestResult> UpdateAsync([FromBody]RightParam param)
        {
            try
            {
                var tRight = await _iTRightRepository.GetAsync(c => c.Id == param.Id);
                tRight = param.UpdateEntity<TRight,RightParam>(tRight);
                await _iTRightRepository.UpdateAsync(tRight);
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