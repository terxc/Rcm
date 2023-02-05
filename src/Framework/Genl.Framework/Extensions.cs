using FluentValidation;
using Genl.Framework.Behaviours;
using Genl.Serialization;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Genl.Framework;

public static class Extensions
{
    public static void AddGenl(this IServiceCollection services)
    {
        var options = services.GetOptions<AppOptions>("app");

        services.AddSingleton(options);
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();

        services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());
        services.AddMediatR(Assembly.GetCallingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        RenderLogo(options);
    }

    private static void RenderLogo(AppOptions app)
    {
        if (string.IsNullOrWhiteSpace(app.Name))
        {
            return;
        }

        Console.WriteLine(Figgle.FiggleFonts.Slant.Render($"{app.Name} {app.Version}"));
    }
}