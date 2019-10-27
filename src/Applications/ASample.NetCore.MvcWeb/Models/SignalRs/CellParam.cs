
namespace ASample.NetCore.MvcWeb.Models.SignalRs
{
    public class CellParam
    {
        // cell名稱
        public string CellName { get; set; }
        // Cell內容
        public string Text { get; set; }
        // 編輯者
        public string Editor { get; set; }
        // 有無上鎖
        public bool LockState { get; set; }
    }
}
