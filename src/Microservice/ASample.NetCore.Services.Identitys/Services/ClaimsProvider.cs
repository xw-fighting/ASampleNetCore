using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASample.NetCore.AuthenticationService.Services
{
    public class ClaimsProvider : IClaimsProvider
    {
        public async Task<IDictionary<string, string>> GetAsync(Guid userId)
        {
            return await Task.FromResult(new Dictionary<string, string>());
        }
    }
}
