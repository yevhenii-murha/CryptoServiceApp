// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Text.Json.Serialization;

namespace CryptoService.DTOs
{
    public record Datum(
        [property: JsonPropertyName("id")] string id,
        [property: JsonPropertyName("rank")] string rank,
        [property: JsonPropertyName("symbol")] string symbol,
        [property: JsonPropertyName("name")] string name,
        [property: JsonPropertyName("supply")] string supply,
        [property: JsonPropertyName("maxSupply")] string maxSupply,
        [property: JsonPropertyName("marketCapUsd")] string marketCapUsd,
        [property: JsonPropertyName("volumeUsd24Hr")] string volumeUsd24Hr,
        [property: JsonPropertyName("priceUsd")] string priceUsd,
        [property: JsonPropertyName("changePercent24Hr")] string changePercent24Hr,
        [property: JsonPropertyName("vwap24Hr")] string vwap24Hr,
        [property: JsonPropertyName("explorer")] string explorer
    );
}