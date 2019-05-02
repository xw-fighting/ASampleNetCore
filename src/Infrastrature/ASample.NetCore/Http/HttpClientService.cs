using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ASample.NetCore.Http
{
    public class HttpClientService
    {
        private  static IHttpClientFactory _httpClientFactory;
        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public static async Task<HttpRequestResult> PostAsync<T>(string url,string content)
        {
            var client = _httpClientFactory.CreateClient();
            var httpContent = new StringContent(content);
            var response = await client.PostAsync(url, httpContent);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
                return HttpRequestResult.Success(result, "");
            else
                return HttpRequestResult.Error(result, response.StatusCode);
        }

        public static async Task<HttpRequestResult> GetAsync(string url)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
                return HttpRequestResult.Success(result, "");
            else
                return HttpRequestResult.Error(result, response.StatusCode);
        }



       
    }
}
