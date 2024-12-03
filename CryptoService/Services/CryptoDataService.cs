using CryptoService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoService.Services
{
    public class CryptoDataService : ICryptoDataService
    {
        private readonly ICryptoApiService _cryptoApiService;
        private readonly ConsoleLogger _logger;

        public CryptoDataService(ICryptoApiService cryptoApiService)
        {
            _cryptoApiService = cryptoApiService;
            _logger = new ConsoleLogger();
        }

        public async Task<IEnumerable<Cryptocurrency>> GetTopCryptosAsync(string baseAddress, string requestUri)
        {
            try
            {
                _logger.Info($"Fetching top cryptos from {baseAddress}{requestUri}...");
                var root = await _cryptoApiService.GetCryptosAsync(baseAddress, requestUri);
                var topCryptos = root?.data?.Select(cryptoDto => (Cryptocurrency)cryptoDto).OrderBy(crypto => crypto.Rank).Take(10) ?? Enumerable.Empty<Cryptocurrency>();
                _logger.Info($"Fetched {topCryptos.Count()} top cryptos.");
                return topCryptos;
            }
            catch (Exception ex)
            {
                _logger.Error("Error fetching top cryptos.", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Cryptocurrency>> GetAllCryptosAsync(string baseAddress, string requestUri)
        {
            try
            {
                _logger.Info($"Fetching all cryptos from {baseAddress}{requestUri}...");
                var root = await _cryptoApiService.GetCryptosAsync(baseAddress, requestUri);
                var allCryptos = root?.data?.Select(cryptoDto => (Cryptocurrency)cryptoDto) ?? Enumerable.Empty<Cryptocurrency>();
                _logger.Info($"Fetched {allCryptos.Count()} cryptos.");
                return allCryptos;
            }
            catch (Exception ex)
            {
                _logger.Error("Error fetching all cryptos.", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Market>> GetCryptoMarketsAsync(string baseAddress, string cryptoId)
        {
            try
            {
                _logger.Info($"Fetching markets for crypto {cryptoId} from {baseAddress}...");
                var root = await _cryptoApiService.GetCryptoMarketsAsync(baseAddress, cryptoId);
                var markets = root?.data?.Select(marketDto => (Market)marketDto) ?? Enumerable.Empty<Market>();
                _logger.Info($"Fetched {markets.Count()} markets for crypto {cryptoId}.");
                return markets;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error fetching markets for crypto {cryptoId}.", ex);
                throw;
            }
        }
    }
}
