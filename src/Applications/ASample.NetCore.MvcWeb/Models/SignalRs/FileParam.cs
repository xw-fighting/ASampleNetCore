using System.Collections.Generic;

namespace ASample.NetCore.MvcWeb.Models.SignalRs
{
    public class FileParam
    {
        // 文件名稱
        public string FileName { get; set; }
        // 行
        public int Row { get; set; }
        // 列
        public int Column { get; set; }
        // 文件內容
        public List<CellParam> TextList { get; set; }
        // 使用中的文件用戶
        public List<UserParam> Editor { get; set; }
        // 建立者
        public string Creator { get; set; }
    }
}
