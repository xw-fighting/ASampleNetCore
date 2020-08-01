using System.Collections.Generic;
using System.Threading.Tasks;
using ASample.NetCore.RelationalDb;
using ASample.NetCore.Services.IdentityServers.Domain;
using IdentityServer4.Models;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public interface IResourcesRepository:IRepository<ApiResourceEntity>
    {
        Task<ApiResource> FindApiResourceByNameAsync(string name);

        Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames);

        Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames);

        Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames);

        Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames);

        Task<Resources> GetAllResourcesAsync();
    }
}
