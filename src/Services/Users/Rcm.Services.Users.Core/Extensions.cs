using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rcm.Services.Users.Core.DAL;
using Rcm.Shared.Common;

namespace Rcm.Services.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlServer(services.GetOptions<SqlServerOptions>("sqlserver").ConnectionString));

        return services;
    }
}
