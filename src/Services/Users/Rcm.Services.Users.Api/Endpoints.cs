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

        app.MediatePost<SignUpCommand>("api/account/sign-up", StatusCodes.Status204NoContent);
        app.MediatePost<SignInCommand>("api/account/sign-in");

        app.MediateGet<GetUserQuery>("api/users/{id}", policyNames: new[] { "UsersView" });
        app.MediatePut<UpdateUserCommand>("api/users/{id}", policyNames: new[] { "UsersEdit" });
    }
}
