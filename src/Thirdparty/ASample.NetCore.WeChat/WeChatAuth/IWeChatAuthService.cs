using ASample.NetCore.Http;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat
{
    public interface IWeChatAuthService
    {
        Task<HttpRequestResult> GetAccessTokenAsync();

        Task<HttpRequestResult> GetBasicInfoAsync(string openId);

        Task<HttpRequestResult> GetJsApiConfigAsync(string url);

        Task<HttpRequestResult> GetJsApiTicketAsync();
    }
}
