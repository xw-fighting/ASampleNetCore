﻿using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.WebApi.Messages.Events;
using ASample.NetCore.WebApi.Repositories.User;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ASample.NetCore.WebApi.Handlers.User
{
    public class UserInfoCreateHandler : IEventHandler<UserInfoCreateEvent>
    {
        private readonly IUserInfoRepository _repository;
        private readonly ILogger _logger;
        public UserInfoCreateHandler(IUserInfoRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task HandleAsync(UserInfoCreateEvent @event, ICorrelationContext context)
        {
            var user = new Domain.UserInfo()
            {
                Name = @event.Name,
                Address = @event.Address,
                Age = @event.Age,
            };
            await _repository.AddAsync(user);
            _logger.LogInformation($"Created customer with id: '{@event.Id}'.");
        }
    }
}
