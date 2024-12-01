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
using CryptoService.ViewModels;

namespace CryptoService
{
    public partial class CryptoDetailsPage : Page
    {
        private readonly CryptoViewModel _viewModel;
        private readonly string _cryptoId;

        public CryptoDetailsPage(CryptoViewModel viewModel, string cryptoId)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _cryptoId = cryptoId;
            DataContext = _viewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadCryptoMarketsAsync(ApiConfig.BaseAddress, _cryptoId);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
