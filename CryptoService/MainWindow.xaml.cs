using CryptoService.Services;
using CryptoService.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoService
{
    public partial class MainWindow : Window
    {
        private readonly CryptoViewModel _viewModel;
        private readonly ConsoleLogger _logger;

        public MainWindow()
        {
            InitializeComponent();

            _logger = new ConsoleLogger();
            _logger.Info("MainWindow initialized.");

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
                try
                {
                    _logger.Info("Initializing the view model...");
                    await _viewModel.InitializeAsync(ApiConfig.BaseAddress, ApiConfig.AssetsEndpoint);
                    _logger.Info("View model initialized successfully.");

                    MainFrame.Navigate(new CryptosPage(_viewModel));
                }
                catch (Exception ex)
                {
                    _logger.Error("Error occurred during initialization.", ex);
                }
            };
        }
    }
}
