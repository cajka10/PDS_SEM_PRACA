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
    /// Interaction logic for BestsellersUC.xaml
    /// </summary>
    public partial class BestsellersUC : UserControl
    {
        private StatisticsService _service;
        public BestsellersUC()
        {
            InitializeComponent();
            _service = new StatisticsService();
            Reload();
        }

        private void Reload()
        {
            var dt = this._service.GetBestSellers();
            this.MedicamentDataGrid.DataContext = dt;
            this.MedicamentDataGrid.ItemsSource = dt.DefaultView;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
