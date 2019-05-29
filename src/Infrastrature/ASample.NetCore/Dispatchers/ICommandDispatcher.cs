using ASample.NetCore.Domain.Message;
using System.Threading.Tasks;

namespace ASample.NetCore.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}
