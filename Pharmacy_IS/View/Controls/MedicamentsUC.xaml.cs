﻿using Pharmacy_IS.Model.Entities;
using Pharmacy_IS.ViewModel.Service;
using System;
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

namespace Pharmacy_IS.View.Controls
{
    /// <summary>
    /// Interaction logic for MedicamentsUC.xaml
    /// </summary>
    public partial class MedicamentsUC : UserControl
    {
        private MedicamentService _medicamentService;
        public MedicamentsUC()
        {
            InitializeComponent();
            _medicamentService = new MedicamentService();
            this.Reload();

        }

        private void Reload()
        {         
            var dt = this._medicamentService.GetMedicaments();
            this.MedicamentDataGrid.DataContext = dt;
            this.MedicamentDataGrid.ItemsSource = dt.DefaultView;
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            InsertMedWindow insertWindow = new InsertMedWindow(State.Adding);
            if (insertWindow.ShowDialog() == true)
            {
                _medicamentService.InsertMedicament(insertWindow.Medicament, insertWindow.ImagePath);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var index = (DataRowView)this.MedicamentDataGrid.SelectedItem;
            if (index == null)
            {
                MessageBox.Show("You need to select item from the table");
                return;
            }
            int id = Convert.ToInt32(index["ID"]);
            InsertMedWindow insertWindow = new InsertMedWindow(State.Editing, _medicamentService.GetMedicament(id));
            if (insertWindow.ShowDialog() == true)
            {
                _medicamentService.UpdateMedicament(insertWindow.Medicament);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int index = this.MedicamentDataGrid.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("You need to select item from the table");
                return;
            }
            //TODO vykonat vymazanie refreshnut tabulku
        }
    }
}
