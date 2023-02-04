using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Rcm.Shared.MediatR;
public static class Extensions
{
    public static IEndpointRouteBuilder MediateGet<TRequest>(this IEndpointRouteBuilder app,
        string template, int statusCode = StatusCodes.Status200OK, IEnumerable<string>? policyNames = null) where TRequest : notnull
    {
        var builder = app.MapGet(template, async (ISender mediator, [AsParameters] TRequest request, HttpResponse response) =>
        {
            response.StatusCode = statusCode;
            await response.WriteAsJsonAsync(await mediator.Send(request));
        });
        if (policyNames != null)
            builder.RequireAuthorization(policyNames.ToArray());
        return app;
    }

    public static IEndpointRouteBuilder MediatePost<TRequest>(this IEndpointRouteBuilder app,
        string template, int statusCode = StatusCodes.Status200OK, IEnumerable<string>? policyNames = null) where TRequest : notnull
    {
        var builder = app.MapPost(template, async (ISender mediator, TRequest request, HttpResponse response) =>
        {
            response.StatusCode = statusCode;
            await response.WriteAsJsonAsync(await mediator.Send(request));
        });
        if (policyNames != null)
            builder.RequireAuthorization(policyNames.ToArray());
        return app;
    }

    public static IEndpointRouteBuilder MediatePut<TRequest>(this IEndpointRouteBuilder app,
        string template, int statusCode = StatusCodes.Status204NoContent, IEnumerable<string>? policyNames = null) where TRequest : notnull
    {
        var builder = app.MapPut(template, async (ISender mediator, TRequest request, HttpResponse response) =>
        {
            response.StatusCode = statusCode;
            await response.WriteAsJsonAsync(await mediator.Send(request));
        });
        if (policyNames != null)
            builder.RequireAuthorization(policyNames.ToArray());
        return app;
    }

    public static IEndpointRouteBuilder MediateDelete<TRequest>(this IEndpointRouteBuilder app,
        string template, int statusCode = StatusCodes.Status204NoContent, IEnumerable<string>? policyNames = null) where TRequest : notnull
    {
        var builder = app.MapDelete(template, async (ISender mediator, TRequest request, HttpResponse response) =>
        {
            response.StatusCode = statusCode;
            await response.WriteAsJsonAsync(await mediator.Send(request));
        });
        if (policyNames != null)
            builder.RequireAuthorization(policyNames.ToArray());
        return app;
    }
}
