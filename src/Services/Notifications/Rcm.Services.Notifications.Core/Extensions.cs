using Genl.App;
using Genl.MassTransit;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rcm.Services.Notifications.Core.Consumers;

namespace Rcm.Services.Notifications.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApp(configuration);

        services.AddMassTransit(configuration, x =>
        {
            x.AddConsumer<UserSignedUpConsumer>()
                .Endpoint(cfg =>
                {
                    cfg.Temporary = true;
                });
        });

        return services;
    }
}