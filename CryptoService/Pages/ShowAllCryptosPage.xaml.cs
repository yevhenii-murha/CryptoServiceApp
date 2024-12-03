using CryptoService.Models;
using CryptoService.Services;
using CryptoService.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CryptoService
{
    public partial class ShowAllCryptosPage : Page
    {
        private readonly CryptoViewModel _viewModel;
        private readonly ConsoleLogger _logger;

        public ShowAllCryptosPage(CryptoViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _logger = new ConsoleLogger();
            DataContext = _viewModel;
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _logger.Info("Loading all cryptos...");
                await _viewModel.LoadAllCryptos(ApiConfig.BaseAddress, ApiConfig.AssetsEndpoint);
                _logger.Info("All cryptos loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error loading all cryptos.", ex);
            }
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                var selectedCrypto = (Cryptocurrency)button.DataContext;

                if (selectedCrypto != null)
                {
                    NavigationService.Navigate(new CryptoDetailsPage(_viewModel, selectedCrypto.Id));
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error navigating to crypto details.", ex);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var searchQuery = SearchTextBox.Text;
                _viewModel.SearchQuery = searchQuery;
                _logger.Info($"Search initiated for: {searchQuery}");
            }
            catch (Exception ex)
            {
                _logger.Error("Error performing search.", ex);
            }
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _logger.Info("Refreshing all cryptos...");
                await _viewModel.LoadAllCryptos(ApiConfig.BaseAddress, ApiConfig.AssetsEndpoint);
                _logger.Info("All cryptos refreshed successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error refreshing all cryptos.", ex);
            }
        }
    }
}
