using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ApiResponse
{
    [JsonPropertyName("data")]
    public List<Cryptocurrency> Data { get; set; }
}

public class Cryptocurrency
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("priceUsd")]
    public string PriceUsd { get; set; }

    [JsonPropertyName("marketCapUsd")]
    public string MarketCapUsd { get; set; }

    [JsonPropertyName("volumeUsd24Hr")]
    public string VolumeUsd24Hr { get; set; }

    [JsonPropertyName("changePercent24Hr")]
    public string ChangePercent24Hr { get; set; }
}
