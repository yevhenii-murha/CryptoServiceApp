using CryptoService.Services;
using CryptoService.ViewModels;
using System;
using System.Linq;
using System.Net.Http;
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
    public partial class MainWindow : Window
    {
        private readonly CryptoViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new CryptoViewModel(
                new CryptoDataService(
                    new CoinCapApiService(
                        new HttpClient()
                        )
                    ),
                new CryptoFilterService()
                );

            Loaded += async (s, e) =>
            {
                await _viewModel.InitializeAsync(ApiConfig.BaseAddress, ApiConfig.AssetsEndpoint);

                MainFrame.Navigate(new CryptosPage(_viewModel));
            };
        }
    }
}
