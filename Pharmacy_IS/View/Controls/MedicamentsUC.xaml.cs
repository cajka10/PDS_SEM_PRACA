using Pharmacy_IS.Model.Entities;
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
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;

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
                MessageBox.Show("Medicament has been inserted!");
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
                _medicamentService.UpdateMedicament(insertWindow.Medicament, insertWindow.ImagePath);
                MessageBox.Show("Medicament has been updated!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var index = (DataRowView)this.MedicamentDataGrid.SelectedItem;
            int id = Convert.ToInt32(index["ID"]);
            if (id == -1)
            {
                MessageBox.Show("You need to select item from the table");
                return;
            }
            //TODO vykonat vymazanie refreshnut tabulku
            //Medicament Service delete id

           if( _medicamentService.deleteMedicament(id) == true)
            {
                MessageBox.Show("Medicament has been removed from the table");
            }
            else
            {
                MessageBox.Show("Failed to remove!");
            }

            this.Reload();
        }

        private void MedicamentDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var index = (DataRowView)this.MedicamentDataGrid.SelectedItem;
            int id = Convert.ToInt32(index["ID"]);
            InsertMedWindow insertWindow = new InsertMedWindow(State.View, _medicamentService.GetMedicament(id));
            insertWindow.ShowDialog();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            this.Reload();
        }
    }
}
