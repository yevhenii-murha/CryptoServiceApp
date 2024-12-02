using CryptoService.Models;
using CryptoService.Services;
using CryptoService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoService
{
    public partial class ShowAllCryptosPage : Page
    {
        private readonly CryptoViewModel _viewModel;

        public ShowAllCryptosPage(CryptoViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAllCryptos(ApiConfig.BaseAddress, ApiConfig.AssetsEndpoint);
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedCrypto = (Cryptocurrency)button.DataContext;

            if (selectedCrypto != null)
            {
                NavigationService.Navigate(new CryptoDetailsPage(_viewModel, selectedCrypto.Id));
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchQuery = SearchTextBox.Text;
            _viewModel.SearchQuery = searchQuery;
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAllCryptos(ApiConfig.BaseAddress, ApiConfig.AssetsEndpoint);
        }
    }
}
