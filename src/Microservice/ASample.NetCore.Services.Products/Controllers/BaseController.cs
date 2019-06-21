using System.Threading.Tasks;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Services.Products.Controllers
{

    public class BaseController : ControllerBase
    {
        private IDispatcher _dispatcher;

        public BaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var result = await _dispatcher.QueryAsync<TResult>(query);
            return result;
        }
    }
}