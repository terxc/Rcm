namespace Genl.Auth.JWT;
public class JwtOptions
{
    public string? SecretKey { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? ValidIssuer { get; set; }
    public string? ValidAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateLifetime { get; set; }
    public int ExpiryMinutes { get; set; }
}
