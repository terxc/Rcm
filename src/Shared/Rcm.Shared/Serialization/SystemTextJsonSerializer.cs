using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Rcm.Shared.Serialization;
public class SystemTextJsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions _options;

    public SystemTextJsonSerializer(JsonSerializerOptions options = null)
    {
        _options = options ?? new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
    }

    public string Serialize<T>(T value) => JsonSerializer.Serialize(value, _options);

    public T Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value, _options);
}
