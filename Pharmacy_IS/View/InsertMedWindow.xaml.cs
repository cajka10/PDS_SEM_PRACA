using Pharmacy_IS.Model.Entities;
using System;
using System.Windows;
using System.Windows.Input;
namespace Pharmacy_IS.View
{
    /// <summary>
    /// Interaction logic for InsertMedWindow.xaml
    /// </summary>
    public partial class InsertMedWindow : Window
    {
        private State state;

        public Medicament Medicament { get; set; }

        public InsertMedWindow(State p_state)
        {
            state = p_state;
            InitializeComponent();
            if (state == State.Editing)
            {
                this.heading.Content = "Editing Medicament";
                this.confirmButton.Content = "Confirm";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(this.nameText.Text.Length == 0)
            {
                MessageBox.Show("You need to fill name field");
                return;
            }
            if (this.manufacturer.Text.Length == 0)
            {
                MessageBox.Show("You need to fill manufacturer field");
                return;
            }
            if (this.amount.Text.Length == 0)
            {
                MessageBox.Show("You need to fill amount field");
                return;
            }
            int numAmount;

            try
            {
                numAmount = Int32.Parse(this.amount.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Amount value must be number");
                return;
            }

            if (this.medType.Text.Length == 0)
            {
                MessageBox.Show("You need to fill type field");
                return;
            }
            if (this.medImage.Text.Length == 0)
            {
                MessageBox.Show("You need to fill image field");
                return;
            }
            if (this.ingredients.Text.Length == 0)
            {
                MessageBox.Show("You need to fill image field");
                return;
            }

            this.Medicament.MedName = this.nameText.Text;
            this.Medicament.ManufacturerId = (int)this.manufacturer.SelectedValue;
            this.Medicament.Amount = int.TryParse(this.amount.Text, out int temp) ? 0 : temp;
            this.Medicament.MedType = this.medType.Text;
            this.Medicament.ActiveIngredients = this.ingredients.Text;
            this.Medicament.Description = this.DesctiptionTextBox.Text;

            //TODO spracovanie do objektu/vloženie
            //cez query + spracovanie obsahu pola ingredients do nejakeho listu
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
    }
}
