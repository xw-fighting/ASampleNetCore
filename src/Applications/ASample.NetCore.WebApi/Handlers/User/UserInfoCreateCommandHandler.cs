﻿using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.WebApi.Domain;
using ASample.NetCore.WebApi.Messages.Command;
using ASample.NetCore.WebApi.Messages.Events;
using ASample.NetCore.WebApi.Repositories.User;
using System.Threading.Tasks;

namespace ASample.NetCore.WebApi.Handlers.User
{
    public class UserInfoCreateCommandHandler : ICommandHandler<UserInfoCreateCommand>
    {
        private readonly IUserInfoRepository _repository;
        //private readonly ILogger _logger;
        private readonly IBusPublisher _busPublisher;
        public UserInfoCreateCommandHandler(IUserInfoRepository repository,  IBusPublisher busPublisher)//ILogger logger,
        {
            _repository = repository;
            //_logger = logger;
            _busPublisher = busPublisher;
        }
        public async Task HandleAsync(UserInfoCreateCommand command, ICorrelationContext context)
        {
            var userInfo = new UserInfo()
            {
                Id = command.Id,
                Name = command.Name,
                Address = command.Address,
                Age = command.Age
            };
            await _repository.AddAsync(userInfo);
            await _busPublisher.PublishAsync(new UserInfoCreateEvent(command.Id,
                command.Name, command.Address, command.Age), context);
        }
    }
}
