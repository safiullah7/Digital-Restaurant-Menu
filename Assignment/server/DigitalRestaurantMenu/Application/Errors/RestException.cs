using System;
using System.Net;

namespace Application.Errors
{
    public class RestException: Exception
    {
        public string Errors { get; set; }
        public HttpStatusCode Code { get; set; }
        public RestException(HttpStatusCode code, string errors = null)
        {
            this.Code = code;
            this.Errors = errors;
        }
    }
}