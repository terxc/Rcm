using Microsoft.Extensions.DependencyInjection;

namespace Genl.Framework.App;

public static class Extensions
{
    private static readonly string SectionName = "app";
    public static void AddApp(this IServiceCollection services)
    {
        var options = services.GetOptions<AppOptions>(SectionName);
        var appInfo = new AppInfo(options.Name, options.Version, options.Project);
        services.AddSingleton(appInfo);

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
