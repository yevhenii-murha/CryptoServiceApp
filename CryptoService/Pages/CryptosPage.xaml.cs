﻿using System;
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
using CryptoService.ViewModels;

namespace CryptoService
{
    public partial class CryptosPage : Page
    {
        private readonly CryptoViewModel _viewModel;

        public CryptosPage(CryptoViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ShowAllCryptosPage(_viewModel));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadTopCryptos(ApiConfig.BaseAddress, ApiConfig.AssetsEndpoint);
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadTopCryptos(ApiConfig.BaseAddress, ApiConfig.AssetsEndpoint);
        }
    }
}
