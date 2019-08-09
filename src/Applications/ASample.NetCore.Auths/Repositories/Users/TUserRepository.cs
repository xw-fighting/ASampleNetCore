using ASample.NetCore.Auths.DbConexts;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.SqlServerDb.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Auths.Repositories
{
    public class TUserRepository : BaseRepository<TUser>,ITUserRepository
    {
        private readonly IUnitOfWork<ASampleIdentityDbContext> _unitOfWork;
        public TUserRepository(
            ISqlServerRepository<TUser> sqlServerRepository
            , IUnitOfWork<ASampleIdentityDbContext> unitOfWork
            ):base(sqlServerRepository, unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TUserRoleRelation>> GetUserRolesAsync(Guid userId)
        {
            var userRoles = await _unitOfWork.GetDbContext().Set<TUserRoleRelation>().Where(c => c.UserId == userId).ToListAsync();
            return userRoles;
        }
    }
}
