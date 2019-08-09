using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Auths.DbConexts;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.SqlServerDb.Repository;
using Microsoft.EntityFrameworkCore;

namespace ASample.NetCore.Auths.Repositories
{
    public class TRoleRepository : BaseRepository<TRole>,ITRoleRepository
    {
        private readonly ASampleIdentityDbContext _dbContext;
        public TRoleRepository(
            ISqlServerRepository<TRole> sqlServerRepository
            , IUnitOfWork<ASampleIdentityDbContext> unitOfWork
            , ASampleIdentityDbContext dbContext
            ):base(sqlServerRepository, unitOfWork)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TRoleRightRelation>> GetRoleRightsAsync(Guid roleId)
        {
            var roleRight =await _dbContext.Set<TRoleRightRelation>().Where(c => c.RoleId == roleId).ToListAsync();
            return roleRight;
        }
    }
}
