using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace ASample.NetCore.Services.IdentityServers.Services
{
    public interface IResourcesService
    {
        Task<ApiResource> FindApiResourceByNameAsync(string name);

        Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames);

        Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames);

        Task<Resources> GetAllResourcesAsync();
    }
}
