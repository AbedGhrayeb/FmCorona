using Application.Common.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class ErrorHandlingMeddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMeddleware> _logger;
        public ErrorHandlingMeddleware(RequestDelegate next, ILogger<ErrorHandlingMeddleware> logger)
        {
            _logger = logger;
            _next = next;

        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context,ex,_logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMeddleware> logger)
        {
            object errors=null;
            switch (ex)
            {
                case RestException re:
                    logger.LogError(ex,"REST ERROR");
                    errors=re.Error;
                    context.Response.StatusCode=(int)re.Code;
                break;

                case Exception e:
                logger.LogError(e,"SERVER ERROR");
                errors=String.IsNullOrWhiteSpace(e.Message) ? "Error":e.Message;
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                break;
            }
            context.Response.ContentType="application/json";
            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(new{
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}