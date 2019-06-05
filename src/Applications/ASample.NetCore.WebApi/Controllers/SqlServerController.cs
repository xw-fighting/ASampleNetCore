using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.WebApi.Dto.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlServerController : ControllerBase
    {

        [HttpGet]
        public async Task Get()
        {

        }

        [HttpGet("{id:guid}")]
        public async Task Get([FromRoute] UserInfoDto userInfo)
        {

        }
    }
}