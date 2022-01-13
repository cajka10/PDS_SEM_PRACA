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
            String datef = null;
            String dateTo = null;
            
            if (datepickerFrom.Text.Length > 2)
            {
                datef = datepickerFrom.Text.Replace(" ", "");
            }

            if (datepickeTo.Text.Length > 2)
            {
                dateTo = datepickeTo.Text.Replace(" ", "");
            }

            Console.WriteLine("From: " + datef);//10. 1. 2018
            //22. 1. 2022
            Console.WriteLine("To: " + dateTo);


            var dt = this._service.GetEmployeesRanking(datef, dateTo);
            this.EmployeeDataGrid.DataContext = dt;
            this.EmployeeDataGrid.ItemsSource = dt.DefaultView;

            var dt2 = this._service.GetMonthsIncome(datef, dateTo);
            this.incomeGridTable.DataContext = dt2;
            this.incomeGridTable.ItemsSource = dt2.DefaultView;
        }

        public void Filter(object sender, RoutedEventArgs e)
        {
            this.Reload();
        }
    }

}
