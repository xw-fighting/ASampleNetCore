using ASample.NetCore.Auths.Domains;
using ASample.NetCore.RelationalDb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASample.NetCore.Auths.Repositories
{
    public interface ITUserRepository:IRepository<TUser>
    {
        Task<List<TUserRoleRelation>> GetUserRolesAsync(Guid userId);

        Task<bool> UpdateUserRoleAsync(Guid userId, List<Guid> rolesIds);

        Task<bool> DeleteUserRoleAsync(Guid userId);
    }
}
