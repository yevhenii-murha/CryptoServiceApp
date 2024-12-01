using CryptoService.DTOs;
using System.Threading.Tasks;

namespace CryptoService.Services
{
    public interface ICryptoApiService
    {
        Task<CryptoRoot> GetCryptosAsync(string baseAddress, string requestUri);
        Task<MarketRoot> GetCryptoMarketsAsync(string baseAddress, string cryptoId);
    }
}