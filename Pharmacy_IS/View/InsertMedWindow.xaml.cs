using Pharmacy_IS.Model.Entities;
using Pharmacy_IS.ViewModel.Service;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
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
        public string ImagePath { get; set; }
   
        private MedicamentService _service;
        public InsertMedWindow(State p_state)
        {
            _service = new MedicamentService();
            this.Medicament = new Medicament();
            state = p_state;

            InitializeComponent();
            if (state == State.Editing)
            {
                this.heading.Content = "Editing Medicament";
                this.confirmButton.Content = "Confirm";
            }
            if(state == State.View)
            {
                this.heading.Content = "Medicament Detail";
                this.confirmButton.Content = "Close";
                this.nameText.IsEnabled = false;
                this.manufacturer.IsEnabled = false;
                this.amount.IsEnabled = false;
                this.medType.IsEnabled = false;
                this.medImage.IsEnabled = false;
                this.ingredients.IsEnabled = false;
                this.medPrescribed.IsEnabled = false;
                this.DesctiptionTextBox.IsEnabled = false;
                this.chooseButton.IsEnabled = false;


            }
            SetManufacturers();
        }

        public InsertMedWindow(State p_state, Medicament medicament)
        {
            _service = new MedicamentService();
            state = p_state;
            InitializeComponent();
            this.Medicament = medicament;
            this.Reload();
            if (state == State.Editing)
            {
                this.heading.Content = "Editing Medicament";
                this.confirmButton.Content = "Confirm";
            }
            if (state == State.View)
            {
                this.heading.Content = "Medicament Detail";
                this.confirmButton.Content = "Close";
                this.nameText.IsEnabled = false;
                this.manufacturer.IsEnabled = false;
                this.amount.IsEnabled = false;
                this.medType.IsEnabled = false;
                this.medImage.IsEnabled = false;
                this.ingredients.IsEnabled = false;
                this.medPrescribed.IsEnabled = false;
                this.DesctiptionTextBox.IsEnabled = false;
                this.chooseButton.IsEnabled = false;
                return;

            }
            SetManufacturers();
        }

        private void SetManufacturers()
        {

            var dict = _service.GetManufacturers();
            manufacturer.ItemsSource = dict;
        }

        private void Reload()
        {
            if (Medicament != null)
            {
                this.nameText.Text = this.Medicament.MedName;
                this.manufacturer.SelectedValue = this.Medicament.ManufacturerId;
                this.amount.Text = "10ml";
                this.medType.Text = this.Medicament.MedType;
                this.ingredients.Text = this.Medicament.ActiveIngredients;
                this.DesctiptionTextBox.Text = this.Medicament.Description;
            }
            else
            {
                MessageBoxResult res = System.Windows.MessageBox.Show("Nepodarilo sa nacitat data.");
            }                             
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if(this.nameText.Text.Length == 0)
            //{
            //    MessageBox.Show("You need to fill name field");
            //    return;
            //}
            //if (this.manufacturer.Text.Length == 0)
            //{
            //    MessageBox.Show("You need to fill manufacturer field");
            //    return;
            //}
            //if (this.amount.Text.Length == 0)
            //{
            //    MessageBox.Show("You need to fill amount field");
            //    return;
            //}
            //int numAmount;

            //try
            //{
            //    numAmount = Int32.Parse(this.amount.Text);
            //}
            //catch (FormatException)
            //{
            //    MessageBox.Show("Amount value must be number");
            //    return;
            //}

            //if (this.medType.Text.Length == 0)
            //{
            //    MessageBox.Show("You need to fill type field");
            //    return;
            //}
            //if (this.medImage.Text.Length == 0)
            //{
            //    MessageBox.Show("You need to fill image field");
            //    return;
            //}
            //if (this.ingredients.Text.Length == 0)
            //{
            //    MessageBox.Show("You need to fill image field");
            //    return;
            //}

            //this.Medicament.MedName = this.nameText.Text;
            //this.Medicament.ManufacturerId = "111222"; //TODO
            //this.Medicament.Amount = this.amount.Text;
            //this.Medicament.MedType = this.medType.Text;
            //this.Medicament.ActiveIngredients = this.ingredients.Text;
            //this.Medicament.Description = this.DesctiptionTextBox.Text;
            //this.Medicament.IsPrescribed = (bool)this.medPrescribed.IsChecked;

            if(state == State.View)
            {
                this.Close();
                return;
            }

            this.Medicament.MedName = "UPDATE -test liek";
            this.Medicament.ManufacturerId = "111222";
            this.Medicament.Amount = " 10 ml";
            this.Medicament.MedType = "tablety";
            this.Medicament.ActiveIngredients = "sirup, test";
            this.Medicament.Description = "UPDATE - test popis";
            this.Medicament.IsPrescribed = true;
            this.ImagePath = this.medImage.Text;

            //TODO spracovanie do objektu/vloženie
            //cez query + spracovanie obsahu pola ingredients do nejakeho listu
            this.DialogResult = true;
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

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            ShowImageWindow window = new ShowImageWindow(this.Medicament.Id);
            window.ShowDialog();

        }

        private void ChooseButton_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
                this.medImage.Text = openFileDialog.FileName;
            }

        }
    }
}
