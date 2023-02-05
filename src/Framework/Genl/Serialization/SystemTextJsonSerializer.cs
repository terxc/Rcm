using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace Genl.Serialization;
public class SystemTextJsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        //DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        //Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public string Serialize<T>(T value) => JsonSerializer.Serialize(value, _options);
    public T? Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value, _options);
}
