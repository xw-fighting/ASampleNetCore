using RawRabbit.Configuration;

namespace ASample.NetCore.RabbitMq.Options
{
    public class RabbitMqOptions: RawRabbitConfiguration
    {
        public string Namespace { get; set; }
        public int Retries { get; set; }
        public int RetryInterval { get; set; }
    }
}
