using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rcm.Shared;

public static class Extensions
{
    public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName) where TModel : new()
    {
        var model = new TModel();
        configuration.GetSection(sectionName).Bind(model);
        return model;
    }

    public static TModel GetOptions<TModel>(this IServiceCollection services, string sectionName) where TModel : new()
    {
        using (ServiceProvider provider = services.BuildServiceProvider())
        {
            return provider.GetRequiredService<IConfiguration>().GetOptions<TModel>(sectionName);
        }
    }
}
