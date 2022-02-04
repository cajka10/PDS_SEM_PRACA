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
    /// Interaction logic for MedApplicationUC.xaml
    /// </summary>
    public partial class MedApplicationUC : UserControl
    {

        private MedicamentService _medicamentService;

        public MedApplicationUC()
        {
            InitializeComponent();
            _medicamentService = new MedicamentService();




            this.initializeElements();
        }

        private void initializeElements()
        {
            //this.Medicaments.SelectedItem = "Aspirin";
            var dict = _medicamentService.getMedicamentsNames();

            foreach (var item in dict)
            {
                this.Medicaments.Items.Add(new MyMed() { Id = item.Key, Name = item.Value });
                this.Medicaments.DisplayMemberPath = "Name";
            }


            List<int> input = new List<int>();

            for (int i = 1; i < 100; i++)
            {
                input.Add(i);
                
            }

            foreach (var v in input)
            {
                this.Type.Items.Add(v.ToString());
            }
        }

        public void CalculatePrice(object sender, RoutedEventArgs e)
        {
            MyMed med = (MyMed) this.Medicaments.SelectedItem;
     

            double price = _medicamentService.getMedicamentPrice(med.Name);

            if(price == 0)
            {
                MessageBoxResult result = MessageBox.Show("Zadaný liek ešte  nemá stanovanú cenu Eur", "Upozornenie", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                int ks = Convert.ToInt32(this.Type.SelectedValue.ToString());
                this.PriceField.Text = Convert.ToString(price * ks);
            }

            //int ks = Convert.ToInt32(this.Type.SelectedValue.ToString());
            //this.PriceField.Text = Convert.ToString( price*ks);
        }

        public void clearInfo(object sender, RoutedEventArgs e)
        {
            this.Medicaments.SelectedItem = null;
            this.PriceField.Text = "";
            this.Type.SelectedIndex = 1;
        }

       

    }



       
}
