﻿using ASample.NetCore.Auths.Domains;
using ASample.NetCore.RelationalDb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASample.NetCore.Auths.Repositories
{
    public interface ITRoleRepository:IRepository<TRole>
    {
        Task<List<TRoleRightRelation>> GetRoleRightsAsync(Guid roleId);

        Task<bool> DeleteRoleRightAsync(Guid roleId);

        Task<bool> UpdateRoleRightAsync(Guid roleId, List<Guid> rightIds);
    }
}
