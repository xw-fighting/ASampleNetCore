using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ASample.NetCore.Http;
using ASample.NetCore.Services.IdentityServers.Dtos;
using ASample.NetCore.Services.IdentityServers.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Services.IdentityServers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ASampleHttpClient _aSampleHttpClient;

        public IdentityController(IIdentityService identityService,ASampleHttpClient aSampleHttpClient)
        {
            _identityService = identityService;
            _aSampleHttpClient = aSampleHttpClient;
        }
        //[HttpGet("access_token")]
        //public async Task<AccessTokenDto> GetAccessToken([FromQuery]string appId,string secreat)
        //{
        //    var client = new HttpClient();
        //    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        //    {
        //        Address = disco.TokenEndpoint,
        //        ClientId = "client",
        //        ClientSecret = "secret",

        //        Scope = "api1"
        //    });
        //}
    }
}