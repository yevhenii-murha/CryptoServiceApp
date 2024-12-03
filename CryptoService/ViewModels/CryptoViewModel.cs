using CryptoService.Models;
using CryptoService.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CryptoService.ViewModels
{
    public class CryptoViewModel : INotifyPropertyChanged
    {
        private readonly ICryptoDataService _cryptoDataService;
        private readonly ICryptoFilterService _cryptoFilterService;
        private readonly ConsoleLogger _logger;

        private ObservableCollection<Cryptocurrency> _topCryptos;
        private ObservableCollection<Cryptocurrency> _allCryptos;
        private ObservableCollection<Cryptocurrency> _filteredCryptos;
        private ObservableCollection<Market> _markets;
        private string _searchQuery;

        public ObservableCollection<Cryptocurrency> TopCryptos
        {
            get { return _topCryptos; }
            set
            {
                try
                {
                    _topCryptos = value;
                    OnPropertyChanged();
                    FilterCryptos();
                }
                catch (Exception ex)
                {
                    _logger.Error("Error updating TopCryptos.", ex);
                }
            }
        }

        public ObservableCollection<Cryptocurrency> AllCryptos
        {
            get { return _allCryptos; }
            set
            {
                try
                {
                    _allCryptos = value;
                    OnPropertyChanged();
                    FilterCryptos();
                }
                catch (Exception ex)
                {
                    _logger.Error("Error updating AllCryptos.", ex);
                }
            }
        }

        public ObservableCollection<Cryptocurrency> FilteredCryptos
        {
            get { return _filteredCryptos; }
            set
            {
                try
                {
                    _filteredCryptos = value;
                    OnPropertyChanged();
                }
                catch (Exception ex)
                {
                    _logger.Error("Error updating FilteredCryptos.", ex);
                }
            }
        }

        public ObservableCollection<Market> AllMarkets
        {
            get { return _markets; }
            private set
            {
                try
                {
                    _markets = value;
                    OnPropertyChanged();
                }
                catch (Exception ex)
                {
                    _logger.Error("Error updating AllMarkets.", ex);
                }
            }
        }

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                try
                {
                    _searchQuery = value;
                    OnPropertyChanged();
                    FilterCryptos();
                }
                catch (Exception ex)
                {
                    _logger.Error("Error updating SearchQuery.", ex);
                }
            }
        }

        public CryptoViewModel(ICryptoDataService cryptoDataService, ICryptoFilterService cryptoFilterService)
        {
            _cryptoDataService = cryptoDataService;
            _cryptoFilterService = cryptoFilterService;
            _logger = new ConsoleLogger();

            TopCryptos = new ObservableCollection<Cryptocurrency>();
            AllCryptos = new ObservableCollection<Cryptocurrency>();
            FilteredCryptos = new ObservableCollection<Cryptocurrency>();
            AllMarkets = new ObservableCollection<Market>();
        }

        private void FilterCryptos()
        {
            try
            {
                FilteredCryptos = _cryptoFilterService.FilterCryptos(AllCryptos, SearchQuery);
            }
            catch (Exception ex)
            {
                _logger.Error("Error filtering cryptocurrencies.", ex);
            }
        }

        public async Task InitializeAsync(string baseAddress, string requestUri)
        {
            try
            {
                _logger.Info("Initializing CryptoViewModel...");
                await LoadTopCryptos(baseAddress, requestUri);
            }
            catch (Exception ex)
            {
                _logger.Error("Error during initialization.", ex);
            }
        }

        public async Task LoadTopCryptos(string baseAddress, string requestUri)
        {
            try
            {
                _logger.Info($"Loading top cryptos from {baseAddress}{requestUri}...");
                var topCryptos = await _cryptoDataService.GetTopCryptosAsync(baseAddress, requestUri);
                TopCryptos.Clear();
                foreach (var crypto in topCryptos)
                {
                    TopCryptos.Add(crypto);
                }
                _logger.Info("Top cryptos loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error loading top cryptos.", ex);
            }
        }

        public async Task LoadAllCryptos(string baseAddress, string requestUri)
        {
            try
            {
                _logger.Info($"Loading all cryptos from {baseAddress}{requestUri}...");
                var cryptos = await _cryptoDataService.GetAllCryptosAsync(baseAddress, requestUri);
                AllCryptos.Clear();
                foreach (var crypto in cryptos)
                {
                    AllCryptos.Add(crypto);
                }
                FilterCryptos();
                _logger.Info("All cryptos loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error loading all cryptos.", ex);
            }
        }

        public async Task LoadCryptoMarketsAsync(string baseAddress, string cryptoId)
        {
            try
            {
                _logger.Info($"Loading markets for crypto {cryptoId} from {baseAddress}...");
                var markets = await _cryptoDataService.GetCryptoMarketsAsync(baseAddress, cryptoId);
                AllMarkets.Clear();
                foreach (var market in markets)
                {
                    if (market.BaseId == cryptoId)
                        AllMarkets.Add(market);
                }
                _logger.Info($"Markets for crypto {cryptoId} loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error loading crypto markets.", ex);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                _logger.Error("Error triggering PropertyChanged event.", ex);
            }
        }
    }
}
