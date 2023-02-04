namespace Genl.Auth.JWT;
public class JsonWebToken
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public long Expires { get; set; }
    public string UserId { get; set; } = string.Empty;
    public IEnumerable<string>? Roles { get; set; }
    public IDictionary<string, IEnumerable<string>>? Claims { get; set; }
}
