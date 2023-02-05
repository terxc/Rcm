using Genl.DAL.SqlServer.Initializers;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Genl.DAL.SqlServer;

public static class Extensions
{
    public static void AddSqlServer<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<SqlServerOptions>("sqlserver");

        services.AddSingleton(options);
        services.AddDbContext<T>(x => x.UseSqlServer(options.ConnectionString));
        services.AddHostedService<DatabaseInitializer<T>>();
        services.AddHostedService<DataInitializer>();
    }

    public static void AddInitializer<T>(this IServiceCollection services) where T : class, IDataInitializer
    {
        services.AddTransient<IDataInitializer, T>();
    }
}