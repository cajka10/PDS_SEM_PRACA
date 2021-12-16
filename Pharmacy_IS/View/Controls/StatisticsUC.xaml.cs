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
    /// Interaction logic for StatisticsUC.xaml
    /// </summary>
    public partial class StatisticsUC : UserControl
    {
        private StatisticsService _service;

        public StatisticsUC()
        {
            InitializeComponent();
            _service = new StatisticsService();
            this.Reload();
        }

        private void Reload()
        {
            var dt = this._service.GetEmployeesRanking();
            this.EmployeeDataGrid.DataContext = dt;
            this.EmployeeDataGrid.ItemsSource = dt.DefaultView;
        }
    }
}
