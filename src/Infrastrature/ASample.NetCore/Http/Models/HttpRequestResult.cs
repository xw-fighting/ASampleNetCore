using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ASample.NetCore.Http
{
    public class HttpRequestResult
    {
        public bool IsError { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public  string Data { get; set; }
        public  string Message { get; set; }

        public static HttpRequestResult Success(string data,string message)
        {
            return new HttpRequestResult
            {
                IsError = false,
                Data = data,
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
    }
}
