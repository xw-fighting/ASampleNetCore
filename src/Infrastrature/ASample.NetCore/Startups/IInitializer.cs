using System.Threading.Tasks;

namespace ASample.NetCore.Startups
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
