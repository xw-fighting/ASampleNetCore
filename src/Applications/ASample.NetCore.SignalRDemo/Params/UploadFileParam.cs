using System;

namespace ASample.NetCore.SignalRDemo.Params
{
    public class UploadFileParam
    {
        /// <summary>
        /// 行李图片,// System.Text.Encoding.GetEncoding("iso-8859-1").GetString(bytes)
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string ImgName { get; set; }

        /// <summary>
        /// 行李时间
        /// </summary>
        public DateTime CaptureTime { get; set; } = DateTime.Now;

        /// <summary>
        /// X光机标识码
        /// </summary>

        public string MachineCode { get; set; }


        /// <summary>
        /// 机场标识
        /// </summary>
        public string IdentityCode { get; set; }


    }
}
