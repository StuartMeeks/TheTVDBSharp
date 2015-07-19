using System;
using System.Net;

namespace TheTVDBSharp
{
    public class TheTvDbBadResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public TheTvDbBadResponseException(HttpStatusCode statusCode, string message = "Bad response", Exception innerException = null)
            : base(message, innerException)

        {
            StatusCode = statusCode;
        }
    }
}
