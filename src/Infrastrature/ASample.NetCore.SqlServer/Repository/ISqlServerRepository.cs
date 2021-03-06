﻿using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork;
using Microsoft.EntityFrameworkCore;

namespace ASample.NetCore.SqlServerDb.Repository
{
    public interface ISqlServerRepository<TEntity> :IASampleRepository<TEntity> 
        where TEntity:AggregateRoot
    {

    }
}
