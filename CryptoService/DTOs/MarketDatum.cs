// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Text.Json.Serialization;

namespace CryptoService.DTOs
{
    public record MarketDatum(
        [property: JsonPropertyName("exchangeId")] string exchangeId,
        [property: JsonPropertyName("baseId")] string baseId,
        [property: JsonPropertyName("quoteId")] string quoteId,
        [property: JsonPropertyName("baseSymbol")] string baseSymbol,
        [property: JsonPropertyName("quoteSymbol")] string quoteSymbol,
        [property: JsonPropertyName("volumeUsd24Hr")] string volumeUsd24Hr,
        [property: JsonPropertyName("priceUsd")] string priceUsd,
        [property: JsonPropertyName("volumePercent")] string volumePercent
    );
}