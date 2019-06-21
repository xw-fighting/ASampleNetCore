using System.Net;

namespace ASample.NetCore.Http
{
    //public class HttpRequestResult:HttpRequestResult<string>
    //{

    //}
    public class HttpRequestResult
    {
        public bool IsError { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object Data { get; set; }
        public  string Message { get; set; }

        public static HttpRequestResult Success<T>(T data,string message) where T:class
        {
            return new HttpRequestResult
            {
                IsError = false,
                Data = data as T,
                Message = message,
                StatusCode = HttpStatusCode.OK
            };
        }

        public static HttpRequestResult Success(string message)
        {
            return new HttpRequestResult
            {
                IsError = false,
                Data = "",
                Message = message
            };
        }

        public static HttpRequestResult Error(string message,HttpStatusCode httpStatusCode)
        {
            return new HttpRequestResult
            {
                IsError = true,
                Data = "",
                Message = message,
                StatusCode = httpStatusCode
            };
        }

        public static HttpRequestResult Error(string message)
        {
            return new HttpRequestResult
            {
                IsError = true,
                Data = "",
                Message = message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}
