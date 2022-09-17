using System.Collections.Generic;

namespace Rcm.Shared.Auth;

public class JsonWebTokenPayload
{
    public string Subject { get; set; }
    public long Expires { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public IDictionary<string, IEnumerable<string>> Claims { get; set; }
}