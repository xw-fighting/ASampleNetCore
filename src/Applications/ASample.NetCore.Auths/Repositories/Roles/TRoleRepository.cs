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
        //private readonly IUnitOfWork<ASampleIdentityDbContext> _unitOfWork;
        public TRoleRepository(
            ISqlServerRepository<TRole> sqlServerRepository
            , IUnitOfWork<ASampleIdentityDbContext> unitOfWork
            ):base(sqlServerRepository, unitOfWork)
        {
            //_unitOfWork = unitOfWork;
            _dbSet = unitOfWork.GetDbContext().Set<TRoleRightRelation>();
        }

        public DbSet<TRoleRightRelation> _dbSet;

        public async Task<List<TRoleRightRelation>> GetRoleRightsAsync(Guid roleId)
        {
            var roleRight =await _dbSet.Where(c => c.RoleId == roleId).ToListAsync();
            return roleRight;
        }

        public async Task<bool> DeleteRoleRightAsync(Guid roleId)
        {
            var roleRights =  _dbSet.Where(c => c.RoleId == roleId);
             _dbSet.RemoveRange(roleRights);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateRoleRightAsync(Guid roleId,List<Guid> rightIds)
        {
            var roleRights = _dbSet.Where(c => c.RoleId == roleId);
            _dbSet.RemoveRange(roleRights);
            var roleRightEntitys = new List<TRoleRightRelation>();
            foreach (var item in rightIds)
            {
                var roleRight = new TRoleRightRelation
                {
                    RoleId = roleId,
                    RightId = item
                };
                roleRightEntitys.Add(roleRight);
            }
            await _dbSet.AddRangeAsync(roleRightEntitys);
            return await Task.FromResult(true);
        }
    }
}
