using FluentValidation;
using Genl.Framework.Behaviours;
using Genl.Serialization;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Genl.Framework;

public static class Extensions
{
    public static void AddGenl(this IServiceCollection services)
    {
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();

        services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());
        services.AddMediatR(Assembly.GetCallingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}