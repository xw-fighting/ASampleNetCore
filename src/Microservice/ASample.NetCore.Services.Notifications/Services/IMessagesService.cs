using MimeKit;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Notifications.Services
{
    public interface IMessagesService
    {
        Task SendAsync(MimeMessage message);
    }
}
