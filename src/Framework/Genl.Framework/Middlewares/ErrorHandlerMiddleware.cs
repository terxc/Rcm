using Genl.Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Rcm.Shared.Middlewares;
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger)
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
        catch (Exception exception)
        {
            await HandleErrorAsync(context, exception);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;

        object? content = null;

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
            await response.WriteAsJsonAsync(content);
        }
    }
}
