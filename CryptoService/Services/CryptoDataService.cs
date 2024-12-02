using CryptoService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoService.Services
{
    public class CryptoDataService : ICryptoDataService
    {
        private readonly ICryptoApiService _cryptoApiService;

        public CryptoDataService(ICryptoApiService cryptoApiService)
        {
            _cryptoApiService = cryptoApiService;
        }

        public async Task<IEnumerable<Cryptocurrency>> GetTopCryptosAsync(string baseAddress, string requestUri)
        {
            var root = await _cryptoApiService.GetCryptosAsync(baseAddress, requestUri);
            return root?.data?.Select(cryptoDto => (Cryptocurrency)cryptoDto).OrderBy(crypto => crypto.Rank).Take(10) ?? Enumerable.Empty<Cryptocurrency>();
        }

        public async Task<IEnumerable<Cryptocurrency>> GetAllCryptosAsync(string baseAddress, string requestUri)
        {
            var root = await _cryptoApiService.GetCryptosAsync(baseAddress, requestUri);
            return root?.data?.Select(cryptoDto => (Cryptocurrency)cryptoDto) ?? Enumerable.Empty<Cryptocurrency>();
        }

        public async Task<IEnumerable<Market>> GetCryptoMarketsAsync(string baseAddress, string cryptoId)
        {
            var root = await _cryptoApiService.GetCryptoMarketsAsync(baseAddress, cryptoId);
            return root?.data?.Select(marketDto => (Market)marketDto) ?? Enumerable.Empty<Market>();
        }
    }
}