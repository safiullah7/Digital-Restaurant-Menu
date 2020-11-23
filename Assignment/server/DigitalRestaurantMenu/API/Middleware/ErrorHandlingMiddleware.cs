using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {
            // object errors = null;
            ResponseWrapper<ErrorHandlingMiddleware> responseWrapper = null;

            switch (ex)
            {
                case RestException re:
                    logger.LogError("REST ERROR");
                    responseWrapper = ResponseWrapper<ErrorHandlingMiddleware>.GetInstance((int)re.Code, re.Errors, false, null);
                    context.Response.StatusCode = (int)re.Code;
                    break;

                case Exception e:
                    logger.LogError("SERVER ERROR");
                    // errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    responseWrapper = ResponseWrapper<ErrorHandlingMiddleware>
                        .GetInstance((int)HttpStatusCode.InternalServerError, string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message, false, null);
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            var contents = responseWrapper.Contents;
            var errorMessage = responseWrapper.ErrorMessage;
            var statusCode = responseWrapper.StatusCode;
            var hasData = responseWrapper.HasData;

            if (responseWrapper != null)
            {
                var result = JsonSerializer.Serialize(new
                {
                    contents,
                    errorMessage,
                    statusCode,
                    hasData
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}