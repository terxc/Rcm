using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rcm.Services.Users.Core.DAL;
using Rcm.Shared;
using Rcm.Shared.Auth;
using Rcm.Shared.Behaviours;
using Rcm.Shared.Middlewares;
using Rcm.Shared.Serialization;

namespace Rcm.Services.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddJwt();
        services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy("users", x => x.RequireClaim("permissions", "users"));
            });
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


        services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlServer(services.GetOptions<SqlServerOptions>("sqlserver").ConnectionString));

        return services;
    }

    public static IApplicationBuilder UseCore(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseAuthentication();

        return app;
    }


}
