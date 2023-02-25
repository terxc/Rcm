using Genl.DAL.SqlServer;
using Genl.Framework;
using Genl.Framework.Middlewares;
using Genl.MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rcm.Services.Users.Core.Constants;
using Rcm.Services.Users.Core.DAL;
using Rcm.Services.Users.Core.Entities;

namespace Rcm.Services.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGenl(configuration);
        services.AddAuthorization(authorization =>
        {
                authorization.AddPolicy(Policy.UsersView, x => x.RequireClaim("permissions", Permission.UsersView, Permission.UsersEdit));
                authorization.AddPolicy(Policy.UsersEdit, x => x.RequireClaim("permissions", Permission.UsersEdit));
            });

        services.AddSqlServer<UsersDbContext>(configuration);
        services.AddInitializer<UsersDataInitializer>();

        services.AddMassTransit(configuration);

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
