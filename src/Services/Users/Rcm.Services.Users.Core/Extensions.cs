using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rcm.Services.Users.Core.DAL;
using Rcm.Services.Users.Core.Entities;
using Genl.Auth;
using Rcm.Shared.Middlewares;
using Genl;
using Genl.Serialization;
using Genl.Framework.Behaviours;
using Genl.Framework.App;

namespace Rcm.Services.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddApp();
        services.AddJwt();
        services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy("UsersView", x => x.RequireClaim("permissions", Permission.UsersView, Permission.UsersEdit));
                authorization.AddPolicy("UsersEdit", x => x.RequireClaim("permissions", Permission.UsersEdit));
            });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlServer(services.GetOptions<SqlServerOptions>("sqlserver").ConnectionString));
        services.AddTransient<UsersInitializer>();

        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
        services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();

        return services;
    }

    public static IApplicationBuilder UseCore(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        using (var scope = app.ApplicationServices.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<UsersInitializer>();
            initializer.InitAsync().GetAwaiter().GetResult();
        }

        return app;
    }


}
