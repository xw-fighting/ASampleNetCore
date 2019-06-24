using ASample.NetCore.WeChat.WeChatRedPack;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat
{
    public interface IWeChatRedPackService
    {
        Task<RedPackResult> SendRedPackAsync(RedPackParam param);

        Task<GroupRedPackResult> SendGroupRedPackAsync(GroupRedPackParam param);

        Task<QueryRedPackRecordResult> QueryRedPackRecordAsync(QueryRedPackRecordParam param);
    }
}
