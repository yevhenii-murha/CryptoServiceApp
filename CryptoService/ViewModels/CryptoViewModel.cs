using CryptoService.DTOs;
using CryptoService.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using CryptoService.Models;

namespace CryptoService.ViewModels
{
    public class CryptoViewModel : INotifyPropertyChanged
    {
        private readonly ICryptoApiService _cryptoApiService;
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

        public CryptoViewModel(ICryptoApiService cryptoApiService)
        {
            _cryptoApiService = cryptoApiService;
            TopCryptos = new ObservableCollection<Cryptocurrency>();
            AllCryptos = new ObservableCollection<Cryptocurrency>();
            FilteredCryptos = new ObservableCollection<Cryptocurrency>();
            AllMarkets = new ObservableCollection<Market>();
        }

        public async Task InitializeAsync(string baseAddress, string requestUri)
        {
            await LoadTopCryptos(baseAddress, requestUri);
        }

        private void FilterCryptos()
        {
            if (AllCryptos == null)
            {
                FilteredCryptos = new ObservableCollection<Cryptocurrency>();
                return;
            }

            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                FilteredCryptos = new ObservableCollection<Cryptocurrency>(AllCryptos);
            }
            else
            {
                var queryLower = SearchQuery.ToLower();
                var filtered = AllCryptos.Where(crypto =>
                    crypto.Name.ToLower().Contains(queryLower) ||
                    crypto.Symbol.ToLower().Contains(queryLower)).ToList();
                FilteredCryptos = new ObservableCollection<Cryptocurrency>(filtered);
            }
        }

        public async Task LoadTopCryptos(string baseAddress, string requestUri)
        {
            try
            {
                var root = await _cryptoApiService.GetCryptosAsync(baseAddress, requestUri);
                var cryptos = ToCryptos(root);

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
                var root = await _cryptoApiService.GetCryptosAsync(baseAddress, requestUri);
                var cryptos = ToCryptos(root);

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
                var root = await _cryptoApiService.GetCryptoMarketsAsync(baseAddress, cryptoId);
                var markets = ToMarkets(root);

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

        private static IEnumerable<Cryptocurrency> ToCryptos(CryptoRoot root)
        {
            return root?.data?.Select(cryptoDto => (Cryptocurrency)cryptoDto) ?? Enumerable.Empty<Cryptocurrency>();
        }

        private static IEnumerable<Market> ToMarkets(MarketRoot root)
        {
            return root?.data?.Select(marketDto => (Market)marketDto) ?? Enumerable.Empty<Market>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
