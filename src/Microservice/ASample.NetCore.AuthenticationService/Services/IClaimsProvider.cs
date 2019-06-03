using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASample.NetCore.AuthenticationService.Services
{
    public interface IClaimsProvider
    {
        Task<IDictionary<string, string>> GetAsync(Guid userId);
    }
}
