using CryptoService.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace CryptoService.Services
{
    public class CryptoFilterService : ICryptoFilterService
    {
        public ObservableCollection<Cryptocurrency> FilterCryptos(ObservableCollection<Cryptocurrency> cryptos, string searchQuery)
        {
            if (cryptos == null)
            {
                return new ObservableCollection<Cryptocurrency>();
            }

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return new ObservableCollection<Cryptocurrency>(cryptos);
            }

            var queryLower = searchQuery.ToLower();
            var filtered = cryptos.Where(crypto =>
                crypto.Name.ToLower().Contains(queryLower) ||
                crypto.Symbol.ToLower().Contains(queryLower)).ToList();

            return new ObservableCollection<Cryptocurrency>(filtered);
        }
    }
}