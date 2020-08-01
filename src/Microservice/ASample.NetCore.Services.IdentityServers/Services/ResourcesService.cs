using System.Collections.Generic;
using System.Threading.Tasks;
using ASample.NetCore.Services.IdentityServers.Repositories;
using IdentityServer4.Models;

namespace ASample.NetCore.Services.IdentityServers.Services
{
    public class ResourcesService:IResourcesService
    {
        private readonly IResourcesRepository _resourcesRepository;

        public ResourcesService(IResourcesRepository resourcesRepository)
        {
            _resourcesRepository = resourcesRepository;
        }

        public Task<ApiResource> FindApiResourceByNameAsync(string name)
        {
            return _resourcesRepository.FindApiResourceByNameAsync(name);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return _resourcesRepository.FindApiResourcesByScopeNameAsync(scopeNames);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return _resourcesRepository.FindIdentityResourcesByScopeNameAsync(scopeNames);
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return _resourcesRepository.GetAllResourcesAsync();
        }
    }
}
