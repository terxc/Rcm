using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rcm.Shared.Exceptions;
using Rcm.Shared.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rcm.Shared.Middlewares;
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly IJsonSerializer _serializer;

    public ErrorHandlerMiddleware(RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger,
        IJsonSerializer serializer)
    {
        _next = next;
        _logger = logger;
        _serializer = serializer;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleErrorAsync(context, exception);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json; charset=utf-8";

        object content = null;

        switch (exception)
        {
            case ValidationException e:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                content = new { exception.Message, e.Errors };
                break;
            case NotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case CustomException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                content = new { exception.Message };
                break;
            default:
                _logger.LogError(exception, exception.Message);
                content = new { exception.Message };
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        if (content != null)
        {
            var result = _serializer.Serialize(content);
            await response.WriteAsync(result);
        }
    }
}
