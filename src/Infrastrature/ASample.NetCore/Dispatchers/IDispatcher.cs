using ASample.NetCore.Domain;
using ASample.NetCore.Domain.Message;
using System.Threading.Tasks;

namespace ASample.NetCore.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
