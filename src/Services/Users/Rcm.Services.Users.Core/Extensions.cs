using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rcm.Services.Users.Core.DAL;
using Rcm.Services.Users.Core.Entities;
using Genl.Framework;
using Genl.Framework.Middlewares;
using Genl.Auth;
using Genl.Security;
using Genl.DAL.SqlServer;

namespace Rcm.Services.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services) 
    {
        services.AddGenl();
        services.AddJwt();
        services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy("UsersView", x => x.RequireClaim("permissions", Permission.UsersView, Permission.UsersEdit));
                authorization.AddPolicy("UsersEdit", x => x.RequireClaim("permissions", Permission.UsersEdit));
            });

        services.AddSqlServer<UsersDbContext>();
        services.AddInitializer<UsersDataInitializer>();

        services.AddSecurity();

        return services;
    }

    public static IApplicationBuilder UseCore(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }


}
