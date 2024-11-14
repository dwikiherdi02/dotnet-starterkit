

using System.Net;

namespace Apps.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;

        public HttpResponseException(string message) : base(message) 
        {}

        public HttpResponseException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}