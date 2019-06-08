using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Identitys.Services
{
    public class ClaimsProvider : IClaimsProvider
    {
        public async Task<IDictionary<string, string>> GetAsync(Guid userId)
        {
            return await Task.FromResult(new Dictionary<string, string>());
        }
    }
}
