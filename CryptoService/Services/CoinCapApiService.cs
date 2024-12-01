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

        public CoinCapApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CryptoRoot> GetCryptosAsync(string baseAddress, string requestUri)
        {
            try
            {
                var fullUrl = new Uri(new Uri(baseAddress), requestUri);
                var response = await _httpClient.GetStringAsync(fullUrl);
                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("API response is empty.");
                }

                return JsonSerializer.Deserialize<CryptoRoot>(response);
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Error occurred while making an HTTP request.", httpEx);
            }
            catch (JsonException jsonEx)
            {
                throw new Exception("Error occurred while deserializing API response.", jsonEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while fetching data from the API.", ex);
            }
        }

        public async Task<MarketRoot> GetCryptoMarketsAsync(string baseAddress, string cryptoId)
        {
            try
            {
                string requestUri = $"/v2/assets/{cryptoId}/markets";
                var fullUrl = new Uri(new Uri(baseAddress), requestUri);
                var response = await _httpClient.GetStringAsync(fullUrl);

                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("Empty response from API.");
                }

                return JsonSerializer.Deserialize<MarketRoot>(response);
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"HTTP request error: {httpEx.Message}", httpEx);
            }
            catch (JsonException jsonEx)
            {
                throw new Exception($"JSON deserialization error: {jsonEx.Message}", jsonEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}", ex);
            }
        }
    }
}