using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using LMS.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LMS.API.Configuration
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger, bool isDevelopment)
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions()
            {
                AllowStatusCode404Response = true, // important!
                ExceptionHandler = (async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var errorCode = ErrorCodes.Undefined;
                        var message = "";
                        if (contextFeature.Error is RequestException exception)
                        {
                            errorCode = exception.ErrorCode;
                            if (exception.StatusCode != null)
                                context.Response.StatusCode = (int)exception.StatusCode;
                            else
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            message = exception.Message;
                        }

                        var additionalInfo = new Dictionary<string, string>();
                        if (isDevelopment)
                        {
                            additionalInfo["DebugMessage"] = $"{contextFeature.Error?.Message}";
                            additionalInfo["DebugStackTrace"] = $"{contextFeature.Error?.StackTrace}";
                        }

                        await context.Response.WriteAsync(new ErrorResponse
                        {
                            ErrorCode = errorCode,
                            Message = message,
                            AdditionalInfo = additionalInfo
                        }.ToString());
                    }
                })
            });
        }
    }
}