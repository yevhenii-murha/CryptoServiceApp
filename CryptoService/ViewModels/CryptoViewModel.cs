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

        private ObservableCollection<Cryptocurrency> _topCryptos;
        private ObservableCollection<Cryptocurrency> _allCryptos;
        private ObservableCollection<Cryptocurrency> _filteredCryptos;
        private ObservableCollection<Market> _markets;
        private string _searchQuery;
        private string _errorMessage;

        public ObservableCollection<Cryptocurrency> TopCryptos
        {
            get { return _topCryptos; }
            set
            {
                _topCryptos = value;
                OnPropertyChanged();
                FilterCryptos();
            }
        }

        public ObservableCollection<Cryptocurrency> AllCryptos
        {
            get { return _allCryptos; }
            set
            {
                _allCryptos = value;
                OnPropertyChanged();
                FilterCryptos();
            }
        }

        public ObservableCollection<Cryptocurrency> FilteredCryptos
        {
            get { return _filteredCryptos; }
            set
            {
                _filteredCryptos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Market> AllMarkets
        {
            get { return _markets; }
            private set
            {
                _markets = value;
                OnPropertyChanged();
            }
        }

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                FilterCryptos();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public CryptoViewModel(ICryptoDataService cryptoDataService, ICryptoFilterService cryptoFilterService)
        {
            _cryptoDataService = cryptoDataService;
            _cryptoFilterService = cryptoFilterService;

            TopCryptos = new ObservableCollection<Cryptocurrency>();
            AllCryptos = new ObservableCollection<Cryptocurrency>();
            FilteredCryptos = new ObservableCollection<Cryptocurrency>();
            AllMarkets = new ObservableCollection<Market>();
        }

        private void FilterCryptos()
        {
            FilteredCryptos = _cryptoFilterService.FilterCryptos(AllCryptos, SearchQuery);
        }

        public async Task InitializeAsync(string baseAddress, string requestUri)
        {
            await LoadTopCryptos(baseAddress, requestUri);
        }

        public async Task LoadTopCryptos(string baseAddress, string requestUri)
        {
            try
            {
                var cryptos = await _cryptoDataService.GetTopCryptosAsync(baseAddress, requestUri);
                TopCryptos.Clear();
                var topCryptos = cryptos.OrderBy(crypto => crypto.Rank).Take(10);
                foreach (var crypto in topCryptos)
                {
                    TopCryptos.Add(crypto);
                }
                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
        }

        public async Task LoadAllCryptos(string baseAddress, string requestUri)
        {
            try
            {
                var cryptos = await _cryptoDataService.GetAllCryptosAsync(baseAddress, requestUri);
                AllCryptos.Clear();
                foreach (var crypto in cryptos)
                {
                    AllCryptos.Add(crypto);
                }
                FilterCryptos();
                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
        }

        public async Task LoadCryptoMarketsAsync(string baseAddress, string cryptoId)
        {
            try
            {
                var markets = await _cryptoDataService.GetCryptoMarketsAsync(baseAddress, cryptoId);
                AllMarkets.Clear();
                foreach (var market in markets)
                {
                    if (market.BaseId == cryptoId)
                        AllMarkets.Add(market);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading market data: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
