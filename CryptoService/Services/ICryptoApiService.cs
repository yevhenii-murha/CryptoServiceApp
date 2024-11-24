using CryptoService.DTOs;
using System.Threading.Tasks;

namespace CryptoService.Services
{
    public interface ICryptoApiService
    {
        Task<Root> GetCryptosAsync(string baseAddress, string requestUri);
    }
}