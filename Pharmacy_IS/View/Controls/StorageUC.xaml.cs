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
using Pharmacy_IS.Model.Entities;
using Pharmacy_IS.ViewModel.Service;

namespace Pharmacy_IS.View.Controls
{
    /// <summary>
    /// Interaction logic for StorageUC.xaml
    /// </summary>
    public partial class StorageUC : UserControl
    {
        private StorageService _storageService;

        public StorageUC()
        {
            InitializeComponent();
            _storageService = new StorageService();
            //this.Reload();
        }

        private void Reload()
        {
            var dt = this._storageService.GetStorageData();
            this.storageDataGrid.DataContext = dt;
            this.storageDataGrid.ItemsSource = dt.DefaultView;
        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            InsertStorageWindow insertWindow = new InsertStorageWindow(State.Adding);
            if (insertWindow.DialogResult == true)
            {
            }
            insertWindow.Show();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            int index = this.storageDataGrid.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("You need to select item from the table");
                return;
            }
            InsertStorageWindow insertWindow = new InsertStorageWindow(State.Editing);
            insertWindow.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
