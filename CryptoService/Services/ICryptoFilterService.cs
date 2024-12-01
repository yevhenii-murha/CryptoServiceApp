using CryptoService.Models;
using System.Collections.ObjectModel;

namespace CryptoService.Services
{
    public interface ICryptoFilterService
    {
        ObservableCollection<Cryptocurrency> FilterCryptos(ObservableCollection<Cryptocurrency> cryptos, string searchQuery);
    }
}