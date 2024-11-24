// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CryptoService.DTOs
{
    public record Root(
        [property: JsonPropertyName("data")] IReadOnlyList<Datum> data,
        [property: JsonPropertyName("timestamp")] long timestamp
    );
}