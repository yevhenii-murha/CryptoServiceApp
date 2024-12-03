using CryptoService.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace CryptoService.Services
{
    public class CryptoFilterService : ICryptoFilterService
    {
        private readonly ConsoleLogger _logger;

        public CryptoFilterService()
        {
            _logger = new ConsoleLogger(); // Ініціалізація логера
        }

        public ObservableCollection<Cryptocurrency> FilterCryptos(ObservableCollection<Cryptocurrency> cryptos, string searchQuery)
        {
            if (cryptos == null)
            {
                _logger.Info("Cryptos collection is null. Returning empty collection.");
                return new ObservableCollection<Cryptocurrency>();
            }

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                _logger.Info("Search query is empty or whitespace. Returning all cryptos.");
                return new ObservableCollection<Cryptocurrency>(cryptos);
            }

            _logger.Info($"Filtering cryptos with search query: {searchQuery}");

            var queryLower = searchQuery.ToLower();
            var filtered = cryptos.Where(crypto =>
                crypto.Name.ToLower().Contains(queryLower) ||
                crypto.Symbol.ToLower().Contains(queryLower)).ToList();

            _logger.Info($"Found {filtered.Count} cryptos matching the search query.");

            return new ObservableCollection<Cryptocurrency>(filtered);
        }
    }
}
