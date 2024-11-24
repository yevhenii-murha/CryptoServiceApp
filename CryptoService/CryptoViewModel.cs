using CryptoService.Model;
using CryptoService.DTOs;
using CryptoService.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CryptoService
{
    public class CryptoViewModel : INotifyPropertyChanged
    {
        private readonly ICryptoApiService _cryptoApiService;
        private ObservableCollection<Cryptocurrency> _cryptos;

        public ObservableCollection<Cryptocurrency> Cryptos
        {
            get { return _cryptos; }
            set
            {
                _cryptos = value;
                OnPropertyChanged();
            }
        }

        private string _errorMessage;
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
            Cryptos = new ObservableCollection<Cryptocurrency>();
        }

        public async Task InitializeAsync(string baseAddress, string requestUri)
        {
            await LoadTopCryptos(baseAddress, requestUri);
        }

        public async Task LoadTopCryptos(string baseAddress, string requestUri)
        {
            try
            {
                var root = await _cryptoApiService.GetCryptosAsync(baseAddress, requestUri);
                var cryptos = ToCryptos(root);
                Cryptos.Clear();
                var topRankedCryptos = cryptos.OrderBy(crypto => crypto.Rank).Take(10);
                foreach (var crypto in topRankedCryptos)
                {
                    Cryptos.Add(crypto);
                }
                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
            }
        }

        private IEnumerable<Cryptocurrency> ToCryptos(Root root)
        {
            return root?.data?.Select(cryptoDto => (Cryptocurrency)cryptoDto) ?? Enumerable.Empty<Cryptocurrency>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
