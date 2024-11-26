// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CryptoService.DTOs
{
    public record MarketRoot(
        [property: JsonPropertyName("data")] IReadOnlyList<MarketDatum> data,
        [property: JsonPropertyName("timestamp")] long timestamp
    );
}