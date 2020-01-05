using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ASample.NetCore.Http
{
    public interface IASampleHttpClient
    {
        Task<TOutResult> PostAsync<TOutResult>(string url, string content, DeserializeType deserializeType = DeserializeType.JsonDeserialize, X509Certificate2 cert = null) where TOutResult : class,new();
        Task<string> PostAsync(string url, string content);
        Task<string> PostAsync(string url, IFormFile formFile);
        Task<TOutResult> GetAsync<TOutResult>(string url, DeserializeType deserializeType = DeserializeType.JsonDeserialize) where TOutResult : class,new();
        Task<string> GetAsync(string url);
        Task<string> DeleteAsync(string url, string content);
    }
}
