using Genl.Auth.JWT;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Genl.Auth;
public static class Extensions
{
    private static readonly string SectionName = "jwt";

    public static void AddJwt(this IServiceCollection services)
    {
        var options = services.GetOptions<JwtOptions>(SectionName);
        if (string.IsNullOrWhiteSpace(options.SecretKey))
        {
            throw new InvalidOperationException("Missing secret key.");
        }

        services.AddSingleton(options);
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
    }
}
