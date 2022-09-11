using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Rcm.Shared.MediatR;
public static class Extensions
{
    public static IEndpointRouteBuilder MediateGet<TRequest>(this IEndpointRouteBuilder app, string template) where TRequest : notnull
    {
        app.MapGet(template, async (ISender mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        return app;
    }

    public static IEndpointRouteBuilder MediatePost<TRequest>(this IEndpointRouteBuilder app, string template) where TRequest : notnull
    {
        app.MapPost(template, async (ISender mediator, TRequest request) => await mediator.Send(request));
        return app;
    }
}
