namespace ASample.NetCore.Ftps.Options
{
    public class FtpClientOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RemoteDir { get; set; }
        public string LocalDir { get; set; }
    }
}
