using Genl.Framework;
using Genl.Framework.Middlewares;
using Genl.MassTransit;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rcm.Services.Navigation.Core.Consumers;
using System.Reflection;

namespace Rcm.Services.Navigation.Core;
public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGenl(configuration);

        services.AddMassTransit(configuration, x =>
        {
            x.AddConsumer<UserSignedUpConsumer, UserSignedUpConsumerDefinition>()
                .Endpoint(cfg =>
                {
                    cfg.Temporary = true;
                });
        });

        return services;
    }

    public static IApplicationBuilder UseCore(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseAuthentication();

        return app;
    }
}
