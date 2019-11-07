using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Auths.DbConexts;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb;
using Microsoft.EntityFrameworkCore;

namespace ASample.NetCore.Auths.Repositories
{
    public class TRoleRepository : Repository<ASampleIdentityDbContext,TRole>,ITRoleRepository
    {
        public TRoleRepository(IUnitOfWork<ASampleIdentityDbContext> unitOfWork ):base( unitOfWork)
        {
            _dbRelateSet = unitOfWork.GetDbContext().Set<TRoleRightRelation>();
        }

        public DbSet<TRoleRightRelation> _dbRelateSet;

        public async Task<List<TRoleRightRelation>> GetRoleRightsAsync(Guid roleId)
        {
            var roleRight =await _dbRelateSet.Where(c => c.RoleId == roleId).ToListAsync();
            return roleRight;
        }

        public async Task<bool> DeleteRoleRightAsync(Guid roleId)
        {
            var roleRights =  _dbRelateSet.Where(c => c.RoleId == roleId);
             _dbRelateSet.RemoveRange(roleRights);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateRoleRightAsync(Guid roleId,List<Guid> rightIds)
        {
            var roleRights = _dbRelateSet.Where(c => c.RoleId == roleId);
            _dbRelateSet.RemoveRange(roleRights);
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
            await _dbRelateSet.AddRangeAsync(roleRightEntitys);
            return await Task.FromResult(true);
        }
    }
}
