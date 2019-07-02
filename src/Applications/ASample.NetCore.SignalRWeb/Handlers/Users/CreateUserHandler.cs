using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.SignalRWeb.Domains.Users;
using ASample.NetCore.SignalRWeb.Messages.Commands.Users;
using ASample.NetCore.SignalRWeb.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.SignalRWeb.Handlers.Users
{
    public class CreateUserHandler : ICommandHandler<CreateUserInfoCommand>
    {
        private readonly IUserRepository _repository;

        public CreateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(CreateUserInfoCommand command, ICorrelationContext context)
        {
            var userInfo = new UserInfo()
            {
                Id = command.Id,
                Name = command.Name,
                Address = command.Address,
                Age = command.Age
            };
            await _repository.AddAsync(userInfo);
        }
    }
}
