﻿
namespace ASample.NetCore.WeChat.WeChatAuth
{
    public class JsApiConfigResult
    {
        public string AppId { get; set; }
        public string TimeStamp { get; set; }
        public string NonceStr { get; set; }
        public string Signature { get; set; }
    }
}
