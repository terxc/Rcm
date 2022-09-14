using System.Collections.Generic;

namespace Rcm.Shared.Auth;

public interface IJwtHandler
{
    JsonWebToken CreateToken(string userId, string role = null, IDictionary<string, string> claims = null);
    JsonWebTokenPayload GetTokenPayload(string accessToken);
}