using ASample.NetCore.Domain.Message;
using ASample.NetCore.Domain.RabbitMq;
using System.Threading.Tasks;

namespace ASample.NetCore.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}
