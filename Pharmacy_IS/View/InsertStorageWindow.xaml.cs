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
using System.Windows.Shapes;
using Pharmacy_IS.Model.Entities;
using Pharmacy_IS.ViewModel.Service;

namespace Pharmacy_IS.View
{
    /// <summary>
    /// Interaction logic for InsertStorageWindow.xaml
    /// </summary>
    public partial class InsertStorageWindow : Window
    {
        private State state;
        public StoredItem StoredItem { get; set; }
        private MedicamentService _service;

        public InsertStorageWindow(State p_state)
        {
            state = p_state;
            _service = new MedicamentService();

            this.StoredItem = new StoredItem();
            InitializeComponent();
            if (state == State.Editing)
            {
                this.heading.Content = "Editing Storage Record";
                this.confirmButton.Content = "Confirm";
            }
            this.Setmedicaments();
        }

        public InsertStorageWindow(State p_state, StoredItem storedItem)
        {
            state = p_state;
            _service = new MedicamentService();

            this.StoredItem = storedItem;
            InitializeComponent();
            if (state == State.Editing)
            {
                this.heading.Content = "Editing Storage Record";
                this.confirmButton.Content = "Confirm";
            }
            this.Reload();

        }

        private void Reload()
        {
            this.ExpirationDatePicker.SelectedDate = this.StoredItem.ExpirationDate;
            this.QuantityTextBox.Text = this.StoredItem.Quantity.ToString();
            //this.MedicamentComboBox.SelectedItem
        }

        private void Setmedicaments()
        {
            var dict = _service.getMedicamentsNames();

            foreach (var item in dict)
            {
                this.MedicamentComboBox.Items.Add(new MyMed() { Id = item.Key, Name = item.Value });
                this.MedicamentComboBox.DisplayMemberPath = "Name";
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ButtonMin_Clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void ButtonClose_Clicked(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            this.StoredItem.ExpirationDate = (DateTime)this.ExpirationDatePicker.SelectedDate;
            this.StoredItem.Quantity = int.TryParse(this.QuantityTextBox.Text, out int temp) ? temp : 0;
            this.StoredItem.MedicamentId = ((MyMed)this.MedicamentComboBox.SelectedItem).Id;
            DialogResult = true;
        }
    }

    public class MyMed
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
