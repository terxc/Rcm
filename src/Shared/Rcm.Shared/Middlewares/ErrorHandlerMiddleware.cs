using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rcm.Shared.Exceptions;
using Rcm.Shared.Serialization;

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

        var errors = new List<string>();

        switch (exception)
        {
            case ValidationException e:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errors = e.Errors;
                break;
            case CustomException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                _logger.LogError(exception, exception.Message);
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var result = _serializer.Serialize(new { Message = exception.Message, Errors = errors });
        await response.WriteAsync(result);
    }
}
