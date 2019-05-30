using ASample.NetCore.Domain.Models;
using ASample.NetCore.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.SqlServer.Repository
{
    public interface  ISqlServerRepository<TEntity>:IASampleRepository<TEntity> where TEntity:AggregateRoot
    {

    }
}
