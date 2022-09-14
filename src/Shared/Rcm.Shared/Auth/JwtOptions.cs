namespace Rcm.Shared.Auth;
public class JwtOptions
{
    public string Issuer { get; set; }
    public string SecretKey { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateLifetime { get; set; }
    public int ExpiryMinutes { get; set; }
}
