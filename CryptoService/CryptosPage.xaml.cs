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
    /// Логика взаимодействия для CryptosPage.xaml
    /// </summary>
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
            var showAllPage = new ShowAllCryptosPage(_viewModel);
            NavigationService.Navigate(showAllPage);
        }
    }
}
