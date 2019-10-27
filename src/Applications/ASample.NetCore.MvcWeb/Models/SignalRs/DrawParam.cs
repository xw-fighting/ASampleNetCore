namespace ASample.NetCore.MvcWeb.Models.SignalRs
{
    public class DrawParam
    {
        public string Mode { get; set; }
        public int[] StartPos { get; set; }
        public int[] EndPos { get; set; }
        public string Color { get; set; }
        public int LineWidth { get; set; }
        public int EraserWidth { get; set; }
    }
}
