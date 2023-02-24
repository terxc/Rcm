using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Rcm.Services.Users.Core.DAL;
using Rcm.Services.Users.Core.Entities;
using Genl.Framework;
using Genl.Framework.Middlewares;
using Genl.App;
using Genl.Auth;
using Genl.Security;
using Genl.DAL.SqlServer;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Rcm.Services.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApp(configuration);
        services.AddGenl();
        services.AddJwt(configuration);
        services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy("UsersView", x => x.RequireClaim("permissions", Permission.UsersView, Permission.UsersEdit));
                authorization.AddPolicy("UsersEdit", x => x.RequireClaim("permissions", Permission.UsersEdit));
            });

        services.AddSqlServer<UsersDbContext>(configuration);
        services.AddInitializer<UsersDataInitializer>();

        services.AddSecurity(configuration);

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });

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
