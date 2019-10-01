using System;
using System.Net;

namespace SpeedUpCoreAPIExample.Exceptions
{
    // Custom Http Exception
    public class HttpException : Exception
    {
        // Holds Http status code: 404 NotFound, 400 BadRequest, ...
        public int StatusCode { get; }
        public string MessageDetail { get; set; }

        public HttpException(HttpStatusCode statusCode, string message = null, string messageDetail = null) : base(message)
        {
            StatusCode = (int)statusCode;
            MessageDetail = messageDetail;
        }
    }
}
