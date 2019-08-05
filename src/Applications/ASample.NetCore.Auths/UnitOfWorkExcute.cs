using ASample.NetCore.Auths.DbConexts;
using ASample.NetCore.EntityFramwork;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ASample.NetCore.Auths
{
    public class UnitOfWorkExcute : IActionFilter
    {
        private IUnitOfWork<ASampleIdentityDbContext> _unitOfWork;
        public UnitOfWorkExcute(IUnitOfWork<ASampleIdentityDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _unitOfWork.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
