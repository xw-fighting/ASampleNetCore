using ASample.NetCore.HttpClient.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ASample.NetCore.HttpClient
{
    public class HttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpRequestResult> PostAsync<T>(string url,string content)
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

        public async Task<HttpRequestResult> GetAsync<T>(string url)
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
