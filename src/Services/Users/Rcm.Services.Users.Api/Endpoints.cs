using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Rcm.Services.Users.Core.Commands.SignUp;
using Rcm.Services.Users.Core.Queries.GetUser;
using Rcm.Shared.MediatR;

namespace Rcm.Services.Users.Api;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var appName = app.Configuration["app:name"];
        app.MapGet("/", () => appName);

        //app.MapGet("api/users/{userId}", async (ISender mediator, [AsParameters] GetUserQuery query) => await mediator.Send(query));
        //app.MapPost("api/account/sign-up", async (ISender mediator, SignUpCommand command) => await mediator.Send(command));

        app.MediateGet<GetUserQuery>("api/users/{userId}");
        app.MediatePost<SignUpCommand>("api/account/sign-up");
    }
}
