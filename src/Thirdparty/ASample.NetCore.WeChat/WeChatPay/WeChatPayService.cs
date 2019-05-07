using ASample.NetCore.Configuration;
using ASample.NetCore.Serialize;
using ASample.NetCore.WeChat.Core;
using ASample.NetCore.WeChat.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat.WeChatPay
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WeChatPayService
    {
        /// <summary>
        /// 统一下单
        /// </summary>
        public async Task<UnifiedOrderResult> UnifiedOrder(UnifiedOrderParam param)
        {
            var url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            var dataDic = new SortedDictionary<string, object>();
            var wxConfig = ConfigurationReader.GetValue<WeChatConfig>("wechatconfig");
            dataDic.SetValue("appid", wxConfig.WxAppId);//配置文件读取
            dataDic.SetValue("mch_id", wxConfig.WxMechId);//配置文件读取
            dataDic.SetValue("nonce_str", WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("body", param.Body);//传入或者配置
            dataDic.SetValue("out_trade_no",param.OutTradeNo??WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("total_fee", param.TotalFee);//传入
            dataDic.SetValue("spbill_create_ip", "");//
            dataDic.SetValue("notify_url", param.NotifyUrl);//配置读取
            dataDic.SetValue("trade_type", param.TradeType);//传入
            if (param.TradeType == "JSAPI")
            {
                dataDic.SetValue("OpenId", param.OpenId);
            }
            //可选参数
            dataDic.SetValue("goods_tag", "xw_test");
            dataDic.SetValue("attach", "xw_test");
            dataDic.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            dataDic.SetValue("time_expire", DateTime.Now.AddMinutes(40).ToString("yyyyMMddHHmmss"));

            var sign = WeChatPayUtility.MakeSign(dataDic.ToUrlString());
            dataDic.SetValue("sign", sign);

            var httpClient = new HttpClient();
            var content = new StringContent(dataDic.ToXml());
            var response = await httpClient.PostAsync(url, content);
            var xmlResult = await response.Content.ReadAsStringAsync();

            var xmlSerialize = new XmlSerialize();
            var result = xmlSerialize.Deserialize<UnifiedOrderResult>(xmlResult);
            return result;
        }

        /// <summary>
        /// 扫码支付
        /// </summary>
        /// <returns></returns>
        public async Task<string> CodePay(UnifiedOrderParam param)
        {
            param.TradeType = "NATIVE";
            var result = await UnifiedOrder(param);
            if (result.ReturnCode == "SUCCESS" && result.ResultCode == "SUCCESS")
            {
                return result.CodeUrl;
            }
            return string.Empty;
        }

        public async Task<string> JSAPIPay(UnifiedOrderParam param)
        {
            param.TradeType = "JSAPI";
            var result = await UnifiedOrder(param);
            if (result.ReturnCode == "SUCCESS" && result.ResultCode == "SUCCESS")
            {
                return result.CodeUrl;
            }
            return string.Empty;
        }

        public async Task<QueryOrderResult> QueryOrder(QueryOrderParam param)
        {
            var url = "https://api.mch.weixin.qq.com/pay/orderquery";
            var wxConfig = ConfigurationReader.GetValue<WeChatConfig>("wechatconfig");
            var dataDic = new SortedDictionary<string, object>();
            dataDic.SetValue("appid", wxConfig.WxAppId);//配置
            dataDic.SetValue("mch_id", wxConfig.WxMechId);//配置
            dataDic.SetValue("out_trade_no", param.OutTradeNo);
            dataDic.SetValue("transaction_id", param.TransactionId);
            dataDic.SetValue("nonce_str", WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("sign", WeChatPayUtility.MakeSign(dataDic.ToUrlString()));

            var xml = dataDic.ToXml();
            var content = new StringContent(xml);
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, content);
            var xmlResult = await response.Content.ReadAsStringAsync();
            var xmlSer = new XmlSerialize();
            var result = xmlSer.Deserialize<QueryOrderResult>(xmlResult);
            return result;
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <returns></returns>
        public async Task<CloseOrderResult> CloseOrder(CloseOrderParam param)
        {
            var url = "https://api.mch.weixin.qq.com/pay/closeorder";
            var wxConfig = ConfigurationReader.GetValue<WeChatConfig>("wechatconfig");

            var dataDic = new SortedDictionary<string, object>();
            dataDic.SetValue("appid", wxConfig.WxAppId);//配置
            dataDic.SetValue("mch_id", wxConfig.WxMechId);//配置
            dataDic.SetValue("out_trade_no", param.OutTradeNo);//传值
            dataDic.SetValue("nonce_str", WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("sign", WeChatPayUtility.MakeSign(dataDic.ToUrlString()));

            var xml = dataDic.ToXml();
            var content = new StringContent(xml);
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, content);
            var xmlResult = await response.Content.ReadAsStringAsync();
            var xmlSer = new XmlSerialize();
            var result = xmlSer.Deserialize<CloseOrderResult>(xmlResult);
            return result;
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="total_fee"></param>
        /// <param name="refund_fee"></param>
        public async Task<RefundResult> Refund(RefundParam param)
        {
            try
            {
                var url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
                var wxConfig = ConfigurationReader.GetValue<WeChatConfig>("wechatconfig");

                var data = new SortedDictionary<string, object>();
                data.SetValue("appid", wxConfig.WxAppId);
                data.SetValue("mch_id", wxConfig.WxMechId);
                data.SetValue("nonce_str", WeChatPayUtility.GeneratorNonceStr());
                data.SetValue("out_trade_no", param.OutTradeNo);
                data.SetValue("out_refund_no", WeChatPayUtility.GenerateOutTradeNo());
                data.SetValue("total_fee", param.TotalFee);
                data.SetValue("refund_fee", param.RefundFee);
                data.SetValue("sign", WeChatPayUtility.MakeSign(data.ToUrlString()));

                var xml = data.ToXml();
                var httpClient = new HttpClient();
                var content = new StringContent(xml);
                var response = await httpClient.PostAsync(url, content);
                var xmlResult = await response.Content.ReadAsStringAsync();
                var xmlSerialize = new XmlSerialize();
                var result = xmlSerialize.Deserialize<RefundResult>(xmlResult);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
