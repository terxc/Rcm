using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Genl.Security;

public static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("security");
        services.Configure<SecurityOptions>(section);

        services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();

        return services;
    }
}