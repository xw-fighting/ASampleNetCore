
using System.Threading.Tasks;

namespace ASample.NetCore.EntityFramwork
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
