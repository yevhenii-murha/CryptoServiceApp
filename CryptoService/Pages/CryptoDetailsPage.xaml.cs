using System;
using System.Windows;
using System.Windows.Controls;
using CryptoService.ViewModels;
using CryptoService.Services;

namespace CryptoService
{
    public partial class CryptoDetailsPage : Page
    {
        private readonly CryptoViewModel _viewModel;
        private readonly string _cryptoId;
        private readonly ConsoleLogger _logger;

        public CryptoDetailsPage(CryptoViewModel viewModel, string cryptoId)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _cryptoId = cryptoId;
            _logger = new ConsoleLogger();
            DataContext = _viewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.LoadCryptoMarketsAsync(ApiConfig.BaseAddress, _cryptoId);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error loading crypto markets for crypto ID: {_cryptoId}", ex);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                _logger.Error("Error navigating back.", ex);
            }
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.LoadCryptoMarketsAsync(ApiConfig.BaseAddress, _cryptoId);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error refreshing crypto markets for crypto ID: {_cryptoId}", ex);
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new CryptosPage(_viewModel));
            }
            catch (Exception ex)
            {
                _logger.Error("Error navigating to home page.", ex);
            }
        }
    }
}
