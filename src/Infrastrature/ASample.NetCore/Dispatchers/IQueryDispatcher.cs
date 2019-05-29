using ASample.NetCore.Domain;
using System.Threading.Tasks;

namespace ASample.NetCore.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
