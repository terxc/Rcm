using FluentValidation;
using Genl.App;
using Genl.Auth;
using Genl.Framework.Behaviours;
using Genl.Security;
using Genl.Serialization;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Genl.Framework;

public static class Extensions
{
    public static void AddGenl(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApp(configuration);
        services.AddJwt(configuration);
        services.AddSecurity(configuration);
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
        services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());
        services.AddMediatR(Assembly.GetCallingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}