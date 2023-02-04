namespace Genl.Auth.JWT;

public interface IJwtHandler
{
    JsonWebToken CreateToken(string userId, IEnumerable<string>? roles = null, IDictionary<string, IEnumerable<string>>? claims = null);
    JsonWebTokenPayload GetTokenPayload(string accessToken);
}