using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Notifications.Messages.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Notifications.Handlers
{
    public class SendEmailNotificationHandler : ICommandHandler<SendEmailNotificationCommand>
    {
        private readonly ILogger<SendEmailNotificationHandler> _logger;

        public Task HandleAsync(SendEmailNotificationCommand command, ICorrelationContext context)
        {
            _logger.LogInformation($"Sending an email message to: '{command.Email}'.");

            // Publish: EmailSent
            // Publish: SendEmailNotificationRejected

            return Task.CompletedTask;
        }
    }
}
