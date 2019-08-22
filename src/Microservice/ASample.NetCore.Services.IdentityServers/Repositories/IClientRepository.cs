using ASample.NetCore.NonInertialDb.Repositories;
using ASample.NetCore.Services.IdentityServers.Domain;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public interface IClientRepository :IRepository<Clients>
    {
        
    }
}
