using CryptoService.DTOs;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoService.Services
{
    public class CoinCapApiService : ICryptoApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ConsoleLogger _logger;

        public CoinCapApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = new ConsoleLogger();
        }

        public async Task<CryptoRoot> GetCryptosAsync(string baseAddress, string requestUri)
        {
            try
            {
                var fullUrl = new Uri(new Uri(baseAddress), requestUri);
                _logger.Info($"Sending request to {fullUrl}...");
                var response = await _httpClient.GetStringAsync(fullUrl);

                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("API response is empty.");
                }

                _logger.Info("API response received, deserializing...");
                return JsonSerializer.Deserialize<CryptoRoot>(response);
            }
            catch (HttpRequestException httpEx)
            {
                _logger.Error("Error occurred while making an HTTP request.", httpEx);
                throw new Exception("Error occurred while making an HTTP request.", httpEx);
            }
            catch (JsonException jsonEx)
            {
                _logger.Error("Error occurred while deserializing API response.", jsonEx);
                throw new Exception("Error occurred while deserializing API response.", jsonEx);
            }
            catch (Exception ex)
            {
                _logger.Error("An unexpected error occurred while fetching data from the API.", ex);
                throw new Exception("An unexpected error occurred while fetching data from the API.", ex);
            }
        }

        public async Task<MarketRoot> GetCryptoMarketsAsync(string baseAddress, string cryptoId)
        {
            try
            {
                string requestUri = $"/v2/assets/{cryptoId}/markets";
                var fullUrl = new Uri(new Uri(baseAddress), requestUri);
                _logger.Info($"Sending request to {fullUrl} for crypto {cryptoId} markets...");
                var response = await _httpClient.GetStringAsync(fullUrl);

                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("Empty response from API.");
                }

                _logger.Info("API response received, deserializing...");
                return JsonSerializer.Deserialize<MarketRoot>(response);
            }
            catch (HttpRequestException httpEx)
            {
                _logger.Error($"HTTP request error for {cryptoId} markets: {httpEx.Message}", httpEx);
                throw new Exception($"HTTP request error: {httpEx.Message}", httpEx);
            }
            catch (JsonException jsonEx)
            {
                _logger.Error($"JSON deserialization error for {cryptoId} markets: {jsonEx.Message}", jsonEx);
                throw new Exception($"JSON deserialization error: {jsonEx.Message}", jsonEx);
            }
            catch (Exception ex)
            {
                _logger.Error($"Unexpected error while fetching markets for {cryptoId}: {ex.Message}", ex);
                throw new Exception($"Unexpected error: {ex.Message}", ex);
            }
        }
    }
}
