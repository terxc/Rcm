using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Genl.App;
public static class Extensions
{
    public static IServiceCollection AddApp(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("app").BindOptions<AppOptions>();

        services.AddSingleton(options);

        if (!string.IsNullOrWhiteSpace(options.Name))
        {
            Console.WriteLine(Figgle.FiggleFonts.Slant.Render($"{options.Name} {options.Version}"));
        }

        return services;
    }
}
