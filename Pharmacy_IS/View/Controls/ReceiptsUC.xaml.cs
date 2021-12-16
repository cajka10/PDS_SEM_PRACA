using Pharmacy_IS.ViewModel.Service;
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

namespace Pharmacy_IS.View.Controls
{
    /// <summary>
    /// Interaction logic for ReceiptsUC.xaml
    /// </summary>
    public partial class ReceiptsUC : UserControl
    {
        private StorageService _storageService;
        public ReceiptsUC()
        {
            InitializeComponent();
            _storageService = new StorageService();
        }

        private void Reload()
        {
            var dt = this._storageService.GetSaleHistory();
            this.ReceiptsDataGrid.DataContext = dt;
            this.ReceiptsDataGrid.ItemsSource = dt.DefaultView;
        }

        private void saleButton_Click(object sender, RoutedEventArgs e)
        {
            SaleWindow insertWindow = new SaleWindow();
            insertWindow.Show();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            this.Reload();
        }
    }
}
