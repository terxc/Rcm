using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Genl;

public static class Extensions
{
    public static T BindOptions<T>(this IConfigurationSection section) where T : new()
    {
        var options = new T();
        section.Bind(options);
        return options;
    }

    public static T BindOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        => BindOptions<T>(configuration.GetSection(sectionName));

    public static long ToTimestamp(this DateTime dateTime) => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
}
