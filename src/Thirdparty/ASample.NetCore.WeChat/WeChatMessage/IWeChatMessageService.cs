using ASample.NetCore.WeChat.WeChatMessage;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat
{
    public interface IWeChatMessageService
    {
        Task<SendMsgResult> SendTemplateMsgAsync<TData>(string openId, string templateId, TData data) 
            where TData : MsgTemplateDataBasicParameter;

        Task<bool> SendTextMsgAsync(string msg);
    }
}
