using Genl.App;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Rcm.Services.Notifications.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApp(configuration);

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumers(Assembly.GetExecutingAssembly());

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h => {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}