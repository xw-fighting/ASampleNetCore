using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;

namespace ASample.NetCore.Services.Notifications.Messages.Commands
{
    public class SendEmailNotificationCommand:ICommand
    {
        public string Email { get; }
        public string Title { get; }
        public string Message { get; }

        [JsonConstructor]
        public SendEmailNotificationCommand(string email, string title, string message)
        {
            Email = email;
            Title = title;
            Message = message;
        }
    }
}
