using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.EntityFramwork
{
    public interface IUnitOfWork:IDisposable
    {
        int SaveChanges();
    }
}
