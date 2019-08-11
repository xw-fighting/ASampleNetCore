using ASample.NetCore.Auths.Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASample.NetCore.Auths.Repositories
{
    public interface ITUserRepository:IBaseRepository<TUser>
    {
        Task<List<TUserRoleRelation>> GetUserRolesAsync(Guid userId);

        Task<bool> UpdateUserRoleAsync(Guid userId, List<Guid> rolesIds);

        Task<bool> DeleteUserRoleAsync(Guid userId);
    }
}
