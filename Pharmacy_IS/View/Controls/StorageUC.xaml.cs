﻿using System;
using System.Collections.Generic;
using System.Data;
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
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;

            _storageService = new StorageService();
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
            if (insertWindow.ShowDialog() == true)
            {
                _storageService.InsertStoredItem(insertWindow.StoredItem);
                MessageBox.Show("Medicament to storage added!");
            }
            
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var index = (DataRowView)this.storageDataGrid.SelectedItem;
            if (index == null)
            {
                MessageBox.Show("You need to select item from the table");
                return;
            }
            int id = Convert.ToInt32(index["Id"]);
            InsertStorageWindow insertWindow = new InsertStorageWindow(State.Editing, _storageService.GetStoredItem(id));
            if (insertWindow.ShowDialog() == true)
            {
                _storageService.UpdateStoredItem(insertWindow.StoredItem);
                MessageBox.Show("Medicament in storage updated!");
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var index = (DataRowView)this.storageDataGrid.SelectedItem;
            if (index == null)
            {
                MessageBox.Show("You need to select item from the table");
                return;
            }
            int id = Convert.ToInt32(index["Id"]);
            _storageService.DeleteStoredItem(id);
            MessageBox.Show("Medicament has been removed from storage!");
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            this.Reload();
        }
    }
}
