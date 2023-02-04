namespace Rcm.Shared.Auth;

public interface IJwtHandler
{
    JsonWebToken CreateToken(string userId, IEnumerable<string>? roles = null, IDictionary<string, IEnumerable<string>>? claims = null);
    JsonWebTokenPayload GetTokenPayload(string accessToken);
}