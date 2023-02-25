using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Genl.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration,
        Action<IBusRegistrationConfigurator>? configure = null)
    {
        var section = configuration.GetSection("masstransit");
        var options = section.BindOptions<MassTransitOptions>();
        services.Configure<MassTransitOptions>(section);
        if (!section.Exists())
        {
            return services;
        }

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            configure?.Invoke(x);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(options.Host, options.VirtualHost, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;

    }
}