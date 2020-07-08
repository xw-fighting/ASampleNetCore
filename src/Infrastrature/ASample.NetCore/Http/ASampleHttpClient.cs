using ASample.NetCore.Extension;
using ASample.NetCore.Serialize;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ASample.NetCore.Http
{
    public class ASampleHttpClient:IASampleHttpClient
    {
        private  readonly  IHttpClientFactory _httpClientFactory;
        public ASampleHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public  async Task<TOutResult> PostAsync<TOutResult>(string url,string content,DeserializeType deserializeType = DeserializeType.JsonDeserialize, X509Certificate2 cert = null) where TOutResult:class ,new()
        {
            var client = _httpClientFactory.CreateClient();
            //HttpResponseMessage sendCertResponse = new HttpResponseMessage();
            if (cert != null)
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get,
                };
                request.Headers.Add("X-ARR-ClientCert", cert.GetRawCertDataString());
                var sendCertResponse = await client.SendAsync(request);
                if (!sendCertResponse.IsSuccessStatusCode)
                    throw new ASampleException("certificate is error");
            }

            HttpContent httpContent = null;
            if(!string.IsNullOrEmpty(content))
                httpContent = new StringContent(content);

            var response = await client.PostAsync(url, httpContent);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var resultStr = await response.Content.ReadAsStringAsync();
            var result = new TOutResult();
            switch (deserializeType)
            {
                case DeserializeType.JsonDeserialize:
                    result = JsonConvert.DeserializeObject<TOutResult>(resultStr);
                    break;
                case DeserializeType.XmlDeserialize:
                    var xmlSerialize = new XmlSerialize();
                    result = xmlSerialize.Deserialize<TOutResult>(resultStr);
                    break;
                case DeserializeType.Default:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(deserializeType), deserializeType, null);
            }
            return result;
        }

        public async Task<string> PostAsync(string url, string content)
        {
            var client = _httpClientFactory.CreateClient();
            HttpContent httpContent = null;
            if (!string.IsNullOrEmpty(content))
                httpContent = new StringContent(content);

            var response = await client.PostAsync(url, httpContent);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var resultStr = await response.Content.ReadAsStringAsync();
            return resultStr;
        }

        public async Task<string> PostAsync(string url, IFormFile file)
        {
            var client = _httpClientFactory.CreateClient();
            var httpContent = new MultipartFormDataContent();

            var byteArr = await file.GetBytesAsync();
            var imageContent = new ByteArrayContent(byteArr);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            httpContent.Add(imageContent, "image", "image.jpg");
            var response = await client.PostAsync(url, httpContent);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var resultStr = await response.Content.ReadAsStringAsync();
            return resultStr;
        }

        //public async Task<string> PostAsync(string url, List<IFormFile> files)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    var httpContent = new MultipartFormDataContent();
        //    if (files != null && files.Count > 0)
                
        //    foreach (var file in files)
        //    {
        //        var byteArr = await file.GetBytesAsync();
        //        var imageContent = new ByteArrayContent(byteArr);
        //        imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
        //    }
        //    var response = await client.PostAsync(url, httpContent);
        //    if (response.StatusCode != HttpStatusCode.OK)
        //        return null;
        //    var resultStr = await response.Content.ReadAsStringAsync();
        //    return resultStr;
        //}

        public async Task<TOutResult> GetAsync<TOutResult>(string url, DeserializeType deserializeType = DeserializeType.JsonDeserialize) where TOutResult:class,new()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var resultStr = await response.Content.ReadAsStringAsync();
            var result = new TOutResult();
            switch (deserializeType)
            {
                case DeserializeType.JsonDeserialize:
                    result = JsonConvert.DeserializeObject<TOutResult>(resultStr);
                    break;
                case DeserializeType.XmlDeserialize:
                    var xmlSerialize = new XmlSerialize();
                    result = xmlSerialize.Deserialize<TOutResult>(resultStr);
                    break;
                case DeserializeType.Default:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(deserializeType), deserializeType, null);
            }
            return result;
        }

        public async Task<string> GetAsync(string url) 
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var resultStr = await response.Content.ReadAsStringAsync();
            return resultStr;
        }

        public async Task<string> DeleteAsync(string url,string content)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.DeleteAsync(url+"/"+ content);
            if (response.StatusCode != HttpStatusCode.OK)
                return string.Empty;
            var resultStr = await response.Content.ReadAsStringAsync();
            return resultStr;
        }
    }
}
