using System.Threading.Tasks;

namespace ASample.NetCore.Consul
{
    public interface IConsulHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}
