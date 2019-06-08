﻿using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.MySqlDb.Repositories
{
    public interface  IMySqlRepository<TEntity> : IASampleRepository<TEntity>
        where TEntity : AggregateRoot
    {

    }
}
