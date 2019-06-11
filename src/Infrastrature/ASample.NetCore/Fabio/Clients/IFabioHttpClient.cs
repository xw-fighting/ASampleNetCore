using System.Threading.Tasks;

namespace ASample.NetCore.Fabio
{
    public interface IFabioHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}
