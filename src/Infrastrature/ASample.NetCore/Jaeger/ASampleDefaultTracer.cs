using OpenTracing;
using System.Reflection;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;


namespace ASample.NetCore.Jaeger
{
    public class ASampleDefaultTracer
    {
        public static ITracer Create()
            => new Tracer.Builder(Assembly.GetEntryAssembly().FullName)
                .WithReporter(new NoopReporter())
                .WithSampler(new ConstSampler(false))
                .Build();
    }
}
