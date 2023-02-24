using Genl.Auth.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Genl.Auth;
public static class Extensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("jwt");
        var options = section.BindOptions<JwtOptions>();
        services.Configure<JwtOptions>(section);

        if (!section.Exists())
        {
            return services;
        }


        if (string.IsNullOrWhiteSpace(options.SecretKey))
        {
            throw new InvalidOperationException("Missing secret key.");
        }

        services.AddSingleton<IJwtHandler, JwtHandler>();
        services.AddAuthentication()
            .AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                    ValidIssuer = options.ValidIssuer,
                    ValidAudience = options.ValidAudience,
                    ValidateIssuer = options.ValidateIssuer,
                    ValidateAudience = options.ValidateAudience,
                    ValidateLifetime = options.ValidateLifetime,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
