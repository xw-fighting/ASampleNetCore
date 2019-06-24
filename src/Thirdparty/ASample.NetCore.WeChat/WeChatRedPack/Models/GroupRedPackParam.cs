using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.WeChat.WeChatRedPack
{
    public class GroupRedPackParam
    {
        /// <summary>
        /// 随机字符串(不长于32位)
        /// </summary>

        public string NonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string MchBillno { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 公众账号appid
        /// </summary>
        public string WxAppId { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string SendName { get; set; }

        /// <summary>
        /// 用户openid(接受红包的用户openid)
        /// </summary>
        public string ReOpenId { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public string TotalAmount { get; set; }

        /// <summary>
        /// 红包发放总人数
        /// </summary>
        public string TotalNum { get; set; }

        /// <summary>
        /// 红包金额设置方式(红包金额设置方式 ALL_RAND—全部随机,商户指定总金额和红包发放总人数，由微信支付随机计算出各红包金额)
        /// </summary>
        public string AmtType { get; set; }

        /// <summary>
        /// 红包祝福语
        /// </summary>
        public string Wishing { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 场景id
        /// </summary>
        public string SceneId { get; set; }

        /// <summary>
        /// 活动信息
        /// </summary>
        public string RiskInfo { get; set; }
    }
}
