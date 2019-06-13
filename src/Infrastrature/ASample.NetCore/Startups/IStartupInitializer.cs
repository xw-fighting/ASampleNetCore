
namespace ASample.NetCore.Startups
{
    public interface IStartupInitializer
    {
        void AddInitializer(IInitializer initializer);
    }
}
