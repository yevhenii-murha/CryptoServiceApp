using CryptoService.Services;
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
    /// <summary>
    /// Логика взаимодействия для ShowAllCryptosPage.xaml
    /// </summary>
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
            var cryptosPage = new CryptosPage(_viewModel);
            NavigationService.Navigate(cryptosPage);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string baseAddress = "https://api.coincap.io";
            string requestUri = "/v2/assets";
            await _viewModel.LoadAllCryptos(baseAddress, requestUri);
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            //var crypto = clickedButton.DataContext as Crypto;
        }

    }
}
