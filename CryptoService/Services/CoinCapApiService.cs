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

        public async Task<Root> GetCryptosAsync(string baseAddress, string requestUri)
        {
            try
            {
                var fullUrl = new Uri(new Uri(baseAddress), requestUri);
                var response = await _httpClient.GetStringAsync(fullUrl);
                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("API response is empty.");
                }

                return JsonSerializer.Deserialize<Root>(response);
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
    }
}
