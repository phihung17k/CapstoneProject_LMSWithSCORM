using System;
using System.Net;

namespace LMS.Infrastructure.Exceptions
{
    public class RequestException : Exception
    {
        public RequestException(string errorCode = ErrorCodes.Undefined,
            string message = null) : this(HttpStatusCode.BadRequest, errorCode, message)
        {
        }

        public RequestException(
            HttpStatusCode statusCode,
            string errorCode = ErrorCodes.Undefined,
            string message = null) :
            base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public HttpStatusCode? StatusCode { get; }
        public string ErrorCode { get; }
    }
}