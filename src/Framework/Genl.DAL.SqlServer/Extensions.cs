using Genl.DAL.SqlServer.Initializers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Genl.DAL.SqlServer;

public static class Extensions
{
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        var section = configuration.GetSection("sqlserver");
        var options = section.BindOptions<SqlServerOptions>();
        services.Configure<SqlServerOptions>(section);
        if (!section.Exists())
        {
            return services;
        }

        services.AddDbContext<T>(x => x.UseSqlServer(options.ConnectionString));
        services.AddHostedService<DatabaseInitializer<T>>();
        services.AddHostedService<DataInitializer>();

        return services;

    }

    public static void AddInitializer<T>(this IServiceCollection services) where T : class, IDataInitializer
    {
        services.AddTransient<IDataInitializer, T>();
    }
}