using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace ASample.NetCore.Log.Test
{
    [TestClass]
    public class LogTest
    {
        private readonly ILogger _logger;
        [TestMethod]
        public void WriteLog()
        {
            //var log = new LoggerConfiguration()
            //    .WriteTo.MongoDB("mongodb://localhost:27017/admin")
            //    .CreateLogger();

            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            log.Warning("ÄãºÃ");
        }
    }
}
