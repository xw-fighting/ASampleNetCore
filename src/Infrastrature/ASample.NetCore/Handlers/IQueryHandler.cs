using ASample.NetCore.Domain;
using System.Threading.Tasks;

namespace ASample.NetCore.Handlers
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
