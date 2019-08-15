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
    public class TUserRepository : SqlServerRepository<ASampleIdentityDbContext,TUser>,ITUserRepository
    {
        public TUserRepository(
            IUnitOfWork<ASampleIdentityDbContext> unitOfWork
            ):base(unitOfWork)
        {
            _dbRelationSet = unitOfWork.GetDbContext().Set<TUserRoleRelation>();
        }
        public DbSet<TUserRoleRelation> _dbRelationSet;

        public async Task<List<TUserRoleRelation>> GetUserRolesAsync(Guid userId)
        {
            var userRoles = await _dbRelationSet.Where(c => c.UserId == userId).ToListAsync();
            return userRoles;
        }

        public async Task<bool> DeleteUserRoleAsync(Guid userId)
        {
            var roleRights = _dbRelationSet.Where(c => c.UserId == userId);
            _dbRelationSet.RemoveRange(roleRights);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateUserRoleAsync(Guid userId, List<Guid> rolesIds)
        {
            var roleRights = _dbRelationSet.Where(c => c.UserId == userId);
            _dbRelationSet.RemoveRange(roleRights);
            var roleRightEntitys = new List<TUserRoleRelation>();
            foreach (var item in rolesIds)
            {
                var roleRight = new TUserRoleRelation
                {
                    UserId = userId,
                    RoleId = item
                };
                roleRightEntitys.Add(roleRight);
            }
            await _dbRelationSet.AddRangeAsync(roleRightEntitys);
            return await Task.FromResult(true);
        }
    }
}
