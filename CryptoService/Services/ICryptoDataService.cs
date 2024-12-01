using CryptoService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoService.Services
{
    public interface ICryptoDataService
    {
        Task<IEnumerable<Cryptocurrency>> GetTopCryptosAsync(string baseAddress, string requestUri);
        Task<IEnumerable<Cryptocurrency>> GetAllCryptosAsync(string baseAddress, string requestUri);
        Task<IEnumerable<Market>> GetCryptoMarketsAsync(string baseAddress, string cryptoId);
    }
}