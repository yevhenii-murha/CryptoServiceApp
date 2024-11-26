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
        private readonly CryptoViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new CryptoViewModel(new CoinCapApiService(new HttpClient()));

            Loaded += async (s, e) =>
            {
                string baseAddress = "https://api.coincap.io";
                string requestUri = "/v2/assets";
                await _viewModel.InitializeAsync(baseAddress, requestUri);

                MainFrame.Navigate(new CryptosPage(_viewModel));
            };
        }
    }
}
