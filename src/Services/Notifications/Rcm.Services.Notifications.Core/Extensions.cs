using Genl.App;
using Genl.MassTransit;
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

        services.AddMassTransit(configuration, x => {
            x.AddConsumers(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}