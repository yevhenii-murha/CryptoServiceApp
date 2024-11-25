using CryptoService.Services;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += async (s, e) => await InitializeViewAsync();
        }

        private async Task InitializeViewAsync()
        {
            var cryptoService = new CoinCapApiService(new HttpClient());
            var viewModel = new CryptoViewModel(cryptoService);

            string baseAddress = "https://api.coincap.io";
            string requestUri = "/v2/assets";
            await viewModel.InitializeAsync(baseAddress, requestUri);

            MainFrame.Content = new CryptosPage(viewModel);
        }
    }
}
