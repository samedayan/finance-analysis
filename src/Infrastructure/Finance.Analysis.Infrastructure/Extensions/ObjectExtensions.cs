using System.Text.Json;
using System.Text.Json.Serialization;

namespace Finance.Analysis.Infrastructure.Extensions;

public static class ObjectExtensions
{
    public static string AsJson(this object? source)
    {
        if (source is null) return string.Empty;

        return JsonSerializer.Serialize(source, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        });
    }
}