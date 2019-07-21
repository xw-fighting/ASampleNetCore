using Newtonsoft.Json;
using System.Net;

namespace ASample.NetCore
{
    public class ApiRequestResult
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string StatusCode { get; set; }
        public static ApiRequestResult Success(object list, string message)
        {
            return new ApiRequestResult()
            {
                IsError = false,
                Data = list,
                StatusCode = HttpStatusCode.OK.ToString(),
                Message = message
            };
        }

        public static ApiRequestResult Success(string message)
        {
            return new ApiRequestResult()
            {
                IsError = false,
                StatusCode = HttpStatusCode.OK.ToString(),
                Message = message
            };
        }

        public static ApiRequestResult Error(string message)
        {
            return new ApiRequestResult()
            {
                IsError = true,
                Data = null,
                Message = message,
                StatusCode = HttpStatusCode.OK.ToString()
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
