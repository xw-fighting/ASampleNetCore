using ASample.NetCore.WeChat.Models;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat
{
    public interface IWeChatPayService
    {
        Task<UnifiedOrderResult> UnifiedOrder(UnifiedOrderParam param);

        Task<string> CodePay(UnifiedOrderParam param);

        Task<string> JSAPIPay(UnifiedOrderParam param);

        Task<QueryOrderResult> QueryOrder(QueryOrderParam param);

        Task<CloseOrderResult> CloseOrder(CloseOrderParam param);

        Task<RefundResult> Refund(RefundParam param);
    }
}
