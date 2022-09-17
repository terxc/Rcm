using System.Collections.Generic;

namespace Rcm.Shared.Auth;
public class JsonWebToken
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public long Expires { get; set; }
    public string Id { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public IDictionary<string, IEnumerable<string>> Claims { get; set; }
}
