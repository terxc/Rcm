using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Genl.Security;

public static class Extensions
{
    public static void AddSecurity(this IServiceCollection services)
    {
        var options = services.GetOptions<SecurityOptions>("security");

        services.AddSingleton(options);
        services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();
    }
}