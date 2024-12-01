// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CryptoService.DTOs
{
    public record CryptoRoot(
        [property: JsonPropertyName("data")] IReadOnlyList<CryptoDatum> data,
        [property: JsonPropertyName("timestamp")] long timestamp
    );
}