﻿using ASample.NetCore.Domain.Models;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.MySql.Repositories
{
    public interface  IMySqlRepository<TEntity> : IASampleRepository<TEntity>
        where TEntity : AggregateRoot
    {

    }
}