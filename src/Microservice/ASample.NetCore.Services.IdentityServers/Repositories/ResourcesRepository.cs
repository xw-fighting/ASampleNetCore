using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb;
using ASample.NetCore.Services.IdentityServers.Domain;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public class ResourcesRepository : Repository<ASampleSqlServerDbContext, ApiResourceEntity>,IResourcesRepository,IResourceStore
    {
        public ResourcesRepository(IUnitOfWork<ASampleSqlServerDbContext> unitOfWork) :base(unitOfWork)
        {
            ApiResourcesSet = unitOfWork.GetDbContext().Set<ApiResourceEntity>();
            IdentityResourceSet = unitOfWork.GetDbContext().Set<IdentityResourceEntity>();
            ApiScopesSet = unitOfWork.GetDbContext().Set<ApiScopesEntity>();
        }

        public DbSet<ApiResourceEntity> ApiResourcesSet { get; set; }
        public DbSet<IdentityResourceEntity> IdentityResourceSet { get; set; }
        public DbSet<ApiScopesEntity> ApiScopesSet { get; set; }

        public async  Task<ApiResource> FindApiResourceByNameAsync(string name)
        {
            var apiResource = await ApiResourcesSet.FirstOrDefaultAsync(c => c.ApiResourceName == name)??new ApiResourceEntity();
            return apiResource.ApiResource;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var apiResourceList = new List<ApiResource>();
            foreach (var apiResourceName in apiResourceNames)
            {
                var apiResource = await ApiResourcesSet.FirstOrDefaultAsync(c => c.ApiResourceName == apiResourceName) ?? new ApiResourceEntity();
                apiResourceList.Add(apiResource.ApiResource);
            }
            return apiResourceList;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var apiApiScopeList = new List<ApiResource>();
            foreach (var scopeName in scopeNames)
            {
                var apiResources = await ApiResourcesSet.FirstOrDefaultAsync(c => c.ApiResourceName == scopeName) ?? new ApiResourceEntity();
                apiApiScopeList.Add(apiResources.ApiResource);
            }
            return apiApiScopeList;
        }

        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var apiApiScopeList = new List<ApiScope>();
            foreach (var apiScopeName in scopeNames)
            {
                var apiApiScope = await ApiScopesSet.FirstOrDefaultAsync(c => c.ApiScopeName == apiScopeName) ?? new ApiScopesEntity();
                apiApiScopeList.Add(apiApiScope.ApiScope);
            }
            return apiApiScopeList;
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var identityResourceList = new List<IdentityResource>();
            foreach (var scopeName in scopeNames)
            {
                var identityResource = await IdentityResourceSet.FirstOrDefaultAsync(c => c.IdentityResourceName == scopeName);
                if(identityResource == null)
                    continue;
                identityResourceList.Add(identityResource.IdentityResource);
            }

            return identityResourceList;
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var identityResourcesEntities = IdentityResourceSet.Where(c => !c.IsDeleted);
            var apiResourcesEntities = ApiResourcesSet.Where(c => !c.IsDeleted);
            var apiScopesEntities = ApiScopesSet.Where(c => !c.IsDeleted);

            var apiResources = new List<ApiResource>();
            var identityResources = new List<IdentityResource>();
            var apiScopes = new List<ApiScope>();

            foreach (var apiResourceEntity in apiResourcesEntities)
            {
                apiResources.Add(apiResourceEntity.ApiResource);
            }

            foreach (var identityResourceEntity in identityResourcesEntities)
            {
                identityResources.Add(identityResourceEntity.IdentityResource);
            }

            foreach (var apiScopesEntitity in apiScopesEntities)
            {
                apiScopes.Add(apiScopesEntitity.ApiScope);
            }
            var resources = new Resources(identityResources,apiResources, apiScopes);
            return await Task.FromResult(resources);
        }
    }
}
