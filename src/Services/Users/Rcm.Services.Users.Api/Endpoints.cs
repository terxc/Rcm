using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Rcm.Services.Users.Core.Commands;
using Rcm.Services.Users.Core.Queries;
using Rcm.Shared.MediatR;

namespace Rcm.Services.Users.Api;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var appName = app.Configuration["app:name"];
        app.MapGet("/", () => appName);

        app.MediatePost<SignInCommand>("api/account/sign-in");
        app.MediatePost<SignUpCommand>("api/account/sign-up");

        app.MediateGet<GetUserQuery>("api/users/{userId}");
    }
}
