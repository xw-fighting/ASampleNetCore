using ASample.NetCore.Http;
using ASample.NetCore.WeChat.WeChatRedPack;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat
{
    public class WeChatRedPackService:IWeChatRedPackService
    {
        private readonly IOptions<WechatOptions> _options;
        private readonly IASampleHttpClient _aSampleHttpClient;

        public WeChatRedPackService(IOptions<WechatOptions> options,IASampleHttpClient aSampleHttpClient)
        {
            _options = options;
            _aSampleHttpClient = aSampleHttpClient;
        }
        public async Task<RedPackResult> SendRedPackAsync(RedPackParam param)
        {
            var url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
            var dataDic = new SortedDictionary<string, object>();
            var wxConfig = _options.Value;
            dataDic.SetValue("nonce_str", WeChatPayUtility.GeneratorNonceStr());//配置文件读取
            dataDic.SetValue("mch_billno", param.MchBillno ?? WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("mch_id", wxConfig.MechId);//传入或者配置
            dataDic.SetValue("wxappid", wxConfig.WxAppId);
            dataDic.SetValue("send_name", param.SendName);//传入
            dataDic.SetValue("re_openid", param.ReOpenId);//
            dataDic.SetValue("total_amount", param.TotalAmount);//配置读取
            dataDic.SetValue("total_num", param.TotalNum);//传入
            dataDic.SetValue("wishing", param.Wishing);//传入
            dataDic.SetValue("client_ip", param.ClientIp);//传入
            dataDic.SetValue("act_name", param.ActName);//传入
            dataDic.SetValue("remark", param.Remark);//传入
            dataDic.SetValue("scene_id", param.SceneId);//传入
            dataDic.SetValue("risk_info", param.RiskInfo);//传入

            var sign = WeChatPayUtility.MakeSign(dataDic.ToUrlString(),wxConfig.MechKey);
            dataDic.SetValue("sign", sign);
            
            var certPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, wxConfig.CertPath);
            var cert = new X509Certificate2(certPath);
            var result =await _aSampleHttpClient.PostAsync<RedPackResult>(url, dataDic.ToXml(), DeserializeType.XmlDeserialize, cert);
            return result;
        }

        public async Task<GroupRedPackResult> SendGroupRedPackAsync(GroupRedPackParam param)
        {
            var url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendgroupredpack";
            var dataDic = new SortedDictionary<string, object>();
            var wxConfig = _options.Value;
            dataDic.SetValue("nonce_str", WeChatPayUtility.GeneratorNonceStr());//配置文件读取
            dataDic.SetValue("mch_billno", param.MchBillno ?? WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("mch_id", wxConfig.MechId);//传入或者配置
            dataDic.SetValue("wxappid", wxConfig.WxAppId);
            dataDic.SetValue("send_name", param.SendName);//传入
            dataDic.SetValue("re_openid", param.ReOpenId);//
            dataDic.SetValue("total_amount", param.TotalAmount);//配置读取
            dataDic.SetValue("total_num", param.TotalNum);//传入
            dataDic.SetValue("wishing", param.Wishing);//传入
            dataDic.SetValue("act_name", param.ActName);//传入
            dataDic.SetValue("remark", param.Remark);//传入
            dataDic.SetValue("scene_id", param.SceneId);//传入
            dataDic.SetValue("risk_info", param.RiskInfo);//传入

            var sign = WeChatPayUtility.MakeSign(dataDic.ToUrlString(),wxConfig.MechKey);
            dataDic.SetValue("sign", sign);
            var certPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, wxConfig.CertPath);
            var cert = new X509Certificate2(certPath);
            var result = await _aSampleHttpClient.PostAsync<GroupRedPackResult>(url, dataDic.ToXml(), DeserializeType.XmlDeserialize, cert);
            return result;
        }

        public async Task<QueryRedPackRecordResult> QueryRedPackRecordAsync(QueryRedPackRecordParam param)
        {
            var url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendgroupredpack";
            var dataDic = new SortedDictionary<string, object>();
            var wxConfig = _options.Value;
            dataDic.SetValue("nonce_str", WeChatPayUtility.GeneratorNonceStr());//配置文件读取
            dataDic.SetValue("mch_billno", param.MchBillNo ?? WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("mch_id", wxConfig.MechId);//传入或者配置
            dataDic.SetValue("appid", wxConfig.WxAppId);
            dataDic.SetValue("bill_type", param.BillType);//传入

            var sign = WeChatPayUtility.MakeSign(dataDic.ToUrlString(), wxConfig.MechKey);
            dataDic.SetValue("sign", sign);
            var certPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, wxConfig.CertPath);
            var cert = new X509Certificate2(certPath);
            var result = await _aSampleHttpClient.PostAsync<QueryRedPackRecordResult>(url, dataDic.ToXml(), DeserializeType.XmlDeserialize, cert);
            return result;
        }
    }
}
