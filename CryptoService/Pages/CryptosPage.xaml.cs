using System;
using System.Windows;
using System.Windows.Controls;
using CryptoService.ViewModels;
using CryptoService.Services;

namespace CryptoService
{
    public partial class CryptosPage : Page
    {
        private readonly CryptoViewModel _viewModel;
        private readonly ConsoleLogger _logger;

        public CryptosPage(CryptoViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _logger = new ConsoleLogger();
            DataContext = _viewModel;
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new ShowAllCryptosPage(_viewModel));
            }
            catch (Exception ex)
            {
                _logger.Error("Error navigating to ShowAllCryptosPage.", ex);
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _logger.Info("Loading top cryptos...");
                await _viewModel.LoadTopCryptos(ApiConfig.BaseAddress, ApiConfig.AssetsEndpoint);
                _logger.Info("Top cryptos loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error loading top cryptos.", ex);
            }
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _logger.Info("Refreshing top cryptos...");
                await _viewModel.LoadTopCryptos(ApiConfig.BaseAddress, ApiConfig.AssetsEndpoint);
                _logger.Info("Top cryptos refreshed successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error refreshing top cryptos.", ex);
            }
        }
    }
}
