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
            Loaded += async (s, e) => await InitializeViewModelAsync();
        }

        private async Task InitializeViewModelAsync()
        {
            var cryptoService = new CoinCapApiService(new HttpClient());
            string baseAddress = "https://api.coincap.io";
            string requestUri = "/v2/assets";

            var viewModel = new CryptoViewModel(cryptoService);
            await viewModel.InitializeAsync(baseAddress, requestUri);
            DataContext = viewModel;
        }
    }
}
