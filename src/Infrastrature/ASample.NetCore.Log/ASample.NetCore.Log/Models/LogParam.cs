
namespace ASample.NetCore.Log.Models
{
    public class LogParam
    {
        public LogType LogType { get; set; }

        public string LogContent { get; set; }

        public string UserAction { get; set; }

        public string LogFileName { get; set; }
    }
}
