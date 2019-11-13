using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eRoom.Shared.Api.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHostingEnvironment env;
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        public ErrorHandlingMiddleware(RequestDelegate _next, IHostingEnvironment _env, ILogger<ErrorHandlingMiddleware> _logger)
        {

            next = _next;
            env = _env;
            logger = _logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            var errorMsg = "";
            Exception exception = new Exception($"Exception occurred.!");
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                errorMsg = GetErrorResponse(ex);
                exception = ex;

                if (!httpContext.Response.HasStarted)
                {
                    logger.LogError("Exception occurred at {name} {stacktrace} {inner}", httpContext.Request.Path, string.IsNullOrEmpty(ex.StackTrace) ? "" : ex.StackTrace, ex.InnerException == null ? "" : ex.InnerException.ToString());
                    httpContext.Response.ContentType = "application/json";
                    var traceId = httpContext.TraceIdentifier.Replace(":", "");
                    ApiErrorResponse apiResponse = new ApiErrorResponse(new ApiResponse{ResponseCode = 500, ResponseDesc = errorMsg }, new { traceId = traceId, code = (int)HttpStatusCode.InternalServerError, message = errorMsg });
                    if (env.IsDevelopment())
                    {
                        apiResponse.Result = new { traceId = traceId, message = ex.Message, stackTrace = ex.StackTrace };
                    }
                    // will update the custom API exception soon
                    var json = JsonConvert.SerializeObject(apiResponse);
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await httpContext.Response.WriteAsync(json);
                }
            }

        }

        private static string GetErrorResponse(Exception e)
        {
            string message = e.Message;
            string innerMessage = e.InnerException == null ? "" : e.InnerException.Message;
            string innerInnerMessage = string.Empty;
            if (e.InnerException != null && e.InnerException.InnerException != null && e.InnerException.InnerException != null)
            {
                innerInnerMessage = e.InnerException.InnerException.Message;
            }
            string stackTrace = e.StackTrace == null ? "" : e.StackTrace.ToString();
            string errorMessage = string.Format("{0} {1} {2} {3}", message, innerMessage, innerInnerMessage, stackTrace);
            return errorMessage;

        }
    }
}
