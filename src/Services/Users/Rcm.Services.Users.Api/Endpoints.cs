using Genl.Framework;
using Genl.Framework.Mediating;
using Rcm.Services.Users.Core.Commands;
using Rcm.Services.Users.Core.Queries;

namespace Rcm.Services.Users.Api;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGet("/", (AppOptions options) => options.AppInfo);

        app.MediatePost<SignUpCommand>("api/account/sign-up", StatusCodes.Status204NoContent);
        app.MediatePost<SignInCommand>("api/account/sign-in");

        app.MediateGet<GetUserQuery>("api/users/{id}", policyNames: new[] { "UsersView" });
        app.MediatePut<UpdateUserCommand>("api/users/{id}", policyNames: new[] { "UsersEdit" });
    }
}
