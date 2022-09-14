using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using System.Text.Encodings.Web;

namespace Rcm.Shared.Serialization;
public class SystemTextJsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() },
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

    public string Serialize<T>(T value) => JsonSerializer.Serialize(value, _options);

    public T Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value, _options);
}
