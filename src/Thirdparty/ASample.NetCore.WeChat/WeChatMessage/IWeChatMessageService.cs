using ASample.NetCore.WeChat.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
