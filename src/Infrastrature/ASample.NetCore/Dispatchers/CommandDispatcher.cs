﻿using System.Threading.Tasks;
using ASample.NetCore.Domain.Message;
using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using Autofac;

namespace ASample.NetCore.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }
        public async Task SendAsync<T>(T command) where T : ICommand
         => await _context.Resolve<ICommandHandler<T>>().HandleAsync(command, CorrelationContext.Empty);
    }
}
